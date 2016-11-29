namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// State of Object Implementing <see cref="T:System.IDisposable"/> Interface
    /// </summary>
    /// <remarks>Used to Store Disposing State Of Object.</remarks>
    public enum DisposeState
    {
        /// <summary>
        /// Object is Fully Initialized.
        /// </summary>
        None = 0,

        /// <summary>
        /// Object is Trying to be Dispose.
        /// </summary>
        Disposing = 1,

        /// <summary>
        /// Object is Fully Disposed
        /// </summary>
        Disposed = 2
    }
}
