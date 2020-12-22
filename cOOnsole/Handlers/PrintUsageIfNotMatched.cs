using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole.Handlers
{
    /// <summary>
    /// This node prints the usage for the application enhancing it with the assembly name and version in case the child handler did not match.
    /// This is most useful at the root of the handler tree. 
    /// </summary>
    public class PrintUsageIfNotMatched : SingleChildHandler
    {
        /// <summary>Initializes an instance of <see cref="PrintUsageIfNotMatched" />.</summary>
        public PrintUsageIfNotMatched(IHandler wrapped) : base(wrapped)
        {
        }

        /// <inheritdoc />
        public override void PrintSelf(IPrinter printer)
        {
            var assembly = Assembly.GetEntryAssembly()
                           ?? AppDomain.CurrentDomain
                              .GetAssemblies()
                              .First(x => !x.FullName.StartsWith("mscorlib") && !x.FullName.StartsWith("System."));

            var version = assembly
              ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
              ?.InformationalVersion;

            var description = assembly
              ?.GetCustomAttribute<AssemblyDescriptionAttribute>()
              ?.Description is { } d
                ? $" - {d}"
                : "";

            using (printer
               .Print($"{assembly!.GetName().Name} v{version}{description}")
               .NewLine()
               .Print("Usage:")
               .NewLine()
               .NewLine()
               .Indent())
            {
                Wrapped.PrintSelf(printer);
            }
        }

        /// <inheritdoc />
        public override async Task<HandleResult> HandleAsync(string[] input)
        {
            var result = await Wrapped.HandleAsync(input).ConfigureAwait(false);
            if (result == HandleResult.NotMatched)
            {
                PrintSelf(Context.Printer);
                return HandleResult.Handled;
            }

            return result;
        }
    }
}