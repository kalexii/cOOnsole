using System.Reflection;
using cOOnsole.Utilities;

namespace cOOnsole.ArgumentParsing
{
    internal sealed record ArgumentProp(PropertyInfo Property, ArgumentAttribute Argument)
    {
        public bool IsRequired { get; } = !Property.IsNullable();
    }
}