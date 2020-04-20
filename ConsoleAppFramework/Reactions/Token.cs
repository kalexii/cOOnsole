using System;
using System.Linq;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Token : IReaction
    {
        private readonly string token;
        private readonly IReaction inner;

        public Token([NotNull] string token, [NotNull] IReaction inner)
        {
            this.token = Guard.Argument(token, nameof(token)).NotNull();
            this.inner = Guard.Argument(inner, nameof(inner)).NotNull().Value;
        }

        public bool React(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            return argument.Length > 0
                   && string.Equals(argument[0], token, StringComparison.OrdinalIgnoreCase)
                   && inner.React(argument.Skip(1).ToArray());
        }

        public void PrintSelf(IPrinter printer)
        {
            printer.Indent();
            printer.Print(token, ConsoleColor.Green);
            printer.NewLine();
            printer.Indent();
            inner.PrintSelf(printer);
            printer.Unindent();
            printer.Unindent();
            printer.NewLine();
            
        }
    }
}