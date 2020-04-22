using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using JetBrains.Annotations;

namespace ConsoleAppFramework
{
    /// <summary>
    /// Represents abstract reaction to the input. This interface is generic to support both raw and parsed input.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Base method that represents the reaction to the input.
        /// </summary>
        /// <param name="args">The input to react to.</param>
        /// <returns>Returns true if input has been handled, otherwise false.</returns>
        Task<bool> HandleAsync([NotNull] string[] args);

        void PrintSelf([NotNull] IPrinter printer);
    }
}