namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// Execution Status of WorkItem object
    /// </summary>
    public enum WorkItemStatus
    {
        /// <summary>
        /// Item execution is completed
        /// </summary>
        Completed,

        /// <summary>
        /// Item is currently in queue for execution
        /// </summary>
        Queued,

        /// <summary>
        /// Item is currently executing
        /// </summary>
        Executing,

        /// <summary>
        /// Item execition is aborted
        /// </summary>
        Aborted
    }
}
