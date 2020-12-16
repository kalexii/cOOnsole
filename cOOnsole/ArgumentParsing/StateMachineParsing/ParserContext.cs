using System;
using System.Collections.Generic;
using System.Linq;
using cOOnsole.ArgumentParsing.Conversions;
using cOOnsole.Utilities;
using kalexi.Monads.Either.Code;

namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal sealed record ParseAttempt(ArgumentProp? Argument,
                                        string AttemptedKey,
                                        Either<string?, List<string>> AttemptedValue,
                                        ParsingErrorKind? ErrorKind = null);

    internal sealed class ParserContext
    {
        private readonly Dictionary<Type, IStringConverter> _converters = new();
        private readonly Dictionary<string, ArgumentProp> _arguments = new();
        private readonly HashSet<ArgumentProp> _required = new();
        private readonly HashSet<ArgumentProp> _populated = new();
        private readonly List<ParseAttempt> _parseAttempts = new();

        public ParserContext(object target, IReadOnlyList<ArgumentProp> props)
        {
            Target = target;
            for (var i = 0; i < props.Count; i++)
            {
                var pair = props[i];
                _arguments[pair.Argument.LongName] = pair;

                if (pair.Argument.ShortName is { } sn) _arguments[sn] = pair;

                if (!pair.Property.IsNullable()) _required.Add(pair);
            }
        }

        public void SaveAttempt(ParseAttempt attempt)
        {
            _parseAttempts.Add(attempt);
            if (attempt.ErrorKind is null && attempt.Argument is not null)
            {
                _populated.Add(attempt.Argument);
            }
        }

        public List<ArgumentProp> GetUnpopulatedRequired() => _required.Except(_populated).ToList();

        public object Target { get; }

        public IReadOnlyList<ParseAttempt> ParseAttempts => _parseAttempts;
        public IList<ParseAttempt> ErrorAttempts => _parseAttempts.Where(x => x.ErrorKind is not null).ToList();

        public ArgumentProp? FindArgumentByToken(string token)
            => _arguments.TryGetValue(token, out var prop) ? prop : default;

        public IStringConverter GetOrAddConverter(Type type)
        {
            IStringConverter MakeConverter() => type.GetUnderlyingNullableOrThis() switch
            {
                {IsEnum: true} t => new EnumConverter(t),
                { } t when typeof(IConvertible).IsAssignableFrom(t) => new ConvertConverter(t),
                var _ => throw new NotSupportedException($"Unable to convert values of type {type}."),
            };

            return _converters.TryGetValue(type, out var converter) ? converter : _converters[type] = MakeConverter();
        }
    }
}