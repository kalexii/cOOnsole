using System;
using cOOnsole.Handlers.Base;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Moq;
using Xunit;
using static cOOnsole.Shortcuts;
using Action = cOOnsole.Handlers.Action;

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
        public void StaysSilentIfHandled()
            => Token("a1", NoopHandler.Handled).ExecuteInCliAndCaptureOutput("a1").Should().Be(string.Empty);

        [Fact]
        public void PrettyPrintsExceptions()
            => new Action("does stuff", (_, _) => throw new Exception())
               .ExecuteInCliAndCaptureOutput().Split(Environment.NewLine)[0].Should().Be(AsExpectedForThisTest());
    }
}