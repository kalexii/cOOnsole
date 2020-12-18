using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cOOnsole.ArgumentParsing;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Xunit;

namespace cOOnsole.Tests.Handlers
{
    public class TypedActionTests : ResourceAssistedTest
    {
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
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

            [Argument("--intList", "-il", Description = "This is my required int list parameter")]
            public List<int> IntListParam { get; set; } = default!;

            [Argument("--obool", Description = "This is my optional bool parameter")]
            public bool? OptionalBoolParam { get; set; }

            [Argument("--ostr", Description = "This is my optional string parameter")]
            public string? OptionalStringParam { get; set; }

            [Argument("--oint", Description = "This is my optional int parameter")]
            public int? OptionalIntParam { get; set; }

            [Argument("--oenum", Description = "This is my optional enum parameter")]
            public Option? OptionalEnumParam { get; set; }

            [Argument("--ointArray", "-oia", Description = "This is my int array parameter")]
            public int[]? OptionalIntArrayParam { get; set; }

            [Argument("--ointList", "-oil", Description = "This is my required int list parameter")]
            public List<int>? OptionalIntListParam { get; set; }
        }

        [Fact]
        public void PrintsItselfCorrectly()
        {
            var (printer, sb) = HandlerMocks.MockPrinter();
            var action = new TypedAction<ComplexArg>(Action);

            action.PrintSelf(printer);

            sb.ToString().Should().Be(AsExpectedForThisTest());
        }

        [Fact]
        public async Task PrintsOutMissedRequiredsIfOnlyOptionalsAreGiven()
        {
            var action = new TypedAction<ComplexArg>(Action);
            var expectation = AsExpectedForThisTest();

            var (handled, printed) = await action.ExecuteAndCaptureAsync("--obool");

            handled.Should().BeFalse();
            printed.Should().Be(expectation);
        }

        [Fact]
        public async Task PrintsOutMissedRequiredsIfNotAllRequiredsGiven()
        {
            var action = new TypedAction<ComplexArg>(Action);
            var expectation = AsExpectedForThisTest();

            var (handled, printed) = await action.ExecuteAndCaptureAsync("--int", "123");

            handled.Should().BeFalse();
            printed.Should().Be(expectation);
        }

        [Fact]
        public async Task PrintsOutUnknownOptionError()
        {
            var action = new TypedAction<ComplexArg>(Action);
            var expectation = AsExpectedForThisTest();

            var (handled, printed) = await action.ExecuteAndCaptureAsync("--unknown");

            handled.Should().BeFalse();
            printed.Should().Be(expectation);
        }

        [Fact]
        public async Task PrintsOutMissingValueError()
        {
            var action = new TypedAction<ComplexArg>(Action);
            var expectation = AsExpectedForThisTest();

            var (handled, printed) = await action.ExecuteAndCaptureAsync("--int" /*, "1" - missing*/);

            handled.Should().BeFalse();
            printed.Should().Be(expectation);
        }

        [Fact]
        public async Task PrintsOutUnconvertibleValueError()
        {
            var action = new TypedAction<ComplexArg>(Action);
            var expectation = AsExpectedForThisTest();

            var (handled, printed) = await action.ExecuteAndCaptureAsync("--int", "hello");

            handled.Should().BeFalse();
            printed.Should().Be(expectation);
        }

        [Fact]
        public async Task PassesParsedResultToDelegateCorrectly()
        {
            var expected = new ComplexArg
            {
                BoolParam = true,
                EnumParam = Option.First,
                IntParam = 123,
                StringParam = "hello",
                IntArrayParam = new[] {-1, 0, 1},
                IntListParam = new List<int> {-9, 0, 9},
                OptionalBoolParam = true,
                OptionalEnumParam = Option.Second,
                OptionalIntParam = 234,
                OptionalStringParam = "world",
                OptionalIntArrayParam = new[] {-2, 2},
                OptionalIntListParam = new List<int> {-11, 0, 11},
            };

            var argumentParser = new PrintUsageIfUnmatched(new Token("arg", new TypedAction<ComplexArg>(Action)));
            var (handled, printed) = await argumentParser.ExecuteAndCaptureAsync("arg",
                "-b", "-s", "hello", "-i", "123", "-e", "first", "-ia", "-1", "0", "1", "-il", "-9", "0", "9",
                "--obool", "--ostr", "world", "--oint", "234", "--oenum", "second", "-oia", "-2", "2", "-oil", "-11",
                "0", "11");
            handled.Should().BeTrue();
            var actual = JsonConvert.DeserializeObject<ComplexArg>(printed);
            actual.Should().BeEquivalentTo(expected);
        }

        private static void Action<T>(T a, IHandlerContext c) => c.Printer.Print(JsonConvert.SerializeObject(a));

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class DuplicateAliasArg
        {
            [Argument("--param", Description = "This is my required string parameter")]
            public string StringParam { get; set; } = default!;

            [Argument("--param", Description = "This is my required int parameter")]
            public int IntParam { get; set; }
        }

        [Fact]
        public async Task ThrowsWhenThereAreDuplicateAliases()
        {
            var typedAction = new TypedAction<DuplicateAliasArg>(Action);

            var (handled, printed) = await typedAction.ExecuteAndCaptureAsync();
            handled.Should().BeFalse();
            printed.Split(Environment.NewLine)[0]
               .Should().Be(
                    "cOOnsole.ArgumentParsing.Exceptions.DuplicateAliasException: Duplicate alias in class DuplicateAliasArg: --param");
        }
    }
}