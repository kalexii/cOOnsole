using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using cOOnsole.Description;

namespace cOOnsole
{
    public class Cli
    {
        private readonly IHandler _root;
        private readonly IPrinter _printer;

        public Cli(IHandler root, IWritableWindow? window = null)
        {
            _root = root;
            _printer = new Printer(window ?? new ConsoleWindow());
            _root.SetContext(new HandlerContext(this, _printer));
        }

        public async Task<bool> HandleAsync(string[] argument)
        {
            try
            {
                var (result, errorScope) = await _root.HandleAsync(argument);
                switch (result)
                {
                    case HandleStatus.NotHandled:
                        PrintSelf();
                        return false;

                    case HandleStatus.Error:
                        PrintSelf(errorScope);
                        return false;

                    default:
                        return true;
                }
            }
            catch (Exception e)
            {
                _printer.ResetIndent();
                _printer.Print(e.ToStringDemystified());
                return false;
            }
        }

        public void PrintSelf(IHandler? root = null)
        {
            _printer.ResetIndent();
            if (root == null || root == _root)
            {
                var assembly = Assembly.GetEntryAssembly();
                var version = assembly
                  ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                  ?.InformationalVersion;

                _printer
                   .Print($"{assembly!.GetName().Name} v{version}")
                   .NewLine()
                   .Print("Usage:")
                   .NewLine()
                   .NewLine();
                _printer.Indent();
            }

            (root ?? _root).PrintSelf(_printer);
            _printer.Flush();
        }
    }
}