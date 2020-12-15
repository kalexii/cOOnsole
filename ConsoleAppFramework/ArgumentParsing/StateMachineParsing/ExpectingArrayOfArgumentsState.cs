using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppFramework.ArgumentParsing.StateMachineParsing
{
    internal class ExpectingArrayOfArgumentsState : IParserState
    {
        private readonly ParserState _state;

        public ExpectingArrayOfArgumentsState(ParserState state, ArgumentProp argument)
        {
            _state = state;
            Argument = argument;
        }

        public ArgumentProp Argument { get; }

        public List<string> Captured { get; } = new();

        public IParserState ParseNextToken(string nextToken)
        {
            if (_state.FindArgumentByToken(nextToken) is null)
            {
                Captured.Add(nextToken);
                return this;
            }

            Flush();
            return new ExpectingArgumentNameState(_state);
        }

        public void Flush()
        {
            var (p, _) = Argument;

            var typeArg = p.PropertyType.HasElementType
                ? p.PropertyType.GetElementType()!
                : p.PropertyType.GetGenericArguments().FirstOrDefault() ?? typeof(string);
            var items = Captured.Select(x => _state.GetOrAddConverter(typeArg).Convert(x)).ToList();

            object value;

            switch (p.PropertyType)
            {
                case {IsArray: true}:
                    var array = Array.CreateInstance(typeArg, items.Count);
                    for (var i = 0; i < items.Count; i++)
                    {
                        array.SetValue(items[i], i);
                    }

                    value = array;
                    break;

                case { } t when typeof(IList).IsAssignableFrom(t):
                    var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(typeArg));
                    items.ForEach(x => list.Add(x));
                    value = list;
                    break;

                default:
                    throw new NotSupportedException($"Type [{p.PropertyType}] is not supported");
            }

            p.SetValue(_state.Target, value);
        }
    }
}