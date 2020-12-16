using cOOnsole.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class IntArrayArgumentClassParsingTests : ParserTest<IntArrayArgumentClassParsingTests.Arg>
    {
        public class Arg
        {
            [Argument("--arr", "-a")]
            public int[] MyProp { get; set; } = default!;
        }

        [Fact]
        public void ParsesLongStringArrayArgument()
            => Parse("--arr", "1", "2", "3").Should().BeEquivalentTo(new Arg {MyProp = new[] {1, 2, 3}});

        [Fact]
        public void ParsesShortStringArrayArgument()
            => Parse("-a", "1", "2", "3").Should().BeEquivalentTo(new Arg {MyProp = new[] {1, 2, 3}});
    }
}