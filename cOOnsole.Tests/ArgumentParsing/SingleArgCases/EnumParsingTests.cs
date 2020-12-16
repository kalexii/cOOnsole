using System.Collections.Generic;
using cOOnsole.ArgumentParsing;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;
using static cOOnsole.Tests.ArgumentParsing.SingleArgCases.EnumParsingTests.Option;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class EnumParsingTests : ParserTest
    {
        public enum Option
        {
            None,
            Verbose,
            Debug,
            Info,
            Warning,
            Error,
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class BasicArg
        {
            [Argument("--long", "-s")]
            public Option MyProp { get; set; }
        }

        [Theory]
        [InlineData("--long", "verbose", Verbose)]
        [InlineData("--long", "debug", Debug)]
        [InlineData("--long", "info", Info)]
        [InlineData("--long", "warning", Warning)]
        [InlineData("--long", "error", Error)]
        [InlineData("-s", "verbose", Verbose)]
        [InlineData("-s", "debug", Debug)]
        [InlineData("-s", "info", Info)]
        [InlineData("-s", "warning", Warning)]
        [InlineData("-s", "error", Error)]
        [InlineData("--long", "VERBOSE", Verbose)]
        [InlineData("--long", "DEBUG", Debug)]
        [InlineData("--long", "INFO", Info)]
        [InlineData("--long", "WARNING", Warning)]
        [InlineData("--long", "ERROR", Error)]
        [InlineData("-s", "VERBOSE", Verbose)]
        [InlineData("-s", "DEBUG", Debug)]
        [InlineData("-s", "INFO", Info)]
        [InlineData("-s", "WARNING", Warning)]
        [InlineData("-s", "ERROR", Error)]
        [InlineData("--long", "VerbosE", Verbose)]
        [InlineData("--long", "DebuG", Debug)]
        [InlineData("--long", "InfO", Info)]
        [InlineData("--long", "WarninG", Warning)]
        [InlineData("--long", "ErroR", Error)]
        [InlineData("-s", "VerbosE", Verbose)]
        [InlineData("-s", "DebuG", Debug)]
        [InlineData("-s", "InfO", Info)]
        [InlineData("-s", "WarninG", Warning)]
        [InlineData("-s", "ErroR", Error)]
        public void BasicArgWorks(string arg, string param, Option option)
            => Parse<BasicArg>(arg, param).Should().BeEquivalentTo(new BasicArg {MyProp = option});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class NullableArg
        {
            [Argument("--long", "-s")]
            public Option MyProp { get; set; }
        }

        [Theory]
        [InlineData("--long", "verbose", Verbose)]
        [InlineData("--long", "debug", Debug)]
        [InlineData("--long", "info", Info)]
        [InlineData("--long", "warning", Warning)]
        [InlineData("--long", "error", Error)]
        [InlineData("-s", "verbose", Verbose)]
        [InlineData("-s", "debug", Debug)]
        [InlineData("-s", "info", Info)]
        [InlineData("-s", "warning", Warning)]
        [InlineData("-s", "error", Error)]
        [InlineData("--long", "VERBOSE", Verbose)]
        [InlineData("--long", "DEBUG", Debug)]
        [InlineData("--long", "INFO", Info)]
        [InlineData("--long", "WARNING", Warning)]
        [InlineData("--long", "ERROR", Error)]
        [InlineData("-s", "VERBOSE", Verbose)]
        [InlineData("-s", "DEBUG", Debug)]
        [InlineData("-s", "INFO", Info)]
        [InlineData("-s", "WARNING", Warning)]
        [InlineData("-s", "ERROR", Error)]
        [InlineData("--long", "VerbosE", Verbose)]
        [InlineData("--long", "DebuG", Debug)]
        [InlineData("--long", "InfO", Info)]
        [InlineData("--long", "WarninG", Warning)]
        [InlineData("--long", "ErroR", Error)]
        [InlineData("-s", "VerbosE", Verbose)]
        [InlineData("-s", "DebuG", Debug)]
        [InlineData("-s", "InfO", Info)]
        [InlineData("-s", "WarninG", Warning)]
        [InlineData("-s", "ErroR", Error)]
        public void NullableArgWorks(string arg, string param, Option option)
            => Parse<NullableArg>(arg, param).Should().BeEquivalentTo(new NullableArg {MyProp = option});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class ArrayArg
        {
            [Argument("--long", "-s")]
            public Option[] MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ArrayArgWorks(string arg) => Parse<ArrayArg>(arg, "error", "warning", "verbose")
           .Should().BeEquivalentTo(new ArrayArg {MyProp = new[] {Error, Warning, Verbose}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class ArrayNullableArg
        {
            [Argument("--long", "-s")]
            public Option?[] MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ArrayNullableArgWorks(string arg) => Parse<ArrayNullableArg>(arg, "error", "warning", "verbose")
           .Should().BeEquivalentTo(new ArrayNullableArg {MyProp = new Option?[] {Error, Warning, Verbose}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class ListArg
        {
            [Argument("--long", "-s")]
            public List<Option> MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ListArgWorks(string arg) => Parse<ListArg>(arg, "error", "warning", "verbose")
           .Should().BeEquivalentTo(new ListArg {MyProp = new List<Option> {Error, Warning, Verbose}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class ListNullableArg
        {
            [Argument("--long", "-s")]
            public List<Option?> MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ListNullableArgWorks(string arg) => Parse<ListNullableArg>(arg, "error", "warning", "verbose")
           .Should().BeEquivalentTo(new ListNullableArg {MyProp = new List<Option?> {Error, Warning, Verbose}});
    }
}