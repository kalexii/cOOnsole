using System;

namespace cOOnsole.Printing
{
    /// <summary>
    /// The default implementation of an <see cref="IPrinter"/>.
    /// </summary>
    /// <seealso cref="IPrinter"/>
    public class Printer : IPrinter
    {
        private readonly IWritableOutput _output;

        /// <summary>
        /// Represents whether the last char is a new line character
        /// (this means we need to add the padding if indent > 0).
        /// </summary>
        private bool _atNewLine = true;

        /// <summary>Current indentation level.</summary>
        private int _indent;

        /// <summary>Initializes an instance of <see cref="Printer" />.</summary>
        /// <param name="window">Window to write to.</param>
        public Printer(IWritableOutput window) => _output = window;

        IPrinter IPrinter.IncreaseIndentationLevel()
        {
            _indent++;
            return this;
        }

        IPrinter IPrinter.DecreaseIndentationLevel()
        {
            _indent--;
            return this;
        }

        /// <inheritdoc />
        public IDisposable Indent(int levels) => new Indent(this, levels);

        /// <inheritdoc />
        public IPrinter Print(string value)
        {
            if (_atNewLine)
            {
                var indentationCharacterCount = Math.Max(0, _indent * 2);
                var leftPaddingString = new string(' ', indentationCharacterCount);
                _output.TextWriter.Write(leftPaddingString);
                _atNewLine = false;
            }

            _output.TextWriter.Write(value);
            return this;
        }

        /// <inheritdoc />
        public IPrinter NewLine()
        {
            _output.TextWriter.WriteLine();
            _atNewLine = true;
            return this;
        }

        /// <inheritdoc />
        public IPrinter Flush()
        {
            _output.TextWriter.Flush();
            return this;
        }

        /// <inheritdoc />
        public IPrinter ResetIndent()
        {
            _indent = 0;
            return this;
        }
    }
}