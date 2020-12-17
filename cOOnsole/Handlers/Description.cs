using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    /// <summary>
    /// Handle node that only alters the usage string.
    /// It adds a description to the wrapped handler, and indents the
    /// child description one level.
    /// Handling is delegated entirely to the wrapped handler.
    /// </summary>
    public sealed class Description : SingleChildHandler
    {
        private readonly string _description;

        /// <summary>Initializes an instance of <see cref="Description" />.</summary>
        /// <param name="description">Description to add to the wrapper subtree.</param>
        /// <param name="wrapped">Wrapped handler.</param>
        public Description(string description, IHandler wrapped) : base(wrapped) => _description = description;

        /// <inheritdoc />
        public override Task<HandleResult> HandleAsync(string[] input)
            => Wrapped.HandleAsync(input);

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_description).NewLine().Indent();
            Wrapped.PrintSelf(printer);
            printer.Unindent();
        }
    }
}