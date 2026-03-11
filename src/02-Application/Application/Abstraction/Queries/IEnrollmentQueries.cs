using Application.Features.Learning.ListItem;

namespace Application.Abstraction.Queries;

public interface IEnrollmentQueries
{
    Task<IEnumerable<StudentListItem>> GetAllEnrollmentsByStudents();
}