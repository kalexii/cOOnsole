using System;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class ForkExampleCli
    {
        public static Task<int> Main_(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Fork(
                    Token("cmd1", Action((_, _) => Console.WriteLine("cmd1 called"))),
                    Token("cmd2", Action((_, _) => Console.WriteLine("cmd2 called"))),
                    Token("cmd3", Action((_, _) => Console.WriteLine("cmd3 called")))))
        ).HandleAndGetExitCode(input);
    }
}