using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Token : Handler
    {
        private readonly IHandler _inner;
        private readonly string _token;

        public Token([NotNull] string token, [NotNull] IHandler inner)
        {
            _token = Guard.Argument(token, nameof(token)).NotNull();
            _inner = Guard.Argument(inner, nameof(inner)).NotNull().Value;
        }

        public override Task<IHandler?> HandleAsync(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            if (argument.Length > 0 && string.Equals(argument[0], _token, StringComparison.OrdinalIgnoreCase))
            {
                return _inner.HandleAsync(argument.Skip(1).ToArray());
            }

            return Task.FromResult<IHandler?>(this);
        }

        public override void SetContext(HandlerContext context)
        {
            base.SetContext(context);
            _inner.SetContext(context);
        }

        public override void PrintSelf(IPrinter printer)
        {
            printer.Indent().Print(_token).NewLine().Indent();
            _inner.PrintSelf(printer);
            printer.Unindent().Unindent().NewLine();
        }
    }
}