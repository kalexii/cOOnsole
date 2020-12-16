using cOOnsole.ArgumentParsing;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing
{
    public class ObjectParsingTests : ResourceAssistedTest
    {
        private enum Option
        {
            First,
            Second,
            Third,
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class ComplexArg
        {
            [Argument("--bool", "-b", Description = "This is my required bool parameter")]
            public bool BoolParam { get; set; }

            [Argument("--str", "-s", Description = "This is my required string parameter")]
            public string StringParam { get; set; } = default!;

            [Argument("--int", "-i", Description = "This is my required int parameter")]
            public int IntParam { get; set; }

            [Argument("--enum", "-e", Description = "This is my required enum parameter")]
            public Option EnumParam { get; set; }

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

        [Fact]
        public void ParsesComplexArgument()
        {
            var argumentParser = new ArgumentParser<ComplexArg>();
            var (result, context) = argumentParser.Parse(new[]
            {
                "--str", "hello",
                "--int", "123",
                "--bool",
                "--enum", "First",
                "--intArray", "-13", "5", "+8",
            });

            context.ErrorAttempts.Should().BeEmpty();
            context.GetMissingRequired().Should().BeEmpty();

            result.Should().BeEquivalentTo(new ComplexArg
            {
                BoolParam = true,
                StringParam = "hello",
                IntParam = 123,
                EnumParam = Option.First,
                IntArrayParam = new[] {-13, 5, 8},
            });
        }
    }
}