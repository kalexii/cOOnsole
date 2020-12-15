using System;

namespace ConsoleAppFramework.ArgumentParsing
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ArgumentAttribute : Attribute
    {
        public string? ShortName { get; }

        public string LongName { get; }

        public string? Description { get; init; }

        public ArgumentAttribute(string longName, string? shortName = null)
        {
            LongName = longName;
            ShortName = shortName;
        }
    }
}