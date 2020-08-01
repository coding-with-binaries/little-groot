using AutoMapper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using LittleGrootServer.Data;
using LittleGrootServer.Exceptions;
using LittleGrootServer.Dto;
using LittleGrootServer.Models;
using LittleGrootServer.Utils;

namespace LittleGrootServer.Services {
    public interface IUsersService {
        IEnumerable<UserDto> GetUsers();
        AuthenticationResponseDto Authenticate(AuthenticationRequestDto authenticationDto);
        UserDto Register(RegistrationDto registrationDto);
        UserDto GetUser(long id);
        bool IsEmailAvailable(string email);
        UserDto GetCurrentUser();
    }

    public class UsersService : IUsersService {

        private LittleGrootDbContext _dbContext;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(LittleGrootDbContext context, IMapper mapper, IOptions<AppSettings> options,
            IHttpContextAccessor httpContextAccessor) {
            _dbContext = context;
            _mapper = mapper;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<UserDto> GetUsers() {
            var users = _dbContext.Users.ToList();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public AuthenticationResponseDto Authenticate(AuthenticationRequestDto authenticationDto) {
            string email = authenticationDto.Email;
            string password = authenticationDto.Password;
            bool rememberMe = authenticationDto.RememberMe;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _dbContext.Users.SingleOrDefault(x => x.Email == email);

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

        public UserDto Register(RegistrationDto registrationDto) {
            var password = registrationDto.Password;
            var user = _mapper.Map<User>(registrationDto);
            if (string.IsNullOrWhiteSpace(password))
                throw new LittleGrootRegistrationException("Password is required");

            if (_dbContext.Users.Any(x => x.Email == user.Email))
                throw new LittleGrootRegistrationException("Email '" + user.Email + "' is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUser(long id) {
            var user = _dbContext.Users.Find(id);

            return _mapper.Map<UserDto>(user);
        }

        public bool IsEmailAvailable(string email) {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

            return user == null;
        }

        public UserDto GetCurrentUser() {
            var currentUserId = _httpContextAccessor.HttpContext.User.Identity.Name;

            return GetUser(long.Parse(currentUserId));
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