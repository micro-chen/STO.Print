//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    ///	BaseManager
    /// 通用基类部分
    /// 
    /// 总觉得自己写的程序不上档次，这些新技术也玩玩，也许做出来的东西更专业了。
    /// 修改纪录
    /// 
    ///		2015.06.16 版本：1.0 JiRiGaLa 进行提炼，把代码进行分组。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.06.16</date>
    /// </author> 
    /// </summary>
    public partial class BaseManager : IBaseManager
    {
        //
        // 获取一些列表的常用方法
        //

        public virtual IDataReader GetDataReaderByWhere(string where = null, IDbDataParameter[] dbParameters = null, string order = null)
        {
            string sqlQuery = "SELECT *"
                            + "   FROM " + this.CurrentTableName;
            if (!string.IsNullOrEmpty(where))
            {
                sqlQuery += "  WHERE " + where + " ";
            }
            if (!string.IsNullOrWhiteSpace(order))
            {
                sqlQuery += "ORDER BY " + order;
            }
            return DbHelper.ExecuteReader(sqlQuery, dbParameters);
        }

        public virtual IDataReader GetDataReaderById(string id)
        {
            return this.GetDataReader(new KeyValuePair<string, object>(BaseBusinessLogic.FieldId, id));
        }

        #region public virtual IDataReader GetDataReaderByCategory(string categoryId) 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="categoryId">类别主键</param>
        /// <returns>数据表</returns>
        public virtual IDataReader GetDataReaderByCategory(string categoryCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseBusinessLogic.FieldCategoryCode, categoryCode));
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parameters);
        }
        #endregion

        #region public virtual IDataReader GetDataReaderByParent(string parentId) 按父亲节点获取数据
        /// <summary>
        /// 按父亲节点获取数据
        /// </summary>
        /// <param name="parentId">父节点主键</param>
        /// <returns>数据表</returns>
        public virtual IDataReader GetDataReaderByParent(string parentId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseBusinessLogic.FieldParentId, parentId));
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parameters, 0, BaseBusinessLogic.FieldSortCode);
        }
        #endregion

        //
        // 读取多条记录
        //
        public virtual IDataReader GetDataReader(int topLimit = 0, string order = null)
        {
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, null, topLimit, order);
        }

        public virtual IDataReader GetDataReader(string where)
        {
            return GetDataReaderByWhere(where);
        }

        public virtual IDataReader GetDataReader(string[] ids)
        {
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, BaseBusinessLogic.FieldId, ids);
        }

        public virtual IDataReader GetDataReader(string name, Object[] values, string order = null)
        {
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, name, values, order);
        }

        public virtual IDataReader GetDataReader(KeyValuePair<string, object> parameter, string order)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(parameter);
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parameters, 0, order);
        }

        public virtual IDataReader GetDataReader(KeyValuePair<string, object> parameter1, KeyValuePair<string, object> parameter2, string order)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(parameter1);
            parameters.Add(parameter2);
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parameters, 0, order);
        }

        public virtual IDataReader GetDataReader(KeyValuePair<string, object> parameter, int topLimit = 0, string order = null)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(parameter);
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parameters, topLimit, order);
        }

        public virtual IDataReader GetDataReader(params KeyValuePair<string, object>[] parameters)
        {
            List<KeyValuePair<string, object>> parametersList = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < parameters.Length; i++)
            {
                parametersList.Add(parameters[i]);
            }
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parametersList);
        }

        public virtual IDataReader GetDataReader(List<KeyValuePair<string, object>> parameters, int topLimit = 0, string order = null)
        {
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parameters, topLimit, order);
        }

        public virtual IDataReader GetDataReader(List<KeyValuePair<string, object>> parameters, string order)
        {
            return DbLogic.GetDataReader(DbHelper, this.CurrentTableName, parameters, 0, order);
        }
    }
}