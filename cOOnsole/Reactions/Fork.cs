using System.Threading.Tasks;
using cOOnsole.Description;

namespace cOOnsole.Reactions
{
    public class Fork : Handler
    {
        private readonly string? _description;
        private readonly IHandler[] _handlers;

        public Fork(string? description, params IHandler[] handlers)
        {
            _handlers = handlers;
            _description = description;
        }

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
                if (result.Status != HandleStatus.NotHandled)
                {
                    return result;
                }
            }

            return HandleResult.NotHandled;
        }

        public override void PrintSelf(IPrinter printer)
        {
            if (_description is not null)
            {
                printer.Print(_description).NewLine().NewLine().Indent();
            }

            foreach (var reaction in _handlers)
            {
                reaction.PrintSelf(printer);
            }

            printer.Unindent();
        }
    }
}