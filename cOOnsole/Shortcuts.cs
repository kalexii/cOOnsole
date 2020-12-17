using System;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;

namespace cOOnsole
{
    public static class Shortcuts
    {
        public static Token Token(string token, IHandler child)
            => new(token, child);

        public static Description Description(string description, IHandler child)
            => new(description, child);

        public static Fork Fork(params IHandler[] children)
            => new(children);

        public static PrintUsageIfUnmatched PrintUsageIfUnmatched(IHandler child)
            => new(child);

        public static Unconditional Unconditional(HandleResult result)
            => new(result);

        public static UntypedAction Action(Action<string[], HandlerContext> action)
            => new(action);

        public static TypedAction<T> Action<T>(Action<T, HandlerContext> action)
            where T : new() => new(action);
    }
}