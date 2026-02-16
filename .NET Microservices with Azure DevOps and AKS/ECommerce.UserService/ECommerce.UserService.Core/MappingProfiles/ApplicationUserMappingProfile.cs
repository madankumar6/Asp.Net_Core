using AutoMapper;
using ECommerce.UserService.Core.Dtos;
using ECommerce.UserService.Core.Entities;

namespace ECommerce.UserService.Core.MappingProfiles
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ApplicationUser, AuthenticationResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuccessful, opt => opt.Ignore());
        }
    }
}
