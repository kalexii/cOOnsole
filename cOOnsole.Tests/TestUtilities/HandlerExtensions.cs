using System.Threading.Tasks;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Tests.TestUtilities
{
    public static class HandlerExtensions
    {
        public static async Task<(bool handled, string printed)> ExecuteAndCaptureAsync(
            this IHandler handler, params string[] arguments)
        {
            var (output, sb) = HandlerMocks.MockOutput();
            var cli = new Cli(handler, output.Object);
            var handled = await cli.HandleAsync(arguments);
            return (handled, sb.ToString());
        }
    }
}