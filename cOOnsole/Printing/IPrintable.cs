namespace cOOnsole.Printing
{
    /// <summary>Represents an entity capable of printing itself into a string that will be read by the user.</summary>
    /// <seealso cref="IPrinter" />
    public interface IPrintable
    {
        /// <summary>
        /// Method that gets called whenever this instance of <see cref="IPrintable"/> is requested to print itself.
        /// </summary>
        /// <param name="printer">The printer to use.</param>
        void PrintSelf(IPrinter printer);
    }
}