using System.Linq;
using cOOnsole.ArgumentParsing.StateMachineParsing.States;

namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal class ParserStateMachine
    {
        private readonly ParserContext _context;

        public ParserStateMachine(ParserContext context) => _context = context;

        public void ParseAndPopulate(string[] arguments)
        {
            IParserState state = new ExpectingArgumentNameState(_context);
            state = arguments.Aggregate(state, (current, t) => current.ParseToken(t));
            if (state is IFlushable f)
            {
                f.Flush();
            }
        }
    }
}