using System;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class TokenExampleCli
    {
        public static Task<int> Main_(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Token("hello",
                    Token("world",
                        Token("please ?",
                            Action((_, _) => Console.WriteLine("Hello!"))))))
        ).HandleAndGetExitCode(input);
    }
}