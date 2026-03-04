using Domain.Commom;

namespace Domain.ValuesObjects;

public class Price
{
    public decimal Value { get; }

    private Price(decimal value)
    {
        Value = value;
    }

    public static Result<Price> Create(decimal value)
    {
        var priceCheck = Guard.AgainstOutOfRange(value < 1, "Price cannot be negative.");
        if (!priceCheck.IsSuccess)
            return Result<Price>.Failure(priceCheck.Error!);

        var price = new Price(value);

        return Result<Price>.Success(price);
    }
}
