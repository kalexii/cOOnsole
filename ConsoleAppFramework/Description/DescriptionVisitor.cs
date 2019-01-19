using System.Collections.Generic;

namespace ConsoleAppFramework.Description
{
    public class DescriptionVisitor : IDescriptionVisitor
    {
        private readonly List<DescriptionItem> items = new List<DescriptionItem>();

        public IReadOnlyList<DescriptionItem> Items => items;

        public void WriteHeader(string header)
            => items.Add(new DescriptionItem(header, DescriptionItemType.Header));

        public void WriteContent(string content)
            => items.Add(new DescriptionItem(content, DescriptionItemType.Content));
    }
}