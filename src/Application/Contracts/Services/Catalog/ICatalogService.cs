using Application.Contracts.Responses.Catalog;
using Application.DTOs.Catalog;
using Domain.Entities.Catalog;

namespace Application.Contracts.Services.Catalog;

public interface ICatalogService
{
    Task<IEnumerable<CourseResponse>> GetAllCourseByAllInstructors();
    Task<IEnumerable<CourseResponse>> GetAllCoursesByModulesAndLessons();
    Task<CourseResponse> GetCourseByModuleAndLesson(Guid id);
    Task<CourseResponse> GetAllCoursesByInstructorId(Guid instructorId);
    Task<CourseResponse> Create(CourseRequest request);
    Task<bool> Update(CourseRequest course, Guid id);
    Task<bool> Delete(Guid id);
}