using System.IO;

namespace cOOnsole.Description
{
    public interface IWritableOutput
    {
        TextWriter TextWriter { get; }
    }
}