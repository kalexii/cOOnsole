using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class ActionDelegate<T> : IHandler<T> where T : class
    {
        private readonly string description;
        private readonly Action<T> action;

        public ActionDelegate(string description, [NotNull] Action<T> action)
        {
            this.description = description ?? "Executes custom user action.";
            this.action = Guard.Argument(action, nameof(action)).NotNull();
        }

        public ActionDelegate([NotNull] Action<T> action) : this("Executes custom user action.", action)
        {
        }

        public Task<bool> HandleAsync(T argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            action(argument);
            return Task.FromResult(true);
        }

        public void PrintSelf(IPrinter printer)
        {
            printer.Print(description);
            printer.NewLine();
        }
    }

    public class ActionDelegate : IHandler
    {
        private readonly string description;
        private readonly Action<string[]> action;

        public ActionDelegate(string description, [NotNull] Action<string[]> action)
        {
            this.description = description ?? "Executes custom user action.";
            this.action = Guard.Argument(action, nameof(action)).NotNull();
        }

        public ActionDelegate([NotNull] Action<string[]> action) : this("Executes custom user action.", action)
        {
        }

        public Task<bool> HandleAsync(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            action(argument);
            return Task.FromResult(true);
        }

        public void PrintSelf(IPrinter printer)
        {
            printer.Print(description);
            printer.NewLine();
        }
    }
}