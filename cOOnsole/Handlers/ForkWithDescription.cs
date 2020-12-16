using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Handlers
{
    public class ForkWithDescription : Fork
    {
        private readonly string _description;

        public ForkWithDescription(string description, params IHandler[] handlers) : base(handlers)
            => _description = description;

        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_description).NewLine().NewLine().Indent();
            base.PrintSelf(printer);
            printer.Unindent();
        }
    }
}