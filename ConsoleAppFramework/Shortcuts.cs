using System;
using ConsoleAppFramework.Reactions;

namespace ConsoleAppFramework
{
    public static class Shortcuts
    {
        public static Token Token(string token, IHandler handler) => new(token, handler);

        public static Fork Fork(string description, params IHandler[] reactions) => new(description, reactions);

        public static ActionDelegate Action(string description, Action<string[]> action) => new(description, action);
    }
}