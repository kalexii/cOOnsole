namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal class ParserMachine
    {
        private readonly ParserContext _context;

        public ParserMachine(ParserContext context) => _context = context;

        public void ParseAndPopulate(string[] arguments)
        {
            IParserState state = new ExpectingArgumentNameState(_context);
            for (var i = 0; i < arguments.Length; i++)
            {
                var argument = arguments[i];
                state = state.ParseToken(argument);
            }

            switch (state)
            {
                case ExpectingArgumentValueState s:
                    s.Flush();
                    break;

                case ExpectingArrayOfArgumentsState s:
                    s.Flush();
                    break;
            }
        }
    }
}