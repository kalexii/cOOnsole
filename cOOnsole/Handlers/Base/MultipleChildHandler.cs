using System.Collections.Generic;

namespace cOOnsole.Handlers.Base
{
    /// <summary>
    /// Handler that wraps multiple children and stores them in the <see cref="Children"/> property.
    /// </summary>
    /// <remarks>
    /// It's a responsibility of a derived class to populate this property.
    /// </remarks>
    public abstract class MultipleChildHandler : Handler
    {
        protected IReadOnlyList<IHandler> Children { get; init; } = default!;

        public override void SetContext(IHandlerContext context)
        {
            base.SetContext(context);
            foreach (var handler in Children)
            {
                handler.SetContext(context);
            }
        }
    }
}