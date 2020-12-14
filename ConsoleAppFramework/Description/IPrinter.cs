namespace ConsoleAppFramework.Description
{
    public interface IPrinter
    {
        IPrinter Indent();
        IPrinter Unindent();
        IPrinter Print(string value);
        IPrinter NewLine();
    }
}