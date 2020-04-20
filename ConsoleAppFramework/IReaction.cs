using ConsoleAppFramework.Description;
using JetBrains.Annotations;

namespace ConsoleAppFramework
{
    /// <summary>
    /// Represents abstract reaction to the input. This interface is generic to support both raw and parsed input.
    /// </summary>
    /// <typeparam name="TArgument">Type of input to react to.</typeparam>
    public interface IReaction<in TArgument>
    {
        /// <summary>
        /// Base method that represents the reaction to the input.
        /// </summary>
        /// <param name="argument">The input to react to.</param>
        /// <returns>Returns true if input has been handled, otherwise false.</returns>
        bool React([NotNull] TArgument argument);

        void PrintSelf([NotNull] IPrinter printer);
    }

    /// <summary>
    /// Represents reaction to raw input.
    /// </summary>
    /// <seealso cref="IReaction{TArgument}"/>
    public interface IReaction : IReaction<string[]>
    {
    }
}