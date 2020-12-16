namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal interface IParserState
    {
        IParserState ParseToken(string token);
    }
}