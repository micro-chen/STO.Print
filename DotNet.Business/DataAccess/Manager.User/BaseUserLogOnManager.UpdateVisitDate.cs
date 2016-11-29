//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;
    using System.Diagnostics;

    /// <summary>
    /// BaseUserLogOnManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.12.08 版本：1.0 JiRiGaLa	代码进行分离。
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.08</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserLogOnManager
    {
        public void UpdateVisitDateTask(object param)
        {
            var tuple = param as Tuple<BaseUserLogOnEntity, bool, string>;
            BaseUserLogOnEntity userLogOnEntity = tuple.Item1;
            bool createOpenId = tuple.Item2;
            string openId = tuple.Item3;
            UpdateVisitDateTask(userLogOnEntity, createOpenId, openId);
        }

        // public async Task 
        public void UpdateVisitDateTask(BaseUserLogOnEntity userLogOnEntity, bool createOpenId, string openId)
        {
            int errorMark = 0;

            string sqlQuery = string.Empty;
            DateTime? openIdTimeout = DateTime.Now.AddHours(16);
            try
            {
                using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
                {
                    // 是否更新访问日期信息
                    List<IDbDataParameter> dbParameters = null;
                    // 若有一周没登录了，需要重新进行手机验证
                    bool mobileNeedValiated = false;
                    if (userLogOnEntity.PreviousVisit.HasValue || userLogOnEntity.FirstVisit.HasValue)
                    {
                        TimeSpan ts = new System.TimeSpan();
                        if (userLogOnEntity.LastVisit.HasValue)
                        {
                            ts = DateTime.Now.Subtract((DateTime)userLogOnEntity.LastVisit);
                            mobileNeedValiated = (ts.TotalDays > 7);
                        }
                        else if (userLogOnEntity.FirstVisit.HasValue)
                        {
                            ts = DateTime.Now.Subtract((DateTime)userLogOnEntity.FirstVisit);
                            mobileNeedValiated = (ts.TotalDays > 7);
                        }
                        if (mobileNeedValiated)
                        {
                            sqlQuery = " UPDATE " + BaseUserContactEntity.TableName
                                     + "    SET " + BaseUserContactEntity.FieldMobileValiated + " = 0 "
                                     + "  WHERE " + BaseUserContactEntity.FieldId + " = " + DbHelper.GetParameter(BaseUserContactEntity.FieldId)
                                     + "        AND " + BaseUserContactEntity.FieldMobileValiated + " = " + DbHelper.GetParameter(BaseUserContactEntity.FieldMobileValiated);

                            dbParameters = new List<IDbDataParameter>();
                            dbParameters.Add(DbHelper.MakeParameter(BaseUserContactEntity.FieldId, userLogOnEntity.Id));
                            dbParameters.Add(DbHelper.MakeParameter(BaseUserContactEntity.FieldMobileValiated, 1));

                            errorMark = 10;
                            dbHelper.ExecuteNonQuery(sqlQuery, dbParameters.ToArray());
                        }
                    }

                    if (BaseSystemInfo.UpdateVisit)
                    {
                        // 第一次登录时间
                        if (userLogOnEntity.FirstVisit == null)
                        {
                            sqlQuery = " UPDATE " + this.CurrentTableName
                                        + " SET " + BaseUserLogOnEntity.FieldPasswordErrorCount + " = 0 "
                                        + ", " + BaseUserLogOnEntity.FieldUserOnLine + " = 1 "
                                        + ", " + BaseUserLogOnEntity.FieldFirstVisit + " = " + dbHelper.GetDbNow()
                                        + ", " + BaseUserLogOnEntity.FieldLogOnCount + " = 1 "
                                        + ", " + BaseUserLogOnEntity.FieldSystemCode + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldSystemCode)
                                        + ", " + BaseUserLogOnEntity.FieldIPAddress + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldIPAddress)
                                        + ", " + BaseUserLogOnEntity.FieldIPAddressName + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldIPAddressName)
                                        + ", " + BaseUserLogOnEntity.FieldMACAddress + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldMACAddress)
                                        + ", " + BaseUserLogOnEntity.FieldComputerName + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldComputerName);

                            dbParameters = new List<IDbDataParameter>();
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldSystemCode, userLogOnEntity.SystemCode));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldIPAddress, userLogOnEntity.IPAddress));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldIPAddressName, userLogOnEntity.IPAddressName));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldMACAddress, userLogOnEntity.MACAddress));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldComputerName, userLogOnEntity.ComputerName));

                            if (createOpenId)
                            {
                                sqlQuery += ", " + BaseUserLogOnEntity.FieldOpenId + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldOpenId);
                                sqlQuery += ", " + BaseUserLogOnEntity.FieldOpenIdTimeout + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldOpenIdTimeout);
                                dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldOpenId, openId));
                                dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldOpenIdTimeout, openIdTimeout));
                            }

                            sqlQuery += "  WHERE " + BaseUserLogOnEntity.FieldId + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldId)
                                        + "      AND " + BaseUserLogOnEntity.FieldFirstVisit + " IS NULL";
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldId, userLogOnEntity.Id));

                            errorMark = 20;
                            dbHelper.ExecuteNonQuery(sqlQuery, dbParameters.ToArray());
                        }
                        else
                        {
                            // 最后一次登录时间
                            sqlQuery = " UPDATE " + this.CurrentTableName
                                        + " SET " + BaseUserLogOnEntity.FieldPasswordErrorCount + " = 0 "
                                        + ", " + BaseUserLogOnEntity.FieldPreviousVisit + " = " + BaseUserLogOnEntity.FieldLastVisit
                                        + ", " + BaseUserLogOnEntity.FieldUserOnLine + " = 1 "
                                        + ", " + BaseUserLogOnEntity.FieldLastVisit + " = " + dbHelper.GetDbNow()
                                        + ", " + BaseUserLogOnEntity.FieldLogOnCount + " = " + BaseUserLogOnEntity.FieldLogOnCount + " + 1 "
                                        + ", " + BaseUserLogOnEntity.FieldSystemCode + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldSystemCode)
                                        + ", " + BaseUserLogOnEntity.FieldIPAddress + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldIPAddress)
                                        + ", " + BaseUserLogOnEntity.FieldIPAddressName + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldIPAddressName)
                                        + ", " + BaseUserLogOnEntity.FieldMACAddress + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldMACAddress)
                                        + ", " + BaseUserLogOnEntity.FieldComputerName + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldComputerName);

                            dbParameters = new List<IDbDataParameter>();
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldSystemCode, userLogOnEntity.SystemCode));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldIPAddress, userLogOnEntity.IPAddress));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldIPAddressName, userLogOnEntity.IPAddressName));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldMACAddress, userLogOnEntity.MACAddress));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldComputerName, userLogOnEntity.ComputerName));

                            if (createOpenId)
                            {
                                sqlQuery += ", " + BaseUserLogOnEntity.FieldOpenId + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldOpenId);
                                sqlQuery += ", " + BaseUserLogOnEntity.FieldOpenIdTimeout + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldOpenIdTimeout);
                                dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldOpenId, openId));
                                dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldOpenIdTimeout, openIdTimeout));
                            }

                            sqlQuery += "  WHERE " + BaseUserLogOnEntity.FieldId + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldId);
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldId, userLogOnEntity.Id));

                            errorMark = 30;
                            dbHelper.ExecuteNonQuery(sqlQuery, dbParameters.ToArray());
                        }
                    }
                    else
                    {
                        // 2015-12-08 吉日嘎拉，更新时参数化，提高效率 实现单点登录功能，每次都更换Guid
                        if (createOpenId)
                        {
                            sqlQuery = " UPDATE  " + this.CurrentTableName
                                     + "    SET  " + BaseUserLogOnEntity.FieldPasswordErrorCount + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldPasswordErrorCount)
                                     + "       , " + BaseUserLogOnEntity.FieldSystemCode + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldSystemCode)
                                     + "       , " + BaseUserLogOnEntity.FieldOpenId + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldOpenId)
                                     + "       , " + BaseUserLogOnEntity.FieldOpenIdTimeout + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldOpenIdTimeout)
                                     + " WHERE " + BaseUserLogOnEntity.FieldId + " = " + dbHelper.GetParameter(BaseUserLogOnEntity.FieldId);
                            // sqlQuery += " AND " + BaseUserEntity.FieldOpenId + " IS NULL ";

                            dbParameters = new List<IDbDataParameter>();
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldPasswordErrorCount, 0));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldSystemCode, userLogOnEntity.SystemCode));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldOpenId, openId));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldOpenIdTimeout, openIdTimeout));
                            dbParameters.Add(dbHelper.MakeParameter(BaseUserLogOnEntity.FieldId, userLogOnEntity.Id));

                            errorMark = 40;
                            dbHelper.ExecuteNonQuery(sqlQuery, dbParameters.ToArray());
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BaseUserLogOnManager.UpdateVisitDateTask:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "UserInfo:" + this.UserInfo.Serialize()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }
        }

        public string UpdateVisitDate(BaseUserLogOnEntity userLogOnEntity, bool createOpenId = false)
        {
            string result = string.Empty;

            // 不管要不要生成、先默认不生成，超过8个小时的再生成
            // 2016-03-18 吉日嘎拉 让强制生成新OpneId生肖。
            // createOpenId = false;

            if (userLogOnEntity.OpenIdTimeout.HasValue)
            {
                //TimeSpan timeSpan = DateTime.Now - userLogOnEntity.OpenIdTimeout.Value;
                TimeSpan timeSpan = userLogOnEntity.OpenIdTimeout.Value - DateTime.Now;
                if ((timeSpan.TotalHours) < 0)
                {
                    createOpenId = true;
                }
            }
            else
            {
                createOpenId = true;
            }
            if (createOpenId)
            {
                result = Guid.NewGuid().ToString("N");
            }
            else
            {
                result = userLogOnEntity.OpenId;
            }
            // 抛出一个线程
            UpdateVisitDateTask(userLogOnEntity, createOpenId, result);
            // new Thread(UpdateVisitDateTask).Start(new Tuple<BaseUserLogOnEntity, bool, string>(userLogOnEntity, createOpenId, result));

            return result;
        }

        #region public string UpdateVisitDate(string userId, bool createOpenId = false) 更新访问当前访问状态
        /// <summary>
        /// 更新访问当前访问状态
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="createOpenId">是否每次都产生新的OpenId</param>
        /// <returns>OpenId</returns>
        public string UpdateVisitDate(string userId, bool createOpenId = false)
        {
            BaseUserLogOnEntity userLogOnEntity = this.GetObject(userId);
            return UpdateVisitDate(userLogOnEntity, createOpenId);
        }
        #endregion
    }
}