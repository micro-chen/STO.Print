using System.Collections.Generic;
using System.Threading;

namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// 线程池辅助操作类
    /// </summary>
    public class ThreadPoolHelper
    {
        /// <summary>
        /// The delegate for explicit method.
        /// </summary>
        public delegate void WaitCallbackNew();

        /// <summary>
        /// The WaitCallback delegate object in actual of our thread pool.
        /// </summary>
        /// <param name="state"></param>
        static void Callback(object state)
        {
            WaitCallbackHelper wcbh = (state as WaitCallbackHelper);
            wcbh.Callback();
            (wcbh.WaitHandle as AutoResetEvent).Set();
        }

        /// <summary>
        /// Queues a method for execution.
        /// The method executes when a thread pool thread becomes available.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public static bool QueueUserWorkItem(WaitCallbackNew callback)
        {
            WaitCallbackHelper wcbh = new WaitCallbackHelper();
            wcbh.Callback = callback;
            wcbh.WaitHandle = new AutoResetEvent(false);
            if (AutoResetEvents == null)
            {
                AutoResetEvents = new List<WaitHandle>();
            }
            AutoResetEvents.Add(wcbh.WaitHandle);

            return ThreadPool.QueueUserWorkItem(new WaitCallback(Callback), wcbh);
        }

        /// <summary>
        /// Queues a few method for execution.
        /// The method executes when a thread pool thread becomes available.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public static bool QueueUserWorkItems(params WaitCallbackNew[] proc)
        {
            bool result = true;
            foreach (WaitCallbackNew tp in proc)
            {
                result &= QueueUserWorkItem(tp);
            }
            return result;
        }

        /// <summary>
        /// (Collection)Notifies a waiting thread that an event has occurred. This class cannot be inherited.
        /// </summary>
        private static List<WaitHandle> AutoResetEvents;

        /// <summary>
        /// Waits for all the elements in the specified array to receive a signal.
        /// </summary>
        /// <returns>Waits for all the elements in the specified array to receive a signal.</returns>
        public static bool WaitAll()
        {
            return WaitHandle.WaitAll(AutoResetEvents.ToArray());
        }

        /// <summary>
        /// Waits for any of the elements in the specified array to receive a signal.
        /// </summary>
        /// <returns>The array index of the object that satisfied the wait.</returns>
        public static int WaitAny()
        {
            return WaitHandle.WaitAny(AutoResetEvents.ToArray());
        }

        /// <summary>
        /// A callback container with a WaitHandle.
        /// </summary>
        class WaitCallbackHelper
        {
            private WaitCallbackNew callback;
            /// <summary>
            ///
            /// </summary>
            public WaitCallbackNew Callback
            {
                get
                {
                    return this.callback;
                }
                set
                {
                    this.callback = value;
                }
            }

            private WaitHandle waitHandle;
            public WaitHandle WaitHandle
            {
                get
                {
                    return this.waitHandle;
                }
                set
                {
                    this.waitHandle = value;
                }
            }
        }
    }
}
