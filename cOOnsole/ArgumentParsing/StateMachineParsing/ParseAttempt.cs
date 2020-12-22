namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal sealed record ParseAttempt(ArgumentProp? Argument,
                                        string AttemptedKey,
                                        string?[] AttemptedValue,
                                        ParsingErrorKind? ErrorKind = null);
}