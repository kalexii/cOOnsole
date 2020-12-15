namespace ConsoleAppFramework.ArgumentParsing.StateMachineParsing
{
    internal interface IParserState
    {
        IParserState ParseNextToken(string nextToken);
    }
}