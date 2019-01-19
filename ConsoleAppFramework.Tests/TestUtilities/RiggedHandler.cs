namespace ConsoleAppFramework.Tests.TestUtilities
{
    public class RiggedHandler : IReaction
    {
        private readonly bool answer;

        public RiggedHandler(bool answer) => this.answer = answer;

        public bool React(string[] argument) => answer;
    }
}