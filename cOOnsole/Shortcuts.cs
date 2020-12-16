using System;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using Action = cOOnsole.Handlers.Action;

namespace cOOnsole
{
    public static class Shortcuts
    {
        public static Token Token(string token, IHandler child)
            => new(token, child);

        public static SectionDescription Section(string description, IHandler child)
            => new(description, child);

        public static Fork Fork(params IHandler[] children)
            => new(children);

        public static PrintUsageIfUnmatched PrintUsage(IHandler child)
            => new(child);

        public static Action Action(string description, Action<string[], HandlerContext> action)
            => new(description, action);
        
        public static Unconditional Unconditional(HandleResult result)
            => new(result);

        public static ArgumentAction<T> Action<T>(string description, Action<T, HandlerContext> action)
            where T : new() => new(description, action);
    }
}