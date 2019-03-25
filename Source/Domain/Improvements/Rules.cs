using Concepts;
using Concepts.Improvables;
using Concepts.Improvements;

namespace Domain.Improvements
{
    /// <summary>
    /// Indicates whether or not there is an <see cref="Improvement" /> with the specified Id
    /// </summary>
    /// <param name="id">The Id of the improvement</param>
    /// <returns>True if exists, false otherwise</returns>
    public delegate bool ImprovementExists(ImprovementId id);

    /// <summary>
    /// Indicates whether or not there is an <see cref="ImprovableId" /> with the specified Id
    /// </summary>
    /// <param name="id">The Id of the improvable</param>
    /// <returns>True if exists, false otherwise</returns>
    public delegate bool ImprovableExists(ImprovableId id);

    /// <summary>
    /// Indicates whether the improvement has been initiated
    /// </summary>
    /// <param name="id">The Id of the bounded context</param>
    /// <returns>True if an initialized bounded context, false otherwise</returns>
    public delegate bool ImprovementHasBeenInitiated(ImprovementId id);

    /// <summary>
    /// Indicates whether the improvement is complete
    /// </summary>
    /// <param name="id">The Id of the bounded context</param>
    /// <returns>True if complete, false otherwise</returns>
    public delegate bool ImprovementIsComplete(ImprovementId id);

    /// <summary>
    /// Indicates whether the improvement has failed
    /// </summary>
    /// <param name="id">The Id of the bounded context</param>
    /// <returns>True if failed, false otherwise</returns>
    public delegate bool ImprovementHasFailed(ImprovementId id);
}