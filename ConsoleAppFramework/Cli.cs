using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using ConsoleAppFramework.Reactions;
using Dawn;

namespace ConsoleAppFramework
{
    /// <summary>
    /// Contains shortcuts to common reactions.
    /// </summary>
    public class Cli : IHandler
    {
        private readonly IHandler handler;
        private readonly IWritableWindow window;

        public Cli(IHandler handler, IWritableWindow window = null)
        {
            this.handler = Guard.Argument(handler, nameof(handler)).NotNull().Value;
            this.window = window ?? new ConsoleWindow();
        }

        public async Task<bool> HandleAsync(string[] argument)
        {
            if (!await handler.HandleAsync(argument))
            {
                PrintSelf();
            }

            return true;
        }

        public void PrintSelf(IPrinter printer = null)
        {
            printer = new Printer(window);
            printer.Indent();
            handler.PrintSelf(printer);
            printer.Unindent();
        }

        public static Token Token(string token, IHandler handler) => new Token(token, handler);

        public static Fork Fork(params IHandler[] reactions) => new Fork(reactions);

        public static Fork Fork(string description, params IHandler[] reactions) => new Fork(description, reactions);

        public static ActionDelegate Action(Action<string[]> action) => new ActionDelegate(action);

        public static ActionDelegate Action(string description, Action<string[]> action) =>
            new ActionDelegate(description, action);

        public static ActionDelegate<T> Action<T>(Action<T> action) where T : class => new ActionDelegate<T>(action);

        public static ActionDelegate<T> Action<T>(string description, Action<T> action) where T : class =>
            new ActionDelegate<T>(description, action);
    }
}