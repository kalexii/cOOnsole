using System;
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
    }

    public class FuncDelegate : IReaction
    {
        private readonly Func<string[], bool> func;

        public FuncDelegate([NotNull] Func<string[], bool> func)
            => this.func = func ?? throw new ArgumentNullException(nameof(func));

        public bool React(string[] argument)
            => func(argument ?? throw new ArgumentNullException(nameof(argument)));
    }
}