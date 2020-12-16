using cOOnsole.Description;

namespace cOOnsole
{
    public interface IPrintable
    {
        /// <summary>
        /// Method that gets called whenever help message is generated.
        /// </summary>
        /// <param name="printer">The printer.</param>
        void PrintSelf(IPrinter printer);
    }
}