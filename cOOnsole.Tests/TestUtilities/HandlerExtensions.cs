using System.IO;
using System.Text;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;
using Moq;

namespace cOOnsole.Tests.TestUtilities
{
    public static class HandlerExtensions
    {
        public static string ExecuteInCliAndCaptureOutput(this IHandler handler, params string[] arguments)
        {
            var writer = new Mock<TextWriter>();
            var window = Mock.Of<IWritableOutput>(x => x.TextWriter == writer.Object);
            var printed = new StringBuilder();
            writer.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => printed.Append(s));
            writer.Setup(x => x.WriteLine()).Callback(() => printed.AppendLine());
            var cli = new Cli(handler, window);
            cli.HandleAsync(arguments);
            return printed.ToString();
        }
    }
}