using System.IO;

namespace cOOnsole.Description
{
    public interface IWritableWindow
    {
        TextWriter TextWriter { get; }
    }
}