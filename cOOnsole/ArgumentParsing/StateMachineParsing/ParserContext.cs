using System;
using System.Collections.Generic;
using System.Linq;
using cOOnsole.ArgumentParsing.Conversions;
using cOOnsole.Utilities;

namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal sealed class ParserContext
    {
        private readonly Dictionary<string, ArgumentProp> _arguments = new();
        private readonly Dictionary<Type, IStringToTypeConverter> _converters = new();
        private readonly HashSet<ArgumentProp> _notPopulatedRequired = new();
        private readonly List<ParseAttempt> _parseAttempts = new();
        private readonly HashSet<ArgumentProp> _required = new();

        public ParserContext(object target, IReadOnlyList<ArgumentProp> props)
        {
            Target = target;
            for (var i = 0; i < props.Count; i++)
            {
                var pair = props[i];
                foreach (var alias in pair.Argument.Aliases)
                {
                    _arguments[alias] = pair;
                }

                if (pair.IsRequired && pair.Property.PropertyType.GetUnderlyingNullableOrThis() != typeof(bool))
                {
                    _required.Add(pair);
                    _notPopulatedRequired.Add(pair);
                }
            }
        }

        public ISet<ArgumentProp> NotPopulatedRequired => _notPopulatedRequired;

        public object Target { get; }

        public IReadOnlyList<ParseAttempt> ErrorAttempts => _parseAttempts.Where(x => x.ErrorKind is not null).ToList();

        public void SaveAttempt(ParseAttempt attempt)
        {
            _parseAttempts.Add(attempt);
            if (attempt.ErrorKind is null && attempt.Argument is not null)
            {
                _notPopulatedRequired.Remove(attempt.Argument);
            }
        }

        public ArgumentProp? FindArgumentByToken(string token)
            => _arguments.TryGetValue(token, out var prop) ? prop : default;

        public object Convert(string value, Type toType) => GetOrAddConverter(toType).Convert(value);

        public IStringToTypeConverter GetOrAddConverter(Type type)
        {
            IStringToTypeConverter MakeConverter() => type.GetUnderlyingNullableOrThis() switch
            {
                {IsEnum: true} t => new EnumConverter(t),
                { } t when typeof(IConvertible).IsAssignableFrom(t) => new ConvertibleConverter(t),
                var _ => throw new NotSupportedException($"Unable to convert values of type {type}."),
            };

            return _converters.TryGetValue(type, out var converter) ? converter : _converters[type] = MakeConverter();
        }
    }
}