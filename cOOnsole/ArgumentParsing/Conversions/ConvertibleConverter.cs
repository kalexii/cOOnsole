using System;

namespace cOOnsole.ArgumentParsing.Conversions
{
    internal class ConvertibleConverter : IStringToTypeConverter
    {
        private readonly Type _target;

        public ConvertibleConverter(Type target) => _target = target;

        public object Convert(string token) => System.Convert.ChangeType(token, _target);
    }
}