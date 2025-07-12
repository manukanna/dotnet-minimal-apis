using AutoMapper;
using DOTNETPROJECT.DTOs;
using DOTNETPROJECT.Entities;

namespace DOTNETPROJECT.Utilities
{
    public class AddAutoMapperProfiles : Profile
    {
        public AddAutoMapperProfiles()
        {
            // Map UserEntity → UserDataDtos and vice versa
            CreateMap<UserEntity, UserDataDtos>().ReverseMap();

            // Map UserEntity → UserDataDtosId and vice versa
            CreateMap<UserEntity, UserDataDtosId>().ReverseMap();

            CreateMap<UserDtosExpEntity, UserExpEntity>().ReverseMap();

            CreateMap<UserExpEntity, UserDataDtosId>().ReverseMap();    
        }
    }
}
