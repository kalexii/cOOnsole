using ConsoleAppFramework.Description;

namespace ConsoleAppFramework.Tests.TestUtilities
{
    public class NoOp : IReaction
    {
        public bool React(string[] argument)
            => true;

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
        }
    }
}