using Microsoft.Extensions.Logging;
using Phonebook.FunRunRushFlush.Data.Model;
using Phonebook.FunRunRushFlush.Services;
using Phonebook.FunRunRushFlush.Services.Interface;
using Spectre.Console;

namespace Phonebook.FunRunRushFlush.App;

public class PhonebookApp

{
    private ILogger<PhonebookApp> _log;
    private readonly ICrudService _crud;
    private readonly IUserInputValidationService _userInputValidation;

    public PhonebookApp(ILogger<PhonebookApp> log, ICrudService crud, IUserInputValidationService userInputValidationService)
    {
        _log = log;
        _crud = crud;
        _userInputValidation = userInputValidationService;
    }

    public async Task RunApp()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AppHelperFunctions.AppHeader("Phonebook", true);

            var fPhonebooks = await _crud.ShowAllPhonebookData();
            var dummy = new PhonebookTable() {
                Id = 0,
                Name = "Create new registration",
                Email = "Create new registration",
                PhoneNumber = "Create new registration",
            };
            fPhonebooks.Add(dummy);

            var table = new Table().Centered().Expand();
            table.Border = TableBorder.Rounded;

            table.AddColumn("Name").Centered();
            table.AddColumn("Email").Centered();
            table.AddColumn("PhoneNumber").Centered();




            int selectedIndex = 0;
            bool exit = false;
            PhonebookTable selectedPhonebook = null;

            await AnsiConsole.Live(table)
                .Overflow(VerticalOverflow.Ellipsis)
                .StartAsync(async ctx =>
                {
                    while (!exit)
                    {
                        table.Rows.Clear();
                        table.Title("[[ [green] Phonebook Overview [/]]]");
                        table.Caption("[[[blue] [[Up/Down]] Navigation, [[Enter]] Select, [[ESC]] Escape[/]]]");

                        for (int i = 0; i < fPhonebooks.Count; i++)
                        {
                            var Phonebook = fPhonebooks[i];
                            if (fPhonebooks.Count - 1 == i)
                            {
                                if (i == selectedIndex)
                                {
                                    table.AddRow($"[blue] > {Phonebook.Name} <[/]",
                                                 $"[blue]> {Phonebook.Email} <[/]",
                                                 $"[blue]> {Phonebook.PhoneNumber} <[/]");
                                }
                                else
                                {
                                    table.AddRow($"[dim]> {Phonebook.Name} <[/]",
                                                 $"[dim]> {Phonebook.Email} <[/]",
                                                 $"[dim]> {Phonebook.PhoneNumber} <[/]");
                                }

                            }
                            else if (i == selectedIndex)
                            {
                                table.AddRow($"[blue]>{Phonebook.Name}[/]",
                                             $"[blue]{Phonebook.Email}[/]",
                                             $"[blue]{Phonebook.PhoneNumber}[/]");
                            }
                            else
                            {
                                table.AddRow(Phonebook.Name,
                                             Phonebook.Email,
                                             Phonebook.PhoneNumber);
                            }
                        }


                        ctx.Refresh();

                        var key = Console.ReadKey(true).Key;

                        switch (key)
                        {
                            case ConsoleKey.Escape:
                                exit = true;
                                break;

                            case ConsoleKey.UpArrow:
                                selectedIndex--;
                                if (selectedIndex < 0)
                                    selectedIndex = fPhonebooks.Count - 1;
                                break;

                            case ConsoleKey.DownArrow:
                                selectedIndex++;
                                if (selectedIndex >= fPhonebooks.Count)
                                    selectedIndex = 0;
                                break;

                            case ConsoleKey.Enter:

                                selectedPhonebook = fPhonebooks[selectedIndex];
                                exit = true;
                                break;
                        }
                    }
                });


            if (selectedPhonebook != null)
            {
                if (selectedPhonebook.Id == 0)
                {
                    var codSes = _userInputValidation.ValidateUserInput();
                    await _crud.AddPhonebookData(codSes);
                }
                else
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(new[] {
                              "Update", "Delete", "Back"
                            }));



                    if (choice == "Update")
                    {
                        var codSes = _userInputValidation.ValidateUserInput(selectedPhonebook);
                        await _crud.UpdatePhonebookData(codSes);
                    }
                    if (choice == "Delete")
                    {
                        var confirmation = AnsiConsole.Prompt(
                            new TextPrompt<bool>($"[yellow]Are you sure you want to [red]Delete[/] the Phonebook?: [/]")
                                .AddChoice(true)
                                .AddChoice(false)
                                .DefaultValue(false)
                                .WithConverter(choice => choice ? "y" : "n"));

                        if (confirmation)
                        {
                            await _crud.DeletePhonebookData(selectedPhonebook);
                        }
                    }
                }
            }
            else
            {


                if (AppHelperFunctions.ReturnMenu())
                {
                    break;
                }
            }
        }
    }
}