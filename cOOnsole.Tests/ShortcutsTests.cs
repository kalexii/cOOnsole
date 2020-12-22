using System;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using FluentAssertions;
using Xunit;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Tests
{
    public class ShortcutsTests
    {
        private readonly Unconditional _dummy = new(HandleResult.Handled);

        [Fact]
        public void TokenTest() => Token("token", _dummy).Should().BeOfType<Token>();

        [Fact]
        public void DescriptionTest() => Description("description", _dummy).Should().BeOfType<Description>();

        [Fact]
        public void ForkTest() => Fork().Should().BeOfType<Fork>();

        [Fact]
        public void PrintUsageIfUnmatchedTest() => PrintUsageIfNotMatched(new Unconditional(HandleResult.NotMatched))
           .Should().BeOfType<PrintUsageIfNotMatched>();

        [Fact]
        public void UnconditionalTest() => Unconditional(HandleResult.NotMatched).Should().BeOfType<Unconditional>();

        [Fact]
        public void ActionTest()
            => Action((Action<string[], IHandlerContext>) null!).Should().BeOfType<UntypedAction>();

        [Fact]
        public void ActionOfTTest()
            => Action((Action<object, IHandlerContext>) null!).Should().BeOfType<TypedAction<object>>();
    }
}