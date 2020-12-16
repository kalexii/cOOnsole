using System;
using cOOnsole.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class EnumArgumentClassParsingTests : ParserTest<EnumArgumentClassParsingTests.Arg>
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

        public class Arg
        {
            [Argument("--long", "-s")]
            public Option MyProp { get; set; }
        }

        [Theory]
        [InlineData("verbose", Option.Verbose)]
        [InlineData("debug", Option.Debug)]
        [InlineData("info", Option.Info)]
        [InlineData("warning", Option.Warning)]
        [InlineData("error", Option.Error)]
        public void ParsesLongArgument(string param, Option option)
            => Parse("--long", param).Should().BeEquivalentTo(new Arg {MyProp = option});

        [Theory]
        [InlineData("verbose", Option.Verbose)]
        [InlineData("debug", Option.Debug)]
        [InlineData("info", Option.Info)]
        [InlineData("warning", Option.Warning)]
        [InlineData("error", Option.Error)]
        public void ParsesShortArgument(string param, Option option)
            => Parse("-s", param).Should().BeEquivalentTo(new Arg {MyProp = option});

        [Fact]
        public void IgnoresLongArgument()
            => Parse("--xxx").Should().BeEquivalentTo(new Arg {MyProp = Option.None});

        [Fact]
        public void IgnoresShortArgument()
            => Parse("-x").Should().BeEquivalentTo(new Arg {MyProp = Option.None});

        [Fact]
        public void IgnoresEmptyTokens()
            => Parse(Array.Empty<string>()).Should().BeEquivalentTo(new Arg {MyProp = Option.None});
    }
}