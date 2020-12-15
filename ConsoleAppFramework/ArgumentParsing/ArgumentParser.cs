using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ConsoleAppFramework.ArgumentParsing.Conversions;
using ConsoleAppFramework.ArgumentParsing.StateMachineParsing;
using ConsoleAppFramework.Description;

namespace ConsoleAppFramework.ArgumentParsing
{
    public class ArgumentParser<T> : IPrintable where T : new()
    {
        private readonly Lazy<List<ArgumentProp>> _properties;

        public ArgumentParser() => _properties = new Lazy<List<ArgumentProp>>(() => typeof(T)
           .GetProperties()
           .Select(p => (p, p.GetCustomAttribute<ArgumentAttribute>(true)))
           .Where(pair => pair.Item2 is not null)
           .Select(pair => new ArgumentProp(pair.Item1, pair.Item2))
           .ToList(), false);

        public (T, Dictionary<string, ParsingErrorKind>) Parse(string[] arguments)
        {
            var props = _properties.Value;
            var nameToArgument = new Dictionary<string, ArgumentProp>(props.Count);
            props.ForEach(pair => nameToArgument[pair.Argument.LongName] = pair);
            props.ForEach(pair =>
            {
                if (pair.Argument.ShortName is { } sn) nameToArgument[sn] = pair;
            });

            var result = new T();
            var state = new ParserState(result,
                new Dictionary<string, ParsingErrorKind>(),
                nameToArgument,
                new Dictionary<Type, IStringConverter>());
            var parser = new ParserMachine(state);
            parser.ParseAndPopulate(arguments);
            return (result, state.Errors);
        }

        public void PrintSelf(IPrinter printer)
        {
            var props = _properties.Value;

            foreach (var (prop, arg) in props)
            {
                var sb = new StringBuilder();
                sb.Append(arg.LongName);
                if (arg.ShortName is { } sn)
                {
                    sb.AppendFormat(" ({0})", sn);
                }

                sb.AppendFormat(" [{0}] ", prop.PropertyType.Name);

                if (arg.Description is { } d)
                {
                    sb.Append(" -- ").Append(d);
                }

                printer.Print(sb.ToString());
            }
        }
    }
}