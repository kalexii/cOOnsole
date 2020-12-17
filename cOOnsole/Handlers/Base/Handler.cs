using System.Threading.Tasks;
using cOOnsole.Printing;

namespace cOOnsole.Handlers.Base
{
    /// <summary>
    /// Bare minimum handler implementation that provides context to the descendants.
    /// </summary>
    public abstract class Handler : IHandler
    {
        /// <summary>
        /// Exposes the handler context to the descendants. 
        /// </summary>
        protected IHandlerContext Context { get; private set; } = null!;

        public abstract void PrintSelf(IPrinter printer);

        public virtual void SetContext(IHandlerContext context) => Context = context;

        public abstract Task<HandleResult> HandleAsync(string[] arguments);
    }
}