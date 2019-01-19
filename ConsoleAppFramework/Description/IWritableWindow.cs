using System.IO;

namespace ConsoleAppFramework.Description
{
    public interface IWritableWindow
    {
        int Width { get; }
        TextWriter TextWriter { get; }
    }
}