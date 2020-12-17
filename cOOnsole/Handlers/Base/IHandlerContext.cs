using cOOnsole.Printing;

namespace cOOnsole.Handlers.Base
{
    /// <summary>
    /// Represents a context that is accessible to each handler.
    /// This context represents shared data across the application handlers.
    /// </summary>
    public interface IHandlerContext
    {
        IPrinter Printer { get; }
    }
}