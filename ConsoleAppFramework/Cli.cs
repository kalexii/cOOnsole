using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;

namespace ConsoleAppFramework
{
    public class Cli : IHandler
    {
        private readonly IHandler handler;
        private readonly IWritableWindow window;

        public Cli(IHandler handler, IWritableWindow window = null)
        {
            this.handler = Guard.Argument(handler, nameof(handler)).NotNull().Value;
            this.window = window ?? new ConsoleWindow();
        }

        public async Task<bool> HandleAsync(string[] argument)
        {
            if (!await handler.HandleAsync(argument))
            {
                PrintSelf();
            }

            return true;
        }

        public void PrintSelf(IPrinter printer = null)
        {
            printer = new Printer(window);
            printer.Indent();
            handler.PrintSelf(printer);
            printer.Unindent();
        }
    }
}