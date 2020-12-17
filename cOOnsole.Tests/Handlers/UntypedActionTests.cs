using System;
using System.Threading.Tasks;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Moq;
using Xunit;

namespace cOOnsole.Tests.Handlers
{
    public class UntypedActionTests
    {
        public sealed record ActionCall(string[] Arguments, HandlerContext Context);

        [Fact]
        public async Task ComprehensiveTest() // todo: split into more granular tests
        {
            var expected = new ActionCall(new[] {"arg"}, new HandlerContext(new Mock<IPrinter>().Object));
            ActionCall? actual = null;
            var action = new UntypedAction((a, c) => actual = new ActionCall(a, c));
            action.SetContext(expected.Context);

            var handled = await action.HandleAsync(expected.Arguments);

            handled.Should().Be(HandleResult.Handled);
            actual.Should().NotBeNull();
            actual!.Arguments.Should().BeSameAs(expected.Arguments);
            actual!.Context.Should().BeSameAs(expected.Context);
        }

        [Fact]
        public void DoesNotPrintAnything()
        {
            var action = new UntypedAction(null!);
            var (printer, sb) = HandlerMocks.MockPrinter();

            action.PrintSelf(printer);

            sb.ToString().Should().Be("");
        }
    }
}