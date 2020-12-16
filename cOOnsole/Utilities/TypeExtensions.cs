using System;

namespace cOOnsole.Utilities
{
    internal static class TypeExtensions
    {
        public static Type GetUnderlyingNullableOrThis(this Type type) => Nullable.GetUnderlyingType(type) ?? type;
    }
}