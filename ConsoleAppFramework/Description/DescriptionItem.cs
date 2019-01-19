using Dawn;
using JetBrains.Annotations;

namespace ConsoleAppFramework.Description
{
    public class DescriptionItem
    {
        public DescriptionItem([NotNull] string content, DescriptionItemType descriptionItemType)
        {
            Value = Guard.Argument(content).NotNull();
            DescriptionItemType = Guard.Argument(descriptionItemType).Defined().NotDefault();
        }

        public string Value { get; set; }
        public DescriptionItemType DescriptionItemType { get; set; }
    }
}