using Application.Features.Learning.Commands.Create;
using AutoMapper;
using Domain.Entities.Learning;

namespace Application.Features.Learning;

public class LearningMappingProfile : Profile
{
    public LearningMappingProfile()
    {
        CreateMap<CreateEnrollmentCommand, Enrollment>();
    }
}