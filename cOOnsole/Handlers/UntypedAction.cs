using System;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    /// <summary>
    /// Unconditionally calls the wrapped action.
    /// </summary>
    public class UntypedAction : Handler
    {
        private readonly Func<string[], IHandlerContext, Task<HandleResult>> _action;

        /// <summary>Initializes an instance of <see cref="UntypedAction" />.</summary>
        /// <param name="action">Action to call.</param>
        public UntypedAction(Func<string[], IHandlerContext, Task<HandleResult>> action) => _action = action;

        /// <inheritdoc />
        public override Task<HandleResult> HandleAsync(string[] input) => _action(input, Context);

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer)
        {
        }
    }
}