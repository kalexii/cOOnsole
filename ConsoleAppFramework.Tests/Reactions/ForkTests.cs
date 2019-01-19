using System;
using ConsoleAppFramework.Reactions;
using ConsoleAppFramework.Tests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleAppFramework.Tests.Reactions
{
    [TestFixture]
    public class ForkTests
    {
        [Test]
        public void TriesScenariosInOrder()
        {
            var falsePath = new Tracker(new RiggedHandler(false));
            var truePath = new Tracker(new RiggedHandler(true));
            var fork = new Fork(falsePath, truePath);

            fork.React(Array.Empty<string>());

            falsePath.TimesCalled.Should().Be(1);
            truePath.TimesCalled.Should().Be(1);
        }

        [Test]
        public void ShortCircuits()
        {
            var truePath = new Tracker(new RiggedHandler(true));
            var falsePath = new Tracker(new RiggedHandler(false));

            var fork = new Fork(truePath, falsePath);

            fork.React(Array.Empty<string>());

            truePath.TimesCalled.Should().Be(1);
            falsePath.TimesCalled.Should().Be(0);            
        }
    }
}