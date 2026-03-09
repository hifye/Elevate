using System.Data;
using Application.Features.Auth.Responses;
using Application.Interfaces.Repositories.Learning;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Learning;
using Infrastructure.Persistance.Dapper.Queries.Learning;

namespace Infrastructure.Persistance.Repositories.Learning;

public class EnrollmentRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork) : IEnrollmentRepository
{

    public async Task<IEnumerable<StudentResponse>> GetAllEnrollmentsByStudents()
        => await contextDapper.QueryAsync<StudentResponse>(EnrollmentQueries.GetAllEnrollmentsByStudents);

    public async Task<Enrollment?> GetById(Guid userId)
        => (await contextDapper.QueryFirstOrDefaultAsync<Enrollment>(EnrollmentQueries.GetById, new { UserId = userId }))!;
    
    public async Task Create(Enrollment enrollment) 
        => await contextDapper.ExecuteAsync(EnrollmentQueries.Create, new { enrollment.UserId, enrollment.CourseId }, unitOfWork.Transaction);

    public async Task<bool> Delete(Guid userId)
    {
        var rowsAffected = await contextDapper.ExecuteAsync(
            EnrollmentQueries.Delete, new {UserId = userId}, unitOfWork.Transaction);
        return rowsAffected > 0;
    }
}