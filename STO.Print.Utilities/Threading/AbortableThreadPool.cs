using System;
using System.Collections.Generic;
using System.Threading;

namespace STO.Print.Utilities.Threading
{
    /// <summary>
    /// 对象可以中止执行的对象，其中一个主要的线程池
    /// </summary>
    public static class AbortableThreadPool
    {        
        #region Fields
        private static readonly LinkedList<WorkItem> CallbacksList = new LinkedList<WorkItem>();
        private static readonly Dictionary<WorkItem, Thread> ThreadList = new Dictionary<WorkItem, Thread>();
        #endregion

        #region Methods
        /// <summary>
        /// 队列的执行方法。线程池线程变得可用时，该方法执行。
        /// </summary>
        /// <param name="callback">一个WaitCallback代表的方法来执行。</param>
        /// <returns>创建工作项队列中</returns>
        public static WorkItem QueueUserWorkItem(WaitCallback callback)
        {
            return QueueUserWorkItem(callback, null);
        }

        /// <summary>
        /// 队列执行的方法，并指定了一个对象，它包含数据
        /// 使用的方法。线程池线程变得可用时，该方法执行。
        /// </summary>
        /// <param name="callback">一个WaitCallback代表的方法来执行。</param>
        /// <param name="state">一个对象，它包含数据被使用的方法。</param>
        /// <returns>创建工作项队列中</returns>
        public static WorkItem QueueUserWorkItem(WaitCallback callback, object state)
        {
            WorkItem item = new WorkItem(callback, state, ExecutionContext.Capture());

            lock (CallbacksList)
            {
                CallbacksList.AddLast(item);
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(HandleItem));
            return item;
        }

        /// <summary>
        /// 线程池处理队列中的工作项目。
        /// </summary>
        /// <param name="ignored">被忽略的。</param>
        private static void HandleItem(object ignored)
        {
            WorkItem item = null;

            try
            {
                lock (CallbacksList)
                {
                    if (CallbacksList.Count > 0)
                    {
                        item = CallbacksList.First.Value;
                        CallbacksList.RemoveFirst();
                    }

                    if (item == null)
                    {
                        return;
                    }
                    ThreadList.Add(item, Thread.CurrentThread);

                }
                ExecutionContext.Run(item.Context, delegate { item.Callback(item.State); }, null);
            }
            finally
            {
                lock (CallbacksList)
                {
                    if (item != null)
                    {
                        ThreadList.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// 取消指定的队列中的工作项目。
        /// </summary>
        /// <param name="item">该项目取消线程池。</param>
        /// <param name="allowAbort">如果设置为<see langword="true"/> [允许中止].</param>
        /// <returns>项目队列的状态</returns>
        public static WorkItemStatus Cancel(WorkItem item, bool allowAbort)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            lock (CallbacksList)
            {
                LinkedListNode<WorkItem> node = CallbacksList.Find(item);

                if (node != null)
                {
                    CallbacksList.Remove(node);
                    return WorkItemStatus.Queued;
                }
                else if (ThreadList.ContainsKey(item))
                {
                    if (allowAbort)
                    {
                        ThreadList[item].Abort();
                        ThreadList.Remove(item);
                        return WorkItemStatus.Aborted;
                    }
                    else
                    {
                        return WorkItemStatus.Executing;
                    }
                }
                else
                {
                    return WorkItemStatus.Completed;
                }
            }
        }

        /// <summary>
        /// 获取指定队列的工作项的状态。
        /// </summary>
        /// <param name="item">该项目得到线程池的状态。</param>
        /// <returns>项目队列的状态</returns>
        public static WorkItemStatus GetStatus(WorkItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            lock (CallbacksList)
            {
                LinkedListNode<WorkItem> node = CallbacksList.Find(item);

                if (node != null)
                {
                    return WorkItemStatus.Queued;
                }
                else if (ThreadList.ContainsKey(item))
                {
                    return WorkItemStatus.Executing;
                }
                else
                {
                    return WorkItemStatus.Completed;
                }
            }
        }
        #endregion
    }
}
