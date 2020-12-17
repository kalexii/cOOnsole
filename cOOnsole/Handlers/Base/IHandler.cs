using System.Threading.Tasks;
using cOOnsole.Printing;

namespace cOOnsole.Handlers.Base
{
    /// <summary>
    ///     Represents abstract reaction to the input.
    /// </summary>
    public interface IHandler : IPrintable
    {
        void SetContext(IHandlerContext context);

        /// <summary>
        ///     Base method that represents the reaction to the input.
        /// </summary>
        /// <param name="arguments">The input to react to.</param>
        /// <returns>In case of error, returns handler that errored out. In case of success, null is returned.</returns>
        Task<HandleResult> HandleAsync(string[] arguments);
    }
}