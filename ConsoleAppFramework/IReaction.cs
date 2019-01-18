namespace ConsoleAppFramework
{
    public interface IReaction<in TArgument>
    {
        void React(TArgument argument);
    }

    public interface IReaction : IReaction<string[]>
    {
    }
}