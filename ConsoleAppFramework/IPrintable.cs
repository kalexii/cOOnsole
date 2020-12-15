using ConsoleAppFramework.Description;
using JetBrains.Annotations;

namespace ConsoleAppFramework
{
    public interface IPrintable
    {
        /// <summary>
        /// Method that gets called whenever help message is generated.
        /// </summary>
        /// <param name="printer">The printer.</param>
        void PrintSelf([NotNull] IPrinter printer);
    }
}