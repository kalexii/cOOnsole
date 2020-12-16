using System;
using System.Diagnostics;
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
    }
}