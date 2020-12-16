using System.Reflection;

namespace cOOnsole.ArgumentParsing
{
    internal sealed record ArgumentProp(PropertyInfo Property, ArgumentAttribute Argument);
}