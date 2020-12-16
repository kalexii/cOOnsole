using System.Reflection;
using System.Threading.Tasks;
using cOOnsole.Description;
using cOOnsole.Handlers.Base;

namespace cOOnsole.Handlers
{
    public class PrintUsageIfUnmatched : Handler
    {
        public PrintUsageIfUnmatched(IHandler child) => Child = child;

        public override void PrintSelf(IPrinter printer)
        {
            var assembly = Assembly.GetEntryAssembly();
            var version = assembly
              ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
              ?.InformationalVersion;

            printer
               .Print($"{assembly!.GetName().Name} v{version}")
               .NewLine()
               .Print("Usage:")
               .NewLine()
               .NewLine()
               .Indent();
            Child.PrintSelf(printer);
        }

        public override async Task<HandleResult> HandleAsync(string[] arguments)
        {
            var result = await Child.HandleAsync(arguments);
            if (result == HandleResult.NotHandled)
            {
                PrintSelf(Context.Printer);
                return HandleResult.Handled;
            }

            return result;
        }
    }
}