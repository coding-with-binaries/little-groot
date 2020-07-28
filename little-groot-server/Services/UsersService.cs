using AutoMapper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using LittleGrootServer.Data;
using LittleGrootServer.Exceptions;
using LittleGrootServer.Dto;
using LittleGrootServer.Models;
using LittleGrootServer.Utils;

namespace LittleGrootServer.Services {
    public interface IUsersService {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<AuthenticationResponseDto> Authenticate(AuthenticationRequestDto authenticationDto);
        Task<UserDto> Register(RegistrationDto registrationDto);
        Task<UserDto> GetUser(long id);
        Task<bool> IsEmailAvailable(string email);
        Task<UserDto> GetCurrentUser();
    }

    public class UsersService : IUsersService {

        private LittleGrootDbContext _dbContext;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersService(LittleGrootDbContext context, IMapper mapper, IOptions<AppSettings> options) {
            _dbContext = context;
            _mapper = mapper;
            _appSettings = options.Value;
        }

        public async Task<IEnumerable<UserDto>> GetUsers() {
            var users = await _dbContext.Users.ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<AuthenticationResponseDto> Authenticate(AuthenticationRequestDto authenticationDto) {
            string email = authenticationDto.Email;
            string password = authenticationDto.Password;
            bool rememberMe = authenticationDto.RememberMe;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(rememberMe ? 7 : 1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            AuthenticationResponseDto authenticationResponseDto = new AuthenticationResponseDto {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            };
            return authenticationResponseDto;
        }

        public async Task<UserDto> Register(RegistrationDto registrationDto) {
            var password = registrationDto.Password;
            var user = _mapper.Map<User>(registrationDto);
            if (string.IsNullOrWhiteSpace(password))
                throw new LittleGrootRegistrationException("Password is required");

            if (await _dbContext.Users.AnyAsync(x => x.Email == user.Email))
                throw new LittleGrootRegistrationException("Email '" + user.Email + "' is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUser(long id) {
            var user = await _dbContext.Users.FindAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> IsEmailAvailable(string email) {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);

            return user == null;
        }

        public async Task<UserDto> GetCurrentUser() {
            await _dbContext.Users.FindAsync();
            return null;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}