using System.IO;

namespace ConsoleAppFramework.Description
{
    public interface IDescriptionPrinter
    {
        void Print(IWritableWindow window);
    }
}