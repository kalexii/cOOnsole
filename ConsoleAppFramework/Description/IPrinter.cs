using System;

namespace ConsoleAppFramework.Description
{
    public interface IPrinter
    {
        void Indent();
        void Unindent();
        void Print(string value, ConsoleColor? color = null);
        void NewLine();
    }
}