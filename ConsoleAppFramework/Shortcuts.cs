using System;
using ConsoleAppFramework.Reactions;

namespace ConsoleAppFramework
{
    public static class Shortcuts
    {
        public static Token Token(string token, IHandler handler) => new Token(token, handler);

        public static Fork Fork(params IHandler[] reactions) => new Fork(reactions);

        public static Fork Fork(string description, params IHandler[] reactions) => new Fork(description, reactions);

        public static ActionDelegate Action(Action<string[]> action) => new ActionDelegate(action);

        public static ActionDelegate Action(string description, Action<string[]> action) =>
            new ActionDelegate(description, action);

        public static ActionDelegate<T> Action<T>(Action<T> action) where T : class => new ActionDelegate<T>(action);

        public static ActionDelegate<T> Action<T>(string description, Action<T> action) where T : class =>
            new ActionDelegate<T>(description, action);
    }
}