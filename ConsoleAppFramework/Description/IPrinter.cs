using System;

namespace ConsoleAppFramework.Description
{
    public interface IPrinter
    {
        void Indent();
        void Unindent();
        void Print(string value);
        void NewLine();
    }
}