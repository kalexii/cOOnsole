using System.IO;

namespace cOOnsole.Printing
{
    public interface IWritableOutput
    {
        TextWriter TextWriter { get; }
    }
}