namespace cOOnsole.Handlers.Base
{
    /// <summary>
    /// Handler that wraps a child and stores it in the <see cref="Child"/> property.
    /// </summary>
    /// <remarks>
    /// It's a responsibility of a derived class to populate this property.
    /// </remarks>
    public abstract class SingleChildHandler : Handler
    {
        protected IHandler Child { get; init; } = default!;

        public override void SetContext(IHandlerContext context)
        {
            base.SetContext(context);
            Child.SetContext(context);
        }
    }
}