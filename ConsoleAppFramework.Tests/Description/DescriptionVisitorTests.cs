using System.IO;
using System.Linq;
using ConsoleAppFramework.Description;
using ConsoleAppFramework.Reactions;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ConsoleAppFramework.Tests.Description
{
    [TestFixture]
    public class DescriptionVisitorTests
    {
        [Test]
        public void WorksWithFork()
        {
            var fork =
                new Fork(
                    new Token("token a", new FuncDelegate(arg => true)),
                    new Token("token b, longer", new FuncDelegate(arg => true)));

            var descriptionVisitor = new DescriptionVisitor();
            fork.Describe(descriptionVisitor);

            descriptionVisitor.Items.Should().HaveCount(4);
            descriptionVisitor.Items.Select(x => x.DescriptionItemType).Should().BeEquivalentTo(new[]
            {
                DescriptionItemType.Header,
                DescriptionItemType.Content,
                DescriptionItemType.Header,
                DescriptionItemType.Content,
            });

            var printer = new DescriptionPrinter(descriptionVisitor.Items);
            var consoleMock = new Mock<IWritableWindow>();
            var textWriterMock = new Mock<TextWriter>();
            consoleMock.Setup(x => x.TextWriter).Returns(textWriterMock.Object);

            printer.Print(consoleMock.Object);

            var expected =
@"  token a         - Executes custom function.

  token b, longer - Executes custom function.";
            textWriterMock.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once());
            var line = textWriterMock.Invocations.Single().Arguments[0] as string;
            line.Should().BeEquivalentTo(expected);
        }
    }
}