using System;

namespace cOOnsole.Tests.TestUtilities
{
    public static class StringExtensions
    {
        public static string SkipFirstLine(this string value)
            => string.Join(Environment.NewLine, value.Split(Environment.NewLine)[1..]);

        public static string TakeFirstLine(this string value)
            => value.Split(Environment.NewLine)[0];
    }
}