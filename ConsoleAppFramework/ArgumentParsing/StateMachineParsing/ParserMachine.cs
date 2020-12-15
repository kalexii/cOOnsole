namespace ConsoleAppFramework.ArgumentParsing.StateMachineParsing
{
    internal class ParserMachine
    {
        private readonly ParserState _state;

        public ParserMachine(ParserState state) => _state = state;

        public void ParseAndPopulate(string[] arguments)
        {
            IParserState state = new ExpectingArgumentNameState(_state);
            foreach (var argument in arguments)
            {
                state = state.ParseNextToken(argument);
            }

            switch (state)
            {
                case ExpectingArgumentValueState s:
                    _state.Errors[s.Argument.Argument.LongName] = ParsingErrorKind.ValueIsMissing;
                    break;

                case ExpectingArrayOfArgumentsState s:
                    s.Flush();
                    break;
            }
        }
    }
}