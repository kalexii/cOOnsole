using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Tests.TestUtilities
{
    public class NoopHandler : Handler
    {
        public static NoopHandler Handled { get; } = new(HandleResult.Handled);
        public static NoopHandler Error { get; } = new(HandleResult.Error);
        public static NoopHandler NotHandled { get; } = new(HandleResult.NotHandled);

        private readonly HandleResult _result;

        private NoopHandler(HandleResult result) => _result = result;

        public override void PrintSelf(IPrinter printer) => printer.Print("noop").NewLine();

        public override Task<HandleResult> HandleAsync(string[] arguments) => Task.FromResult(_result);
    }
}