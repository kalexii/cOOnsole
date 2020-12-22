using System;
using System.Threading.Tasks;
using cOOnsole.Handlers;
using cOOnsole.Handlers.Base;
using cOOnsole.Printing;

namespace cOOnsole
{
    /// <summary>A set of shortcuts for more convenient handler tree building.</summary>
    public static class Shortcuts
    {
        /// <inheritdoc cref="cOOnsole.Cli" />
        public static Cli Cli(IHandler root, IWritableOutput? output = null) => new(root, output);

        /// <inheritdoc cref="Handlers.Token" />
        public static Token Token(string token, IHandler child)
            => new(token, child);

        /// <inheritdoc cref="Handlers.Description" />
        public static Description Description(string description, IHandler child)
            => new(description, child);

        /// <inheritdoc cref="Handlers.Fork" />
        public static Fork Fork(params IHandler[] children)
            => new(children);

        /// <inheritdoc cref="Handlers.PrintUsageIfNotMatched" />
        public static PrintUsageIfNotMatched PrintUsageIfNotMatched(IHandler child)
            => new(child);

        /// <inheritdoc cref="Handlers.Unconditional" />
        public static Unconditional Unconditional(HandleResult result)
            => new(result);

        /// <inheritdoc cref="Handlers.UntypedAction" />
        public static UntypedAction Action(Action<string[], IHandlerContext> action)
            => new((arg, context) =>
            {
                action(arg, context);
                return Task.FromResult(HandleResult.Handled);
            });

        /// <inheritdoc cref="Handlers.UntypedAction" />
        public static UntypedAction Action(Func<string[], IHandlerContext, HandleResult> action)
            => new((arg, context) => Task.FromResult(action(arg, context)));

        /// <inheritdoc cref="Handlers.UntypedAction" />
        public static UntypedAction Action(Func<string[], IHandlerContext, Task<HandleResult>> action)
            => new(action);

        /// <inheritdoc cref="Handlers.TypedAction{T}" />
        public static TypedAction<T> Action<T>(Action<T, IHandlerContext> action)
            where T : new() => new((arg, context) =>
        {
            action(arg, context);
            return Task.FromResult(HandleResult.Handled);
        });

        /// <inheritdoc cref="Handlers.TypedAction{T}" />
        public static TypedAction<T> Action<T>(Func<T, IHandlerContext, HandleResult> action)
            where T : new() => new((arg, context) => Task.FromResult(action(arg, context)));

        /// <inheritdoc cref="Handlers.TypedAction{T}" />
        public static TypedAction<T> Action<T>(Func<T, IHandlerContext, Task<HandleResult>> action)
            where T : new() => new(action);
    }
}