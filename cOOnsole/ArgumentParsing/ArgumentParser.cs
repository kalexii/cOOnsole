using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using cOOnsole.ArgumentParsing.StateMachineParsing;
using cOOnsole.Description;
using cOOnsole.Utilities;

namespace cOOnsole.ArgumentParsing
{
    internal class ArgumentParser<T> : IPrintable where T : new()
    {
        private readonly Lazy<List<ArgumentProp>> _properties;

        public ArgumentParser() => _properties = new Lazy<List<ArgumentProp>>(() => typeof(T)
           .GetProperties()
           .Select(p => (p, p.GetCustomAttribute<ArgumentAttribute>(true)))
           .Where(pair => pair.Item2 is not null)
           .Select(pair => new ArgumentProp(pair.Item1, pair.Item2))
           .ToList(), false);

        public (T, ParserContext context) Parse(string[] arguments)
        {
            var result = new T();
            var context = new ParserContext(result, _properties.Value);
            var parser = new ParserMachine(context);
            parser.ParseAndPopulate(arguments);
            return (result, context);
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
                    sb.AppendFormat(" | {0}", sn);
                }

                sb.AppendFormat(" [{0}]", prop.ToPrettyTypeName());

                if (arg.Description is { } d)
                {
                    sb.Append(" -- ").Append(d);
                }

                printer.Print(sb.ToString()).NewLine();
            }
        }
    }
}