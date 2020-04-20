using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Reactions;
using ConsoleAppFramework.Tests.TestUtilities;
using Moq;
using NUnit.Framework;
using static ConsoleAppFramework.Tests.TestUtilities.ReactionMocks;

namespace ConsoleAppFramework.Tests.Reactions
{
    [TestFixture]
    public class ForkTests
    {
        [Test]
        public void TriesScenariosInOrder()
        {
            var falsePath = AlwaysFalse();
            var truePath = AlwaysTrue();
            var fork = new Fork(falsePath.Object, truePath.Object);

            fork.HandleAsync(Array.Empty<string>());

            falsePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            truePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }

        [Test]
        public void ShortCircuits()
        {
            var truePath = new Mock<IHandler>().Do(mock => mock
                .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
                .Returns(Task.FromResult(true)));
            var falsePath = new Mock<IHandler>().Do(mock => mock
                .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
                .Returns(Task.FromResult(false)));

            var fork = new Fork(truePath.Object, falsePath.Object);

            fork.HandleAsync(Array.Empty<string>());

            truePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
            falsePath.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Never());
        }
    }
}