using cOOnsole.Printing;

namespace cOOnsole.Handlers.Base
{
    /// <summary>
    /// Represents a context that is accessible to each handler. This context represents shared data across the application
    /// handlers.
    /// </summary>
    public interface IHandlerContext
    {
        /// <summary>The output printer.</summary>
        /// <remarks>Use this instead of <see cref="System.Console"/>.</remarks>
        IPrinter Printer { get; }
    }
}