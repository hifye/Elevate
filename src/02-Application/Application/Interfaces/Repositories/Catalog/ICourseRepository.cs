using Application.Features.Auth.Responses;
using Application.Features.Catalog.Responses;
using Domain.Entities.Catalog;

namespace Application.Interfaces.Repositories.Catalog;

public interface ICourseRepository
{
    Task<IEnumerable<CourseResponse>> GetAll();
    Task<IEnumerable<CourseResponse>> GetCoursesByTitle(string title);
    Task<InstructorResponse> GetInstructorByName(string name);
    Task<Course> GetById(Guid id);
    Task Create(Course course);
    Task<bool> Update(Course course);
    Task<bool> Delete(Guid id);
}