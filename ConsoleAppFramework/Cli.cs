using System;
using ConsoleAppFramework.Reactions;

namespace ConsoleAppFramework
{
    /// <summary>
    /// Contains shortcuts to common reactions.
    /// </summary>
    public static class Cli
    {
        public static Token Token(string token, IReaction reaction) => new Token(token, reaction);

        public static Fork Fork(params IReaction[] reactions) => new Fork(reactions);

        public static FuncDelegate Func(Func<string[], bool> func) => new FuncDelegate(func);

        public static FuncDelegate<T> Func<T>(Func<T, bool> func) => new FuncDelegate<T>(func);

        public static ActionDelegate Action(Action<string[]> action) => new ActionDelegate(action);

        public static ActionDelegate<T> Action<T>(Action<T> action) => new ActionDelegate<T>(action);
    }
}