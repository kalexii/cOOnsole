using cOOnsole.Printing;

namespace cOOnsole.Handlers.Base
{
    /// <inheritdoc cref="IHandlerContext"/>
    public sealed record HandlerContext(IPrinter Printer) : IHandlerContext;
}