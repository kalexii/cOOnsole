using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole
{
    public class Cli
    {
        private readonly IHandler _root;
        private readonly IPrinter _printer;

        public Cli(IHandler root, IWritableOutput? window = null)
        {
            _root = root;
            _printer = new Printer(window ?? new ConsoleWindow());
            _root.SetContext(new HandlerContext(this, _printer));
        }

        public async Task<bool> HandleAsync(string[] argument)
        {
            try
            {
                var result = await _root.HandleAsync(argument);
                switch (result)
                {
                    case HandleResult.NotHandled:
                    case HandleResult.Error:
                        PrintSelf();
                        return false;

                    default:
                        return true;
                }
            }
            catch (Exception e)
            {
                _printer.ResetIndent().Print(e.ToStringDemystified());
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
                   .NewLine()
                   .Indent();
            }

            (root ?? _root).PrintSelf(_printer);
            _printer.Flush();
        }
    }
}