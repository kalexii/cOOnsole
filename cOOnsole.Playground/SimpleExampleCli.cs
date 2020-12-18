using System;
using System.Linq;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class SimpleExampleCli
    {
        public static Task<int> Main_(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Token("add",
                    Description("adds numbers",
                        Action((args, _) => Console.WriteLine(args.Select(double.Parse).Sum())))))
        ).HandleAndGetExitCode(input);
    }
}