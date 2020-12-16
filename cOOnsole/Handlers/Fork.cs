using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Handlers
{
    public class Fork : Handler
    {
        private readonly IHandler[] _handlers;

        public Fork(params IHandler[] handlers) => _handlers = handlers;

        public override void SetContext(HandlerContext context)
        {
            base.SetContext(context);
            foreach (var handler in _handlers)
            {
                handler.SetContext(context);
            }
        }

        public override async Task<HandleResult> HandleAsync(string[] arguments)
        {
            foreach (var reaction in _handlers)
            {
                var result = await reaction.HandleAsync(arguments);
                if (result != HandleResult.NotHandled)
                {
                    return result;
                }
            }

            return HandleResult.NotHandled;
        }

        public override void PrintSelf(IPrinter printer)
        {
            foreach (var reaction in _handlers)
            {
                reaction.PrintSelf(printer);
            }
        }
    }
}