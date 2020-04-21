using System.Drawing;
using System.Threading.Tasks;
using Pastel;
using static ConsoleAppFramework.Cli;

namespace ConsoleAppFramework.Playground
{
    internal static class Program
    {
        public static Task Main(string[] args) => new Cli(
            Fork("top level fork".Pastel(Color.DarkGreen),
                Token("action 1".Pastel(Color.Green), Action("my action description 1", _ => { })),
                Token("action 2".Pastel(Color.Green), Action("my action description 2", _ => { }))
            )).HandleAsync(args);
    }
}