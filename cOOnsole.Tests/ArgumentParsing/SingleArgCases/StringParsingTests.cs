using System.Collections.Generic;
using cOOnsole.ArgumentParsing;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class StringParsingTests : ParserTest
    {
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class Arg
        {
            [Argument("--long", "-s")]
            public string MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long", "hello", "hello")]
        [InlineData("--long", "", "")]
        [InlineData("--long", "HELLO", "HELLO")]
        [InlineData("--long", "кириллица", "кириллица")]
        [InlineData("-s", "hello", "hello")]
        [InlineData("-s", "", "")]
        [InlineData("-s", "HELLO", "HELLO")]
        [InlineData("-s", "кириллица", "кириллица")]
        public void ArgWorks(string arg, string input, string expected)
            => Parse<Arg>(arg, input).Should().BeEquivalentTo(new Arg {MyProp = expected});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class NullableArg
        {
            [Argument("--long", "-s")]
            public string? MyProp { get; set; }
        }

        [Theory]
        [InlineData("--long", "hello", "hello")]
        [InlineData("--long", "", "")]
        [InlineData("--long", "HELLO", "HELLO")]
        [InlineData("--long", "кириллица", "кириллица")]
        [InlineData("-s", "hello", "hello")]
        [InlineData("-s", "", "")]
        [InlineData("-s", "HELLO", "HELLO")]
        [InlineData("-s", "кириллица", "кириллица")]
        public void NullableArgWorks(string arg, string input, string expected)
            => Parse<NullableArg>(arg, input).Should().BeEquivalentTo(new NullableArg {MyProp = expected});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class ArrayArg
        {
            [Argument("--long", "-s")]
            public string[] MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ArrayArgWorks(string arg) => Parse<ArrayArg>(arg, "hello", "world")
           .Should().BeEquivalentTo(new ArrayArg {MyProp = new[] {"hello", "world"}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class NullableArrayArg
        {
            [Argument("--long", "-s")]
            public string?[] MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void NullableArrayArgWorks(string arg) => Parse<NullableArrayArg>(arg, "hello", "world")
           .Should().BeEquivalentTo(new NullableArrayArg {MyProp = new[] {"hello", "world"}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class ListArg
        {
            [Argument("--long", "-s")]
            public List<string> MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ListArgWorks(string arg) => Parse<ListArg>(arg, "hello", "world")
           .Should().BeEquivalentTo(new ListArg {MyProp = new List<string> {"hello", "world"}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class NullableListArg
        {
            [Argument("--long", "-s")]
            public List<string?> MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void NullableListArgWorks(string arg) => Parse<NullableListArg>(arg, "hello", "world")
           .Should().BeEquivalentTo(new NullableListArg {MyProp = new List<string?> {"hello", "world"}});
    }
}