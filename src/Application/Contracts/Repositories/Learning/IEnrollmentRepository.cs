using Application.Contracts.Responses.Auth;
using Domain.Entities.Learning;

namespace Application.Contracts.Repositories.Learning;

public interface IEnrollmentRepository
{
    Task<IEnumerable<StudentResponse>> GetAllEnrollmentsByStudents();
    Task<StudentResponse> GetAllEnrollmentByStudentId(Guid userId);
    Task Create(Enrollment enrollment);
    Task<bool> Delete(Guid userId, int courseId);
}