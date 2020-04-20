using System;
using System.Linq;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Fork : IReaction
    {
        private readonly string description;
        private readonly IReaction[] reactions;

        public Fork([NotNull] params IReaction[] reactions) : this("Fork", reactions)
        {
        }

        public Fork(string description, [NotNull] params IReaction[] reactions)
        {
            this.description = description ?? "Fork";
            this.reactions = Guard.Argument(reactions, nameof(reactions)).NotNull();
        }

        public bool React(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            return reactions.Any(t => t.React(argument));
        }

        public void PrintSelf(IPrinter printer)
        {
            printer.Print(description, ConsoleColor.DarkGreen);
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