using Application.Features.Catalog.Commands.Course.CreateCourse;
using Application.Features.Catalog.Commands.Course.UpdateCourse;
using Application.Features.Catalog.Commands.Lesson.CreateLesson;
using Application.Features.Catalog.Commands.Lesson.UpdateLesson;
using Application.Features.Catalog.Commands.Module.CreateModule;
using Application.Features.Catalog.Commands.Module.UpdateModule;
using AutoMapper;
using Domain.Entities.Catalog;

namespace Application.Features.Catalog;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<CreateCourseCommand, Course>();
        CreateMap<UpdateCourseCommand, Course>();
        CreateMap<CreateModuleCommand, Module>();
        CreateMap<UpdateModuleCommand, Module>();
        CreateMap<CreateLessonCommand, Lesson>();
        CreateMap<UpdateLessonCommand, Lesson>();
    }
}