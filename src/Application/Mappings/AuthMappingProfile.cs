using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities.Auth;

namespace Application.Mappings;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<LoginRequest, User>();
        CreateMap<RegisterRequest, User>();
    }
}