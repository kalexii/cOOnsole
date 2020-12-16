using System;
using cOOnsole.Description;
using FluentAssertions;
using Xunit;

namespace cOOnsole.Tests.Description
{
    public class ConsoleWindowTests
    {
        [Fact]
        public void ConsoleWindowShouldReturnConsoleOut() => new ConsoleWindow().TextWriter.Should().Be(Console.Out);
    }
}