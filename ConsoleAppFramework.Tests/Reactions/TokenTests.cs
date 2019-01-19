using System;
using System.Linq;
using ConsoleAppFramework.Reactions;
using ConsoleAppFramework.Tests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleAppFramework.Tests.Reactions
{
    [TestFixture]
    public class TokenTests
    {
        [Test]
        public void DoesNotAllowNullsInToken()
            => new Action[]
            {
                () => new Token(null, new NoOp()),
                () => new Token("token", null)
            }.Select(x => x.Should().Throw<ArgumentNullException>()).ToArray();

        [Test]
        public void ReturnsTrueIfTokenMatches()
        {
            var tracker = new Tracker();
            var token = new Token("token", tracker);

            var actual = token.React(new[] {"token"});

            actual.Should().BeTrue();
            tracker.TimesCalled.Should().Be(1);
        }

        [Test]
        public void PassesArgumentForwardWithoutMatchedToken()
        {
            var tracker = new Tracker();
            var token = new Token("token", tracker);

            token.React(new[] {"token"});

            tracker.LastInput.Should().BeEquivalentTo(Array.Empty<string>());
        }
    }
}