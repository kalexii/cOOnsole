using System;
using cOOnsole.Reactions;

namespace cOOnsole
{
    public static class Shortcuts
    {
        public static Token Token(string token, IHandler handler)
            => new(token, handler);

        public static Fork Fork(string description, params IHandler[] reactions)
            => new(description, reactions);

        public static Fork Fork(params IHandler[] reactions)
            => new(null, reactions);

        public static ActionDelegate Action(string description, Action<string[], HandlerContext> action)
            => new(description, action);

        public static ParametrizedActionDelegate<T> Action<T>(string description, Action<T, HandlerContext> action)
            where T : new() => new(description, action);
    }
}