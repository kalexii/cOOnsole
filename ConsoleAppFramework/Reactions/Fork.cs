using System;
using System.Linq;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class Fork : IReaction
    {
        private readonly IReaction[] reactions;

        public Fork([NotNull] params IReaction[] reactions)
            => this.reactions = reactions ?? throw new ArgumentNullException(nameof(reactions));

        public bool React(string[] argument)
        {
            if (argument == null) throw new ArgumentNullException(nameof(argument));
            return reactions.Any(t => t.React(argument));
        }
    }
}