using System;
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

            new Cli(action.Object);

            action.Verify(x => x.SetContext(It.IsNotNull<HandlerContext>()), Times.Once());
        }

        [Fact]
        public void PrintsEntireHandlerTreeIfUnmatched()
        {
            var handler =
                Fork("aFork",
                    Token("a1", NoopHandler.NotHandled),
                    Token("a2", NoopHandler.NotHandled),
                    Token("a3", NoopHandler.NotHandled)
                );

            handler.ExecuteInCliAndCaptureOutput("c4").Should().Be(AsExpectedForThisTest());
        }

        [Fact]
        public void PrintsScopedTreeIfErroredOutWithScope()
        {
            var handler =
                Fork(
                    Token("a1", NoopHandler.NotHandled),
                    Token("a2", NoopHandler.NotHandled),
                    Token("a3", Fork(
                        Token("b1", NoopHandler.NotHandled),
                        Token("b2", NoopHandler.Error)
                    )));

            handler.ExecuteInCliAndCaptureOutput("b2").Should().Be(AsExpectedForThisTest());
        }

        [Fact]
        public void StaysSilentIfHandled()
            => Token("a1", NoopHandler.Handled).ExecuteInCliAndCaptureOutput("a1").Should().Be(string.Empty);

        [Fact]
        public void PrettyPrintsExceptions()
            => new ActionDelegate("does stuff", (_, _) => throw new Exception())
               .ExecuteInCliAndCaptureOutput().Split(Environment.NewLine)[0].Should().Be(AsExpectedForThisTest());
    }
}