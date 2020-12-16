using System.Threading.Tasks;
using cOOnsole.Description;

namespace cOOnsole.Handlers.Base
{
    /// <summary>
    /// Bare minimum handler implementation that provides context to the descendants.
    /// </summary>
    public abstract class Handler : IHandler
    {
        protected IHandler[] Children { get; init; } = default!;
        protected IHandler Child { get; init; } = default!;

        /// <summary>
        /// Exposes the handler context to the descendants. 
        /// </summary>
        protected HandlerContext Context { get; private set; } = null!;

        public abstract void PrintSelf(IPrinter printer);

        public virtual void SetContext(HandlerContext context)
        {
            Context = context;

            // ReSharper disable once ConstantConditionalAccessQualifier
            Child?.SetContext(context);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (Children is not null)
            {
                for (var index = 0; index < Children.Length; index++)
                {
                    Children[index].SetContext(context);
                }
            }
        }

        public abstract Task<HandleResult> HandleAsync(string[] arguments);
    }
}