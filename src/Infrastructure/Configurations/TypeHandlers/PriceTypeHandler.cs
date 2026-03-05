using System.Data;
using Dapper;
using Domain.ValuesObjects;

namespace Infrastructure.Configurations.TypeHandlers;

/// <summary>
/// Ensina o Dapper a converter o Value Object Price de/para o banco de dados.
/// Sem esse handler, o Dapper não saberia como tratar o tipo Price.
/// </summary>
public class PriceTypeHandler : SqlMapper.TypeHandler<Price>
{
    /// <summary>
    /// Chamado pelo Dapper ao enviar um Price como parâmetro numa query.
    /// Extrai o valor primitivo (decimal) de dentro do objeto Price para que o banco receba um tipo que ele entende.
    /// </summary>
    /// <param name="parameter">O parametro SQL que será enviado ao banco(ex: @Price).</param>
    /// <param name="value">O objeto Price do qual o decimal será extraído.</param>"
    public override void SetValue(IDbDataParameter parameter, Price? value) =>
        parameter.Value = value?.Value;

    /// <summary>
    /// Chamado pelo Dapper ao ler uma coluna do banco e mapear para um Price.
    /// Convert.ToDecimal é usado no lugar de (decimal) para suportar diferentes tipos numéricos que o banco pode retornar(double, float, etc).
    /// </summary>
    /// <param name="value">O valor cru retornado pelo banco, podendo ser decimal, double ou float.</param>
    /// <returns>Retorna o Preço</returns>
    /// <exception cref="DataException">Dado inválido no banco, não deveria acontecer em condições normais</exception>
    public override Price? Parse(object value)
    {
        var result = Price.Create(Convert.ToDecimal(value));
        if (result.IsFailure)
            throw new DataException($"Invalid price from database {value}");

        return result.Value;
    }
}
