using System.Net.Mail;
using Domain.Commom;

namespace Domain.ValuesObjects;

public record Email
{
    public string Address { get; } = null!;

    private Email(string address)
    {
        Address = address;
    }

    public static Result<Email> Create(string address)
    {
        var addressCheck = Guard.AgainstNullOrWhiteSpace(address, "Email cannot be null or empty.");
        if (!addressCheck.IsSuccess)
            return Result<Email>.Failure(addressCheck.Error!);
        if (address.Length > 200)
            return Result<Email>.Failure("Email cannot be longer than 200 characters.");

        address = address.Trim().ToLower();

        try
        {
            _ = new MailAddress(address);
        }
        catch
        {
            return Result<Email>.Failure("Invalid Email");
        }

        var email = new Email(address);
        return Result<Email>.Success(email);
    }
}
