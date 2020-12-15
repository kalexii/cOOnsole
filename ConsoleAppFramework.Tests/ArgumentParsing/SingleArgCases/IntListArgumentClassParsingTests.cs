using System.Collections.Generic;
using ConsoleAppFramework.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace ConsoleAppFramework.Tests.ArgumentParsing.SingleArgCases
{
    public class IntListArgumentClassParsingTests : ParserTest<IntListArgumentClassParsingTests.Arg>
    {
        public class Arg
        {
            [Argument("--arr", "-a")]
            public List<int> MyProp { get; set; }= default!;
        }

        [Fact]
        public void ParsesLongStringArrayArgument()
            => Parse("--arr", "1", "2", "3").Should().BeEquivalentTo(new Arg {MyProp = new List<int> {1, 2, 3}});

        [Fact]
        public void ParsesShortStringArrayArgument()
            => Parse("-a", "1", "2", "3").Should().BeEquivalentTo(new Arg {MyProp = new List<int> {1, 2, 3}});
    }
}