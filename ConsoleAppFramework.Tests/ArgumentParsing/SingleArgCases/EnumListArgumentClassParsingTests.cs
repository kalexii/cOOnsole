using System.Collections.Generic;
using ConsoleAppFramework.ArgumentParsing;
using FluentAssertions;
using Xunit;

namespace ConsoleAppFramework.Tests.ArgumentParsing.SingleArgCases
{
    public class EnumListArgumentClassParsingTests : ParserTest<EnumListArgumentClassParsingTests.Arg>
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
            public List<Option> MyProp { get; set; } = default!;
        }

        [Fact]
        public void ParsesLongStringArrayArgument()
            => Parse("--arr", "a", "b", "c").Should()
               .BeEquivalentTo(new Arg {MyProp = new List<Option> {Option.A, Option.B, Option.C}});

        [Fact]
        public void ParsesShortStringArrayArgument()
            => Parse("-a", "a", "b", "c").Should()
               .BeEquivalentTo(new Arg {MyProp = new List<Option> {Option.A, Option.B, Option.C}});
    }
}