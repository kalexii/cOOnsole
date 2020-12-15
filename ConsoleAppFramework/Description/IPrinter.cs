namespace ConsoleAppFramework.Description
{
    /// <summary>
    /// This interface provides the access to the output stream with a handful of utility methods.
    /// </summary>
    public interface IPrinter
    {
        /// <summary>
        /// Increases the indentation level.
        /// </summary>
        IPrinter Indent();

        /// <summary>
        /// Decreases the indentation level.
        /// </summary>
        IPrinter Unindent();

        /// <summary>
        /// Prints a string according to the current indentation level.
        /// </summary>
        IPrinter Print(string value);

        /// <summary>
        /// Outputs new line.
        /// </summary>
        IPrinter NewLine();

        IPrinter Flush();
    }
}