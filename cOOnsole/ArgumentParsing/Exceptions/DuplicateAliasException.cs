using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace cOOnsole.ArgumentParsing.Exceptions
{
    /// <summary>
    /// Represents error that occurs when trying to parse an argument class and there are duplicate parameter aliases. 
    /// </summary>
    [Serializable]
    public class DuplicateAliasException : Exception
    {
        /// <inheritdoc />
        public DuplicateAliasException(MemberInfo type, string alias)
            : base($"Duplicate alias in class {type.Name}: {alias}")
        {
        }

        /// <inheritdoc />
        protected DuplicateAliasException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}