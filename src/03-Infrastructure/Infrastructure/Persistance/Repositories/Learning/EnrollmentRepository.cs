using System.Data;
using Application.Abstraction.Persistance.Repositories.Learning;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Learning;
using Infrastructure.Data.Sql;

namespace Infrastructure.Persistance.Repositories.Learning;

public class EnrollmentRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : IEnrollmentRepository
{
    public async Task<Enrollment?> GetById(Guid userId)
        => (await contextDapper.QueryFirstOrDefaultAsync<Enrollment>(EnrollmentSql.GetById, new { UserId = userId }))!;
    
    public async Task Create(Enrollment enrollment) 
        => await contextDapper.ExecuteAsync(EnrollmentSql.Create, new { enrollment.UserId, enrollment.CourseId }, unitOfWork.Transaction);

    public async Task<bool> Delete(Guid userId)
    {
        var rowsAffected = await contextDapper.ExecuteAsync(
            EnrollmentSql.Delete, new {UserId = userId}, unitOfWork.Transaction);
        return rowsAffected > 0;
    }
}