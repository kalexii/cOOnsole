using System.Reflection;

namespace ConsoleAppFramework.ArgumentParsing
{
    internal sealed record ArgumentProp(PropertyInfo Property, ArgumentAttribute Argument);
}