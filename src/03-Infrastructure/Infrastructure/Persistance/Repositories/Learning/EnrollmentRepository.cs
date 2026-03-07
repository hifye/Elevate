using System.Data;
using Application.Features.Auth.Responses;
using Application.Features.Learning.Responses;
using Application.Interfaces.Repositories.Learning;
using Application.Interfaces.UnitOfWork;
using Dapper;
using Domain.Entities.Learning;

namespace Infrastructure.Persistance.Repositories.Learning;

public class EnrollmentRepository : IEnrollmentRepository
{
    #region Dependencies
    
    private readonly IDbConnection _contextDapper;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentRepository(IDbConnection contextDapper, IUnitOfWork unitOfWork)
    {
        _contextDapper = contextDapper;
        _unitOfWork = unitOfWork;
    }

    #endregion


    /// <summary>
    /// Obtém todas as matrículas agrupadas por estudantes.
    /// </summary>
    /// <returns>
    /// Uma coleção enumerável de objetos do tipo <c>StudentResponse</c>,
    /// onde cada objeto representa um estudante contendo suas matrículas associadas.
    /// </returns>
    public async Task<IEnumerable<StudentResponse>> GetAllEnrollmentsByStudents()
    {
        var studentDictionary = new Dictionary<Guid, StudentResponse>();

        await _contextDapper.QueryAsync<StudentResponse, EnrollmentResponse, StudentResponse>(
            @"select u.id as Id,
	                     u.name as Name,
	                     u.email as Email,
	                     c.id as CourseId,
	                     c.title as CourseTitle,
	                     e.enrolled_at as EnrolledAt
                from learning.enrollments e 
                inner join auth.users u 
	                on u.id = e.user_id  
                inner join catalog.courses c 
	                on c.id = e.course_id",
            (student, enrollment) =>
            {
                if (!studentDictionary.TryGetValue(student.Id, out var existingStudent))
                {
                    existingStudent = student;
                    studentDictionary[student.Id] = existingStudent;
                }

                existingStudent.Enrollments.Add(enrollment);
                return existingStudent;
            },
            splitOn: "CourseId");
        return studentDictionary.Values;
    }


    /// <summary>
    /// Obtém todas as matrículas de um estudante específico.
    /// </summary>
    /// <param name="userId">
    ///     Identificador único do estudante cujas matrículas devem ser recuperadas.
    /// </param>
    /// <returns>
    /// Um objeto do tipo <c>StudentResponse</c> representando o estudante, incluindo suas matrículas associadas.
    /// Retorna <c>null</c> se o estudante não for encontrado ou se não houver matrículas associadas.
    /// </returns>
    public async Task<IEnumerable<StudentResponse>> GetAllEnrollmentByStudentId(Guid userId)
    {
        var studentDictionary = new Dictionary<Guid, StudentResponse>();

        await _contextDapper.QueryAsync<StudentResponse, EnrollmentResponse, StudentResponse>(
            @"select u.id as Id,
	                     u.name as Name,
	                     u.email as Email,
	                     c.id as CourseId,
	                     c.title as CourseTitle,
	                     e.enrolled_at as EnrolledAt
                from learning.enrollments e 
                inner join auth.users u 
	                on u.id = e.user_id  
                inner join catalog.courses c 
	                on c.id = e.course_id
	                where u.id = @UserId",
            (student, enrollment) =>
            {
                if (!studentDictionary.TryGetValue(student.Id, out var existingStudent))
                {
                    existingStudent = student;
                    studentDictionary[student.Id] = existingStudent;
                }

                existingStudent.Enrollments.Add(enrollment);
                return existingStudent;
            },
            splitOn: "CourseId",
            param: new { UserId = userId });
        return studentDictionary.Values;
    }

    public async Task Create(Enrollment enrollment) => await _contextDapper.ExecuteAsync(
                                                   @"insert into learning.enrollments(user_id,course_id)
                                                       values(@UserId, @CourseId)",
                                                   new
                                                   {
                                                       enrollment.UserId,
                                                       enrollment.CourseId
                                                   }, _unitOfWork.Transaction);

    public async Task<bool> Delete(Guid userId)
    {
        var rowsAffected = await _contextDapper.ExecuteAsync(
            @"delete from learning.enrollments 
                  where user_id = @UserId 
                    and course_id = @CourseId", new {UserId = userId}, _unitOfWork.Transaction);
        return rowsAffected > 0;
    }
}