using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class ActionDelegate : IHandler
    {
        private readonly string description;
        private readonly Action<string[]> action;

        public ActionDelegate([NotNull] string description, [NotNull] Action<string[]> action)
        {
            this.description = Guard.Argument(description, nameof(description)).NotNull();
            this.action = Guard.Argument(action, nameof(action)).NotNull();
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