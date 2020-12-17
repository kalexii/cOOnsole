using System;
using System.IO;

namespace cOOnsole.Printing
{
    /// <summary>
    /// Default output that grabs <see cref="Console.Out"/>.
    /// </summary>
    public class ConsoleOutput : IWritableOutput
    {
        /// <inheritdoc />
        public TextWriter TextWriter => Console.Out;
    }
}