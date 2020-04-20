using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using static ConsoleAppFramework.Tests.TestUtilities.ReactionMocks;
using ConsoleAppFramework.Reactions;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ConsoleAppFramework.Tests.Reactions
{
    [TestFixture]
    public class TokenTests
    {
        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void DoesNotAllowNullsInToken() => new Action[]
                {
                    () => new Token(null, AlwaysTrue().Object),
                    () => new Token("token", null)
                }
                .Select(x => x.Should().Throw<ArgumentNullException>())
                .ToArray();

        [Test]
        public async Task ReturnsTrueIfTokenMatches()
        {
            var tracker = AlwaysTrue();
            var token = new Token("token", tracker.Object);

            var actual = await token.HandleAsync(new[] {"token"});

            actual.Should().BeTrue();
            tracker.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }

        [Test]
        public async Task PassesArgumentForwardWithoutMatchedToken()
        {
            var tracker = AlwaysTrue();
            var token = new Token("token", tracker.Object);

            await token.HandleAsync(new[] {"token"});

            tracker.Verify(x => x.HandleAsync(Array.Empty<string>()), Times.Once());
        }
    }
}