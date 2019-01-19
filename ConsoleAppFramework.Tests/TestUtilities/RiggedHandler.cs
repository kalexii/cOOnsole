using ConsoleAppFramework.Description;
using Dawn;

namespace ConsoleAppFramework.Tests.TestUtilities
{
    public class RiggedHandler : IReaction
    {
        private readonly bool answer;

        public RiggedHandler(bool answer) => this.answer = answer;

        public bool React(string[] argument) => answer;

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
            Guard.Argument(descriptionVisitor).NotNull();
            descriptionVisitor.WriteContent("Doesn't do anything but returns specified handled status.");
        }
    }
}