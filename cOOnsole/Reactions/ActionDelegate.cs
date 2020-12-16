using System;
using System.Threading.Tasks;
using cOOnsole.Description;

namespace cOOnsole.Reactions
{
    public class ActionDelegate : Handler
    {
        private readonly string _description;
        private readonly Action<string[], HandlerContext> _action;

        public ActionDelegate(string description, Action<string[], HandlerContext> action)
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