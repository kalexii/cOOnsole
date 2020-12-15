using System;
using System.Threading.Tasks;
using ConsoleAppFramework.ArgumentParsing;
using static ConsoleAppFramework.Shortcuts;

namespace ConsoleAppFramework.Playground
{
    internal static class Program
    {
        public class Params
        {
            [Argument("--myParam", "-m")]
            public string MyParam { get; set; } = default!;
        }

        public static Task Main(string[] args) => new Cli(
            Fork("This is an example of a top-level fork.",
                Token("command1", Action("my action description 1", _ => { })),
                Token("command2", Action("my action description 2", _ => { })),
                Token("commandWithArgs", Action<Params>("my typed action", arg => Console.WriteLine(arg.MyParam)))
            )).HandleAsync(args);
    }
}