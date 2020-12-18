using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    /// <summary>
    /// This handler has it's uses when you want to manipulate the <see cref="HandleResult"/> returned by handlers.
    /// Most useful when creating a <c>help</c> command when there's a <see cref="PrintUsageIfNotMatched"/> is in the root of the tree.
    /// </summary>
    public class Unconditional : Handler
    {
        private readonly HandleResult _result;

        /// <summary>Initializes an instance of <see cref="Unconditional" />.</summary> 
        /// <param name="result">Result to always return.</param>
        public Unconditional(HandleResult result) => _result = result;

        /// <inheritdoc />
        public override Task<HandleResult> HandleAsync(string[] input) => Task.FromResult(_result);

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer)
        {
        }
    }
}