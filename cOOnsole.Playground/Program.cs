using System;
using System.Text.Json;
using System.Threading.Tasks;
using cOOnsole.ArgumentParsing;
using cOOnsole.Handlers.Base;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    internal static class Program
    {
        public enum Option
        {
            First,
            Second,
            Third,
        }

        public class Params
        {
            [Argument("--bool", "-b", Description = "This is my required bool parameter")]
            public bool BoolParam { get; set; } = default!;

            [Argument("--str", "-s", Description = "This is my required string parameter")]
            public string StringParam { get; set; } = default!;

            [Argument("--int", "-i", Description = "This is my required int parameter")]
            public int IntParam { get; set; } = default!;

            [Argument("--enum", "-e", Description = "This is my required enum parameter")]
            public Option EnumParam { get; set; } = default!;

            [Argument("--intArray", "-ia", Description = "This is my required int array parameter")]
            public int[] IntArrayParam { get; set; } = default!;

            [Argument("--obool", Description = "This is my optional bool parameter")]
            public bool? OptionalBoolParam { get; set; } = default!;

            [Argument("--ostr", Description = "This is my optional string parameter")]
            public string? OptionalStringParam { get; set; } = default!;

            [Argument("--oint", Description = "This is my optional int parameter")]
            public int? OptionalIntParam { get; set; } = default!;

            [Argument("--oenum", Description = "This is my optional enum parameter")]
            public Option? OptionalEnumParam { get; set; } = default!;

            [Argument("--ointArray", "-oia", Description = "This is my int array parameter")]
            public int[]? OptionalIntArrayParam { get; set; } = default!;
        }

        public static Task<int> Main(string[] args) => new Cli(
            PrintUsageIfUnmatched(
                Fork(
                    Token("command1", Action((_, _) => { })),
                    Token("command2", Action((_, _) => { })),
                    Token("command3", Action<Params>((a, _) => PrintIndented(a))),
                    Token("help", Unconditional(HandleResult.NotHandled)))
            )).HandleAndGetExitCode(args);

        private static void PrintIndented(Params a)
            => Console.WriteLine(JsonSerializer.Serialize(a, new JsonSerializerOptions {WriteIndented = true}));
    }
}