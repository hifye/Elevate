using Application.Features.Catalog.Responses;
using Domain.Entities.Catalog;

namespace Application.Interfaces.Repositories.Catalog;

public interface ICourseRepository
{
    Task<IEnumerable<CourseResponse>> GetAllCoursesByAllInstructors();
    Task<IEnumerable<CourseResponse>> GetAllWithModulesAndLessons();
    Task<CourseResponse> GetWithModulesAndLessons(Guid id);
    Task<CourseResponse> GetByInstructorId(Guid instructorId);
    Task<Course> GetById(Guid id);
    Task Create(Course course);
    Task<bool> Update(Course course);
    Task<bool> Delete(Guid id);
}