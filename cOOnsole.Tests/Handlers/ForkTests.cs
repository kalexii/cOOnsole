using System;
using System.Threading.Tasks;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using cOOnsole.Tests.TestUtilities;
using Moq;
using Xunit;
using static cOOnsole.Tests.TestUtilities.ReactionMocks;

namespace cOOnsole.Tests.Handlers
{
    public class ForkTests
    {
        [Fact]
        public void TriesScenariosInOrder()
        {
            var falsePath = AlwaysNotHandled();
            var truePath = AlwaysHandled();
            var fork = new Fork(falsePath.Object, truePath.Object);

            fork.HandleAsync(Array.Empty<string>());

            falsePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            truePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }

        [Fact]
        public void ShortCircuits()
        {
            var truePath = new Mock<IHandler>().Do(mock => mock
               .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
               .Returns(Task.FromResult(HandleResult.Handled)));
            var falsePath = new Mock<IHandler>().Do(mock => mock
               .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
               .Returns(Task.FromResult(HandleResult.Error)));

            var fork = new Fork(truePath.Object, falsePath.Object);

            fork.HandleAsync(Array.Empty<string>());

            truePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            falsePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Never());
        }
    }
}