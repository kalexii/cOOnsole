using System.IO;

namespace cOOnsole.Printing
{
    /// <summary>
    /// This interface provides the access to the output stream enhancing it with the indentation capabilities.
    /// </summary>
    public interface IPrinter
    {
        /// <summary>
        /// Increases the indentation level.
        /// </summary>
        IPrinter Indent();

        /// <summary>
        /// Decreases the indentation level.
        /// </summary>
        IPrinter Unindent();

        /// <summary>
        /// Prints a string on the current line.
        /// If indentation level > 0 and we're on the beginning of the string, string will be left-padded.
        /// </summary>
        /// <remarks>
        /// Do NOT print multiline strings through this method, otherwise it'll break indentation.
        /// </remarks>
        IPrinter Print(string value);

        /// <summary>
        /// Outputs new line character.
        /// </summary>
        IPrinter NewLine();

        /// <summary>
        /// Flushes the underlying <see cref="TextWriter"/> contents.
        /// </summary>
        IPrinter Flush();

        /// <summary>
        /// Resets indentation level to zero.
        /// </summary>
        IPrinter ResetIndent();
    }
}