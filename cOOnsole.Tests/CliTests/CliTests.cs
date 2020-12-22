using System;
using System.Threading.Tasks;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Moq;
using Xunit;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Tests.CliTests
{
    public class CliTests : ResourceAssistedTest
    {
        [Fact]
        public void PopulatesContext()
        {
            var action = new Mock<IHandler>();

            var _ = new Cli(action.Object);

            action.Verify(x => x.SetContext(It.IsNotNull<HandlerContext>()), Times.Once());
        }

        [Fact]
        public async Task StaysSilentIfHandled()
        {
            var tree = Unconditional(HandleResult.Handled);

            var (handled, printed) = await Token("a1", tree).ExecuteAndCaptureAsync("a1");

            handled.Should().BeTrue();
            printed.Should().Be(string.Empty);
        }

        [Fact]
        public async Task PrettyPrintsExceptions()
        {
            var expectation = AsExpectedForThisTest();

            var (handled, printed) = await new UntypedAction((_, _) => throw new Exception()).ExecuteAndCaptureAsync();

            handled.Should().BeFalse();
            printed.TakeFirstLine().Should().Be(expectation);
        }

        [Theory]
        [InlineData(HandleResult.HandledWithError)]
        [InlineData(HandleResult.NotMatched)]
        public async Task NotSuccessConvertsToFalse(HandleResult stubbedResult)
        {
            var tree = Unconditional(stubbedResult);

            var result = await new Cli(tree).HandleAsync(new string[0]);

            result.Should().Be(false);
        }
    }
}