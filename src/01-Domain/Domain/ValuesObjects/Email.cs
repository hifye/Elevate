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
        address = address.Trim().ToLower();

        return Guard
            .AgainstNullOrWhiteSpace(address, "Email cannot be null")
            .Bind(() =>
                Guard.AgainstOutOfRange(
                    address.Length > 200,
                    "Email cannot be longer than 200 characters."
                )
            )
            .Bind(() => Result.Try(() => new MailAddress(address), "Invalid Email"))
            .Map(() => new Email(address));
    }
}
