using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Fork : IHandler
    {
        private readonly string _description;
        private readonly IHandler[] _reactions;

        public Fork([NotNull] string description, [NotNull] params IHandler[] reactions)
        {
            _description = Guard.Argument(description, nameof(description)).NotNull();
            _reactions = Guard.Argument(reactions, nameof(reactions)).NotNull();
        }

        public async Task<bool> HandleAsync(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            foreach (var reaction in _reactions)
            {
                if (await reaction.HandleAsync(argument))
                {
                    return true;
                }
            }

            return false;
        }

        public void PrintSelf(IPrinter printer)
        {
            printer.Print(_description).NewLine().NewLine().Indent();
            foreach (var reaction in _reactions)
            {
                reaction.PrintSelf(printer);
            }

            printer.Unindent();
        }
    }
}