using System;
using System.Globalization;
using System.Threading;

namespace STO.Print.Utilities
{
    public class ThreadHelper
    {
        private static CultureInfo mainCulture;

        /// <summary>
        /// Thread name: No longer than 10 chars!!!
        /// </summary>
        /// <param name="name">TORTOISE, UPDATE, REFRESH...</param>
        public static void SetThreadName(string name)
        {
            if (Thread.CurrentThread.Name != null)
            {
                return;
            }

            Thread.CurrentThread.Name = "SVNM_" + name.PadRight(10);

            if (mainCulture != null)
            {
                Thread.CurrentThread.CurrentUICulture = mainCulture;
            }
        }

        public static void SetThreadPriority(ThreadPriority priority)
        {
            if (Thread.CurrentThread.Priority != priority)
            {
                Thread.CurrentThread.Priority = priority;
            }
        }

        public static void SetMainThreadUICulture(string cultureName)
        {
            try
            {
                LogHelper.Info(string.Format("UICulture = {0}", cultureName));

                var culture = new CultureInfo(cultureName);

                mainCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("Error setting UICulture: {0}", cultureName), ex);
            }
        }

        /// <summary>
        ///     Queues a method for execution, and specifies an object containing data to
        ///     be used by the method. The method executes when a thread pool thread becomes
        ///     available.
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="threadName">TORTOISE, UPDATE, REFRESH...</param>
        /// <returns></returns>
        public static bool Queue(WaitCallback callBack, string threadName, ThreadPriority priority)
        {
            return Queue(callBack, threadName, null, priority);
        }

        /// <summary>
        ///     Queues a method for execution, and specifies an object containing data to
        ///     be used by the method. The method executes when a thread pool thread becomes
        ///     available.
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="threadName">TORTOISE, UPDATE, REFRESH...</param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool Queue(WaitCallback callBack, string threadName, object state, ThreadPriority priority)
        {
            WaitCallback start = delegate(object _state)
            {
                SetThreadName(threadName);

                SetThreadPriority(priority);

                callBack(_state);
            };

            return ThreadPool.QueueUserWorkItem(start, state);
        }

        public static void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        public static void Sleep(TimeSpan timeOut)
        {
            Thread.Sleep(timeOut);
        }
    }
}
