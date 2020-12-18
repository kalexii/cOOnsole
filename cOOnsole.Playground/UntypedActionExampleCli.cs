using System;
using System.Data;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class UntypedActionExampleCli
    {
        private class Calculator
        {
            private readonly DataTable _dataTable = new();

            public double Calculate(string[] expressionTokens)
            {
                var result = _dataTable.Compute(string.Concat(expressionTokens), null);
                return result is DBNull ? 0.0 : Convert.ToDouble(result);
            }
        }

        public static Task<int> Main_(string[] input)
        {
            var calculator = new Calculator();
            return Cli(
                PrintUsageIfNotMatched(
                    Token("calc",
                        Description("calculates arithmetic expressions",
                            Action((args, _) => Console.WriteLine(calculator.Calculate(args))))))
            ).HandleAndGetExitCode(input);
        }
    }
}