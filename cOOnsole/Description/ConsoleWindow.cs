using System;
using System.IO;

namespace cOOnsole.Description
{
    public class ConsoleWindow : IWritableOutput
    {
        public TextWriter TextWriter => Console.Out;
    }
}