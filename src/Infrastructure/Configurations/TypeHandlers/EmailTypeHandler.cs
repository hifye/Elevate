using System.Data;
using Dapper;
using Domain.ValuesObjects;

namespace Infrastructure.Configurations.TypeHandlers;

/// <summary>
/// Ensina o Dapper a converter o Value Object Email de/para o banco.
/// Sem esse handler, o Dapper não saberia como tratar o tipo Email.
/// </summary>
public class EmailTypeHandler : SqlMapper.TypeHandler<Email>
{
    /// <summary>
    /// Chamado pelo dapper ao enviar um Email como parametro numa query.
    /// Extrai o valor primitivo (string) de dentro do objeto Email para que o banco receba um tipo que ele entende.
    /// </summary>
    /// <param name="parameter">O parametro SQL que será enviado ao banco (ex: @Email).</param>
    /// <param name="value">O objeto Email do qual o string será extraído</param>
    public override void SetValue(IDbDataParameter parameter, Email? value)
        => parameter.Value = value?.Address;

    /// <summary>
    /// Chamado pelo Dapper ao converter um dado do banco de dados para o tipo de Value Object Email.
    /// Valida se o dado recebido é um email válido e retorna a instância correspondente do Value Object Email.
    /// Em caso de erro na validação, lança uma exceção informando o valor inválido.
    /// </summary>
    /// <param name="value">O valor do banco de dados que será convertido para o tipo Email.</param>
    /// <returns>O objeto Email criado a partir do valor recebido.</returns>
    /// <exception cref="System.Data.DataException">Lançada quando o valor recebido do banco de dados não é um email válido.</exception>
    public override Email? Parse(object value)
    {
        var result = Email.Create(value.ToString()!);
        if (result.IsFailure)
            throw new DataException($"Invalid email from database: {value}");
        
        return result.Value;
    }
}