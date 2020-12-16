using System;
using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Handlers
{
    public class Unconditional : Handler
    {
        private readonly HandleResult _result;

        public Unconditional(HandleResult result) => _result = result;

        public override void PrintSelf(IPrinter printer)
        {
        }

        public override Task<HandleResult> HandleAsync(string[] arguments) => Task.FromResult(_result);
    }

    public class Action : Handler
    {
        private readonly string _description;
        private readonly Action<string[], HandlerContext> _action;

        public Action(string description, Action<string[], HandlerContext> action)
        {
            _description = description;
            _action = action;
        }

        public override Task<HandleResult> HandleAsync(string[] arguments)
        {
            _action(arguments, Context);
            return Task.FromResult(HandleResult.Handled);
        }

        public override void PrintSelf(IPrinter printer) => printer.Print(_description).NewLine();
    }
}