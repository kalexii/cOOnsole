using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Reactions;
using ConsoleAppFramework.Tests.TestUtilities;
using Moq;
using Xunit;
using static ConsoleAppFramework.Tests.TestUtilities.ReactionMocks;

namespace ConsoleAppFramework.Tests.Reactions
{
    public class ForkTests
    {
        [Fact]
        public void TriesScenariosInOrder()
        {
            var falsePath = AlwaysError();
            var truePath = AlwaysSuccess();
            var fork = new Fork("test", falsePath.Object, truePath.Object);

            fork.HandleAsync(Array.Empty<string>());

            falsePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            truePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }

        [Fact]
        public void ShortCircuits()
        {
            var truePath = new Mock<IHandler>().Do(mock => mock
               .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
               .Returns(Task.FromResult((IHandler?) null)));
            var falsePath = new Mock<IHandler>().Do(mock => mock
               .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
#pragma warning disable 8620
               .Returns(Task.FromResult(mock.Object)));
#pragma warning restore 8620

            var fork = new Fork("test", truePath.Object, falsePath.Object);

            fork.HandleAsync(Array.Empty<string>());

            truePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            falsePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Never());
        }
    }
}