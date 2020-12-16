using System;

namespace cOOnsole.Tests.TestUtilities
{
    public static class ObjectExtensions
    {
        public static T Do<T>(this T obj, Action<T> mutator)
        {
            mutator(obj);
            return obj;
        }
    }
}