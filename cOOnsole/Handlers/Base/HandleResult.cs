namespace cOOnsole.Handlers.Base
{
    /// <summary>Represents a set of possible handling results.</summary>
    /// <remarks>
    /// Any input can either be matched and handled successfully, handled with an error or not matched at all.
    /// </remarks>
    public enum HandleResult
    {
        /// <summary>The input was not matched with any handlers.</summary>
        NotMatched,

        /// <summary>The input was matched with a handler and the handler has handled the input successfully.</summary>
        Handled,

        /// <summary>Input was matched with a handler, but the handler has reported handle error.</summary>
        HandledWithError,
    }
}