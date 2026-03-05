using Domain.Entities.Catalog;

namespace Application.Contracts.Repositories.Catalog;

public interface ILessonRepository
{
    Task Create(Lesson lesson);
    void Update(Lesson lesson);
    Task<bool> Delete(int id);
}