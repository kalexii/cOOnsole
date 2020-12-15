using System;
using System.Collections;

namespace ConsoleAppFramework.ArgumentParsing.StateMachineParsing
{
    internal class ExpectingArgumentNameState : IParserState
    {
        private readonly ParserState _state;

        public ExpectingArgumentNameState(ParserState state) => _state = state;

        public IParserState ParseNextToken(string nextToken)
        {
            if (_state.FindArgumentByToken(nextToken) is { } prop)
            {
                var (property, _) = prop;
                switch (property.PropertyType)
                {
                    case {IsValueType: true} t when t == typeof(bool):
                        property.SetValue(_state.Target, true);
                        return this;

                    case {IsArray: false} t when typeof(IConvertible).IsAssignableFrom(t):
                        return new ExpectingArgumentValueState(_state, prop);

                    case {IsArray: true}:
                    case { } t when typeof(IEnumerable).IsAssignableFrom(t):
                        return new ExpectingArrayOfArgumentsState(_state, prop);

                    default:
                        throw new NotSupportedException($"Property type of {property.PropertyType} is not supported.");
                }
            }

            _state.Errors[nextToken] = ParsingErrorKind.OptionNotRecognized;
            return this;
        }
    }
}