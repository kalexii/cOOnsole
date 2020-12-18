using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cOOnsole.ArgumentParsing;
using cOOnsole.Handlers.Base;
using JetBrains.Annotations;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class ComplexExampleCli
    {
        public static Task<int> Main_(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Description("commands", Fork(
                    Token("echo", Description("this command simply prints the arguments given to it",
                        Action((rest, _) =>
                            Console.WriteLine("echo!. rest: {0}", string.Join(", ", rest))))),
                    Token("parse", Description("this command parses the arguments and prints them",
                        Action<SimpleExampleParams>((arg, _) =>
                            Console.WriteLine(ToJson(arg))))),
                    Token("help", Description("this command prints this help message",
                        Unconditional(HandleResult.NotMatched))))))
        ).HandleAndGetExitCode(input);

        [UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
        private class SimpleExampleParams
        {
            [Argument("--bool", "-b", Description = "Required bool parameter")]
            public bool BoolParam { get; set; }

            [Argument("--str", "-s", Description = "Required string parameter")]
            public string StringParam { get; set; } = default!;

            [Argument("--int", "-i", Description = "Required int parameter")]
            public int IntParam { get; set; }

            [Argument("--enum", "-e", Description = "Required enum parameter")]
            public Option EnumParam { get; set; }

            [Argument("--intArray", "-ia", Description = "Required int array parameter")]
            public int[] IntArrayParam { get; set; } = default!;

            [Argument("--enum-list", "-el", Description = "Required enum list parameter")]
            public List<Option> EnumListParam { get; set; } = default!;
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private enum Option
        {
            First,
            Second,
            Third,
        }

        private static string ToJson<T>(T obj) => JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new JsonStringEnumConverter()},
        });
    }
}