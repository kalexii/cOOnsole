using JetBrains.Annotations;

namespace ConsoleAppFramework.Description
{
    /// <summary>
    /// Visitor that should be accepted by <see cref="IReaction.Describe"/>.
    /// </summary>
    public interface IDescriptionVisitor
    {
        /// <summary>
        /// Writes header, a short bit of important information.
        /// </summary>
        /// <param name="header">Short bit of important information.</param>
        void WriteHeader([NotNull] string header);

        /// <summary>
        /// Writes content, a longer bit of important information.
        /// </summary>
        /// <param name="content">A longer bit of important information.</param>
        void WriteContent([NotNull] string content);
    }
}