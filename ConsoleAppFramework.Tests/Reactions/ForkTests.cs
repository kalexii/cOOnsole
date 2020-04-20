using System;
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

            fork.React(Array.Empty<string>());

            falsePath.Verify(x => x.React(It.IsAny<string[]>()), Times.Once());
            truePath.Verify(x => x.React(It.IsAny<string[]>()), Times.Once());
        }

        [Test]
        public void ShortCircuits()
        {
            var truePath = new Mock<IReaction>().Do(mock => mock
                .Setup(x => x.React(It.IsAny<string[]>()))
                .Returns(true));
            var falsePath = new Mock<IReaction>().Do(mock => mock
                .Setup(x => x.React(It.IsAny<string[]>()))
                .Returns(false));

            var fork = new Fork(truePath.Object, falsePath.Object);

            fork.React(Array.Empty<string>());

            truePath.Verify(x => x.React(It.IsAny<string[]>()), Times.Once());
            falsePath.Verify(x => x.React(It.IsAny<string[]>()), Times.Never());
        }
    }
}