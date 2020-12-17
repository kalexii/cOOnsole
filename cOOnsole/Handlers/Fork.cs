using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    /// <summary>Wraps multiple child handlers and tries every one of them until one of them matches.</summary>
    public class Fork : MultipleChildHandler
    {
        /// <inheritdoc />
        /// <summary>Initializes an instance of <see cref="Fork" />.</summary>
        public Fork(params IHandler[] wrapped): base(wrapped) {}

        /// <inheritdoc />
        public override async Task<HandleResult> HandleAsync(string[] input)
        {
            foreach (var reaction in Wrapped)
            {
                var result = await reaction.HandleAsync(input).ConfigureAwait(false);
                if (result != HandleResult.NotMatched)
                {
                    return result;
                }
            }

            return HandleResult.NotMatched;
        }

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer)
        {
            foreach (var child in Wrapped)
            {
                child.PrintSelf(printer);
            }
        }
    }
}