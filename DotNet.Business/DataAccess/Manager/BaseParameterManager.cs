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

    /// <summary>
    /// BaseParameterManager
    /// 参数类
    /// 
    /// 修改记录
    /// 
    ///     2015.07.16 版本：3.0 JiRiGaLa   进行分表保存。
    ///     2011.04.05 版本：2.2 JiRiGaLa   修改AddObject 为public 方法，ip限制功能中使用
    ///     2009.04.01 版本：2.1 JiRiGaLa   创建者、修改者进行完善。
    ///     2008.04.30 版本：2.0 JiRiGaLa   按面向对象，面向服务进行改进。
    ///     2007.06.08 版本：1.4 JiRiGaLa   重新调整方法。
    ///		2006.02.05 版本：1.3 JiRiGaLa	重新调整主键的规范化。
    ///		2006.01.28 版本：1.2 JiRiGaLa	对一些方法进行改进，主键整理，调用性能也进行了修改，主键顺序进行整理。
    ///		2005.08.13 版本：1.1 JiRiGaLa	主键整理好。
    ///		2004.11.12 版本：1.0 JiRiGaLa	主键进行了绝对的优化，这是个好东西啊，平时要多用，用得要灵活些。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.16</date>
    /// </author> 
    /// </summary>
    public partial class BaseParameterManager : BaseManager
    {
        #region public string Add(BaseParameterEntity entity)
        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="entity">内容</param>
        /// <returns>主键</returns>
        public string Add(BaseParameterEntity entity, bool identity = false, bool returnId = false)
        {
            string result = string.Empty;

            this.Identity = identity;
            this.ReturnId = returnId;

            // 此处检查this.exist()
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, entity.CategoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, entity.ParameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, entity.ParameterCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterContent, entity.ParameterContent));
            // 2015-12-22 吉日嘎拉 检查没有删除标志的，有删除标志的当日志用了
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));
            if (this.Exists(parameters))
            {
                // 编号已重复
                this.StatusCode = Status.ErrorCodeExist.ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(entity.Id))
                {
                    entity.Id = Guid.NewGuid().ToString("N");
                }
                result = this.AddObject(entity);
                // 运行成功
                this.StatusCode = Status.OKAdd.ToString();
            }

            return result;
        }
        #endregion

        #region public int Update(BaseParameterEntity entity) 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">参数基类表结构定义</param>
        /// <returns>影响行数</returns>
        public int Update(BaseParameterEntity entity)
        {
            int result = 0;
            // 检查是否已被其他人修改
            //if (DbLogic.IsModifed(DbHelper, BaseParameterEntity.TableName, parameterEntity.Id, parameterEntity.ModifiedUserId, parameterEntity.ModifiedOn))
            //{
            //    // 数据已经被修改
            //    this.StatusCode = StatusCode.ErrorChanged.ToString();
            //}
            //else
            //{
            // 检查编号是否重复
            if (this.Exists(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, entity.ParameterCode), entity.Id))
            {
                // 文件夹名已重复
                this.StatusCode = Status.ErrorCodeExist.ToString();
            }
            else
            {
                // 进行更新操作
                result = this.UpdateObject(entity);
                if (result == 1)
                {
                    this.StatusCode = Status.OKUpdate.ToString();
                }
                else
                {
                    // 数据可能被删除
                    this.StatusCode = Status.ErrorDeleted.ToString();
                }
            }
            // }
            return result;
        }
        #endregion

        #region public string GetParameter(string tableName, string categoryCode, string parameterId, string parameterCode)
        /// <summary>
        /// 获取参数
        /// 2015-07-24 吉日嘎拉，获取最新的一条，增加排序字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">编码</param>
        /// <returns>参数值</returns>
        public string GetParameter(string tableName, string categoryCode, string parameterId, string parameterCode)
        {
            this.CurrentTableName = tableName;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, parameterCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));
            return this.GetProperty(parameters, BaseParameterEntity.FieldParameterContent, BaseParameterEntity.FieldCreateOn + " DESC");
        }
        #endregion

        #region public string GetParameter(string categoryCode, string parameterId, string parameterCode)
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">编码</param>
        /// <returns>参数值</returns>
        public string GetParameter(string categoryCode, string parameterId, string parameterCode)
        {
            string tableName = BaseParameterEntity.TableName;
            if (!string.IsNullOrEmpty(categoryCode) && categoryCode == "System")
            {
                tableName = "SystemParameter";
            }

            return GetParameter(tableName, categoryCode, parameterId, parameterCode);
        }
        #endregion

        #region public int SetParameter(string tableName, string categoryCode, string parameterId, string parameterCode, string parameterContent)
        /// <summary>
        /// 更新参数设置
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">编码</param>
        /// <param name="parameterContent">参数内容</param>
        /// <returns>影响行数</returns>
        public int SetParameter(string tableName, string categoryCode, string parameterId, string parameterCode, string parameterContent)
        {
            int result = 0;

            this.CurrentTableName = tableName;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, parameterCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));
            // 检测是否无效数据
            if ((parameterContent == null) || (parameterContent.Length == 0))
            {
                result = this.Delete(parameters);
            }
            else
            {
                // 检测是否存在
                result = this.SetProperty(parameters, new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterContent, parameterContent));
                if (result == 0)
                {
                    // 进行增加操作
                    BaseParameterEntity entity = new BaseParameterEntity();
                    entity.Id = Guid.NewGuid().ToString("N");
                    entity.CategoryCode = categoryCode;
                    entity.ParameterId = parameterId;
                    entity.ParameterCode = parameterCode;
                    entity.ParameterContent = parameterContent;
                    entity.Enabled = true;
                    entity.DeletionStateCode = 0;
                    this.Add(entity);
                    result = 1;
                }
            }
            return result;
        }
        #endregion

        #region public int SetParameter(string categoryCode, string parameterId, string parameterCode, string parameterContent)
        /// <summary>
        /// 更新参数设置
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">编码</param>
        /// <param name="parameterContent">参数内容</param>
        /// <returns>影响行数</returns>
        public int SetParameter(string categoryCode, string parameterId, string parameterCode, string parameterContent)
        {
            int result = 0;

            string tableName = BaseParameterEntity.TableName;
            if (!string.IsNullOrEmpty(categoryCode) && categoryCode == "System")
            {
                tableName = "SystemParameter";
            }

            return SetParameter(tableName, categoryCode, parameterId, parameterCode, parameterContent);
        }
        #endregion

        #region public int AddParameter(string categoryCode, string parameterId, string parameterCode, string parameterContent)
        /// <summary>
        /// 添加参数设置
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">编码</param>
        /// <param name="parameterContent">参数内容</param>
        /// <returns>主键</returns>
        public string AddParameter(string categoryCode, string parameterId, string parameterCode, string parameterContent)
        {
            return AddParameter(BaseParameterEntity.TableName, categoryCode, parameterId, parameterCode, parameterContent);
        }
        #endregion

        #region public int AddParameter(string tableName, string categoryCode, string parameterId, string parameterCode, string parameterContent)
        /// <summary>
        /// 添加参数设置
        /// 2015-07-24 吉日嘎拉 按表名来添加、尽管添加的功能实现。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">编码</param>
        /// <param name="parameterContent">参数内容</param>
        /// <returns>主键</returns>
        public string AddParameter(string tableName, string categoryCode, string parameterId, string parameterCode, string parameterContent)
        {
            string result = string.Empty;

            this.CurrentTableName = tableName;

            /*
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, parameterCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));
            // 检测是否无效数据
            if ((parameterContent == null) || (parameterContent.Length == 0))
            {
                this.SetDeleted(parameters);
            }
            else
            {
                parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterContent, parameterContent));
                result = this.GetProperty(parameters, BaseParameterEntity.FieldId);
                // 检测是否存在
                if (string.IsNullOrEmpty(result))
                {
             */
            // 进行增加操作
            BaseParameterEntity entity = new BaseParameterEntity();
            entity.Id = Guid.NewGuid().ToString("N");
            entity.CategoryCode = categoryCode;
            entity.ParameterId = parameterId;
            entity.ParameterCode = parameterCode;
            entity.ParameterContent = parameterContent;
            entity.Enabled = true;
            entity.DeletionStateCode = 0;
            result = this.AddObject(entity);
            /*
                }
            }
            */

            return result;
        }
        #endregion

        #region public DataTable GetSystemParameter()
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <returns>数据表</returns>
        public DataTable GetSystemParameter()
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "System"));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldEnabled, 1));
            return this.GetDataTable(parameters);
        }
        #endregion

        #region public DataTable GetDataTableByParameter(string categoryCode, string parameterId)
        /// <summary>
        /// 获取记录
        /// 2015-12-22 吉日嘎拉 被删除的不显示出来
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByParameter(string categoryCode, string parameterId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));
            return this.GetDataTable(parameters);
        }
        #endregion

        #region public DataTable GetDataTableParameterCode(string categoryCode, string parameterId, string parameterCode)
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">编码</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableParameterCode(string categoryCode, string parameterId, string parameterCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, parameterCode));
            return this.GetDataTable(parameters, BaseParameterEntity.FieldCreateOn);
        }
        #endregion

        #region public int DeleteByParameter(string categoryCode, string parameterId)
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <returns>影响行数</returns>
        public int DeleteByParameter(string categoryCode, string parameterId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            return this.Delete(parameters);
        }
        #endregion

        #region public int DeleteByParameterCode(string categoryCode, string parameterId, string parameterCode)
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">参数编号</param>
        /// <returns>影响行数</returns>
        public int DeleteByParameterCode(string categoryCode, string parameterId, string parameterCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, parameterCode));
            return this.Delete(parameters);
        }
        #endregion

        #region public int Delete(string categoryCode, string parameterId, string parameterCode, string parameterContent)
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="categoryCode">类别编号</param>
        /// <param name="parameterId">参数主键</param>
        /// <param name="parameterCode">参数编号</param>
        /// <returns>影响行数</returns>
        public int Delete(string categoryCode, string parameterId, string parameterCode, string parameterContent)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, parameterCode));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterContent, parameterContent));
            return this.Delete(parameters);
        }
        #endregion
    }
}