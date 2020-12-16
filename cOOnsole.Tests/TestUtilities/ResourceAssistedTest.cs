using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace cOOnsole.Tests.TestUtilities
{
    public class ResourceAssistedTest
    {
        protected string AsExpectedForThisTest([CallerMemberName] string? caller = null, string extension = ".txt")
        {
            var assembly = GetType().Assembly;
            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(caller + extension));
            if (resourceName is null)
            {
                throw new InvalidOperationException($"There is no resource that ends with `{caller}`");
            }

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
            {
                throw new InvalidOperationException($"Resource `{resourceName}` is null.");
            }

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}