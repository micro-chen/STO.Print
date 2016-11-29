//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Data;

namespace DotNet.Utilities
{
    /// <summary>
    ///	BaseSortLogic
    /// 通用排序逻辑基类（程序OK）
    /// 
    /// 修改记录
    /// 
    ///		2010.10.09 版本：   1.4 JiRiGaLa 更新函数名为*Id。
    ///		2007.12.10 版本：   1.3 JiRiGaLa 改进 序列产生码的长度问题。
    ///		2007.11.01 版本：   1.2 JiRiGaLa 改进 BUOperatorInfo 去掉这个变量，可以提高性能，提高速度，基类的又一次飞跃。
    ///		2007.03.01 版本：   1.0 JiRiGaLa 将主键从 BUBaseBusinessLogic 类分离出来。
    ///	
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.12.10</date>
    /// </author> 
    /// </summary>
    public class BaseSortLogic
    {
        //
        // 排序操作在内存中的运算方式定义
        //

        #region public static string GetNextId(DataTable dt, string id) 获取下一条记录主键
        /// <summary>
        /// 获取下一条记录主键
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="tableName">当前表</param>
        /// <param name="id">当前主键</param>
        /// <returns>主键</returns>
        public static string GetNextId(DataTable dt, string id)
        {
            return GetNextId(dt.DefaultView, id, BaseBusinessLogic.FieldId);
        }
        #endregion

        #region  public static string GetNextId(DataView dataView, string id) 获取下一条记录主键
        /// <summary>
        /// 获取下一条记录主键
       /// </summary>
       /// <param name="dataView"></param>
       /// <param name="id"></param>
       /// <returns></returns>
        public static string GetNextId(DataView dataView, string id)
        {
            return GetNextId(dataView, id, BaseBusinessLogic.FieldId);
        }
        public static string GetNextIdDyn(dynamic lstT, string id)
        {
            return GetNextIdDyn(lstT, id, BaseBusinessLogic.FieldId);
        }
        public static string GetNextIdDyn(dynamic lstT, string id, string field)
        {
            string result = string.Empty;
            bool find = false;
            foreach (dynamic t in lstT)
            {
                if (find)
                {
                    result = ReflectionUtil.GetProperty(t,field).ToString();
                    break;
                }
                if (ReflectionUtil.GetProperty(t, field).ToString().Equals(id))
                {
                    find = true;
                }
            }
            return result;
        }
        #endregion

        #region public static string GetNextId(DataTable dt, string id, string field) 获取下一条记录主键
        /// <summary>
        /// 获取下一条记录主键
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="tableName">当前表</param>
        /// <param name="id">当前主键Id</param>
        /// <param name="field">当前字段</param>
        /// <returns>主键</returns>
        public static string GetNextId(DataTable dt, string id, string field)
        {
            return GetNextId(dt.DefaultView, id, field);
        }
        #endregion

        #region public static string GetNextId(DataView dataView, string id, string field) 获取下一条记录 具体方法
        /// <summary>
        /// 获取下一条记录 具体方法
       /// </summary>
       /// <param name="dataView"></param>
       /// <param name="id"></param>
       /// <param name="field"></param>
       /// <returns></returns>
        public static string GetNextId(DataView dataView, string id, string field)
        {
            string result = string.Empty;
            bool find = false;
            foreach (DataRowView dataRow in dataView)
            {
                if (find)
                {
                    result = dataRow[field].ToString();
                    break;
                }
                if (dataRow[field].ToString().Equals(id))
                {
                    find = true;
                }
            }
            return result;
        }
        #endregion



        #region public static string GetPreviousId(DataTable dt, string id) 获取上一条记录主键
        /// <summary>
        /// 获取上一条记录主键
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="tableName">当前表</param>
        /// <param name="id">当前主键</param>
        /// <returns>主键</returns>
        public static string GetPreviousId(DataTable dt, string id)
        {
            return GetPreviousId(dt.DefaultView, id, BaseBusinessLogic.FieldId);
        }
        #endregion

        #region  public static string GetPreviousId(DataView dataView, string id) 获取上一条记录主键
        /// <summary>
        /// 获取上一条记录主键
       /// </summary>
       /// <param name="dataView"></param>
       /// <param name="id"></param>
       /// <returns></returns>
        public static string GetPreviousId(DataView dataView, string id)
        {
            return GetPreviousId(dataView, id, BaseBusinessLogic.FieldId);
        }
        #endregion

        #region public static string GetPreviousId(DataTable dt, string id) 获取上一条记录主键
        /// <summary>
        /// 获取上一条记录主键
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="tableName">当前表</param>
        /// <param name="id">当前主键</param>
        /// <param name="field">当前字段</param>
        /// <returns>主键</returns>
        public static string GetPreviousId(DataTable dt, string id, string field)
        {
            return GetPreviousId(dt.DefaultView, id, field);
        }
        #endregion

        #region  public static string GetPreviousId(DataView dataView, string id, string field) 获取上一条记录主键 具体方法
        /// <summary>
        ///  获取上一条记录主键 具体方法
       /// </summary>
       /// <param name="dataView"></param>
       /// <param name="id"></param>
       /// <param name="field"></param>
       /// <returns></returns>
        public static string GetPreviousId(DataView dataView, string id, string field)
        {
            string result = string.Empty;
            foreach (DataRowView dataRow in dataView)
            {
                if (dataRow[field].ToString().Equals(id))
                {
                    break;
                }
                result = dataRow[field].ToString();
            }
            return result;
        }
        #endregion



        #region public static int Swap(DataTable dt, string id, string targetId) 两条记录交换排序顺序
        /// <summary>
        /// 两条记录交换排序顺序
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="id">要移动的记录主键</param>
        /// <param name="targetId">目标记录主键</param>
        /// <returns>影响行数</returns>
        public static int Swap(DataTable dt, string id, string targetId)
        {
            int result = 0;
            string sortCode = BaseBusinessLogic.GetProperty(dt, id, BaseBusinessLogic.FieldSortCode);
            string targetSortCode = BaseBusinessLogic.GetProperty(dt, targetId, BaseBusinessLogic.FieldSortCode);
            result = BaseBusinessLogic.SetProperty(dt, id, BaseBusinessLogic.FieldSortCode, targetSortCode);
            result += BaseBusinessLogic.SetProperty(dt, targetId, BaseBusinessLogic.FieldSortCode, sortCode);
            return result;
        }
        public static int SwapDyn(dynamic lstT, string id, string targetId)
        {
            int result = 0;
            string sortCode = BaseBusinessLogic.GetPropertyDyn(lstT, id, BaseBusinessLogic.FieldSortCode);
            string targetSortCode = BaseBusinessLogic.GetPropertyDyn(lstT, targetId, BaseBusinessLogic.FieldSortCode);
            result = BaseBusinessLogic.SetPropertyDyn(lstT, id, BaseBusinessLogic.FieldSortCode, targetSortCode);
            result += BaseBusinessLogic.SetPropertyDyn(lstT, targetId, BaseBusinessLogic.FieldSortCode, sortCode);         
            return result;
        }
        #endregion

        public static string GetPreviousIdDyn(dynamic lstT, string id)
        {
            return GetPreviousIdDyn(lstT, id, BaseBusinessLogic.FieldId);
        }

        public static string GetPreviousIdDyn(dynamic lstT, string id, string field)
        {
            string result = string.Empty;
            foreach (dynamic t in lstT)
            {
                if (ReflectionUtil.GetProperty(t,field).ToString().Equals(id))
                {
                    break;
                }
                result = ReflectionUtil.GetProperty(t, field).ToString().ToString();
            }
            return result;
        }
    }
}