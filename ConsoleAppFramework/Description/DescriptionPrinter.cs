using System;
using System.Collections.Generic;
using System.Text;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Description
{
    public class DescriptionPrinter : IDescriptionPrinter
    {
        private readonly IReadOnlyList<DescriptionItem> items;

        public DescriptionPrinter(IReadOnlyList<DescriptionItem> items)
            => this.items = Guard.Argument(items).NotNull().DoesNotContainNull().Value;

        public void Print([NotNull] IWritableWindow window)
        {
            Guard.Argument(window).NotNull();
            const int indentationSpaces = 2;
            var (maxHeaderWidthWithIndent, maxHeaderWidth) = GetMaxHeaderWidths(indentationSpaces);
            var formattedOutput = BuildPrintedString(indentationSpaces, maxHeaderWidth);
            window.TextWriter.WriteLine(formattedOutput);
        }

        private string BuildPrintedString(int indentationSpaces, int maxHeaderWidth)
        {
            var sb = new StringBuilder();
            IterateWithIndent(context =>
            {
                var item = context.Item;
                switch (item.DescriptionItemType)
                {
                    case DescriptionItemType.Header:
                        AppendHeader(sb, indentationSpaces * context.Indentation, item.Value.PadRight(maxHeaderWidth));
                        break;
                    case DescriptionItemType.Content:
                        AppendContent(sb, item);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
            return sb.ToString().TrimEnd();
        }

        private static void AppendContent(StringBuilder sb, DescriptionItem item)
        {
            sb.AppendLine(item.Value);
            sb.AppendLine();
        }

        private static void AppendHeader(StringBuilder sb, int indentationSpaces, string value)
        {
            for (var i = 0; i < indentationSpaces; i++)
            {
                sb.Append(' ');
            }

            sb.Append(value);
            sb.Append(" - ");
        }

        private (int maxHeaderWidthWithIndent, int maxHeaderWidth) GetMaxHeaderWidths(int indentationSpaces)
        {
            var maxHeaderWidthWithIndent = 0;
            var maxHeaderWidth = 0;
            IterateWithIndent(callBack: context =>
            {
                if (context.Item.DescriptionItemType == DescriptionItemType.Header)
                {
                    var possibleHeaderLength = context.Indentation * indentationSpaces + context.Item.Value.Length;
                    maxHeaderWidthWithIndent = Math.Max(maxHeaderWidthWithIndent, possibleHeaderLength);
                    maxHeaderWidth = Math.Max(maxHeaderWidth, context.Item.Value.Length);
                }
            });
            return (maxHeaderWidthWithIndent, maxHeaderWidth);
        }

        private struct Context
        {
            public Context(DescriptionItem item, int indentation)
            {
                Item = item;
                Indentation = indentation;
            }

            public DescriptionItem Item { get; }
            public int Indentation { get; }
        }

        private void IterateWithIndent(Action<Context> callBack)
        {
            var indentLevel = 0;
            foreach (var item in items)
            {
                if (item.DescriptionItemType == DescriptionItemType.Header)
                {
                    indentLevel++;
                    callBack(new Context(item, indentLevel));
                }
                else if (item.DescriptionItemType == DescriptionItemType.Content)
                {
                    callBack(new Context(item, indentLevel));
                    indentLevel--;
                }
            }
        }
    }
}