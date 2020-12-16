using System;

namespace cOOnsole.ArgumentParsing.Conversions
{
    internal class EnumConverter : IStringConverter
    {
        private readonly Type _enumType;

        public EnumConverter(Type enumType) => _enumType = enumType;

        public object Convert(string token) => Enum.Parse(_enumType, token, true);
    }
}