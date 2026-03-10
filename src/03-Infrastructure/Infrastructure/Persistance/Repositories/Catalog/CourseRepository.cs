using System.Data;
using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Catalog;
using Infrastructure.Data.Sql;

namespace Infrastructure.Persistance.Repositories.Catalog;

public class CourseRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork)
    : ICourseRepository
{
    public async Task<Course> GetById(Guid id) =>
        (await contextDapper.QueryFirstOrDefaultAsync<Course>(CourseSql.GetById, new { Id = id }))!;

    public async Task Create(Course course) =>
        await contextDapper.ExecuteAsync(
            CourseSql.Create,
            new
            {
                course.Title,
                course.Description,
                course.Price,
                course.InstructorId,
            },
            unitOfWork.Transaction
        );

    public async Task<bool> Update(Course course)
    {
        var rowsAffected = await contextDapper.ExecuteAsync(
            CourseSql.Update,
            new
            {
                course.Title,
                course.Description,
                course.Price,
                course.Id,
            },
            unitOfWork.Transaction
        );

        return rowsAffected > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var rowsAffected = await contextDapper.ExecuteAsync(
            CourseSql.Delete, new { Id = id },
            unitOfWork.Transaction
        );

        return rowsAffected > 0;
    }
}
