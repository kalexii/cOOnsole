using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using JetBrains.Annotations;

namespace ConsoleAppFramework
{
    /// <summary>
    /// Represents abstract reaction to the input.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Base method that represents the reaction to the input.
        /// </summary>
        /// <param name="args">The input to react to.</param>
        /// <returns>Returns true if input has been handled, otherwise false.</returns>
        Task<bool> HandleAsync([NotNull] string[] args);

        /// <summary>
        /// Method that gets called whenever help message is generated.
        /// </summary>
        /// <param name="printer">The printer.</param>
        void PrintSelf([NotNull] IPrinter printer);
    }
}