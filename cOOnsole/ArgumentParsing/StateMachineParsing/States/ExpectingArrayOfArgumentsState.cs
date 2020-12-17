using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace cOOnsole.ArgumentParsing.StateMachineParsing.States
{
    internal class ExpectingArrayOfArgumentsState : IParserState, IFlushable
    {
        private readonly ParserContext _context;
        private readonly string _key;

        public ExpectingArrayOfArgumentsState(ParserContext context, ArgumentProp argument, string key)
        {
            _context = context;
            _key = key;
            Argument = argument;
        }

        public ArgumentProp Argument { get; }

        public List<string> Captured { get; } = new();

        public void Flush()
        {
            var (p, _) = Argument;

            var typeArg = p.PropertyType.HasElementType
                ? p.PropertyType.GetElementType()!
                : p.PropertyType.GetGenericArguments().FirstOrDefault() ?? typeof(string);
            var converter = _context.GetOrAddConverter(typeArg);
            var items = Captured.Select(x => converter.Convert(x)).ToList();

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

                case { } t when t.IsAssignableFrom(typeof(List<>).MakeGenericType(typeArg)):
                    var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(typeArg));
                    items.ForEach(x => list.Add(x));
                    value = list;
                    break;

                default:
                    throw new NotSupportedException($"Type [{p.PropertyType}] is not supported");
            }

            p.SetValue(_context.Target, value);
            _context.SaveAttempt(new ParseAttempt(Argument, _key, Captured.ToArray()));
        }

        public IParserState ParseToken(string token)
        {
            if (_context.FindArgumentByToken(token) is not null)
            {
                Flush();
                return new ExpectingArgumentNameState(_context).ParseToken(token);
            }

            Captured.Add(token);
            return this;
        }
    }
}