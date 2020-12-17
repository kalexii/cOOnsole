using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    public class Unconditional : Handler
    {
        private readonly HandleResult _result;

        public Unconditional(HandleResult result) => _result = result;

        public override void PrintSelf(IPrinter printer)
        {
        }

        public override Task<HandleResult> HandleAsync(string[] arguments) => Task.FromResult(_result);
    }
}