using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using static CipherPlayground.Library.Common;

namespace CipherPlayground.CLI.Commands
{
    public class Commands
    {
        public class DefaultCommandSettings : CommandSettings
        {
            [CommandOption("-t|--text")]
            [Description("Text to work with")]
            public string? Text { get; set; }

            [CommandOption("-m|--mode")]
            [Description("Cipher mode (Strict, Loose, Preserve)")]
            public CipherMode? Mode { get; set; }

            [CommandOption("-s|--save")]
            [Description("Whether or not to save the output to a file")]
            public bool? Save { get; set; }

            [CommandOption("-p|--path")]
            [Description("If saving is enabled, where to store the output")]
            public string? Path { get; set; }

            [CommandOption("-f|--fileName")]
            [Description("If saving is enabled, what to name the output file")]
            public string? FileName { get; set; }
        }

        public static void SaveToFile(string path, string content)
        {
            try
            {
                AnsiConsole.MarkupLine($"[yellow]Saving file to path[/] [green]{path}[/][yellow]...[/]");
                File.WriteAllText(path, content);
                AnsiConsole.MarkupLine($"[green]File saved successfully to {path}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error saving file: {ex.Message}[/]");
            }
        }
        public static string GetInputSavePath(string? path, string? fileName)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = AnsiConsole.Ask<string>("Enter the path to save the output:", Environment.CurrentDirectory);
                if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    path += Path.DirectorySeparatorChar;
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = AnsiConsole.Ask<string>("Enter the file name:", $"Output.{DateTime.Now:yyyy-MMM-ddHH---HH-mm-ss}.txt");
            }
            return $"{path}{fileName}";
        }
    }
}
