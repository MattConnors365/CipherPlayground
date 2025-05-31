using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CipherPlayground.CLI.Commands
{
    public class A1Z26Command : Command<A1Z26Command.A1Z26Settings>
    {
        public override int Execute(CommandContext context, A1Z26Settings settings)
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
            var charDelimiter = settings.CharacterDelimiter ?? AnsiConsole.Ask<string>($"Enter a character delimiter:", A1Z26Cipher.defaultCharDelimiter);
            var wordDelimiter = settings.WordDelimiter ?? AnsiConsole.Ask<string>($"Enter a word delimiter:", A1Z26Cipher.defaultWordDelimiter);
            var mode = settings.Mode ?? AnsiConsole.Prompt(
                new SelectionPrompt<CipherMode>()
                    .Title("Select cipher mode:")
                    .AddChoices(CipherMode.Preserve, CipherMode.Strict, CipherMode.Loose));
            string result = "";
            switch (option)
            {
                case "encrypt":
                    result = A1Z26Cipher.Encrypt(text, charDelimiter, wordDelimiter, mode);
                    break;
                case "decrypt":
                    result = A1Z26Cipher.Decrypt(text, charDelimiter, wordDelimiter, mode);
                    break;
            }
            AnsiConsole.MarkupLine($"[green]Text: [/] {result}");
            if (settings.Save ?? AnsiConsole.Confirm("Save the output to a file?", false))
            {
                Commands.SaveToFile(Commands.GetInputSavePath(settings.Path, settings.FileName), result);
            }
            return 0;
        }
        public class A1Z26Settings : Commands.DefaultCommandSettings
        {
            [CommandOption("-o|--option")]
            [Description("What cipher operation to perform (encrypt, decrypt)")]
            public string? Option { get; set; }

            [CommandOption("-c|--characterDelimiter")]
            [Description("String separating characters of individual words")]
            public string? CharacterDelimiter { get; set; }

            [CommandOption("-w|--wordDelimiter")]
            [Description("String separating neighboring characters of two words, changing whitespace to it")]
            public string? WordDelimiter { get; set; }
        }
    }
}