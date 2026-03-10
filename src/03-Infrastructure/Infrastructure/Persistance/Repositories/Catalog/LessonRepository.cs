using System.Data;
using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Catalog;
using Infrastructure.Data.Sql;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class LessonRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : ILessonRepository
{
    public async Task<Lesson> GetById(int id)
        => (await contextDapper.QueryFirstOrDefaultAsync<Lesson>(LessonSql.GetById, new { Id = id }))!;

    public async Task Create(Lesson lesson) => await contextDapper.ExecuteAsync(
        LessonSql.Create, new { lesson.ModuleId, lesson.Title, lesson.VideoUrl, lesson.OrderNumber },
        unitOfWork.Transaction);

    public async Task<bool> Update(Lesson lesson)
    {
        var rowsAffected =
            await contextDapper.ExecuteAsync(
                LessonSql.Update, new { lesson.Title, lesson.VideoUrl, lesson.OrderNumber, lesson.Id },
                unitOfWork.Transaction);
        return rowsAffected > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var rowsAffected = 
            await contextDapper.ExecuteAsync(
                LessonSql.Delete ,new { Id = id }, unitOfWork.Transaction);
        return rowsAffected > 0;
    }
}