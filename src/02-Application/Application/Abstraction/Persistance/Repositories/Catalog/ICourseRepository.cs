using Domain.Entities.Catalog;

namespace Application.Abstraction.Persistance.Repositories.Catalog;

public interface ICourseRepository
{
    Task<Course> GetById(Guid id);
    Task Create(Course course);
    Task<bool> Update(Course course);
    Task<bool> Delete(Guid id);
}