using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Fork : IHandler
    {
        private readonly string description;
        private readonly IHandler[] reactions;

        public Fork([NotNull] string description, [NotNull] params IHandler[] reactions)
        {
            this.description = Guard.Argument(description, nameof(description)).NotNull();
            this.reactions = Guard.Argument(reactions, nameof(reactions)).NotNull();
        }

        public async Task<bool> HandleAsync(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            foreach (var reaction in reactions)
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
            printer.Print(description);
            printer.NewLine();
            printer.NewLine();
            printer.Indent();
            foreach (var reaction in reactions)
            {
                reaction.PrintSelf(printer);
            }

            printer.Unindent();
        }
    }
}