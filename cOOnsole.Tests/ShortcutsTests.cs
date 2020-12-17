using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests
{
    public class ShortcutsTests
    {
        private readonly Unconditional _dummy = new(HandleResult.Handled);

        [Fact]
        public void Token() => Shortcuts.Token("token", _dummy)
           .Should().BeOfType<Token>();

        [Fact]
        public void Description() => Shortcuts.Description("description", _dummy)
           .Should().BeOfType<Description>();

        [Fact]
        public void Fork() => Shortcuts.Fork()
           .Should().BeOfType<Fork>();

        [Fact]
        public void PrintUsageIfUnmatched() => Shortcuts
           .PrintUsageIfUnmatched(new Unconditional(HandleResult.NotHandled))
           .Should().BeOfType<PrintUsageIfUnmatched>();

        [Fact]
        public void Unconditional() => Shortcuts
           .Unconditional(HandleResult.NotHandled)
           .Should().BeOfType<Unconditional>();

        [Fact]
        public void Action() => Shortcuts.Action((_, _) => { })
           .Should().BeOfType<UntypedAction>();

        [Fact]
        public void ActionOfT() => Shortcuts.Action<object>((_, _) => { })
           .Should().BeOfType<TypedAction<object>>();
    }
}