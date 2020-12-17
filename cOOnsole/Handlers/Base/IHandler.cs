using System.Threading.Tasks;
using cOOnsole.Printing;

namespace cOOnsole.Handlers.Base
{
    /// <summary>Represents abstract reaction to the input.</summary>
    public interface IHandler : IPrintable
    {
        /// <summary>Populates current handler's context value.</summary>
        /// <param name="context">Context to populate the handler with.</param>
        void SetContext(IHandlerContext context);

        /// <summary>Base method that represents the reaction to the input.</summary>
        /// <param name="input">The input to handle.</param>
        /// <returns>The instance of <see cref="HandleResult"/> that denotes how the handling went.</returns>
        Task<HandleResult> HandleAsync(string[] input);
    }
}