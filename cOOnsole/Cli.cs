using System;
using System.Diagnostics;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole
{
    public class Cli
    {
        private readonly IHandler _root;
        private readonly IPrinter _printer;

        public Cli(IHandler root, IWritableOutput? window = null)
        {
            _root = root;
            _printer = new Printer(window ?? new ConsoleOutput());
            _root.SetContext(new HandlerContext(_printer));
        }

        public async Task<int> HandleAndGetExitCode(string[] argument)
        {
            try
            {
                var result = await _root.HandleAsync(argument).ConfigureAwait(false);
                _printer.Flush();
                switch (result)
                {
                    case HandleResult.NotHandled:
                        return -1;

                    case HandleResult.Error:
                        return 1;

                    default:
                        return 0;
                }
            }
            catch (Exception e)
            {
                _printer.ResetIndent().Print(e.ToStringDemystified());
                _printer.Flush();
                return 1;
            }
        }

        public Task<bool> HandleAsync(string[] argument)
            => HandleAndGetExitCode(argument).ContinueWith(x => x.Result == 0);
    }
}