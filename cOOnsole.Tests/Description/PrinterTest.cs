using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using cOOnsole.Description;
using cOOnsole.Tests.TestUtilities;
using FluentAssertions;
using Moq;
using Xunit;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Tests.Description
{
    public class PrinterTest : ResourceAssistedTest
    {
        [Fact]
        public void Test1()
        {
            var noop = Action("noop", A);
            var reaction = Fork("root fork",
                Token("token a", noop),
                Token("token b, longer", noop));

            Execute(reaction);
        }

        [Fact]
        public void Test2()
        {
            var noop = Action("noop", A);
            var reaction = Fork("outer fork",
                Fork("inner fork",
                    Token("root-fork-token1", noop),
                    Token("root-fork-token2", noop)),
                Token("root-token1", noop),
                Token("root-token2", noop));

            Execute(reaction);
        }

        private void Execute(IHandler handler, [CallerMemberName] string? caller = null)
        {
            var window = new Mock<IWritableWindow>();
            var writer = new Mock<TextWriter>();
            window.Setup(x => x.TextWriter).Returns(writer.Object);
            var printed = new StringBuilder();
            writer.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => printed.Append(s));
            writer.Setup(x => x.WriteLine()).Callback(() => printed.AppendLine());
            var cli = new Cli(handler, window.Object);
            cli.HandleAsync(new string[0]);

            var expected = GetExpected(caller!);
            printed.ToString().Should().Be(expected);
        }

        private static void A(string[] strings, HandlerContext handlerContext)
        {
        }
    }
}