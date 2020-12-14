using System.Reflection;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;

namespace ConsoleAppFramework
{
    public class Cli : IHandler
    {
        private readonly IHandler _handler;
        private readonly IWritableWindow _window;

        public Cli(IHandler handler, IWritableWindow? window = null)
        {
            _handler = Guard.Argument(handler, nameof(handler)).NotNull().Value;
            _window = window ?? new ConsoleWindow();
        }

        public async Task<bool> HandleAsync(string[] argument)
        {
            if (!await _handler.HandleAsync(argument))
            {
                PrintSelf();
            }

            return true;
        }

        public void PrintSelf(IPrinter? printer = null)
        {
            printer ??= new Printer(_window);

            var assembly = Assembly.GetEntryAssembly();
            var version = assembly
              ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
              ?.InformationalVersion;

            printer
               .Print($"{assembly!.GetName().Name} v{version}")
               .NewLine()
               .Print("Usage:")
               .NewLine()
               .NewLine();

            printer.Indent();
            _handler.PrintSelf(printer);
            printer.Unindent();
        }
    }
}