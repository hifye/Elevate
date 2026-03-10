using System.Data;
using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Dapper;
using Infrastructure.Data.Sql;

namespace Infrastructure.Data.Queries;

public class LessonQueries(IDbConnection connection) : ILessonQueries
{
	public async Task<IEnumerable<LessonListItem>> GetAllLessons()
		=> await connection.QueryAsync<LessonListItem>(LessonSql.GetAllLessons);
}