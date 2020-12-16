using System;
using System.Threading.Tasks;
using cOOnsole.ArgumentParsing;
using cOOnsole.ArgumentParsing.StateMachineParsing;
using cOOnsole.Description;

namespace cOOnsole.Reactions
{
    public class ParametrizedActionDelegate<T> : Handler where T : new()
    {
        private readonly string _description;
        private readonly Action<T, HandlerContext> _action;
        private readonly ArgumentParser<T> _parser;

        public ParametrizedActionDelegate(string description, Action<T, HandlerContext> action)
        {
            _description = description;
            _action = action;
            _parser = new ArgumentParser<T>();
        }

        public override Task<HandleResult> HandleAsync(string[] argument)
        {
            var (typedArgument, context) = _parser.Parse(argument);
            var errors = context.ErrorAttempts;
            if (errors.Count > 0)
            {
                var printer = Context.Printer;
                printer.Print("Error!").NewLine().Indent();
                foreach (var (_, attemptedKey, attemptedValue, errorKind) in errors)
                {
                    var message = errorKind switch
                    {
                        ParsingErrorKind.OptionNotRecognized => "unknown option. please, refer to usage again",
                        ParsingErrorKind.ValueIsMissing      => "value is not provided for option",
                        ParsingErrorKind.ValueCouldNotBeParsedToType =>
                            $"value \"{attemptedValue.Join(x => x, x => string.Join(" ", x))}\" could not be converted to the parameter type. please, refer to usage",
                        var _ => throw new ArgumentOutOfRangeException(),
                    };

                    printer.Print($"{attemptedKey}: {message}").NewLine();
                }

                printer.NewLine();

                return Task.FromResult(HandleResult.Error(this));
            }

            var missingRequireds = context.GetUnpopulatedRequired();
            if (missingRequireds.Count > 0)
            {
                var printer = Context.Printer;
                printer.Print("Error!").NewLine().Indent();
                foreach (var missingRequired in missingRequireds)
                {
                    printer.Print($"Missing required argument: {missingRequired.Argument.LongName}").NewLine();
                }

                printer.NewLine();

                return Task.FromResult(HandleResult.Error(this));
            }

            _action(typedArgument, Context);
            return Task.FromResult(HandleResult.Handled);
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