using System;
using System.IO;

namespace cOOnsole.Description
{
    public class ConsoleWindow : IWritableWindow
    {
        public TextWriter TextWriter => Console.Out;
    }
}