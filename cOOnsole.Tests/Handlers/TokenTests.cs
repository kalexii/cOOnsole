using System.Threading.Tasks;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using FluentAssertions;
using Moq;
using Xunit;
using static cOOnsole.Tests.TestUtilities.HandlerMocks;

namespace cOOnsole.Tests.Handlers
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

            var actual = await token.HandleAsync(new[] {"token"});

            actual.Should().BeEquivalentTo(HandleResult.Handled);
            tracker.Verify(x => x.HandleAsync(new string[0]), Times.Once());
        }
    }
}