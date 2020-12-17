using System;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    /// <summary>
    /// Unconditionally calls the wrapped action, and returns <see cref="HandleResult.Handled"/>.
    /// </summary>
    public class UntypedAction : Handler
    {
        private readonly Action<string[], IHandlerContext> _action;

        /// <summary>Initializes an instance of <see cref="UntypedAction" />.</summary>
        /// <param name="action">Action to call.</param>
        public UntypedAction(Action<string[], IHandlerContext> action) => _action = action;

        /// <inheritdoc />
        public override Task<HandleResult> HandleAsync(string[] input)
        {
            _action(input, Context);
            return Task.FromResult(HandleResult.Handled);
        }

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer)
        {
        }
    }
}