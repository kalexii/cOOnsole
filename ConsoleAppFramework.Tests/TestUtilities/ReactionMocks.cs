using System;
using System.Threading.Tasks;
using Moq;

namespace ConsoleAppFramework.Tests.TestUtilities
{
    public static class ReactionMocks
    {
        private static Mock<IHandler> Always(Func<Mock<IHandler>, IHandler?> result) => new Mock<IHandler>()
           .Do(mock => mock
               .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
               .Returns(Task.FromResult(result(mock))));

        public static Mock<IHandler> AlwaysSuccess() => Always(_ => null);
        public static Mock<IHandler> AlwaysError() => Always(x => x.Object);
    }
}