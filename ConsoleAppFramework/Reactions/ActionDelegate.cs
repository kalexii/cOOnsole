using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class ActionDelegate : IHandler
    {
        private readonly string _description;
        private readonly Action<string[]> _action;

        public ActionDelegate([NotNull] string description, [NotNull] Action<string[]> action)
        {
            _description = Guard.Argument(description, nameof(description)).NotNull();
            _action = Guard.Argument(action, nameof(action)).NotNull();
        }

        public Task<bool> HandleAsync(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            _action(argument);
            return Task.FromResult(true);
        }

        public void PrintSelf(IPrinter printer) => printer.Print(_description).NewLine();
    }
}