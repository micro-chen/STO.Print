//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Reflection;

namespace DotNet.Business
{
    using DotNet.Utilities;
    
    /// <summary>
    /// ServiceUtil
	/// 使用独创的委托加匿名函数对传统的模板模式作出了新的诠释
	///	
	/// <author>
	///		<name>张祈璟</name>
	///		<date>2013.06.05</date>
	/// </author> 
	/// </summary>
	public class ServiceUtil
	{
		public delegate void ProcessFun(IDbHelper dbHelper);

		public delegate bool ProcessFunWithLock(IDbHelper dbHelper, bool getOnLine);

        public static void ProcessUserCenterReadDb(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.UserCenterRead);
            }
		}

        public static void ProcessUserCenterWriteDb(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.UserCenterWrite);
            }
		}

        public static void ProcessUserCenterDb(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.UserCenter);
            }
		}

        public static void ProcessUserCenterWriteDbWithLock(BaseUserInfo userInfo, ServiceInfo parameter, object locker, ProcessFunWithLock fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                int milliStart = Begin(parameter.UserInfo, parameter.CurrentMethod);

                bool getOnLine = false;

                lock (locker)
                {
                    using (IDbHelper dbHelper = DbHelperFactory.GetHelper(GetDbType(DbType.UserCenterWrite)))
                    {
                        try
                        {
                            dbHelper.Open(GetDbConnection(DbType.UserCenterWrite));
                            getOnLine = fun(dbHelper, getOnLine);
                            AddLog(parameter);
                        }
                        catch (Exception ex)
                        {
                            BaseExceptionManager.LogException(dbHelper, parameter.UserInfo, ex);
                            throw;
                        }
                        finally
                        {
                            dbHelper.Close();
                        }
                    }
                }
                End(parameter.UserInfo, milliStart, parameter.CurrentMethod, getOnLine);
            }
		}

        public static void ProcessUserCenterWriteDbWithLock(BaseUserInfo userInfo, ServiceInfo parameter, object locker, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                int milliStart = Begin(parameter.UserInfo, parameter.CurrentMethod);
                lock (locker)
                {
                    ProcessDbHelp(parameter, fun, DbType.UserCenterWrite, false);
                }
                End(parameter.UserInfo, milliStart, parameter.CurrentMethod);
            }
		}

        public static void ProcessUserCenterWriteDbWithTransaction(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.UserCenterWrite, true);
            }
		}

        public static void ProcessBusinessDb(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.Business);
            }
		}

        public static void ProcessLoginLogDb(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.LoginLog);
            }
		}

        public static void ProcessMessageDb(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.Message);
            }
		}

        public static void ProcessWorkFlowDb(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.WorkFlow);
            }
		}

        public static void ProcessWorkFlowDbWithTransaction(BaseUserInfo userInfo, ServiceInfo parameter, ProcessFun fun)
		{
            if (BaseSystemInfo.IsAuthorized(userInfo))
            {
                ProcessDb(parameter, fun, DbType.WorkFlow, true);
            }
		}

		/// <summary>
		/// 使用简单的工厂方法，可以做成多态的类
		/// </summary>
		private enum DbType
		{
			UserCenterRead = 1,
			UserCenterWrite = 2,
			Business = 3,
			Message = 4,
			WorkFlow = 5,
			UserCenter = 6,
            LoginLog = 7
		}

		private static CurrentDbType GetDbType(DbType dbType)
		{
			switch(dbType)
			{
				default:
				case DbType.UserCenterRead:
				case DbType.UserCenterWrite:
				case DbType.UserCenter:
					return BaseSystemInfo.UserCenterDbType;
				case DbType.Business:
					return BaseSystemInfo.BusinessDbType;
				case DbType.Message:
					return BaseSystemInfo.MessageDbType;
				case DbType.WorkFlow:
					return BaseSystemInfo.WorkFlowDbType;
                case DbType.LoginLog:
                    return BaseSystemInfo.LoginLogDbType;
			}
		}

		private static string GetDbConnection(DbType dbType)
		{
			switch(dbType)
			{
				default:
				case DbType.UserCenterRead:
					return BaseSystemInfo.UserCenterReadDbConnection;
				case DbType.UserCenterWrite:
					return BaseSystemInfo.UserCenterWriteDbConnection;
				case DbType.UserCenter:
					return BaseSystemInfo.UserCenterDbConnection;
				case DbType.Business:
					return BaseSystemInfo.BusinessDbConnection;
				case DbType.Message:
					return BaseSystemInfo.MessageDbConnection;
				case DbType.WorkFlow:
					return BaseSystemInfo.WorkFlowDbConnection;
                case DbType.LoginLog:
                    return BaseSystemInfo.LoginLogDbConnection;
			}
		}

        private static void ProcessDb(ServiceInfo parameter, ProcessFun fun, DbType dbType, bool inTransaction = false)
		{
            int milliStart = Begin(parameter.UserInfo, parameter.CurrentMethod);

			ProcessDbHelp(parameter, fun, dbType, inTransaction);

            End(parameter.UserInfo, milliStart, parameter.CurrentMethod);
		}

        private static void ProcessDbHelp(ServiceInfo serviceInfo, ProcessFun processFun, DbType dbType, bool inTransaction)
		{
            // 2016-02-14 吉日嘎拉 增加耗时记录功能
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using(IDbHelper dbHelper = DbHelperFactory.GetHelper(GetDbType(dbType), GetDbConnection(dbType)))
			{
				try
                {
                    // dbHelper.Open(GetDbConnection(dbType));
                    if (inTransaction)
                    {
                        // dbHelper.BeginTransaction();
                    }
                    processFun(dbHelper);
                    stopwatch.Stop();
                    serviceInfo.ElapsedTicks = stopwatch.ElapsedTicks;
                    AddLog(serviceInfo);
                    if (inTransaction)
                    {
                        // dbHelper.CommitTransaction();
                    }
                }
				catch(Exception ex)
				{
                    if (inTransaction)
                    {
                        // dbHelper.RollbackTransaction();
                    }
                    BaseExceptionManager.LogException(dbHelper, serviceInfo.UserInfo, ex);
					throw;
				}
				finally
				{
					// dbHelper.Close();
				}
			}
		}

        /// <summary>
        /// 2016-02-14 吉日嘎拉 增加服务器调用耗时统计功能。
        /// </summary>
        /// <param name="serviceInfo"></param>
        private static void AddLog(ServiceInfo serviceInfo)
		{
            if (serviceInfo.RecordLog)
			{
                // 若用户信息没有，就获取现在的用户信息
                if (serviceInfo.UserInfo == null)
                {
                    serviceInfo.UserInfo = BaseSystemInfo.UserInfo;
                }

                // 本地直接写入数据库
                BaseLogManager.AddLog(serviceInfo);
			}
		}

		private static int Begin(BaseUserInfo userInfo, MethodBase currentMethod)
		{
			int milliStart = 0;

			// 写入调试信息
			#if (DEBUG)
                milliStart = BaseBusinessLogic.StartDebug(userInfo, currentMethod);
			#endif

            milliStart = BaseBusinessLogic.StartDebug(userInfo, currentMethod);

			return milliStart;
		}

        private static void End(BaseUserInfo userInfo, int milliStart, MethodBase currentMethod)
		{
			// 写入调试信息
			#if (DEBUG)
				BaseBusinessLogic.EndDebug(userInfo, currentMethod, milliStart);
			#endif
		}

        private static void End(BaseUserInfo userInfo, int milliStart, MethodBase currentMethod, bool getOnLine)
		{
			// 写入调试信息
			#if (DEBUG)
			if(getOnLine)
			{
				BaseBusinessLogic.EndDebug(userInfo, currentMethod, milliStart);
			}
			#endif
		}
	}
}
