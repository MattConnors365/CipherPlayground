using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CipherPlayground.CLI.Commands
{
    public class AtbashCommand : Command<Commands.DefaultCommandSettings>
    {
        public override int Execute(CommandContext context, Commands.DefaultCommandSettings settings)
        {
            // Prompt only if not provided via args
            var text = settings.Text ?? AnsiConsole.Ask<string>("Enter the text:");
            var mode = settings.Mode ?? AnsiConsole.Prompt(new SelectionPrompt<Common.CipherMode>()
                .Title("Choose a cipher mode:")
                .AddChoices(CipherMode.Preserve, CipherMode.Strict, CipherMode.Loose));

            string result = AtbashCipher.Use(text, mode);

            AnsiConsole.MarkupLine($"[green]Text: [/] {result}");
            if (settings.Save ?? AnsiConsole.Confirm("Save the output to a file?", false))
            {
                Commands.SaveToFile(Commands.GetInputSavePath(settings.Path, settings.FileName), result);
            }
            return 0;
        }
    }
}