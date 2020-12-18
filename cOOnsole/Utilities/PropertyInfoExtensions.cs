using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace cOOnsole.Utilities
{
    internal static class PropertyInfoExtensions
    {
        internal static string ToPrettyTypeName(this PropertyInfo propertyInfo)
        {
            static string ToPrettyNameWithoutNullable(Type t) => t switch
            {
                {IsGenericType: true} =>
                    $"{t.Name.Substring(0, t.Name.Length - 2)}<{string.Join(", ", t.GetGenericArguments().Select(ToPrettyNameWithoutNullable))}>",
                {IsEnum: true} => $"({string.Join(" | ", Enum.GetNames(t))})",
                var _          => t.Name,
            };

            var sb = new StringBuilder();
            if (propertyInfo.IsNullable())
            {
                if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) is { } underlying)
                {
                    sb.Append(ToPrettyNameWithoutNullable(underlying));
                }
                else
                {
                    sb.Append(ToPrettyNameWithoutNullable(propertyInfo.PropertyType));
                }

                sb.Append("?");
            }
            else
            {
                sb.Append(ToPrettyNameWithoutNullable(propertyInfo.PropertyType));
            }

            return sb.ToString();
        }

        public static bool IsNullable(this PropertyInfo property) =>
            property.PropertyType.IsValueType
                ? Nullable.GetUnderlyingType(property.PropertyType) != null
                : property.IsNullableReferenceType();

        // ReSharper disable once CognitiveComplexity
        private static bool IsNullableReferenceType(this MemberInfo property)
        {
            const string nullableAttribute = "System.Runtime.CompilerServices.NullableAttribute";
            var nullable = property
               .CustomAttributes
               .FirstOrDefault(x => x.AttributeType.FullName == nullableAttribute);
            if (nullable != null && nullable.ConstructorArguments.Count == 1)
            {
                var attributeArgument = nullable.ConstructorArguments[0];
                if (attributeArgument.ArgumentType == typeof(byte[]))
                {
                    var args = attributeArgument.Value as ReadOnlyCollection<CustomAttributeTypedArgument>;
                    if (args?.Count > 0 && args[0].ArgumentType == typeof(byte))
                    {
                        return (byte) (args[0].Value ?? (byte) 0) == 2;
                    }
                }
                else if (attributeArgument.ArgumentType == typeof(byte))
                {
                    return (byte) (attributeArgument.Value ?? (byte) 0) == 2;
                }
            }

            const string? contextAttribute = "System.Runtime.CompilerServices.NullableContextAttribute";
            var context = property.DeclaringType?
               .CustomAttributes
               .FirstOrDefault(x => x.AttributeType.FullName == contextAttribute);
            if (context != null &&
                context.ConstructorArguments.Count == 1 &&
                context.ConstructorArguments[0].ArgumentType == typeof(byte))
            {
                return (byte) (context.ConstructorArguments[0].Value ?? (byte) 0) == 2;
            }

            return false;
        }
    }
}