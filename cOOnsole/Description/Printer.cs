using System;

namespace cOOnsole.Description
{
    public class Printer : IPrinter
    {
        private readonly IWritableOutput _output;

        /// <summary>
        /// Current indentation level.
        /// </summary>
        private int _indent;

        /// <summary>
        /// Represents whether the last char is a new line character (this means we need to add the padding if indent > 0).
        /// </summary>
        private bool _atNewLine = true;

        public Printer(IWritableOutput window) => _output = window;

        public IPrinter Indent()
        {
            _indent++;
            return this;
        }

        public IPrinter Unindent()
        {
            _indent--;
            return this;
        }

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

        public IPrinter NewLine()
        {
            _output.TextWriter.WriteLine();
            _atNewLine = true;
            return this;
        }

        public IPrinter Flush()
        {
            _output.TextWriter.Flush();
            return this;
        }

        public IPrinter ResetIndent()
        {
            _indent = 0;
            return this;
        }
    }
}