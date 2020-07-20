using AutoMapper;
using LittleGrootServer.Models;
using LittleGrootServer.Dto;

namespace LittleGrootServer.Utils {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<User, UserDto>();
            CreateMap<RegistrationDto, User>();
        }
    }
}