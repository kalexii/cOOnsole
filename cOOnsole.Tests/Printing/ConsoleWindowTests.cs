using System;
using cOOnsole.Printing;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests.Printing
{
    public class ConsoleWindowTests
    {
        [Fact]
        public void ConsoleWindowShouldReturnConsoleOut() => new ConsoleOutput().TextWriter.Should().Be(Console.Out);
    }
}