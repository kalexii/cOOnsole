using System.IO;
using System.Text;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;
using Moq;

namespace cOOnsole.Tests.TestUtilities
{
    public static class HandlerMocks
    {
        public static (Mock<IWritableOutput>, StringBuilder) MockOutput()
        {
            var writer = new Mock<TextWriter>();
            var output = Mock.Of<IWritableOutput>(x => x.TextWriter == writer.Object);
            var printed = new StringBuilder();
            writer.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => printed.Append(s));
            writer.Setup(x => x.WriteLine()).Callback(() => printed.AppendLine());
            return (Mock.Get(output), printed);
        }

        public static (IPrinter, StringBuilder sb) MockPrinter()
        {
            var (output, sb) = MockOutput();
            return (new Printer(output.Object), sb);
        }

        private static Mock<IHandler> Always(HandleResult result) => new Mock<IHandler>()
           .Do(mock => mock
               .Setup(x => x.HandleAsync(It.IsAny<string[]>()))
               .Returns(Task.FromResult(result)));

        public static Mock<IHandler> AlwaysHandled() => Always(HandleResult.Handled);

        public static Mock<IHandler> AlwaysError() => Always(HandleResult.Error);

        public static Mock<IHandler> AlwaysNotHandled() => Always(HandleResult.NotHandled);
    }
}