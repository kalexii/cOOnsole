using System;
using ConsoleAppFramework.Description;
using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Reactions
{
    public class FuncDelegate<T> : IReaction<T>
    {
        private readonly Func<T, bool> func;

        public FuncDelegate([NotNull] Func<T, bool> func)
            => this.func = func ?? throw new ArgumentNullException(nameof(func));

        public bool React(T argument)
        {
            if (argument == null) throw new ArgumentNullException(nameof(argument));
            return func(argument);
        }

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
            Guard.Argument(descriptionVisitor).NotNull();
            descriptionVisitor.WriteContent("Executes custom function.");
        }
    }

    public class FuncDelegate : IReaction
    {
        private readonly Func<string[], bool> func;

        public FuncDelegate([NotNull] Func<string[], bool> func)
            => this.func = func ?? throw new ArgumentNullException(nameof(func));

        public bool React(string[] argument)
            => func(argument ?? throw new ArgumentNullException(nameof(argument)));

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
            Guard.Argument(descriptionVisitor).NotNull();
            descriptionVisitor.WriteContent("Executes custom function.");
        }
    }
}