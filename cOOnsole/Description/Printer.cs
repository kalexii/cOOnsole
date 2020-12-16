namespace cOOnsole.Description
{
    public class Printer : IPrinter
    {
        private readonly IWritableWindow _window;
        private int _indent;
        private bool _isNewLine = true;

        public Printer(IWritableWindow window) => _window = window;

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
            if (_isNewLine)
            {
                _window.TextWriter.Write(new string(' ', _indent * 2));
                _isNewLine = false;
            }

            _window.TextWriter.Write(value);
            return this;
        }

        public IPrinter NewLine()
        {
            _window.TextWriter.WriteLine();
            _isNewLine = true;
            return this;
        }

        public IPrinter Flush()
        {
            _window.TextWriter.Flush();
            return this;
        }

        public IPrinter ResetIndent()
        {
            _indent = 0;
            return this;
        }
    }
}