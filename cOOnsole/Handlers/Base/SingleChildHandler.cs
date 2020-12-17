namespace cOOnsole.Handlers.Base
{
    /// <summary>Handler that wraps a child and stores it in the <see cref="P:cOOnsole.Handlers.Base.SingleChildHandler.Wrapped" /> property.</summary>
    /// <remarks>It's a responsibility of a derived class to populate this property.</remarks>
    public abstract class SingleChildHandler : Handler
    {
        /// <summary>Initializes an instance of single child handler.</summary>
        /// <param name="wrapped">Wrapped (child) handler.</param>
        protected SingleChildHandler(IHandler wrapped) => Wrapped = wrapped;

        /// <summary>A wrapped (child) handler.</summary>
        protected IHandler Wrapped { get; }

        /// <inheritdoc />
        public override void SetContext(IHandlerContext context)
        {
            base.SetContext(context);
            Wrapped.SetContext(context);
        }
    }
}