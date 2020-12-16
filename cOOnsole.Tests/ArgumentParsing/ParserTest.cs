using cOOnsole.ArgumentParsing;
using FluentAssertions;

namespace cOOnsole.Tests.ArgumentParsing
{
    public class ParserTest
    {
        private static ArgumentParser<T> Parser<T>() where T : new() => new();

        protected static T Parse<T>(params string[] items) where T : new()
        {
            var (result, context) = Parser<T>().Parse(items);
            context.ErrorAttempts.Should().BeEmpty();
            context.NotPopulatedRequired.Should().BeEmpty();
            return result;
        }
    }
}