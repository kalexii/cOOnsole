using cOOnsole.ArgumentParsing;

namespace cOOnsole.Tests.ArgumentParsing
{
    public class ParserTest
    {
        private static ArgumentParser<T> Parser<T>() where T : new() => new();

        protected static T Parse<T>(params string[] items) where T : new() => Parser<T>().Parse(items).Item1;
    }
}