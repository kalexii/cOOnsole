using System.Collections.Generic;

namespace cOOnsole.Handlers.Base
{
    /// <summary>Handler that wraps multiple children and stores them in the <see cref="Wrapped" /> property.</summary>
    /// <remarks>It's a responsibility of a derived class to populate this property.</remarks>
    public abstract class MultipleChildHandler : Handler
    {
        /// <summary>Initializes an instance of a multiple child handler.</summary>
        /// <param name="wrapped">A list of wrapped (child) handlers.</param>
        protected MultipleChildHandler(IReadOnlyList<IHandler> wrapped) => Wrapped = wrapped;

        /// <summary>A list of wrapped (child) handlers.</summary>
        protected IReadOnlyList<IHandler> Wrapped { get; }

        /// <inheritdoc />
        public override void SetContext(IHandlerContext context)
        {
            base.SetContext(context);
            foreach (var handler in Wrapped)
            {
                handler.SetContext(context);
            }
        }
    }
}