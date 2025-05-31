using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CipherPlayground.CLI.Commands
{
    public class CaesarCommand : Command<CaesarCommand.CaesarSettings>
    {
        public override int Execute(CommandContext context, CaesarSettings settings)
        {
            // Prompt only if not provided via args
            var option = settings.Option ?? AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What operation to perform?")
                    .AddChoices("Encrypt", "Decrypt", "Bruteforce", "Exit"));
            option = option.ToLower();
            if (option == "exit")
            {
                AnsiConsole.MarkupLine($"[green]Returning to main menu...[/]");
                return 0;
            }
            var text = settings.Text ?? AnsiConsole.Ask<string>("Enter the text:");
            var key = settings.Key ?? AnsiConsole.Ask<int>("Enter the key:");
            var mode = settings.Mode ?? AnsiConsole.Prompt(
                new SelectionPrompt<CipherMode>()
                    .Title("Select cipher mode:")
                    .AddChoices(CipherMode.Preserve, CipherMode.Strict, CipherMode.Loose));
            string result = "";
            switch (option)
            {
                case "encrypt":
                    result = CaesarCipher.Encrypt(text, key, mode);
                    break;
                case "decrypt":
                    result = CaesarCipher.Decrypt(text, key, mode);
                    break;
                case "bruteforce":
                    var rawOutput = CaesarCipher.BruteForce(text, mode);
                    result += $"Bruteforce attempts:\n";
                    foreach (string line in rawOutput)
                    {
                        result += $"\t{line}\n";
                    }
                    break;
            }
            AnsiConsole.MarkupLine($"[green]Text: [/] {result}");
            if (settings.Save ?? AnsiConsole.Confirm("Save the output to a file?", false))
            {
                Commands.SaveToFile(Commands.GetInputSavePath(settings.Path, settings.FileName), result);
            }
            return 0;
        }
        public class CaesarSettings : Commands.DefaultCommandSettings
        {
            [CommandOption("-o|--option")]
            [Description("What cipher operation to perform (encrypt, decrypt, bruteforce)")]
            public string? Option { get; set; }

            [CommandOption("-k|--key")]
            [Description("Key (integer)")]
            public int? Key { get; set; }
        }
    }
}