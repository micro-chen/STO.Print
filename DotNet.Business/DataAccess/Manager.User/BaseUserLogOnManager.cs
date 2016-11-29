//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserLogOnManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.12.08 版本：1.1 JiRiGaLa	缓存预热功能实现。
    ///     2014.03.25 版本：1.0 宋彪       增加登记用户IP，MACAddress 
    ///		2013.04.21 版本：1.1 JiRiGaLa	主键整理。
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.08</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserLogOnManager
    {
        /// <summary>
        /// 是否合法的用户
        /// 若有用户的Id，这个可以走索引，效率会很高，若没有Id会是全表扫描了。
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="openId">Key</param>
        /// <param name="systemCode">独立子系统</param>
        /// <returns>合法</returns>
        public bool ValidateOpenId(string userId, string openId, string systemCode = null)
        {
            bool result = false;

            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = string.Empty;
            }

            // 这个是独立业务系统
            if (systemCode.Equals("PDA"))
            {
                this.CurrentTableName = "PDAUserLogOn";
            }

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, userId));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, openId));
            result = DbLogic.Exists(DbHelper, this.CurrentTableName, parameters);

            return result;
        }

        /// <summary>
        /// 验证openId是否合法性
        /// </summary>
        /// <param name="openId">key</param>
        /// <param name="userId">用户主键</param>
        /// <returns>合法</returns>
        public bool ValidateOpenId(string openId, out string userId)
        {
            bool result = false;

            userId = string.Empty;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, openId));
            userId = DbLogic.GetProperty(DbHelper, this.CurrentTableName, parameters, BaseUserLogOnEntity.FieldId);
            result = !string.IsNullOrEmpty(userId);

            return result;
        }

        public string GetUserOpenId(BaseUserInfo userInfo, string cachingSystemCode = null)
        {
            string result = string.Empty;

            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterReadDbConnection))
            {
                // 需要能支持多个业务子系统的登录方法、多密码、多终端登录
                string userLogOnEntityTableName = "BaseUserLogOn";
                if (!string.IsNullOrEmpty(cachingSystemCode))
                {
                    userLogOnEntityTableName = cachingSystemCode + "UserLogOn";
                }
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, userInfo.Id));
                result = DbLogic.GetProperty(dbHelper, userLogOnEntityTableName, parameters, BaseUserLogOnEntity.FieldOpenId);
                dbHelper.Close();
            }

            return result;
        }

        #region public string GetIdByOpenId(string openId) 获取主键
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="openId">编号</param>
        /// <returns>主键</returns>
        public string GetIdByOpenId(string openId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, openId));
            return DbLogic.GetProperty(DbHelper, this.CurrentTableName, parameters, BaseUserLogOnEntity.FieldId);
        }
        #endregion

        /// <summary>
        /// 获取在线用户，客服
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public string[] GetOnLineUserIds(string[] userIds)
        {
            string[] result = null;

            string sqlQuery = "SELECT " + BaseUserLogOnEntity.FieldId
                            + "  FROM " + this.CurrentTableName
                            + " WHERE " + BaseUserLogOnEntity.FieldUserOnLine + " = 1 ";
            if (userIds != null && userIds.Length > 0)
            {
                sqlQuery += " AND " + BaseUserLogOnEntity.FieldId + " IN (" + BaseBusinessLogic.ObjectsToList(userIds) + ") ";
            }
            DataTable dt = this.DbHelper.Fill(sqlQuery);
            result = BaseBusinessLogic.FieldToArray(dt, BaseUserLogOnEntity.FieldId);

            return result;
        }

        public string CreateOpenId()
        {
            string result = string.Empty;

            if (this.UserInfo != null)
            {
                DateTime? openIdTimeout = DateTime.Now.AddHours(8);
                List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
                dbParameters.Add(this.DbHelper.MakeParameter(BaseUserLogOnEntity.FieldOpenIdTimeout, openIdTimeout));
                result = Guid.NewGuid().ToString("N");
                string sqlQuery = "UPDATE " + this.CurrentTableName
                                + "   SET " + BaseUserLogOnEntity.FieldOpenId + " = '" + result + "' "
                                + "       , " + BaseUserLogOnEntity.FieldOpenIdTimeout + " = " + this.DbHelper.GetParameter(BaseUserLogOnEntity.FieldOpenIdTimeout)
                                + " WHERE " + BaseUserLogOnEntity.FieldOpenId + " = '" + this.UserInfo.OpenId + "' ";

                if (!(this.DbHelper.ExecuteNonQuery(sqlQuery, dbParameters.ToArray()) > 0))
                {
                    result = string.Empty;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取在线人数
        /// </summary>
        public string GetOnLineCount()
        {
            string sqlQuery = "SELECT COUNT(Id) AS UserCount "
                            + "   FROM " + this.CurrentTableName
                            + "  WHERE UserOnLine = 1";
            return this.DbHelper.ExecuteScalar(sqlQuery).ToString();
        }

        public string GetLogOnCount(int days)
        {
            string sqlQuery = "SELECT COUNT(Id) AS UserCount "
                            + "   FROM " + this.CurrentTableName
                            + "  WHERE Enabled = 1 AND (DATEADD(d, " + days.ToString() + ", " + BaseUserLogOnEntity.FieldLastVisit + ") > " + this.DbHelper.GetDbNow() + ")";
            return this.DbHelper.ExecuteScalar(sqlQuery).ToString();
        }

        #region private int ResetVisitInfo(string id) 重置访问情况
        /// <summary>
        /// 重置访问情况
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        private int ResetVisitInfo(string id)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            sqlBuilder.SetNull(BaseUserLogOnEntity.FieldFirstVisit);
            sqlBuilder.SetNull(BaseUserLogOnEntity.FieldPreviousVisit);
            sqlBuilder.SetNull(BaseUserLogOnEntity.FieldLastVisit);
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldLogOnCount, 0);
            sqlBuilder.SetWhere(BaseUserLogOnEntity.FieldId, id);
            return sqlBuilder.EndUpdate();
        }
        #endregion

        #region public int ResetVisitInfo(string[] ids) 重置
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        public int ResetVisitInfo(string[] ids)
        {
            int result = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i].Length > 0)
                {
                    result += this.ResetVisitInfo(ids[i]);
                }
            }
            return result;
        }
        #endregion

        #region public int ResetVisitInfo() 重置访问情况
        /// <summary>
        /// 重置访问情况
        /// </summary>
        /// <returns>影响行数</returns>
        public int ResetVisitInfo()
        {
            int result = 0;
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            sqlBuilder.SetNull(BaseUserLogOnEntity.FieldFirstVisit);
            sqlBuilder.SetNull(BaseUserLogOnEntity.FieldPreviousVisit);
            sqlBuilder.SetNull(BaseUserLogOnEntity.FieldLastVisit);
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldLogOnCount, 0);
            result = sqlBuilder.EndUpdate();
            return result;
        }
        #endregion

        #region public int OnLine(string userId, int onLineState = 1) 用户在线
        /// <summary>
        /// 用户在线
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="onLineState">用户在线状态</param>
        /// <returns>影响行数</returns>
        public int OnLine(string userId, int onLineState = 1)
        {
            int result = 0;
            // 是否更新访问日期信息
            if (!BaseSystemInfo.UpdateVisit)
            {
                return result;
            }

#if (DEBUG)
            int milliStart = Environment.TickCount;
#endif

            string sqlQuery = string.Empty;
            // 最后一次登录时间
            sqlQuery = " UPDATE " + this.CurrentTableName
                     + "    SET " + BaseUserLogOnEntity.FieldUserOnLine + " = " + onLineState.ToString();
            // + " , " + BaseUserLogOnEntity.FieldLastVisit + " = " + this.DbHelper.GetDbNow();

            switch (this.DbHelper.CurrentDbType)
            {
                case CurrentDbType.Access:
                    sqlQuery += "  WHERE (" + BaseUserLogOnEntity.FieldId + " = " + userId + ")";
                    break;
                default:
                    sqlQuery += "  WHERE (" + BaseUserLogOnEntity.FieldId + " = '" + userId + "')";
                    break;
            }

            result += this.DbHelper.ExecuteNonQuery(sqlQuery);

            // 写入调试信息
#if (DEBUG)
            int milliEnd = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " " + " BaseUserManager.OnLine(" + userId + ")");
#endif

            return result;
        }
        #endregion


        // 5分钟不在线，表示用户离开

        #region public int CheckOnLine() 检查用户在线状态(服务器专用)
        /// <summary>
        /// 检查用户在线状态(服务器专用)
        /// </summary>
        /// <returns>影响行数</returns>
        public int CheckOnLine()
        {
            int result = 0;
            // 是否更新访问日期信息
            if (!BaseSystemInfo.UpdateVisit)
            {
                return result;
            }

#if (DEBUG)
            int milliStart = Environment.TickCount;
#endif

            string sqlQuery = string.Empty;

            // 最后一次登录时间,用户多了，需要加索引优化
            switch (this.DbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                    sqlQuery = " UPDATE " + this.CurrentTableName
                            + "     SET " + BaseUserLogOnEntity.FieldUserOnLine + " = 0 "
                            + "   WHERE ((" + BaseUserLogOnEntity.FieldUserOnLine + " > 0) AND (DATEADD (s, " + BaseSystemInfo.OnLineTime0ut.ToString() + ", " + BaseUserLogOnEntity.FieldLastVisit + ") < " + this.DbHelper.GetDbNow() + "))";
                    result += this.DbHelper.ExecuteNonQuery(sqlQuery);
                    break;
                case CurrentDbType.Oracle:
                    sqlQuery = " UPDATE " + this.CurrentTableName
                            + "     SET " + BaseUserLogOnEntity.FieldUserOnLine + " = 0 "
                            + "   WHERE " + BaseUserLogOnEntity.FieldUserOnLine + " > 0 "
                            + "         AND " + BaseUserLogOnEntity.FieldLastVisit + " < (SYSDATE - " + BaseSystemInfo.OnLineTime0ut.ToString() + " / 24 * 60 * 60 )";
                    result += this.DbHelper.ExecuteNonQuery(sqlQuery);
                    break;
                case CurrentDbType.MySql:
                    sqlQuery = " UPDATE " + this.CurrentTableName
                            + "     SET " + BaseUserLogOnEntity.FieldUserOnLine + " = 0 "
                            + "   WHERE (" + BaseUserLogOnEntity.FieldLastVisit + " IS NULL) "
                            + "      OR ((" + BaseUserLogOnEntity.FieldUserOnLine + " > 0) AND (" + BaseUserLogOnEntity.FieldLastVisit + " IS NOT NULL) AND (DATE_ADD(" + BaseUserLogOnEntity.FieldLastVisit + ", Interval " + BaseSystemInfo.OnLineTime0ut.ToString() + " SECOND) < " + this.DbHelper.GetDbNow() + "))";
                    result += this.DbHelper.ExecuteNonQuery(sqlQuery);
                    break;
                case CurrentDbType.DB2:
                    sqlQuery = " UPDATE " + this.CurrentTableName
                            + "     SET " + BaseUserLogOnEntity.FieldUserOnLine + " = 0 "
                            + "   WHERE (" + BaseUserLogOnEntity.FieldLastVisit + " IS NULL) "
                            + "      OR ((" + BaseUserLogOnEntity.FieldUserOnLine + " > 0) AND (" + BaseUserLogOnEntity.FieldLastVisit + " IS NOT NULL) AND (" + BaseUserLogOnEntity.FieldLastVisit + " + " + BaseSystemInfo.OnLineTime0ut.ToString() + " SECONDS < " + this.DbHelper.GetDbNow() + "))";
                    result += this.DbHelper.ExecuteNonQuery(sqlQuery);
                    break;
                case CurrentDbType.Access:
                    break;
            }

            // 写入调试信息
#if (DEBUG)
            int milliEnd = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " " + " BaseUserManager.CheckOnLine()");
#endif

            return result;
        }
        #endregion

        #region public bool CheckOnLineLimit()
        /// <summary>
        /// 同时在线用户数量限制
        /// </summary>
        /// <returns>是否符合限制</returns>
        public bool CheckOnLineLimit()
        {
            bool result = false;

#if (DEBUG)
            int milliStart = Environment.TickCount;
#endif

            this.CheckOnLine();

            string sqlQuery = string.Empty;
            // 最后一次登录时间
            sqlQuery = "SELECT COUNT(1) "
                     + "   FROM " + this.CurrentTableName
                     + "  WHERE " + BaseUserLogOnEntity.FieldUserOnLine + " > 0 ";
            object userOnLine = this.DbHelper.ExecuteScalar(sqlQuery);
            if (userOnLine != null)
            {
                if (BaseSystemInfo.OnLineLimit <= int.Parse(userOnLine.ToString()))
                {
                    result = true;
                }
            }

            // 写入调试信息
#if (DEBUG)
            int milliEnd = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " " + " BaseUserManager.CheckOnLineLimit()");
#endif

            return result;
        }
        #endregion

        #region public DataTable GetOnLineStateDT() 获取列表
        /// <summary>
        /// 获取在线状态列表
        /// </summary>	
        /// <returns>数据表</returns>
        public DataTable GetOnLineStateDT()
        {
            string sqlQuery = string.Empty;

            sqlQuery = "SELECT " + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldId
                              + ", " + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldUserOnLine
                              + " FROM " + this.CurrentTableName
                              + " WHERE " + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLastVisit + " IS NOT NULL ";

            switch (this.DbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                    sqlQuery += " AND (DATEADD (s, " + (BaseSystemInfo.OnLineTime0ut + 5).ToString() + ", " + BaseUserLogOnEntity.FieldLastVisit + ") > " + this.DbHelper.GetDbNow() + ")";
                    break;
            }
            return this.DbHelper.Fill(sqlQuery);
        }
        #endregion


        #region public int ChangeOnLine(string id) 登录、重新登录、扮演时的在线状态进行更新
        /// <summary>
        /// 登录、重新登录、扮演时的在线状态进行更新
        /// </summary>
        /// <param name="id">当前用户</param>
        /// <returns>是否在线</returns>
        public int ChangeOnLine(string id)
        {
            int result = 0;
            // 是自己在线，然后重新登录为别人时，需要把自己注销掉
            if (UserInfo != null && !string.IsNullOrEmpty(UserInfo.Id))
            {
                if (!string.IsNullOrEmpty(UserInfo.OpenId) && !UserInfo.Id.Equals(id))
                {
                    // 要设置为下线状态，这里要判断游客状态
                    if (this.SignOut(UserInfo.Id))
                    {
                        result += 1;
                    }
                }
            }
            // 用户在线
            result += this.OnLine(id);

            return result;
        }
        #endregion
    }
}