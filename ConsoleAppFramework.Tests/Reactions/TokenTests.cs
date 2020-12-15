using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ConsoleAppFramework.Reactions;
using FluentAssertions;
using Moq;
using Xunit;
using static ConsoleAppFramework.Tests.TestUtilities.ReactionMocks;

namespace ConsoleAppFramework.Tests.Reactions
{
    public class TokenTests
    {
        [Fact]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void DoesNotAllowNullsInToken() => new Action[]
            {
                () => new Token(null!, AlwaysSuccess().Object),
                () => new Token("token", null!)
            }
           .Select(x => x.Should().Throw<ArgumentNullException>())
           .ToArray();

        [Fact]
        public async Task ReturnsTrueIfTokenMatches()
        {
            var tracker = AlwaysSuccess();
            var token = new Token("token", tracker.Object);

            var actual = await token.HandleAsync(new[] {"token"});

            actual.Should().BeNull();
            tracker.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }

        [Fact]
        public async Task PassesArgumentForwardWithoutMatchedToken()
        {
            var tracker = AlwaysSuccess();
            var token = new Token("token", tracker.Object);

            await token.HandleAsync(new[] {"token"});

            tracker.Verify(x => x.HandleAsync(Array.Empty<string>()), Times.Once());
        }
    }
}