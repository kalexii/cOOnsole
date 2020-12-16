namespace cOOnsole
{
    /// <summary>
    /// A record that contains a handle status, and the errored out handler, if any.
    /// </summary>
    public sealed record HandleResult(HandleStatus Status, IHandler? ErrorScope)
    {
        /// <summary>
        /// The input is handled successfully.
        /// </summary>
        public static readonly HandleResult Handled = new(HandleStatus.Handled, null);

        /// <summary>
        /// The input wasn't handled because no matching handler has been found.
        /// </summary>
        public static readonly HandleResult NotHandled = new(HandleStatus.NotHandled, null);

        /// <summary>
        /// The input was matched with a handler, but handler threw an error.
        /// </summary>
        /// <param name="handler">Handler that errored out.</param>
        public static HandleResult Error(IHandler handler) => new(HandleStatus.Error, handler);
    }
}