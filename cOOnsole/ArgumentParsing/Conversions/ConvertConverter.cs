using System;

namespace cOOnsole.ArgumentParsing.Conversions
{
    internal class ConvertConverter : IStringConverter
    {
        private readonly Type _target;

        public ConvertConverter(Type target) => _target = target;

        public object Convert(string token) => System.Convert.ChangeType(token, _target);
    }
}