using Domain.Entities.Catalog;

namespace Application.Abstraction.Persistance.Repositories.Catalog;

public interface ILessonRepository
{
    Task<Lesson> GetById(int id);
    Task Create(Lesson lesson);
    Task<bool> Update(Lesson lesson);
    Task<bool> Delete(int id);
}