using System.Data;
using System.Text.Json;
using Dapper;

namespace Infrastructure.Configurations.TypeHandlers;

/// <summary>
/// Manipulador de tipo utilizado para serializar/deserializar objetos JSON em operações com banco de dados utilizando Dapper.
/// </summary>
/// <typeparam name="T">
/// Tipo do objeto que será serializado ou deserializado.
/// </typeparam>
public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    public override void SetValue(IDbDataParameter parameter, T? value)
    {
        parameter.Value = JsonSerializer.Serialize(value);
    }

    public override T? Parse(object value)
    {
        return JsonSerializer.Deserialize<T>(value.ToString()!);
    }
}