using System;
using System.IO;

namespace ConsoleAppFramework.Description
{
    public class ConsoleWindow : IWritableWindow
    {
        public TextWriter TextWriter => Console.Out;
    }
}