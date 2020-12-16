namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal class ExpectingArgumentValueState : IParserState
    {
        private readonly ParserContext _context;
        private readonly string _key;

        public ExpectingArgumentValueState(ParserContext context, ArgumentProp argument, string key)
        {
            _context = context;
            _key = key;
            Argument = argument;
        }

        public ArgumentProp Argument { get; }

        public string? Captured { get; private set; }

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

            var converter = _context.GetOrAddConverter(p.PropertyType);
            try
            {
                p.SetValue(_context.Target, converter.Convert(Captured));
                _context.SaveAttempt(attempt);
            }
            catch
            {
                _context.SaveAttempt(attempt with {ErrorKind = ParsingErrorKind.ValueCouldNotBeParsedToType});
            }
        }
    }
}