using System;
using System.Threading.Tasks;
using cOOnsole.ArgumentParsing;
using cOOnsole.ArgumentParsing.StateMachineParsing;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;
using cOOnsole.Utilities;

namespace cOOnsole.Handlers
{
    public class TypedAction<T> : Handler where T : new()
    {
        private readonly Action<T, IHandlerContext> _action;
        private readonly ArgumentParser<T> _parser;

        public TypedAction(Action<T, IHandlerContext> action)
        {
            _action = action;
            _parser = new ArgumentParser<T>();
        }

        public override Task<HandleResult> HandleAsync(string[] argument)
        {
            var (typedArgument, context) = _parser.Parse(argument);

            var printer = Context.Printer;
            if (context.ErrorAttempts.Count > 0)
            {
                foreach (var (arg, key, value, errorKind) in context.ErrorAttempts)
                {
                    const string usage = "please, refer to the usage again";
                    var message = errorKind switch
                    {
                        ParsingErrorKind.OptionNotRecognized
                            => $"unknown option. {usage}",
                        ParsingErrorKind.ValueIsMissing
                            => "value is not provided for option",
                        ParsingErrorKind.ValueCouldNotBeParsedToType
                            => $"value \"{value.ToString()}\" could not be converted to the [{arg!.Property.ToPrettyTypeName()}] type. {usage}",
                        var _ => throw new ArgumentOutOfRangeException(),
                    };

                    printer.Print($"{key}: {message}").NewLine().NewLine();
                    PrintSelf(printer);
                }

                return Task.FromResult(HandleResult.Error);
            }

            var missingRequireds = context.NotPopulatedRequired;
            if (missingRequireds.Count > 0)
            {
                foreach (var missingRequired in missingRequireds)
                {
                    printer.Print($"Missing required argument: {missingRequired.Argument.LongName}").NewLine();
                }

                printer.NewLine();
                PrintSelf(Context.Printer);
                return Task.FromResult(HandleResult.Error);
            }

            _action(typedArgument, Context);
            return Task.FromResult(HandleResult.Handled);
        }

        public override void PrintSelf(IPrinter printer) => _parser.PrintSelf(printer);
    }
}