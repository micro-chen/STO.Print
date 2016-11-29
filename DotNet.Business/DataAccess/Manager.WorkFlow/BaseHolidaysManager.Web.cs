//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseHolidaysManager 
    /// 节假日表
    ///
    /// 修改记录
    ///
    ///     2012.12.24 版本：1.0 JiRiGaLa 创建主键。
    ///     
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.12.24</date>
    /// </author>
    /// </summary>
    public partial class BaseHolidaysManager : BaseManager
    {
        /// <summary>
        /// 计算截至日期为几号
        /// </summary>
        /// <param name="currentDay">当前日期</param>
        /// <param name="days">几个工作日</param>
        /// <returns>应该在几号完成 yyyy-MM-dd</returns>
        public static string CalculateDays(DateTime currentDate, int days)
        {
            // 计算有几个节假日
            string where = BaseHolidaysEntity.FieldHoliday + " >= '" + currentDate.ToString(BaseSystemInfo.DateFormat) + "'";
            BaseHolidaysManager manager = new DotNet.Business.BaseHolidaysManager();
            List<BaseHolidaysEntity> listEntity = manager.GetList<BaseHolidaysEntity>(where);
            DateTime endDay = currentDate;
            bool find = false; 
            for (int i = 0; i < days; i++)
            {
                find = false;
                endDay = endDay.AddDays(1);
                // 若这个日期是节假日，需要继续加一天
                find = listEntity.Count(entity => !string.IsNullOrEmpty(entity.Holiday) && entity.Holiday.Equals(endDay.ToString(BaseSystemInfo.DateFormat), StringComparison.OrdinalIgnoreCase)) > 0;
                while (find)
                {
                    // 若这个日期是节假日，需要继续加一天
                    endDay = endDay.AddDays(1);
                    find = listEntity.Count(entity => !string.IsNullOrEmpty(entity.Holiday) && entity.Holiday.Equals(endDay.ToString(BaseSystemInfo.DateFormat), StringComparison.OrdinalIgnoreCase)) > 0;
                }
            }
            // 计算
            return endDay.ToString(BaseSystemInfo.DateFormat);
        }

        /// <summary>
        /// 计算截至日期为几号
        /// </summary>
        /// <param name="currentDay">当前日期 yyyy-MM-dd</param>
        /// <param name="days">几个工作日</param>
        /// <returns>应该在几号完成</returns>
        public static string CalculateDays(string currentDate, int days)
        {
            DateTime dateTime = DateTime.Parse(currentDate);
            return CalculateDays(dateTime, days);
        }

        /// <summary>
        /// 前日期与指定一个日期之间的， 工作日天数对吧？
        /// </summary>
        /// <param name="currentDate">开始日期 yyyy-MM-dd</param>
        /// <param name="endDate">结束日期 yyyy-MM-dd</param>
        /// <returns>工作日天数</returns>
        public static int CalculateWorkDays(string currentDate, string endDate)
        {
            int result = 0;
            // 计算这2个日期相差几天
            DateTime from = DateTime.Parse(currentDate);
            DateTime to = DateTime.Parse(endDate);
            TimeSpan timeSpan = new TimeSpan(to.Ticks).Subtract(new TimeSpan(from.Ticks)).Duration();
            if (from <= to)
            {
                result = timeSpan.Days;
            }
            else
            {
                result = timeSpan.Days * -1;
            }
            currentDate = from.ToString("yyyy-MM-dd");
            endDate = to.ToString("yyyy-MM-dd");
            // 计算有几个节假日
            string where = BaseHolidaysEntity.FieldHoliday + " >= '" + currentDate + "'" +
                " AND " + BaseHolidaysEntity.FieldHoliday + " <= '" + endDate + "'";
            if (result < 0)
            {
                where = BaseHolidaysEntity.FieldHoliday + " >= '" + endDate + "'" +
                " AND " + BaseHolidaysEntity.FieldHoliday + " <= '" + currentDate + "'";
            }
            BaseHolidaysManager manager = new DotNet.Business.BaseHolidaysManager();
            List<BaseHolidaysEntity> listEntity = manager.GetList<BaseHolidaysEntity>(where);
            // 在数据库里找还有几个工作日
            if (result > 0)
            {
                // 还没超期的时候
                result = result - listEntity.Count;
            }
            else
            {
                // 已经超期了,把休息日去掉
                result = result + listEntity.Count;
            }
            return result;
        }

        /// <summary>
        /// 前日期与指定一个日期之间的， 工作日天数
        /// </summary>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns>返回天数</returns>
        public static int CalculateWorkDays(DateTime fromDate, DateTime toDate)
        {
            // 去掉时分秒的影响
            DateTime from = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
            DateTime to = new DateTime(toDate.Year, toDate.Month, toDate.Day);
            return CalculateWorkDays(fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));
        }
    }
}
