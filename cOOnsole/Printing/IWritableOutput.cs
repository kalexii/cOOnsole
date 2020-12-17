using System.IO;

namespace cOOnsole.Printing
{
    /// <summary>
    /// Represents writable output. The most obvious candidate is <see cref="System.Console"/>,
    /// however for unit-testing (in parallel) and DI purposes this was extracted to it's own interface. 
    /// </summary>
    public interface IWritableOutput
    {
        /// <summary>
        /// An instance of <see cref="TextWriter"/> to write program output into.
        /// </summary>
        TextWriter TextWriter { get; }
    }
}