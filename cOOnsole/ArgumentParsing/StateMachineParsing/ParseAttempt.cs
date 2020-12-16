using Microsoft.Extensions.Primitives;

namespace cOOnsole.ArgumentParsing.StateMachineParsing
{
    internal sealed record ParseAttempt(ArgumentProp? Argument,
                                        string AttemptedKey,
                                        StringValues? AttemptedValue,
                                        ParsingErrorKind? ErrorKind = null);
}