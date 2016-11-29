using System;
using System.Threading;

namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// 为代表的帮助功能。
    /// </summary>
    public static  class DelegateHelper
    {
        private static WaitCallback dynamicInvoker = new WaitCallback(DynamicInvoke);

        /// <summary>
        /// 执行委托。
        /// </summary>
        /// <param name="target">我们的目标。</param>
        /// <param name="args">The args.</param>
        public static WorkItem InvokeDelegate(Delegate target, params object[] args)
        {
            return AbortableThreadPool.QueueUserWorkItem(dynamicInvoker, new TargetInfo(target, args));
        }

        /// <summary>
        /// Executes the delegate.
        /// </summary>
        /// <param name="target">The target.</param>
        public static WorkItem InvokeDelegate(Delegate target)
        {
            return AbortableThreadPool.QueueUserWorkItem(dynamicInvoker, new TargetInfo(target, null));
        }

        /// <summary>
        /// Aborts the specified Queue delegate..
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>Status of abort operation on item queue</returns>
        public static WorkItemStatus AbortDelegate(WorkItem target)
        {
            return AbortableThreadPool.Cancel(target, true);
        }

        /// <summary>
        /// Dynamically invoke the delegate.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private static void DynamicInvoke(object obj)
        {
            TargetInfo ti = (TargetInfo)obj;
            ti.Target.DynamicInvoke((object[])ti.Arguments);
        }     
    }
}
