using System;
using System.Linq;

namespace cOOnsole.Tests.TestUtilities
{
    public static class StringExtensions
    {
        public static string SkipFirstLine(this string value)
            => string.Join(Environment.NewLine,
                value.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Skip(1));

        public static string TakeFirstLine(this string value)
            => value.Split(new[] {Environment.NewLine}, StringSplitOptions.None)[0];
    }
}