using System;
using ConsoleAppFramework.Description;
using ConsoleAppFramework.Reactions;
using Dawn;

namespace ConsoleAppFramework
{
    /// <summary>
    /// Contains shortcuts to common reactions.
    /// </summary>
    public class Cli : IReaction
    {
        private readonly IReaction reaction;
        private readonly IWritableWindow window;

        public Cli(IReaction reaction, IWritableWindow window = null)
        {
            this.reaction = Guard.Argument(reaction, nameof(reaction)).NotNull().Value;
            this.window = window ?? new ConsoleWindow();
        }

        public bool React(string[] argument)
        {
            if (!reaction.React(argument))
            {
                PrintSelf();
            }

            return true;
        }

        public void PrintSelf(IPrinter printer = null)
        {
            printer = new Printer(window);
            printer.Indent();
            reaction.PrintSelf(printer);
            printer.Unindent();
        }

        public static Token Token(string token, IReaction reaction) => new Token(token, reaction);

        public static Fork Fork(params IReaction[] reactions) => new Fork(reactions);

        public static Fork Fork(string description, params IReaction[] reactions) => new Fork(description, reactions);

        public static ActionDelegate Action(Action<string[]> action) => new ActionDelegate(action);

        public static ActionDelegate Action(string description, Action<string[]> action) =>
            new ActionDelegate(description, action);

        public static ActionDelegate<T> Action<T>(Action<T> action) where T : class => new ActionDelegate<T>(action);

        public static ActionDelegate<T> Action<T>(string description, Action<T> action) where T : class =>
            new ActionDelegate<T>(description, action);
    }
}