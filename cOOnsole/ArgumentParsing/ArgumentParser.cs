using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using cOOnsole.ArgumentParsing.Exceptions;
using cOOnsole.ArgumentParsing.StateMachineParsing;
using cOOnsole.Printing;
using cOOnsole.Utilities;

namespace cOOnsole.ArgumentParsing
{
    internal class ArgumentParser<T> : IPrintable where T : new()
    {
        private readonly Lazy<List<ArgumentProp>> _properties;

        public ArgumentParser()
        {
            _properties = new Lazy<List<ArgumentProp>>(() =>
            {
                var type = typeof(T);
                var result = type
                   .GetProperties()
                   .Select(p => (p, p.GetCustomAttribute<ArgumentAttribute>(true)))
                   .Where(pair => pair.Item2 is not null)
                   .Select(pair => new ArgumentProp(pair.Item1, pair.Item2))
                   .ToList();

                var duplicationDictionary = new Dictionary<string, int>();
                foreach (var alias in result.SelectMany(argument => argument.Argument.Aliases))
                {
                    if (duplicationDictionary.ContainsKey(alias))
                    {
                        throw new DuplicateAliasException(type, alias);
                    }

                    duplicationDictionary.Add(alias, 1);
                }

                return result;
            }, false);
        }

        public void PrintSelf(IPrinter printer)
        {
            var props = _properties.Value;
            var required = props.Where(x => x.IsRequired).ToList();
            var notRequired = props.Except(required).ToList();

            static ParameterParts ToParts(ArgumentProp pair) => new(
                string.Join(" | ", pair.Argument.Aliases),
                pair.Property.ToPrettyTypeName(),
                pair.Argument.Description ?? ""
            );

            static int[] MaxLengths(IEnumerable<ParameterParts> parts)
            {
                var result = new int[3];
                foreach (var (name, type, description) in parts)
                {
                    result[0] = Math.Max(result[0], name.Length);
                    result[1] = Math.Max(result[1], type.Length);
                    result[2] = Math.Max(result[2], description.Length);
                }

                return result;
            }

            static void PrintPart(IPrinter printer, ParameterParts parts, IReadOnlyList<int> requiredMaxLengths)
            {
                var (name, type, description) = parts;
                var sb = new StringBuilder()
                   .Append(name.PadLeft(requiredMaxLengths[0]))
                   .Append(" : ")
                   .Append(type.PadRight(requiredMaxLengths[1]));
                if (description != string.Empty)
                {
                    sb.Append(" # ").Append(description);
                }

                printer.Print(sb.ToString()).NewLine();
            }

            var requiredParts = required.ConvertAll(ToParts);
            if (requiredParts.Count > 0)
            {
                printer.Print("required arguments:").NewLine();
                using (printer.Indent())
                {
                    foreach (var parts in requiredParts)
                    {
                        PrintPart(printer, parts, MaxLengths(requiredParts));
                    }
                }

                printer.NewLine();
            }

            var notRequiredParts = notRequired.ConvertAll(ToParts);
            if (notRequiredParts.Count > 0)
            {
                printer.Print("optional arguments:").NewLine();
                using (printer.Indent())
                {
                    foreach (var parts in notRequiredParts)
                    {
                        PrintPart(printer, parts, MaxLengths(notRequiredParts));
                    }
                }

                printer.NewLine();
            }
        }

        public (T, ParserContext context) Parse(string[] arguments)
        {
            var result = new T();
            var context = new ParserContext(result, _properties.Value);
            var parser = new ParserStateMachine(context);
            parser.ParseAndPopulate(arguments);
            return (result, context);
        }

        public sealed record ParameterParts(string Name, string Type, string Description = "");
    }
}