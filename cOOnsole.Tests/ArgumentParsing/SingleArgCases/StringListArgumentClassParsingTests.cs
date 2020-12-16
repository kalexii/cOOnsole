using System.Collections.Generic;
using cOOnsole.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class StringListArgumentClassParsingTests : ParserTest<StringListArgumentClassParsingTests.Arg>
    {
        public class Arg
        {
            [Argument("--arr", "-a")]
            public List<string> MyProp { get; set; } = default!;
        }

        [Fact]
        public void ParsesLongStringArrayArgument()
            => Parse("--arr", "1", "2", "3").Should()
               .BeEquivalentTo(new Arg {MyProp = new List<string> {"1", "2", "3"}});

        [Fact]
        public void ParsesShortStringArrayArgument()
            => Parse("-a", "1", "2", "3").Should().BeEquivalentTo(new Arg {MyProp = new List<string> {"1", "2", "3"}});
    }
}