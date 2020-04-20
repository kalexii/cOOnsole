using System;

namespace ConsoleAppFramework.Description
{
    public class Printer : IPrinter
    {
        private readonly IWritableWindow window;
        private int indent;
        private bool isNewLine = true;

        public Printer(IWritableWindow window) => this.window = window;

        public void Indent() => indent++;

        public void Unindent() => indent--;

        public void Print(string value, ConsoleColor? color)
        {
            if (color != null)
            {
                window.SetColor(color.Value);
            }

            if (isNewLine)
            {
                window.TextWriter.Write(new string(' ', indent * 2));
                isNewLine = false;
            }

            window.TextWriter.Write(value);

            if (color != null)
            {
                window.ResetColor();
            }
        }

        public void NewLine()
        {
            window.TextWriter.WriteLine();
            isNewLine = true;
        }
    }
}