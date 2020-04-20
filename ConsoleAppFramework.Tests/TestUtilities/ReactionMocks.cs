using System.Threading.Tasks;
using Moq;

namespace ConsoleAppFramework.Tests.TestUtilities
{
    public static class ReactionMocks
    {
        private static Mock<IHandler> Always(bool result) => new Mock<IHandler>()
            .Do(mock => mock
                .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
                .Returns(Task.FromResult(result)));

        public static Mock<IHandler> AlwaysTrue() => Always(true);
        public static Mock<IHandler> AlwaysFalse() => Always(false);
    }
}