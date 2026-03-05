using Application.Contracts.Responses.Catalog;
using Domain.Entities.Catalog;

namespace Application.Contracts.Repositories.Catalog;

public interface ICourseRepository
{
    Task<IEnumerable<CourseResponse>> GetAllCourseByAllInstructors();
    Task<IEnumerable<CourseResponse>> GetAllCoursesByModulesAndLessons();
    Task<CourseResponse> GetCourseByModuleAndLesson(Guid id);
    Task<CourseResponse> GetAllCoursesByInstructorId(Guid instructorId);
    Task Create(Course course);
    void Update(Course course);
    Task<bool> Delete(Guid id);
}