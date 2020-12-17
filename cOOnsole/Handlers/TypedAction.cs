using System;
using System.Threading.Tasks;
using cOOnsole.ArgumentParsing;
using cOOnsole.ArgumentParsing.StateMachineParsing;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;
using cOOnsole.Utilities;

namespace cOOnsole.Handlers
{
    /// <summary>
    /// Tries to parse an array of string command line parameters into an object of type <see cref="T"/>.
    /// In case of successful parsing, calls the wrapped action providing the parsed object.
    /// </summary>
    /// <typeparam name="T">Argument class to parse properties for.</typeparam>
    public class TypedAction<T> : Handler where T : new()
    {
        private readonly Action<T, IHandlerContext> _action;
        private readonly ArgumentParser<T> _parser;

        /// <summary>Initializes an instance of <see cref="TypedAction{T}" />.</summary>
        /// <param name="action">Action to call in case of successful argument parsing.</param>
        public TypedAction(Action<T, IHandlerContext> action)
        {
            _action = action;
            _parser = new ArgumentParser<T>();
        }

        /// <inheritdoc />
        public override Task<HandleResult> HandleAsync(string[] input)
        {
            var (typedArgument, context) = _parser.Parse(input);

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

                return Task.FromResult(HandleResult.HandledWithError);
            }

            var missingRequireds = context.NotPopulatedRequired;
            if (missingRequireds.Count > 0)
            {
                foreach (var missingRequired in missingRequireds)
                {
                    printer
                       .Print($"Missing required argument: {string.Join(" | ", missingRequired.Argument.Aliases)}")
                       .NewLine();
                }

                printer.NewLine();
                PrintSelf(Context.Printer);
                return Task.FromResult(HandleResult.HandledWithError);
            }

            _action(typedArgument, Context);
            return Task.FromResult(HandleResult.Handled);
        }

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer) => _parser.PrintSelf(printer);
    }
}