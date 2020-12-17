using System;
using cOOnsole.Handlers;
using Moq;
using Xunit;
using static cOOnsole.Tests.TestUtilities.HandlerMocks;

namespace cOOnsole.Tests.Handlers
{
    public class ForkTests
    {
        [Fact]
        public void TriesScenariosInOrder()
        {
            var handled = AlwaysNotHandled();
            var error = AlwaysHandled();

            var fork = new Fork(handled.Object, error.Object);

            fork.HandleAsync(Array.Empty<string>());

            handled.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            error.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }

        [Fact]
        public void ShortCircuits()
        {
            var handled = AlwaysHandled();
            var error = AlwaysError();

            var fork = new Fork(handled.Object, error.Object);

            fork.HandleAsync(Array.Empty<string>());

            handled.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            error.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Never());
        }
    }
}