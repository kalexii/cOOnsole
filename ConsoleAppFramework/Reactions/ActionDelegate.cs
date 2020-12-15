using System;
using System.Threading.Tasks;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class ActionDelegate : Handler
    {
        private readonly string _description;
        private readonly Action<string[]> _action;

        public ActionDelegate([NotNull] string description, [NotNull] Action<string[]> action)
        {
            _description = Guard.Argument(description, nameof(description)).NotNull();
            _action = Guard.Argument(action, nameof(action)).NotNull();
        }

        public override Task<IHandler?> HandleAsync(string[] arguments)
        {
            Guard.Argument(arguments, nameof(arguments)).NotNull();
            _action(arguments);
            return Task.FromResult<IHandler?>(null);
        }

        public override void PrintSelf(IPrinter printer) => printer.Print(_description).NewLine();
    }
}