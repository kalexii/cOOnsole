using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Handlers
{
    public sealed class SectionDescription : Handler
    {
        private readonly string _description;

        public SectionDescription(string description, IHandler child)
        {
            _description = description;
            Child = child;
        }

        public override Task<HandleResult> HandleAsync(string[] arguments)
            => Child.HandleAsync(arguments);

        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_description).NewLine().NewLine().Indent();
            Child.PrintSelf(printer);
            printer.Unindent();
        }
    }
}