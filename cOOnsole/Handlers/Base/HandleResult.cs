namespace cOOnsole.Handlers.Base
{
    /// <summary>
    /// Essentially a tri-state of possible handle results.
    /// Any input can either be handled successfully, handled with an error or not handled at all.
    /// </summary>
    public enum HandleResult
    {
        NotHandled,
        Handled,
        Error,
    }
}