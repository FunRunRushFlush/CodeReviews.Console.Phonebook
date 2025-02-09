using Phonebook.FunRunRushFlush.Data.Model;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Phonebook.FunRunRushFlush.Services.Interface;

namespace Phonebook.FunRunRushFlush.Services;

public class UserInputValidationService : IUserInputValidationService
{
    private readonly ILogger<UserInputValidationService> _log;

    public UserInputValidationService(ILogger<UserInputValidationService> log)
    {
        _log = log;
    }

    public PhonebookTable ValidateUserInput(PhonebookTable? existingEntry = null)
    {
        AnsiConsole.MarkupLine("[yellow]Please provide the Name, Email and the Phonenumber for your registration.[/]");


        long? id = existingEntry?.Id;


        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Enter your [green]Full Name[/] (max 100 characters):[/]")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input) || input.Length > 100)
                    {
                        return ValidationResult.Error("[red]Please enter a valid name (up to 100 characters).[/]");
                    }
                    return ValidationResult.Success();
                }));


        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Enter your [green]Email[/] (max 100 characters):[/]")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input) ||
                        input.Length > 100 ||
                        !input.Contains("@"))
                    {
                        return ValidationResult.Error("[red]Please enter a valid email (up to 100 characters and must contain '@').[/]");
                    }
                    return ValidationResult.Success();
                }));


        var phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Enter your [green]Phonenumber[/] (max 100 characters, starting with +CountryCode or 00CountryCode):[/]")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input) ||
                        input.Length > 100 ||
                        !(input.StartsWith("+") || input.StartsWith("00")))
                    {
                        return ValidationResult.Error("[red]Please enter a valid phone number (up to 100 characters, starting with '+' or '00').[/]");
                    }
                    return ValidationResult.Success();
                }));


        var phonebookEntry = new PhonebookTable
        {
            
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber
        };

        if (id.HasValue)
        {
            phonebookEntry.Id = id.Value;
        }

        _log.LogInformation("Validated user input: {PhonebookEntry}", phonebookEntry);

        return phonebookEntry;
    }
}
