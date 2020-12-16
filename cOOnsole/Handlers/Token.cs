using System;
using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Handlers
{
    public class Token : Handler
    {
        private readonly IHandler _inner;
        private readonly string _token;

        public Token(string token, IHandler inner)
        {
            _token = token;
            _inner = inner;
        }

        public override Task<HandleResult> HandleAsync(string[] argument)
            => argument.Length > 0 && string.Equals(argument[0], _token, StringComparison.OrdinalIgnoreCase)
                ? _inner.HandleAsync(argument[1..])
                : Task.FromResult(HandleResult.NotHandled);

        public override void SetContext(HandlerContext context)
        {
            base.SetContext(context);
            _inner.SetContext(context);
        }

        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_token).NewLine();
            printer.Indent();
            _inner.PrintSelf(printer);
            printer.Unindent().NewLine();
        }
    }
}