using System;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole
{
    /// <summary>
    /// Represents cOOnsole's command-line interface entry point. Can be used as a REPL, or directly return error code to the
    /// Main.
    /// </summary>
    public class Cli
    {
        private readonly IPrinter _printer;
        private readonly IHandler _root;

        /// <summary>Initializes the instance of the <see cref="Cli" />.</summary>
        /// <param name="root">Root of the handler tree.</param>
        /// <param name="window">Window to output to.</param>
        public Cli(IHandler root, IWritableOutput? window = null)
        {
            _root = root;
            _printer = new Printer(window ?? new ConsoleOutput());
            _root.SetContext(new HandlerContext(_printer));
        }

        /// <summary>
        /// Feeds arguments to a tree of handlers, and returns error code to return in the Main method. This method should be used
        /// when you don't intend to use cOOnsole in REPL mode.
        /// </summary>
        /// <remarks>
        /// The returned error code is going to be 0 in case the input is <see cref="HandleResult.Handled" />, -1 when the input is
        /// <see cref="HandleResult.NotMatched" /> and 1 when the input is <see cref="HandleResult.HandledWithError" />, or an unhandled
        /// exception has been thrown.
        /// </remarks>
        /// <param name="input">Array of command line arguments fed into the application.</param>
        /// <returns>Error code to return in the main method.</returns>
        public async Task<int> HandleAndGetExitCode(string[] input)
        {
            try
            {
                var result = await _root.HandleAsync(input).ConfigureAwait(false);
                _printer.Flush();
                return result switch
                {
                    HandleResult.NotMatched       => -1,
                    HandleResult.HandledWithError => 1,
                    var _                         => 0,
                };
            }
            catch (Exception e)
            {
                _printer.ResetIndent().Print(e.ToString()).Flush();
                return 1;
            }
        }

        /// <summary>Shortcut for <see cref="HandleAndGetExitCode" />.</summary>
        /// <param name="input">Array of command line arguments fed into the application.</param>
        /// <returns>True, if <see cref="HandleAndGetExitCode" /> returned 0. False, otherwise.</returns>
        public Task<bool> HandleAsync(string[] input)
            => HandleAndGetExitCode(input).ContinueWith(x => x.Result == 0);
    }
}