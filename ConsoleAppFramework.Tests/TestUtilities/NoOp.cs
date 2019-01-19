namespace ConsoleAppFramework.Tests.TestUtilities
{
    public class NoOp : IReaction
    {
        public bool React(string[] argument)
            => true;
    }
}