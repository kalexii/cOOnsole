using cOOnsole.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class EnumArrayArgumentClassParsingTests : ParserTest<EnumArrayArgumentClassParsingTests.Arg>
    {
        public enum Option
        {
            A,
            B,
            C,
        }

        public class Arg
        {
            [Argument("--arr", "-a")]
            public Option[] MyProp { get; set; } = default!;
        }

        [Fact]
        public void ParsesLongStringArrayArgument()
            => Parse("--arr", "a", "b", "c").Should()
               .BeEquivalentTo(new Arg {MyProp = new[] {Option.A, Option.B, Option.C}});

        [Fact]
        public void ParsesShortStringArrayArgument()
            => Parse("-a", "a", "b", "c").Should()
               .BeEquivalentTo(new Arg {MyProp = new[] {Option.A, Option.B, Option.C}});
    }
}