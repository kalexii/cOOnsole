using System;
using System.Collections.Generic;
using ConsoleAppFramework.ArgumentParsing.Conversions;

namespace ConsoleAppFramework.ArgumentParsing.StateMachineParsing
{
    internal sealed record ParserState(object Target,
                                       Dictionary<string, ParsingErrorKind> Errors,
                                       Dictionary<string, ArgumentProp> Arguments,
                                       Dictionary<Type, IStringConverter> Converters)
    {
        public ArgumentProp? FindArgumentByToken(string token)
            => Arguments.TryGetValue(token, out var prop) ? prop : default;

        public IStringConverter GetOrAddConverter(Type type)
        {
            IStringConverter MakeConverter() => type switch
            {
                {IsEnum: true} t => new EnumConverter(t),
                { } t when typeof(IConvertible).IsAssignableFrom(t) => new ConvertConverter(t),
                _ => throw new NotSupportedException($"Unable to convert values of type {type}.")
            };

            return Converters.TryGetValue(type, out var converter) ? converter : Converters[type] = MakeConverter();
        }
    }
}