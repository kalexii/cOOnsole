using ConsoleAppFramework.Description;
using Dawn;

namespace ConsoleAppFramework.Tests.TestUtilities
{
    public class Tracker : IReaction
    {
        private readonly IReaction inner;

        public Tracker(IReaction inner = null) 
            => this.inner = inner;

        public int TimesCalled { get; private set; }

        public string[] LastInput { get; set; }

        public bool React(string[] argument)
        {
            LastInput = argument;
            ++TimesCalled;
            return inner?.React(argument) ?? true;
        }

        public void Describe(IDescriptionVisitor descriptionVisitor)
        {
            Guard.Argument(descriptionVisitor).NotNull();
            descriptionVisitor.WriteContent("Tracks calls to this reaction.");
        }
    }
}