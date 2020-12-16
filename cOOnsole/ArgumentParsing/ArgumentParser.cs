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

        public sealed record ParameterParts(string Name, string Type, string Description = "");

        public void PrintSelf(IPrinter printer)
        {
            var props = _properties.Value;
            var required = props.Where(x => x.IsRequired).ToList();
            var notRequired = props.Except(required).ToList();

            static ParameterParts ToParts(ArgumentProp pair) => new(
                pair.Argument.ShortName is null
                    ? pair.Argument.LongName
                    : $"{pair.Argument.LongName} ({pair.Argument.ShortName})",
                pair.Property.ToPrettyTypeName(),
                pair.Argument.Description ?? ""
            );

            static int[] MaxLengths(IReadOnlyCollection<ParameterParts> parts) => new[]
            {
                parts.Select(x => x.Name).Max(x => x.Length),
                parts.Select(x => x.Type).Max(x => x.Length),
                parts.Select(x => x.Description).Max(x => x.Length),
            };

            var requiredParts = required.ConvertAll(ToParts);
            printer.Print("required arguments:").NewLine().Indent();
            foreach (var parts in requiredParts)
            {
                PrintPart(printer, parts, MaxLengths(requiredParts));
            }

            printer.NewLine().Unindent();

            printer.Print("optional arguments:").NewLine().Indent();
            var notRequiredParts = notRequired.ConvertAll(ToParts);
            foreach (var parts in notRequiredParts)
            {
                PrintPart(printer, parts, MaxLengths(notRequiredParts));
            }

            printer.NewLine().Unindent();
        }

        private static void PrintPart(IPrinter printer, ParameterParts parts, int[] requiredMaxLengths) =>
            printer.Print(new StringBuilder()
               .Append(parts.Name.PadLeft(requiredMaxLengths[0]))
               .Append(" : ")
               .Append(parts.Type.PadRight(requiredMaxLengths[1]))
               .Append(" # ")
               .Append(parts.Description.PadRight(requiredMaxLengths[2])).ToString()).NewLine();
    }
}