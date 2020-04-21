using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Token : IHandler
    {
        private readonly string token;
        private readonly IHandler inner;

        public Token([NotNull] string token, [NotNull] IHandler inner)
        {
            this.token = Guard.Argument(token, nameof(token)).NotNull();
            this.inner = Guard.Argument(inner, nameof(inner)).NotNull().Value;
        }

        public Task<bool> HandleAsync(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            if (argument.Length > 0 && string.Equals(argument[0], token, StringComparison.OrdinalIgnoreCase))
            {
                return inner.HandleAsync(argument.Skip(1).ToArray());
            }

            return Task.FromResult(false);
        }

        public void PrintSelf(IPrinter printer)
        {
            printer.Indent();
            printer.Print(token);
            printer.NewLine();
            printer.Indent();
            inner.PrintSelf(printer);
            printer.Unindent();
            printer.Unindent();
            printer.NewLine();
        }
    }
}