using System;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class DescriptionExampleCli
    {
        public static Task<int> Main(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Description("this is my command group!",
                    Fork(
                        Token("command1", Description("this is command 1 description!",
                            Unconditional(HandleResult.NotMatched))),
                        Token("command2", Description("this is command 2 description!",
                            Unconditional(HandleResult.NotMatched))),
                        Token("command3", Description("this is command 3 description!",
                            Unconditional(HandleResult.NotMatched))))
                )
            )
        ).HandleAndGetExitCode(input);
    }
}