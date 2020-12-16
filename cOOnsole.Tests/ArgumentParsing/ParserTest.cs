using cOOnsole.ArgumentParsing;

namespace cOOnsole.Tests.ArgumentParsing
{
    public class ParserTest<T> where T : new()
    {
        private static ArgumentParser<T> Parser() => new();

        protected T Parse(params string[] items) => Parser().Parse(items).Item1;
    }
}