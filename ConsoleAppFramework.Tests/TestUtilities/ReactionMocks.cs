using Moq;

namespace ConsoleAppFramework.Tests.TestUtilities
{
    public static class ReactionMocks
    {
        private static Mock<IReaction> Always(bool result) => new Mock<IReaction>()
            .Do(mock => mock
                .Setup(x => x.React(It.IsAny<string[]>()))
                .Returns(result));

        public static Mock<IReaction> AlwaysTrue() => Always(true);
        public static Mock<IReaction> AlwaysFalse() => Always(false);
    }
}