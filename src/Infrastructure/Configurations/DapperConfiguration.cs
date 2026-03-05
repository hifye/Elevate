using Dapper;
using Infrastructure.Configurations.TypeHandlers;

namespace Infrastructure.Configurations;

public class DapperConfiguration
{
    public static void RegisterTypeHandlers()
    {
        SqlMapper.AddTypeHandler(new EmailTypeHandler());
        SqlMapper.AddTypeHandler(new PriceTypeHandler());
    }
}