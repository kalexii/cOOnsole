using System;
using cOOnsole.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class IntArgumentClassParsingTests : ParserTest<IntArgumentClassParsingTests.Arg>
    {
        public class Arg
        {
            [Argument("--long", "-s")]
            public int MyProp { get; set; }
        }

        [Fact]
        public void ParsesLongArgument()
            => Parse("--long", "123").Should().BeEquivalentTo(new Arg {MyProp = 123});

        [Fact]
        public void ParsesShortArgument()
            => Parse("-s", "123").Should().BeEquivalentTo(new Arg {MyProp = 123});

        [Fact]
        public void IgnoresLongArgument()
            => Parse("--xxx").Should().BeEquivalentTo(new Arg {MyProp = 0});

        [Fact]
        public void IgnoresShortArgument()
            => Parse("-x").Should().BeEquivalentTo(new Arg {MyProp = 0});

        [Fact]
        public void IgnoresEmptyTokens()
            => Parse(Array.Empty<string>()).Should().BeEquivalentTo(new Arg {MyProp = 0});
    }
}