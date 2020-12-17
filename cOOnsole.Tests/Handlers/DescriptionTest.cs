using System.Threading.Tasks;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Moq;
using Xunit;

namespace cOOnsole.Tests.Handlers
{
    public class DescriptionTest : ResourceAssistedTest
    {
        [Fact]
        public void PrintsDescription()
        {
            var expectation = AsExpectedForThisTest();
            var (printer, sb) = HandlerMocks.MockPrinter();

            new Description("description", new Token("token", Mock.Of<IHandler>())).PrintSelf(printer);

            sb.ToString().Should().Be(expectation);
        }

        [Fact]
        public async Task DelegatesToInnerChild()
        {
            var handler = new Mock<IHandler>();
            handler.Setup(x => x.HandleAsync(It.IsAny<string[]>())).Returns(Task.FromResult(HandleResult.Handled));

            await new Description("description", new Token("token", handler.Object)).HandleAsync(new[] {"token"});

            handler.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }
    }
}