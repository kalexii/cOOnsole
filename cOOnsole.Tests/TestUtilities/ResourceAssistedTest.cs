using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace cOOnsole.Tests.TestUtilities
{
    public class ResourceAssistedTest
    {
        protected string AsExpectedForThisTest(string extension = ".txt")
        {
            var callerMethod = new StackTrace(1)
               .GetFrames()?
               .FirstOrDefault(frame => frame?.GetMethod() is {DeclaringType: {Namespace: { } ns}} method
                                        && ns.StartsWith(nameof(cOOnsole))
                                        && method.Name != "MoveNext")? // omit async state machine frames
               .GetMethod();

            if (callerMethod is null)
            {
                throw new InvalidOperationException("Unable to get caller. " +
                                                    "You are probably in the wrong async context.");
            }

            var expectedEnding = $"{callerMethod.DeclaringType?.Name}.{callerMethod.Name}{extension}";
            var assembly = GetType().Assembly;
            var resourceNames = assembly.GetManifestResourceNames();
            var resourceName = resourceNames.SingleOrDefault(x => x.EndsWith(expectedEnding));
            if (resourceName is null)
            {
                throw new InvalidOperationException($"There is no resource that ends with `{expectedEnding}`");
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