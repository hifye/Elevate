using Application.Features.Auth.Responses;
using Domain.Entities.Learning;

namespace Application.Interfaces.Repositories.Learning;

public interface IEnrollmentRepository
{
    Task<IEnumerable<StudentResponse>> GetAllEnrollmentsByStudents();
    Task<StudentResponse> GetAllEnrollmentByStudentId(Guid userId);
    Task Create(Enrollment enrollment);
    Task<bool> Delete(Guid userId);
}