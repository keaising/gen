using System;
using McMaster.Extensions.CommandLineUtils;

namespace gen
{
    [Command(Name = "gen", Description = "A simple console app."), Subcommand(typeof(Mobile), typeof(Id))]
    class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            //console.WriteLine("You must specify at a subcommand.");
            app.ShowHelp();
            return 0;
        }
    }
}
