namespace cOOnsole.ArgumentParsing.StateMachineParsing.States
{
    internal class ExpectingArgumentValueState : IParserState, IFlushable
    {
        private readonly ParserContext _context;
        private readonly string _key;

        public ExpectingArgumentValueState(ParserContext context, ArgumentProp argument, string key)
        {
            _context = context;
            _key = key;
            Argument = argument;
        }

        private ArgumentProp Argument { get; }

        private string? Captured { get; set; }

        public IParserState ParseToken(string token)
        {
            Captured = token;
            Flush();
            return new ExpectingArgumentNameState(_context);
        }

        public void Flush()
        {
            var (p, _) = Argument;
            var attempt = new ParseAttempt(Argument, _key, Captured);
            if (Captured is null)
            {
                _context.SaveAttempt(attempt with {ErrorKind = ParsingErrorKind.ValueIsMissing});
                return;
            }

            try
            {
                p.SetValue(_context.Target, _context.Convert(Captured, p.PropertyType));
                _context.SaveAttempt(attempt);
            }
            catch
            {
                _context.SaveAttempt(attempt with {ErrorKind = ParsingErrorKind.ValueCouldNotBeParsedToType});
            }
        }
    }
}