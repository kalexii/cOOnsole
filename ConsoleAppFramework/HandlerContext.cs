using ConsoleAppFramework.Description;

namespace ConsoleAppFramework
{
    /// <summary>
    /// Represents a context that is accessible to each handler.
    /// This context represents shared data across the application handlers.
    /// </summary>
    public sealed record HandlerContext(IPrinter Printer);
}