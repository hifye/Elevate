using Application.Features.Catalog.ListItem;
using Application.Features.Catalog.Queries.Lesson.GetAllLessons;
using Dapper;
using Infrastructure.Configurations.TypeHandlers;

namespace Infrastructure.Configurations;

public static class DapperConfiguration
{
    public static void RegisterTypeHandlers()
    {
        SqlMapper.AddTypeHandler(new EmailTypeHandler());
        SqlMapper.AddTypeHandler(new PriceTypeHandler());
    }
}