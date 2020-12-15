using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Fork : Handler
    {
        private readonly string _description;
        private readonly IHandler[] _handlers;

        public Fork([NotNull] string description, [NotNull] params IHandler[] handlers)
        {
            _description = Guard.Argument(description, nameof(description)).NotNull();
            _handlers = Guard.Argument(handlers, nameof(handlers)).NotNull();
        }

        public override void SetContext(HandlerContext context)
        {
            base.SetContext(context);
            foreach (var handler in _handlers)
            {
                handler.SetContext(context);
            }
        }

        public override async Task<IHandler?> HandleAsync(string[] arguments)
        {
            Guard.Argument(arguments, nameof(arguments)).NotNull();
            foreach (var reaction in _handlers)
            {
                var result = await reaction.HandleAsync(arguments);
                if (result == null)
                {
                    return result;
                }
            }

            return this;
        }

        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_description).NewLine().NewLine().Indent();
            foreach (var reaction in _handlers)
            {
                reaction.PrintSelf(printer);
            }

            printer.Unindent();
        }
    }
}