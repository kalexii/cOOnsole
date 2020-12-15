using System;
using ConsoleAppFramework.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace ConsoleAppFramework.Tests.ArgumentParsing.SingleArgCases
{
    public class BooleanArgumentClassParsingTests : ParserTest<BooleanArgumentClassParsingTests.Arg>
    {
        public class Arg
        {
            [Argument("--long", "-s")]
            public bool MyProp { get; set; }
        }

        [Fact]
        public void ParsesLongArgument()
            => Parse("--long").Should().BeEquivalentTo(new Arg {MyProp = true});

        [Fact]
        public void ParsesShortArgument()
            => Parse("-s").Should().BeEquivalentTo(new Arg {MyProp = true});

        [Fact]
        public void IgnoresLongArgument()
            => Parse("--xxx").Should().BeEquivalentTo(new Arg {MyProp = false});

        [Fact]
        public void IgnoresShortArgument()
            => Parse("-x").Should().BeEquivalentTo(new Arg {MyProp = false});

        [Fact]
        public void IgnoresEmptyTokens()
            => Parse(Array.Empty<string>()).Should().BeEquivalentTo(new Arg {MyProp = false});
    }
}