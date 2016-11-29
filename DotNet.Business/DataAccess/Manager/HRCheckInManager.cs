//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2012 , Hairihan TECH, Ltd .
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;
    using DotNet.Business;

    /// <summary>
    /// HRCheckInManager
    /// 上班签到 业务
    ///
    /// 修改记录
    ///
    ///		2014-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///		2015-08-05 版本：2.0 SongBiao 增加夜晚上班考勤。
    ///		
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014-07-15</date>
    /// </author>
    /// </summary>
    public partial class HRCheckInManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 获取用户每月考勤信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yearMonth"></param>
        /// <param name="blank"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetUserMonthCheckIn(string userId, string yearMonth, bool blank = true, string startTime = "", string endTime = "")
        {
            //            string commandText = @"SELECT Id, CompanyId, CompanyName,DepartmentId, DepartmentName, UserId, UserName, CheckInDay
            //                                    , CONVERT(varchar(5), AMStartTime, 108) as AMStartTime
            //                                    , AMStartIp
            //                                    , CONVERT(varchar(5), AMEndTime, 108) as AMEndTime
            //                                    , AMEndIp
            //                                    , CONVERT(varchar(5), PMStartTime, 108) as PMStartTime
            //                                    , PMStartIp
            //                                    , CONVERT(varchar(5), PMEndTime, 108) as PMEndTime
            //                                    , PMEndIp
            //                                    , CONVERT(varchar(5), NightStartTime, 108) as NightStartTime
            //                                    , NightStartIp
            //                                    , CONVERT(varchar(5), NightEndTime, 108) as NightEndTime
            //                                    , NightEndIp
            //                                    , Description
            //                                    FROM " + this.CurrentTableName
            //                               + " WHERE UserId = '" + userId + "' AND CheckInDay LIKE '" + yearMonth + "%'"
            //                            + " ORDER BY CheckInDay";
            string commandText = @"SELECT Id, CompanyId, CompanyName,DepartmentId, DepartmentName, UserId, UserName, CheckInDay
                                    , TO_CHAR(AMStartTime, 'hh24:mi') AMSTARTTIME
                                    , AMStartIp
                                    , TO_CHAR(AMEndTime, 'hh24:mi') AMENDTIME
                                    , AMEndIp
                                    , TO_CHAR(PMStartTime, 'hh24:mi') PMSTARTTIME
                                    , PMStartIp
                                    , TO_CHAR(PMEndTime, 'hh24:mi') PMENDTIME
                                    , PMEndIp
                                    , TO_CHAR(NightStartTime, 'hh24:mi') NIGHTSTARTTIME
                                    , NightStartIp
                                    , TO_CHAR(NightEndTime, 'hh24:mi') NIGHTENDTIME
                                    , NightEndIp
                                    , Description
                                    FROM " + this.CurrentTableName
                                 + " WHERE UserId = '" + userId + "' AND CheckInDay LIKE '" + yearMonth + "%'";
            if (!string.IsNullOrEmpty(startTime))
            {
                commandText += "AND to_date(CheckInDay,'yyyy-mm-dd') "
                               + "      BETWEEN TO_DATE('" + startTime + "','yyyy-mm-dd')"
                               + "    AND TO_DATE('" + endTime + "','yyyy-mm-dd')";
            }
            commandText += " ORDER BY CheckInDay";

            DataTable result = this.DbHelper.Fill(commandText);
            result.TableName = this.CurrentTableName;
            result.Columns.Add("Week");
            if (blank)
            {
                DateTime dtStart = new DateTime(int.Parse(yearMonth.Substring(0, 4)), int.Parse(yearMonth.Substring(5, 2)), 1);
                DateTime dtEnd = dtStart;
                while (dtEnd.Month == dtStart.Month)
                {
                    string day = dtEnd.ToString(BaseSystemInfo.DateFormat);
                    if (!BaseBusinessLogic.Exists(result, "CheckInDay", day))
                    {
                        if (!string.IsNullOrEmpty(startTime))
                        {
                            DateTime startDateTime = DateTime.Parse(startTime);
                            DateTime endDateTime = DateTime.Parse(endTime);
                            if (endDateTime >= dtEnd && dtEnd >= startDateTime)
                            {
                                DataRow dr = result.NewRow();
                                dr["CheckInDay"] = day;
                                result.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = result.NewRow();
                            dr["CheckInDay"] = day;
                            result.Rows.Add(dr);
                        }
                    }
                    dtEnd = dtEnd.AddDays(1);
                }
                foreach (DataRow dr in result.Rows)
                {
                    dtEnd = DateTime.Parse(dr["CheckInDay"].ToString());
                    dr["Week"] = DateUtil.GetDayOfWeek(dtEnd.DayOfWeek.ToString(), true).ToString();
                }
                result.DefaultView.Sort = "CheckInDay";
                result.AcceptChanges();
            }
            return result;
        }
        /// <summary>
        /// 获取用户每日考勤信息
        /// </summary>
        /// <param name="checkInDay"></param>
        /// <param name="blank"></param>
        /// <returns></returns>
        public DataTable GetUserDayCheckIn(string checkInDay, bool blank = true)
        {
            string commandText = @"SELECT HRCheckIn.Id, BaseUser.Id AS UserId, BaseUser.CompanyId, BaseUser.CompanyName,
                            BaseUser.DepartmentId, BaseUser.DepartmentName, BaseUser.Realname, BaseUser.SortCode
                            , HRCheckIn.CheckInDay
                            , CONVERT(varchar(5), HRCheckIn.AMStartTime, 108) as AMStartTime
                            , HRCheckIn.AMStartIp
                            , CONVERT(varchar(5), HRCheckIn.AMEndTime, 108) as AMEndTime
                            , HRCheckIn.AMEndIp
                            , CONVERT(varchar(5), HRCheckIn.PMStartTime, 108) as PMStartTime
                            , HRCheckIn.PMStartIp
                            , CONVERT(varchar(5), HRCheckIn.PMEndTime, 108) as PMEndTime
                            , HRCheckIn.PMEndIp
                            , CONVERT(varchar(5), HRCheckIn.NightStartTime, 108) as NightStartTime
                            , HRCheckIn.NightStartIp
                            , CONVERT(varchar(5), HRCheckIn.NightEndTime, 108) as NightEndTime
                            , HRCheckIn.NightEndIp
                            , HRCheckIn.Description
                            FROM  BaseUser LEFT OUTER JOIN
                              (SELECT *
                              FROM HRCheckIn 
                              WHERE HRCheckIn.CheckInDay = '" + checkInDay + @"'
                              AND HRCheckIn.CompanyId = '" + this.UserInfo.CompanyId + @"') HRCheckIn
                              ON BaseUser.Id = HRCheckIn.UserId
                                WHERE BaseUser.DeletionStateCode = 0 
                                AND BaseUser.DepartmentName != '后勤保障'
                                AND BaseUser.Enabled = 1 
                                AND BaseUser.IsVisible = 1 
                                AND (BaseUser.CompanyId = '" + this.UserInfo.CompanyId + @"') 
                                ORDER BY BaseUser.SortCode";
            DataTable result = this.DbHelper.Fill(commandText);
            result.TableName = this.CurrentTableName;
            result.Columns.Add("Week");
            if (blank)
            {
                DateTime dtCheckInDay = new DateTime();
                foreach (DataRow dr in result.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["CheckInDay"].ToString()))
                    {
                        dtCheckInDay = DateTime.Parse(dr["CheckInDay"].ToString());
                        dr["Week"] = DateUtil.GetDayOfWeek(dtCheckInDay.DayOfWeek.ToString(), true).ToString();
                    }
                }
                result.AcceptChanges();
            }
            return result;
        }
        /// <summary>
        /// 上午上班考勤
        /// </summary>
        /// <returns></returns>
        public HRCheckInEntity AMStart()
        {
            HRCheckInEntity result = null;
            string checkInDay = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldCheckInDay, checkInDay));
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldUserId, this.UserInfo.Id));
            DataTable dt = this.GetDataTable(parameters);
            if (dt.Rows.Count == 0)
            {
                result = new HRCheckInEntity();
                result.CheckInDay = checkInDay;
                result.UserId = this.UserInfo.Id;
                result.UserName = this.UserInfo.RealName;
                result.CompanyId = this.UserInfo.CompanyId;
                result.CompanyName = this.UserInfo.CompanyName;
                result.DepartmentId = this.UserInfo.DepartmentId;
                result.DepartmentName = this.UserInfo.DepartmentName;
                result.AMStartTime = DateTime.Now;
                result.AMStartIp = this.UserInfo.IPAddress;
                this.Add(result, true, false);
            }
            else
            {
                result = new HRCheckInEntity().GetSingle(dt);
                result.AMStartTime = DateTime.Now;
                result.AMStartIp = this.UserInfo.IPAddress;
                this.UpdateObject(result);
            }
            return result;
        }
        /// <summary>
        /// 上午下班考勤
        /// </summary>
        /// <returns></returns>
        public HRCheckInEntity AMEnd()
        {
            HRCheckInEntity result = null;
            string checkInDay = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldCheckInDay, checkInDay));
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldUserId, this.UserInfo.Id));
            DataTable dt = this.GetDataTable(parameters);
            if (dt.Rows.Count == 0)
            {
                result = new HRCheckInEntity();
                result.CheckInDay = checkInDay;
                result.UserId = this.UserInfo.Id;
                result.UserName = this.UserInfo.RealName;
                result.CompanyId = this.UserInfo.CompanyId;
                result.CompanyName = this.UserInfo.CompanyName;
                result.DepartmentId = this.UserInfo.DepartmentId;
                result.DepartmentName = this.UserInfo.DepartmentName;
                result.AMEndTime = DateTime.Now;
                result.AMEndIp = this.UserInfo.IPAddress;
                this.Add(result, true, false);
            }
            else
            {
                result = new HRCheckInEntity().GetSingle(dt);
                result.AMEndTime = DateTime.Now;
                result.AMEndIp = this.UserInfo.IPAddress;
                this.UpdateObject(result);
            }
            return result;
        }
        /// <summary>
        /// 下午上班考勤
        /// </summary>
        /// <returns></returns>
        public HRCheckInEntity PMStart()
        {
            HRCheckInEntity result = null;
            string checkInDay = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldCheckInDay, checkInDay));
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldUserId, this.UserInfo.Id));
            DataTable dt = this.GetDataTable(parameters);
            if (dt.Rows.Count == 0)
            {
                result = new HRCheckInEntity();
                result.CheckInDay = checkInDay;
                result.UserId = this.UserInfo.Id;
                result.UserName = this.UserInfo.RealName;
                result.CompanyId = this.UserInfo.CompanyId;
                result.CompanyName = this.UserInfo.CompanyName;
                result.DepartmentId = this.UserInfo.DepartmentId;
                result.DepartmentName = this.UserInfo.DepartmentName;
                result.PMStartTime = DateTime.Now;
                result.PMStartIp = this.UserInfo.IPAddress;
                this.Add(result, true, false);
            }
            else
            {
                result = new HRCheckInEntity().GetSingle(dt);
                result.PMStartTime = DateTime.Now;
                result.PMStartIp = this.UserInfo.IPAddress;
                this.UpdateObject(result);
            }
            return result;
        }
        /// <summary>
        /// 下午下班考勤
        /// </summary>
        /// <returns></returns>
        public HRCheckInEntity PMEnd()
        {
            HRCheckInEntity result = null;
            string checkInDay = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldCheckInDay, checkInDay));
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldUserId, this.UserInfo.Id));
            DataTable dt = this.GetDataTable(parameters);
            if (dt.Rows.Count == 0)
            {
                result = new HRCheckInEntity();
                result.CheckInDay = checkInDay;
                result.UserId = this.UserInfo.Id;
                result.UserName = this.UserInfo.RealName;
                result.CompanyId = this.UserInfo.CompanyId;
                result.CompanyName = this.UserInfo.CompanyName;
                result.DepartmentId = this.UserInfo.DepartmentId;
                result.DepartmentName = this.UserInfo.DepartmentName;
                result.PMEndTime = DateTime.Now;
                result.PMEndIp = this.UserInfo.IPAddress;
                this.Add(result, true, false);
            }
            else
            {
                result = new HRCheckInEntity().GetSingle(dt);
                result.PMEndTime = DateTime.Now;
                result.PMEndIp = this.UserInfo.IPAddress;
                this.UpdateObject(result);
            }
            return result;
        }

        /// <summary>
        /// 晚上上班考勤
        /// </summary>
        /// <returns></returns>
        public HRCheckInEntity NightStart()
        {
            HRCheckInEntity result = null;
            string checkInDay = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldCheckInDay, checkInDay));
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldUserId, this.UserInfo.Id));
            DataTable dt = this.GetDataTable(parameters);
            if (dt.Rows.Count == 0)
            {
                result = new HRCheckInEntity();
                result.CheckInDay = checkInDay;
                result.UserId = this.UserInfo.Id;
                result.UserName = this.UserInfo.RealName;
                result.CompanyId = this.UserInfo.CompanyId;
                result.CompanyName = this.UserInfo.CompanyName;
                result.DepartmentId = this.UserInfo.DepartmentId;
                result.DepartmentName = this.UserInfo.DepartmentName;
                result.NightStartTime = DateTime.Now;
                result.NightStartIp = this.UserInfo.IPAddress;
                this.Add(result, true, false);
            }
            else
            {
                result = new HRCheckInEntity().GetSingle(dt);
                result.NightStartTime = DateTime.Now;
                result.NightStartIp = this.UserInfo.IPAddress;
                this.UpdateObject(result);
            }
            return result;
        }
        /// <summary>
        /// 晚上下班考勤
        /// </summary>
        /// <returns></returns>
        public HRCheckInEntity NightEnd()
        {
            HRCheckInEntity result = null;
            string checkInDay = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldCheckInDay, checkInDay));
            parameters.Add(new KeyValuePair<string, object>(HRCheckInEntity.FieldUserId, this.UserInfo.Id));
            DataTable dt = this.GetDataTable(parameters);
            if (dt.Rows.Count == 0)
            {
                result = new HRCheckInEntity();
                result.CheckInDay = checkInDay;
                result.UserId = this.UserInfo.Id;
                result.UserName = this.UserInfo.RealName;
                result.CompanyId = this.UserInfo.CompanyId;
                result.CompanyName = this.UserInfo.CompanyName;
                result.DepartmentId = this.UserInfo.DepartmentId;
                result.DepartmentName = this.UserInfo.DepartmentName;
                result.NightEndTime = DateTime.Now;
                result.NightEndIp = this.UserInfo.IPAddress;
                this.Add(result, true, false);
            }
            else
            {
                result = new HRCheckInEntity().GetSingle(dt);
                result.NightEndTime = DateTime.Now;
                result.NightEndIp = this.UserInfo.IPAddress;
                this.UpdateObject(result);
            }
            return result;
        }


    }
}