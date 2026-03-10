using System.Data;
using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Dapper;
using Infrastructure.Data.Sql;

namespace Infrastructure.Data.Queries;

public class CourseQueries(IDbConnection connection) : ICourseQueries
{
    public async Task<IEnumerable<CourseListItem>> GetAll()
        => await connection.QueryAsync<CourseListItem>(CourseSql.GetAll);

    public async Task<IEnumerable<CourseListItem>> GetCoursesByTitle(string title)
        => await connection.QueryAsync<CourseListItem>(CourseSql.GetCoursesByTitle, new { Title = title })!;

    public async Task<InstructorListItem> GetInstructorByName(string name)
        => (await connection.QueryFirstOrDefaultAsync<InstructorListItem>(CourseSql.GetInstructorByName, new { Name = name }))!;
}