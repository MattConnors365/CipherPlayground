using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CipherPlayground.CLI.Commands
{
    public class VigenereCommand : Command<VigenereCommand.VigenereSettings>
    {
        public override int Execute(CommandContext context, VigenereSettings settings)
        {
            // Prompt only if not provided via args
            var option = settings.Option ?? AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What operation to perform?")
                    .AddChoices("Encrypt", "Decrypt", "Exit"));
            option = option.ToLower();
            if (option == "exit")
            {
                AnsiConsole.MarkupLine($"[green]Returning to main menu...[/]");
                return 0;
            }
            var text = settings.Text ?? AnsiConsole.Ask<string>("Enter the text:");
            var key = settings.Key ?? AnsiConsole.Ask<string>("Enter the key:");
            var mode = settings.Mode ?? AnsiConsole.Prompt(
                new SelectionPrompt<CipherMode>()
                    .Title("Select cipher mode:")
                    .AddChoices(CipherMode.Preserve, CipherMode.Strict, CipherMode.Loose));
            string result = "";
            switch (option)
            {
                case "encrypt":
                    result = VigenereCipher.Encrypt(text, key, mode);
                    break;
                case "decrypt":
                    result = VigenereCipher.Decrypt(text, key, mode);
                    break;
            }
            AnsiConsole.MarkupLine($"[green]Text: [/] {result}");
            if (settings.Save ?? AnsiConsole.Confirm("Save the output to a file?", false))
            {
                Commands.SaveToFile(Commands.GetInputSavePath(settings.Path, settings.FileName), result);
            }
            return 0;
        }
        public class VigenereSettings : Commands.DefaultCommandSettings
        {
            [CommandOption("-o|--option")]
            [Description("What cipher operation to perform (encrypt, decrypt)")]
            public string? Option { get; set; }

            [CommandOption("-k|--key")]
            [Description("Key (string)")]
            public string? Key { get; set; }
        }
    }
}