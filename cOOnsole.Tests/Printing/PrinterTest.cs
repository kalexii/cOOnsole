using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Xunit;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Tests.Printing
{
    public class PrinterTest : ResourceAssistedTest
    {
        [Fact]
        public async Task ForkWithDescriptionTest()
        {
            var expectation = AsExpectedForThisTest();
            var noop = Description("noop", Unconditional(HandleResult.Handled));
            var handler =
                PrintUsageIfNotMatched(
                    Fork(
                        Token("token a", noop),
                        Token("token b, longer", noop)));

            var (handled, printed) = await handler.ExecuteAndCaptureAsync();

            handled.Should().BeTrue();
            printed.SkipFirstLine().Should().Be(expectation);
        }

        [Fact]
        public async Task NestedForksWithDescriptionTest()
        {
            var expectation = AsExpectedForThisTest();
            var noop = Description("noop", Unconditional(HandleResult.Handled));
            var handler =
                PrintUsageIfNotMatched(
                    Fork(
                        Fork(
                            Token("root-fork-token1", noop),
                            Token("root-fork-token2", noop)),
                        Token("root-token1", noop),
                        Token("root-token2", noop)));

            var (handled, printed) = await handler.ExecuteAndCaptureAsync();

            handled.Should().BeTrue();
            printed.SkipFirstLine().Should().Be(expectation);
        }
    }
}