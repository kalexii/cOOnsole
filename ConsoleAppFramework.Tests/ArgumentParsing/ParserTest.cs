using System.Collections.Generic;
using ConsoleAppFramework.ArgumentParsing;
using ConsoleAppFramework.ArgumentParsing.StateMachineParsing;

namespace ConsoleAppFramework.Tests.ArgumentParsing
{
    public class ParserTest<T> where T : new()
    {
        private static ArgumentParser<T> Parser() => new();

        protected T Parse(params string[] items) => Parser().Parse(items).Item1;

        protected (T, Dictionary<string, ParsingErrorKind>) ParseWithErrors(params string[] items)
            => Parser().Parse(items);
    }
}