using System;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    public class UntypedAction : Handler
    {
        private readonly Action<string[], HandlerContext> _action;

        public UntypedAction(Action<string[], HandlerContext> action) => _action = action;

        public override Task<HandleResult> HandleAsync(string[] arguments)
        {
            _action(arguments, Context);
            return Task.FromResult(HandleResult.Handled);
        }

        public override void PrintSelf(IPrinter printer)
        {
        }
    }
}