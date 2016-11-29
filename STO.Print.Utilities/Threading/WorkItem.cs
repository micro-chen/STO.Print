using System.Threading;

namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// WorkItem that stores the supplied WaitCallback and user state object,
    /// as well as the current ExecutionContext
    /// </summary>
    public sealed class WorkItem
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:WorkItem"/> class.
        /// </summary>
        /// <param name="callback">The wc.</param>
        /// <param name="state">The state.</param>
        /// <param name="context">The CTX.</param>
        internal WorkItem(WaitCallback callback, object state, ExecutionContext context)
        {
            _Callback = callback;
            _State = state;
            _Context = context;
        }
        #endregion

        #region Fields
        private WaitCallback _Callback;
        private object _State;
        private ExecutionContext _Context;

        #endregion

        #region Properties
        /// <summary>
        /// a callback method to be executed by a thread pool thread. 
        /// </summary>
        public WaitCallback Callback { get { return _Callback; } }

        /// <summary>
        /// An object containing information to be used by the callback method. 
        /// </summary>
        public object State { get { return _State; } }

        /// <summary>
        /// the execution context for the current thread
        /// </summary>
        public ExecutionContext Context { get { return _Context; } }
        #endregion
    }
}
