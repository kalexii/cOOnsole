using System;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    /// <summary>
    /// Matches the first given argument with a token,
    /// and in case of a match passes the rest of the arguments to the wrapped handler.
    /// </summary>
    /// <remarks>
    /// Matching is done by comparing strings with <see cref="Comparison"/> option. 
    /// </remarks>
    public sealed class Token : SingleChildHandler
    {
        private readonly string _token;

        /// <summary>
        /// String comparison option.
        /// </summary>
        public StringComparison Comparison { get; init; } = StringComparison.OrdinalIgnoreCase;

        /// <summary>Initializes an instance of <see cref="Token" />.</summary>
        public Token(string token, IHandler wrapped) : base(wrapped) => _token = token;

        /// <inheritdoc />
        public override Task<HandleResult> HandleAsync(string[] input)
            => input.Length > 0 && string.Equals(input[0], _token, Comparison)
                ? Wrapped.HandleAsync(input[1..])
                : Task.FromResult(HandleResult.NotMatched);

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer)
        {
            printer.Print(_token).NewLine();
            printer.Indent();
            Wrapped.PrintSelf(printer);
            printer.Unindent().NewLine();
        }
    }
}