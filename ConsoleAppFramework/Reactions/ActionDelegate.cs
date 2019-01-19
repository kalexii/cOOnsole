using System;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class ActionDelegate<T> : IReaction<T>
    {
        private readonly Action<T> action;

        public ActionDelegate([NotNull] Action<T> action)
            => this.action = action ?? throw new ArgumentNullException(nameof(action));

        public bool React(T argument)
        {
            if (argument == null) throw new ArgumentNullException(nameof(argument));
            action(argument);
            return true;
        }

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
            Guard.Argument(descriptionVisitor).NotNull();
            descriptionVisitor.WriteContent("Executes custom user action.");
        }
    }

    public class ActionDelegate : IReaction
    {
        private readonly Action<string[]> action;

        public ActionDelegate([NotNull] Action<string[]> action)
            => this.action = action ?? throw new ArgumentNullException(nameof(action));

        public bool React(string[] argument)
        {
            action(argument ?? throw new ArgumentNullException(nameof(argument)));
            return true;
        }

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
            Guard.Argument(descriptionVisitor).NotNull();
            descriptionVisitor.WriteContent("Executes custom user action.");
        }
    }
}