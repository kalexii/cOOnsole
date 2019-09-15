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
            var fork = new Fork(
                new Token("token a", new ActionDelegate(arg => { })),
                new Token("token b, longer", new ActionDelegate(arg => { })));

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

            const string expected = @"  token a         - Executes custom user action.

  token b, longer - Executes custom user action.";
            textWriterMock.Verify(x => x.WriteLine(expected), Times.Once());
        }
    }
}