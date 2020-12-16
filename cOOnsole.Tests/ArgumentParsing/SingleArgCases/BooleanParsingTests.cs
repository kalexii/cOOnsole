using cOOnsole.ArgumentParsing;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace cOOnsole.Tests.ArgumentParsing.SingleArgCases
{
    public class BooleanParsingTests : ParserTest
    {
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class BasicArg
        {
            [Argument("--long", "-s")]
            public bool MyProp { get; set; }
        }

        [Fact]
        public void AllowsOmissionOfRequiredBooleanDefaultingToFalse()
            => Parse<BasicArg>().Should().BeEquivalentTo(new BasicArg {MyProp = false});

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void BasicArgWorks(string arg)
            => Parse<BasicArg>(arg).Should().BeEquivalentTo(new BasicArg {MyProp = true});

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        private class NullableArg
        {
            [Argument("--long", "-s")]
            public bool? MyProp { get; set; }
        }

        [Theory]
        [InlineData("--long")]
        [InlineData("-s")]
        public void NullableArgWorks(string arg)
            => Parse<NullableArg>(arg).Should().BeEquivalentTo(new NullableArg {MyProp = true});
    }
}