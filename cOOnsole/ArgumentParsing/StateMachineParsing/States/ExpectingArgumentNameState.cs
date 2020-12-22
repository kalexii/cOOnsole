using System;
using System.Collections;
using cOOnsole.Utilities;

namespace cOOnsole.ArgumentParsing.StateMachineParsing.States
{
    internal class ExpectingArgumentNameState : IParserState
    {
        private readonly ParserContext _context;

        public ExpectingArgumentNameState(ParserContext context) => _context = context;

        public IParserState ParseToken(string token)
        {
            if (_context.FindArgumentByToken(token) is { } prop)
            {
                var (property, _) = prop;
                switch (property.PropertyType)
                {
                    case {IsValueType: true} t when t.GetUnderlyingNullableOrThis() == typeof(bool):
                        _context.SaveAttempt(new ParseAttempt(prop, token, new[] {token}));
                        property.SetValue(_context.Target, true);
                        return this;

                    case {IsArray: false} t when typeof(IConvertible).IsAssignableFrom(t.GetUnderlyingNullableOrThis()):
                        return new ExpectingArgumentValueState(_context, prop, token);

                    case {IsArray: true}:
                    case { } t when typeof(IEnumerable).IsAssignableFrom(t):
                        return new ExpectingArrayOfArgumentsState(_context, prop, token);

                    default:
                        throw new NotSupportedException($"Property type of {property.PropertyType} is not supported.");
                }
            }

            _context.SaveAttempt(new ParseAttempt(null, token, new string[0], ParsingErrorKind.OptionNotRecognized));
            return this;
        }
    }
}