using System;
using System.Text.Json;
using System.Threading.Tasks;
using cOOnsole.ArgumentParsing;
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
            [Argument("--bool", "-b", Description = "This is my bool parameter")]
            public bool BoolParam { get; set; } = default!;

            [Argument("--str", "-s", Description = "This is my string parameter")]
            public string StringParam { get; set; } = default!;

            [Argument("--int", "-i", Description = "This is my int parameter")]
            public int IntParam { get; set; } = default!;

            [Argument("--enum", "-e", Description = "This is my enum parameter")]
            public Option EnumParam { get; set; } = default!;

            [Argument("--obool", Description = "This is my optional bool parameter")]
            public bool? OptionalBoolParam { get; set; } = default!;

            [Argument("--ostr", Description = "This is my optional string parameter")]
            public string? OptionalStringParam { get; set; } = default!;

            [Argument("--oint", Description = "This is my optional int parameter")]
            public int? OptionalIntParam { get; set; } = default!;

            [Argument("--oenum", Description = "This is my optional enum parameter")]
            public Option? OptionalEnumParam { get; set; } = default!;
        }

        public static Task Main(string[] args) => new Cli(
            Fork(
                Token("command1", Action("my action description 1", (_, _) => { })),
                Token("command2", Action("my action description 2", (_, _) => { })),
                Token("command3", Action<Params>("my typed action", (a, _) => PrintIndented(a))),
                Token("help", Action("prints help", (_, c) => { c.Cli.PrintSelf(); }))
            )).HandleAsync(args);

        private static void PrintIndented(Params a)
            => Console.WriteLine(JsonSerializer.Serialize(a, new JsonSerializerOptions {WriteIndented = true}));
    }
}