using System;

namespace cOOnsole.Printing
{
    internal class Indent : IDisposable
    {
        private readonly IPrinter _printer;
        private readonly int _levels;

        public Indent(IPrinter printer, int levels = 1)
        {
            _printer = printer;
            _levels = levels;
            for (var i = 0; i < _levels; i++)
            {
                _printer.IncreaseIndentationLevel();
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _levels; i++)
            {
                _printer.DecreaseIndentationLevel();
            }
        }
    }
}