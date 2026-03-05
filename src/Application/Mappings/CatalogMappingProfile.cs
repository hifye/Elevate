using Application.DTOs.Catalog;
using AutoMapper;
using Domain.Entities.Catalog;

namespace Application.Mappings;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<CourseRequest, Course>();
        CreateMap<ModuleRequest, Module>();
        CreateMap<LessonRequest, Lesson>();
    }
}