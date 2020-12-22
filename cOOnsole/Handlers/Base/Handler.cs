using System.Threading.Tasks;
using cOOnsole.Printing;

namespace cOOnsole.Handlers.Base
{
    /// <summary>Bare minimum handler implementation that provides context to the descendants.</summary>
    public abstract class Handler : IHandler
    {
        /// <summary>Exposes the handler context to the implementations.</summary>
        protected IHandlerContext Context { get; private set; } = null!;

        /// <inheritdoc />
        public virtual void SetContext(IHandlerContext context) => Context = context;

        /// <inheritdoc />
        public abstract Task<HandleResult> HandleAsync(string[] input);

        /// <inheritdoc cref="IPrintable.PrintSelf" />
        public virtual void PrintSelf(IPrinter printer)
        {
        }
    }
}