using System;
using System.IO;

namespace cOOnsole.Printing
{
    public class ConsoleOutput : IWritableOutput
    {
        public TextWriter TextWriter => Console.Out;
    }
}