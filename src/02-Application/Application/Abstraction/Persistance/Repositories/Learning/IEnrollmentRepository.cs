using Application.Features.Auth.Responses;
using Domain.Entities.Learning;

namespace Application.Abstraction.Persistance.Repositories.Learning;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetById(Guid userId);
    Task Create(Enrollment enrollment);
    Task<bool> Delete(Guid userId);
}