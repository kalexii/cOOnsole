using System;
using System.Linq;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Token : IReaction
    {
        private readonly string token;
        private readonly IReaction inner;

        public Token([NotNull] string token, [NotNull] IReaction inner)
        {
            this.token = token ?? throw new ArgumentNullException(nameof(token));
            this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public bool React(string[] argument)
        {
            if (argument == null) throw new ArgumentNullException(nameof(argument));
            return argument.Length > 0
                   && string.Equals(argument[0], token, StringComparison.OrdinalIgnoreCase)
                   && inner.React(argument.Skip(1).ToArray());
        }
    }
}