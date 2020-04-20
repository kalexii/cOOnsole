using System;
using System.IO;

namespace ConsoleAppFramework.Description
{
    public interface IWritableWindow
    {
        TextWriter TextWriter { get; }
        void SetColor(ConsoleColor color);
        void ResetColor();
    }
}