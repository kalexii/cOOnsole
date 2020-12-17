using System;

namespace cOOnsole.ArgumentParsing
{
    /// <summary>
    /// Applied to argument class properties, this attribute represents argument names and description that is used for help
    /// generation and parameter matching.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ArgumentAttribute : Attribute
    {
        /// <summary>Instantiates an instance of the <see cref="ArgumentAttribute" />.</summary>
        /// <param name="aliases">List of names that the parser with match on.</param>
        public ArgumentAttribute(params string[] aliases) => Aliases = aliases;

        /// <summary>List of names that the parser with match on.</summary>
        /// <example>new [] {"--my-arg", "-m"}</example>
        public string[] Aliases { get; }

        /// <summary>Optional description for the argument. Used when generating the usage string.</summary>
        public string? Description { get; init; }
    }
}