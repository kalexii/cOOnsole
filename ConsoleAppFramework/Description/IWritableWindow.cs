using System.IO;

namespace ConsoleAppFramework.Description
{
    public interface IWritableWindow
    {
        TextWriter TextWriter { get; }
    }
}