using System;
using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Handlers
{
    public sealed class Token : Handler
    {
        private readonly string _token;

        public Token(string token, IHandler child)
        {
            _token = token;
            Child = child;
        }

        public override Task<HandleResult> HandleAsync(string[] argument)
            => argument.Length > 0 && string.Equals(argument[0], _token, StringComparison.OrdinalIgnoreCase)
                ? Child.HandleAsync(argument[1..])
                : Task.FromResult(HandleResult.NotHandled);

        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_token).NewLine();
            printer.Indent();
            Child.PrintSelf(printer);
            printer.Unindent().NewLine();
        }
    }
}