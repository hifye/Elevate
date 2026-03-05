using Application.DTOs.Learning;
using AutoMapper;
using Domain.Entities.Learning;

namespace Application.Mappings;

public class LearningMappingProfile : Profile
{
    public LearningMappingProfile()
    {
        CreateMap<EnrollmentRequest, Enrollment>();
    }
}