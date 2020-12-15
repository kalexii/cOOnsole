using System;
using System.Threading.Tasks;
using ConsoleAppFramework.ArgumentParsing;
using ConsoleAppFramework.Description;
using Dawn;

namespace ConsoleAppFramework.Reactions
{
    public class ParametrizedActionDelegate<T> : Handler where T : new()
    {
        private readonly string _description;
        private readonly Action<T> _action;
        private readonly ArgumentParser<T> _parser;

        public ParametrizedActionDelegate(string description, Action<T> action)
        {
            _description = Guard.Argument(description, nameof(description)).NotNull();
            _action = Guard.Argument(action, nameof(action)).NotNull();
            _parser = new ArgumentParser<T>();
        }

        public override Task<IHandler?> HandleAsync(string[] argument)
        {
            Guard.Argument(argument, nameof(argument)).NotNull();
            var (typedArgument, errors) = _parser.Parse(argument);
            if (errors.Count > 0)
            {
                return Task.FromResult<IHandler?>(this);
            }

            _action(typedArgument);
            return Task.FromResult<IHandler?>(null);
        }

        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_description).NewLine();
            printer.Indent();
            _parser.PrintSelf(printer);
            printer.Unindent();
        }
    }
}