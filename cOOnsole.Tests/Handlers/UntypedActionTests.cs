using System.Threading.Tasks;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Moq;
using Xunit;

namespace cOOnsole.Tests.Handlers
{
    public class UntypedActionTests
    {
        [Fact]
        public async Task CallsUnderlyingAction()
        {
            var called = false;
            var action = new UntypedAction((_, _) => called = true);

            await action.HandleAsync(new string[0]);

            called.Should().BeTrue();
        }

        [Fact]
        public async Task ProvidesArguments()
        {
            var context = Mock.Of<IHandlerContext>();
            var action = new UntypedAction((a, _) => a.Should().BeEquivalentTo("a", "b"));
            action.SetContext(context);

            await action.HandleAsync(new[] {"a", "b"});
        }

        [Fact]
        public async Task ProvidesContext()
        {
            var context = Mock.Of<IHandlerContext>();
            var action = new UntypedAction((_, c) => c.Should().Be(context));
            action.SetContext(context);

            await action.HandleAsync(new string[0]);
        }

        [Fact]
        public async Task ReturnsHandled()
        {
            var action = new UntypedAction((_, _) => { });

            var result = await action.HandleAsync(new string[0]);

            result.Should().Be(HandleResult.Handled);
        }

        [Fact]
        public void DoesNotPrintAnything()
        {
            var action = new UntypedAction(null!);
            var (printer, sb) = HandlerMocks.MockPrinter();

            action.PrintSelf(printer);

            sb.ToString().Should().Be(string.Empty);
        }
    }
}