using CipherPlayground.Library;
using static CipherPlayground.Library.Common;
using static CipherPlayground.CLI.Logic;
using static CipherPlayground.CLI.Loop;
using Spectre.Console.Cli;
using CipherPlayground.CLI.Commands;

namespace CipherPlayground.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandApp();

            app.Configure(config =>
            {
                config.SetApplicationName("cipher");

                config.AddCommand<CaesarCommand>("caesar");
                config.AddCommand<A1Z26Command>("a1z26");
                config.AddCommand<VigenereCommand>("vigenere");
            });

            app.Run(args);
        }
    }
}
