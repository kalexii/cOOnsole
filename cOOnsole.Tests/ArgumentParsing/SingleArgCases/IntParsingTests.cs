using System.Collections.Generic;
using cOOnsole.ArgumentParsing;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class IntParsingTests : ParserTest
    {
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class Arg
        {
            [Argument("--long", "-s")]
            public int MyProp { get; set; }
        }

        [Theory]
        [InlineData("--long", "123", 123)]
        [InlineData("--long", "-123", -123)]
        [InlineData("--long", "0", 0)]
        [InlineData("--long", "-0", -0)]
        [InlineData("-s", "123", 123)]
        [InlineData("-s", "-123", -123)]
        [InlineData("-s", "0", 0)]
        [InlineData("-s", "-0", -0)]
        public void ArgWorks(string arg, string input, int expected)
            => Parse<Arg>(arg, input).Should().BeEquivalentTo(new Arg {MyProp = expected});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class NullableArg
        {
            [Argument("--long", "-s")]
            public int? MyProp { get; set; }
        }

        [Theory]
        [InlineData("--long", "123", 123)]
        [InlineData("--long", "-123", -123)]
        [InlineData("--long", "0", 0)]
        [InlineData("--long", "-0", -0)]
        [InlineData("-s", "123", 123)]
        [InlineData("-s", "-123", -123)]
        [InlineData("-s", "0", 0)]
        [InlineData("-s", "-0", -0)]
        public void NullableArgWorks(string arg, string input, int expected)
            => Parse<NullableArg>(arg, input).Should().BeEquivalentTo(new NullableArg {MyProp = expected});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class ArrayArg
        {
            [Argument("--long", "-s")]
            public int[] MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ArrayArgWorks(string arg) => Parse<ArrayArg>(arg, "1", "2", "3")
           .Should().BeEquivalentTo(new ArrayArg {MyProp = new[] {1, 2, 3}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class NullableArrayArg
        {
            [Argument("--long", "-s")]
            public int?[] MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void NullableArrayArgWorks(string arg) => Parse<NullableArrayArg>(arg, "1", "2", "3")
           .Should().BeEquivalentTo(new NullableArrayArg {MyProp = new int?[] {1, 2, 3}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class ListArg
        {
            [Argument("--long", "-s")]
            public List<int> MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void ListArgWorks(string arg) => Parse<ListArg>(arg, "1", "2", "3")
           .Should().BeEquivalentTo(new ListArg {MyProp = new List<int> {1, 2, 3}});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        public class NullableListArg
        {
            [Argument("--long", "-s")]
            public List<int?> MyProp { get; set; } = default!;
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void NullableListArgWorks(string arg) => Parse<NullableListArg>(arg, "1", "2", "3")
           .Should().BeEquivalentTo(new NullableListArg {MyProp = new List<int?> {1, 2, 3}});
    }
}