using System.Threading.Tasks;
using static ConsoleAppFramework.Cli;

namespace ConsoleAppFramework.Playground
{
    internal static class Program
    {
        public static Task Main(string[] args) => new Cli(
            Fork("top level fork",
                Token("action 1", Action("my action description 1", _ => { })),
                Token("action 2", Action("my action description 2", _ => { }))
            )
        ).HandleAsync(args);
    }
}