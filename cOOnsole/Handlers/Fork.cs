using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    public class Fork : Handler
    {
        public Fork(params IHandler[] children) => Children = children;

        public override void SetContext(HandlerContext context)
        {
            base.SetContext(context);
            foreach (var handler in Children)
            {
                handler.SetContext(context);
            }
        }

        public override async Task<HandleResult> HandleAsync(string[] arguments)
        {
            foreach (var reaction in Children)
            {
                var result = await reaction.HandleAsync(arguments).ConfigureAwait(false);
                if (result != HandleResult.NotHandled)
                {
                    return result;
                }
            }

            return HandleResult.NotHandled;
        }

        public override void PrintSelf(IPrinter printer)
        {
            foreach (var child in Children)
            {
                child.PrintSelf(printer);
            }
        }
    }
}