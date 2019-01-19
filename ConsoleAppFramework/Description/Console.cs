using System.IO;

namespace ConsoleAppFramework.Description
{
    public class Console : IWritableWindow
    {
        public int Width => System.Console.WindowWidth;

        public TextWriter TextWriter => System.Console.Out;
    }
}