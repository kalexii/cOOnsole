using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    public sealed class Description : SingleChildHandler
    {
        private readonly string _description;

        public Description(string description, IHandler child)
        {
            _description = description;
            Child = child;
        }

        public override Task<HandleResult> HandleAsync(string[] arguments)
            => Child.HandleAsync(arguments);

        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_description).NewLine().Indent();
            Child.PrintSelf(printer);
            printer.Unindent();
        }
    }
}