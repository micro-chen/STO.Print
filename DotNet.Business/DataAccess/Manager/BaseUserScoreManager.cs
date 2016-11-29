//-----------------------------------------------------------------
// AllData Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserScoreManager
    /// 用户积分表管理层
    /// 
    /// 修改纪录
    /// 
    ///	2013-05-20 版本：1.0 JiRiGaLa 创建文件。
    ///		
    /// <author>
    ///	<name>JiRiGaLa</name>
    ///	<date>2013-05-20</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserScoreManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseUserScoreEntity entity)
        {
            string result = string.Empty;
            // 添加子表，用户积分表
            result = this.AddObject(entity);
            // 更新用户积分
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginUpdate(BaseUserEntity.TableName);
            sqlBuilder.SetFormula(BaseUserEntity.FieldScore, BaseUserEntity.FieldScore + " + " +  entity.Score);
            sqlBuilder.SetWhere(BaseUserEntity.FieldId, entity.UserId);
            sqlBuilder.EndUpdate();
            // 发送手机短信提醒

            return result;
        }

        /*

        /// <summary>
        /// 批量提交单据
        /// (无工作流)
        /// </summary>
        /// <param name="ids">单据主键</param>
        /// <returns>影响行数</returns>
        public int BatchSend(string[] ids)
        {
            int result = 0;
            foreach (var id in ids)
            {
                result += Send(id);
            }
            return result;
        }

        /// <summary>
        /// 提交单据
        /// (无工作流)
        /// </summary>
        /// <param name="id">单据主键</param>
        /// <returns>影响行数</returns>
        public int Send(string id)
        {
            BaseUserScoreEntity entity = this.GetObject(id);
            if (string.IsNullOrEmpty(entity.Code))
            {
                // 获取编号，产生序列号，按年生成
                string sequenceName = this.CurrentTableName + DateTime.Now.ToString("yyyy");
                BaseSequenceManager sequenceManager = new BaseSequenceManager(this.UserInfo);
                string sequenceCode = sequenceManager.Increment(sequenceName, 1, 6, true);
                entity.Code = DateTime.Now.ToString("yyyy") + "-" + sequenceCode;
            }
            entity.Enabled = 1;
            return this.Update(entity);
        }

        */

        #region public DataTable GetDataTableByPage(string companyId, string departmentId, string userId, string searchValue, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = "CreateOn", string sortDire = "DESC")
        /// <summary>
        /// 按条件分页查询
        /// </summary>
        /// <param name="companyId">查看公司主键</param>
        /// <param name="departmentId">查看部门主键</param>
        /// <param name="userId">查看用户主键</param>
        /// <param name="searchValue">查询字段</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序方向</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByPage(string companyId, string departmentId, string userId, string searchValue, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = "CreateOn", string sortDire = "DESC")
        {
            pageIndex = pageIndex + 1;
            // string whereConditional = "DeletionStateCode  = 0 ";
			string whereConditional = " 1 = 1 ";
            if (!string.IsNullOrEmpty(companyId) && ValidateUtil.IsNumeric(companyId))
            {
                // whereConditional = " AND CompanyId = " + companyId;
            }
            if (!string.IsNullOrEmpty(departmentId) && ValidateUtil.IsNumeric(departmentId))
            {
                // whereConditional = " AND DepartmentId = " + departmentId;
            }
            if (!string.IsNullOrEmpty(userId) && ValidateUtil.IsNumeric(userId))
            {
                // whereConditional = " AND UserId = " + userId;
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = dbHelper.SqlSafe(searchValue);
                // whereConditional = " AND Description LIKE %" + userId + "%";
            }
            return GetDataTableByPage(out recordCount, pageIndex, pageSize, sortExpression, sortDire, this.CurrentTableName, whereConditional, "*");
        }
        #endregion
    }
}
