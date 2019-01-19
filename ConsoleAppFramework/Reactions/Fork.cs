using System;
using System.Linq;
using ConsoleAppFramework.Description;
using Dawn;
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

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
            Guard.Argument(descriptionVisitor).NotNull();
            foreach (var reaction in reactions)
            {
                reaction.Describe(descriptionVisitor);
            }
        }
    }
}