using Application.Contracts.Responses.Auth;
using Application.Contracts.Responses.Learning;
using Application.DTOs.Learning;

namespace Application.Contracts.Services.Learning;

public interface IEnrollmentService
{
    Task<IEnumerable<StudentResponse>> GetAllEnrollmentsByStudents();
    Task<StudentResponse> GetAllEnrollmentByStudentId(Guid userId);
    Task<EnrollmentResponse> Create(EnrollmentRequest request);
    Task<bool> Delete(Guid userId, int courseId);
}