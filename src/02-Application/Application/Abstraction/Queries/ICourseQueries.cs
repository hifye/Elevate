using Application.Features.Catalog.ListItem;
using Application.Features.Catalog.Queries.Course;

namespace Application.Abstraction.Queries;

public interface ICourseQueries
{
    Task<IEnumerable<CourseListItem>> GetAll();
    Task<IEnumerable<CourseListItem>> GetCoursesByTitle(string title);
    Task<InstructorListItem> GetInstructorByName(string name);
}