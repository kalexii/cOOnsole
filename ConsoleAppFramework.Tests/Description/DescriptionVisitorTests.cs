using System.Linq;
using ConsoleAppFramework.Description;
using ConsoleAppFramework.Reactions;
using FluentAssertions;
using NUnit.Framework;
using Console = ConsoleAppFramework.Description.Console;

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
            printer.Print(new Console());
        }
    }
}