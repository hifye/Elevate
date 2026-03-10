using System.Data;
using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Dapper;
using Infrastructure.Data.Sql;

namespace Infrastructure.Data.Queries;

public class ModuleQueries(IDbConnection connection) : IModuleQueries
{
    public async Task<IEnumerable<ModuleListItem>> GetModulesAndLessons()
        => await connection.QueryAsync<ModuleListItem>(ModuleSql.GetModulesAndLessons);
}