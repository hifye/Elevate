using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Login.LoginUser;
using Application.Features.Auth.Commands.Register;
using AutoMapper;
using Domain.Entities.Auth;

namespace Application.Features.Auth;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<LoginCommand, User>();
        CreateMap<RegisterCommand, User>();
    }
}