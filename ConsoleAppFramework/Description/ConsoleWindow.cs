using System;
using System.IO;

namespace ConsoleAppFramework.Description
{
    public class ConsoleWindow : IWritableWindow
    {
        public TextWriter TextWriter => Console.Out;

        public void SetColor(ConsoleColor color) => Console.ForegroundColor = color;

        public void ResetColor() => Console.ResetColor();
    }
}