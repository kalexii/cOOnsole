using System;
using System.Threading.Tasks;
using cOOnsole.Reactions;
using FluentAssertions;
using Moq;
using Xunit;
using static cOOnsole.Tests.TestUtilities.ReactionMocks;

namespace cOOnsole.Tests.Reactions
{
    public class TokenTests
    {
        [Fact]
        public async Task ReturnsTrueIfTokenMatches()
        {
            var tracker = AlwaysHandled();
            var token = new Token("token", tracker.Object);

            var actual = await token.HandleAsync(new[] {"token"});

            actual.Should().BeEquivalentTo(HandleResult.Handled);
            tracker.Verify(x => x.HandleAsync(It.IsAny<string[]>()), Times.Once());
        }

        [Fact]
        public async Task PassesArgumentForwardWithoutMatchedToken()
        {
            var tracker = AlwaysHandled();
            var token = new Token("token", tracker.Object);

            await token.HandleAsync(new[] {"token"});

            tracker.Verify(x => x.HandleAsync(Array.Empty<string>()), Times.Once());
        }
    }
}