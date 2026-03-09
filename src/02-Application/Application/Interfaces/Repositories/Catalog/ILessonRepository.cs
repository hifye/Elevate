using Application.Features.Catalog.Responses;
using Domain.Entities.Catalog;

namespace Application.Interfaces.Repositories.Catalog;

public interface ILessonRepository
{
    Task<IEnumerable<LessonResponse>> GetAllLessons();
    Task<Lesson> GetById(int id);
    Task Create(Lesson lesson);
    Task<bool> Update(Lesson lesson);
    Task<bool> Delete(int id);
}