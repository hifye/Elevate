using System.Data;
using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Catalog;
using Infrastructure.Data.Queries.Lesson;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class LessonRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : ILessonRepository
{
    public async Task<IEnumerable<LessonResponse>> GetAllLessons()
        => await contextDapper.QueryAsync<LessonResponse>(LessonQueries.GetAllLessons);

    public async Task<Lesson> GetById(int id)
        => (await contextDapper.QueryFirstOrDefaultAsync<Lesson>(LessonQueries.GetById, new { Id = id }))!;

    public async Task Create(Lesson lesson) => await contextDapper.ExecuteAsync(
        LessonQueries.Create, new { lesson.ModuleId, lesson.Title, lesson.VideoUrl, lesson.OrderNumber },
        unitOfWork.Transaction);

    public async Task<bool> Update(Lesson lesson)
    {
        var rowsAffected =
            await contextDapper.ExecuteAsync(
                LessonQueries.Update, new { lesson.Title, lesson.VideoUrl, lesson.OrderNumber, lesson.Id },
                unitOfWork.Transaction);
        return rowsAffected > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var rowsAffected = 
            await contextDapper.ExecuteAsync(
                LessonQueries.Delete ,new { Id = id }, unitOfWork.Transaction);
        return rowsAffected > 0;
    }
}