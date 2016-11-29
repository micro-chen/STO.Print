//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Reflection;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// ServiceInfo
    /// 当前操作类
    /// 
    /// 修改记录
    /// 
    ///		2016.02.16 版本：1.0 JiRiGaLa 整理文件、完善日志记录功能。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.16</date>
    /// </author> 
    /// </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime = DateTime.Now;

        /// <summary>
        /// 任务唯一标识
        /// </summary>
        public string TaskId = string.Empty;

        /// <summary>
        /// 方法运行耗时
        /// </summary>
        public long ElapsedTicks = 0;

        private bool recordLog = false;
        /// <summary>
        /// 是否记录操作日志
        /// </summary>
        public bool RecordLog
        {
            set { recordLog = value; }
            get { return recordLog; }
        }

        private BaseUserInfo userInfo = null;

        public BaseUserInfo UserInfo
        {
            set { userInfo = value; }
            get { return userInfo; }
        }

        private MethodBase currentMethod = null;

        public MethodBase CurrentMethod
        {
            set { currentMethod = value; }
            get { return currentMethod; }
        }

        private ServiceInfo(BaseUserInfo userInfo, MethodBase currentMethod)
        {
            this.userInfo = userInfo;
            this.currentMethod = currentMethod;
        }

        private ServiceInfo(MethodBase currentMethod)
        {
            this.currentMethod = currentMethod;
        }

        public static ServiceInfo Create(BaseUserInfo userInfo, MethodBase currentMethod)
        {
            return new ServiceInfo(userInfo, currentMethod) { RecordLog = false, StartTime = DateTime.Now };
        }

        public static ServiceInfo Create(string taskId, MethodBase currentMethod)
        {
            return new ServiceInfo(currentMethod) { RecordLog = false, TaskId = taskId, StartTime = DateTime.Now };
        }

        public static ServiceInfo Create(string taskId, BaseUserInfo userInfo, MethodBase currentMethod)
        {
            return new ServiceInfo(userInfo, currentMethod) { RecordLog = false, TaskId = taskId, StartTime = DateTime.Now };
        }
    }
}
