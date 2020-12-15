using System;
using ConsoleAppFramework.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace ConsoleAppFramework.Tests.ArgumentParsing.SingleArgCases
{
    public class StringArgumentClassParsingTests : ParserTest<StringArgumentClassParsingTests.Arg>
    {
        public class Arg
        {
            [Argument("--long", "-s")]
            public string? MyProp { get; set; }
        }

        [Fact]
        public void ParsesLongArgument()
            => Parse("--long", "123").Should().BeEquivalentTo(new Arg {MyProp = "123"});

        [Fact]
        public void ParsesShortArgument()
            => Parse("-s", "123").Should().BeEquivalentTo(new Arg {MyProp = "123"});

        [Fact]
        public void IgnoresLongArgument()
            => Parse("--xxx").Should().BeEquivalentTo(new Arg {MyProp = null});

        [Fact]
        public void IgnoresShortArgument()
            => Parse("-x").Should().BeEquivalentTo(new Arg {MyProp = null});

        [Fact]
        public void IgnoresEmptyTokens()
            => Parse(Array.Empty<string>()).Should().BeEquivalentTo(new Arg {MyProp = null});
    }
}