using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleAppFramework.Description;
using ConsoleAppFramework.Tests.TestUtilities;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static ConsoleAppFramework.Shortcuts;

namespace ConsoleAppFramework.Tests.Description
{
    [TestFixture]
    public class PrinterTest : ResourceAssistedTest
    {
        [Test]
        public void Test1()
        {
            var noop = Action("noop", _ => { });
            var reaction = Fork("root fork",
                Token("token a", noop),
                Token("token b, longer", noop));

            Execute(reaction);
        }

        [Test]
        public void Test2()
        {
            var noop = Action("noop", _ => { });
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
    }
}