using System.Data;
using System.Text.Json;
using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Application.Features.Learning.ListItem;
using Dapper;
using Infrastructure.Data.Sql;

namespace Infrastructure.Data.Queries;

public class EnrollmentQueries(IDbConnection connection) : IEnrollmentQueries
{
    public async Task<IEnumerable<StudentListItem>> GetAllEnrollmentsByStudents()
    {
        var rows = await connection.QueryAsync<StudentSqlRow>(
            EnrollmentSql.GetAllEnrollmentsByStudents
        );
        return rows.Select(r => new StudentListItem(r.StudentId, r.StudentName, r.StudentEmail)
        {
            Enrollments = JsonSerializer.Deserialize<List<EnrollmentListItem>>(r.Courses ?? "[]")!,
        });
    }
}
