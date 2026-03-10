using Application.Features.Learning.ListItem;
using Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

namespace Application.Abstraction.Queries;

public interface IEnrollmentQueries
{
    Task<IEnumerable<StudentListItem>> GetAllEnrollmentsByStudents();
}