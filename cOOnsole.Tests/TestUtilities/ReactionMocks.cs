using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using Moq;

namespace cOOnsole.Tests.TestUtilities
{
    public static class ReactionMocks
    {
        private static Mock<IHandler> Always(HandleResult result) => new Mock<IHandler>()
           .Do(mock => mock
               .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
               .Returns(Task.FromResult(result)));

        public static Mock<IHandler> AlwaysHandled() => Always(HandleResult.Handled);

        public static Mock<IHandler> AlwaysError() => Always(HandleResult.Error);

        public static Mock<IHandler> AlwaysNotHandled() => Always(HandleResult.NotHandled);
    }
}