namespace ConsoleAppFramework.ArgumentParsing.StateMachineParsing
{
    internal class ExpectingArgumentValueState : IParserState
    {
        private readonly ParserState _state;

        public ExpectingArgumentValueState(ParserState state, ArgumentProp argument)
        {
            _state = state;
            Argument = argument;
        }

        public ArgumentProp Argument { get; }

        public IParserState ParseNextToken(string nextToken)
        {
            var (p, _) = Argument;
            p.SetValue(_state.Target, _state.GetOrAddConverter(p.PropertyType).Convert(nextToken));
            return new ExpectingArgumentNameState(_state);
        }
    }
}