using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Xunit;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Tests.Description
{
    public class PrinterTest : ResourceAssistedTest
    {
        [Fact]
        public void ForkWithDescriptionTest()
        {
            var noop = NoopHandler.Handled;
            var handler =
                PrintUsage(
                    Fork(
                        Token("token a", noop),
                        Token("token b, longer", noop)));

            handler.ExecuteInCliAndCaptureOutput().Should().Be(AsExpectedForThisTest());
        }

        [Fact]
        public void NestedForksWithDescriptionTest()
        {
            var noop = NoopHandler.Handled;
            var handler =
                PrintUsage(
                    Fork(
                        Fork(
                            Token("root-fork-token1", noop),
                            Token("root-fork-token2", noop)),
                        Token("root-token1", noop),
                        Token("root-token2", noop)));

            handler.ExecuteInCliAndCaptureOutput().Should().Be(AsExpectedForThisTest());
        }
    }
}