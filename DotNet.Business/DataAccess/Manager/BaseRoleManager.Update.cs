//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseRoleManager 
    /// 角色表结构定义部分
    ///
    /// 修改记录
    ///
    ///     2015.07.02 版本：1.0 JiRiGaLa 独立修改日志。
    ///     
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.02</date>
    /// </author>
    /// </summary>
    public partial class BaseRoleManager : BaseManager //, IBaseRoleManager
    {
        #region public int Update(BaseRoleEntity entity, out string statusCode) 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public int Update(BaseRoleEntity entity, out string statusCode)
        {
            int result = 0;
            // 检查是否已被其他人修改

            if (DbLogic.IsModifed(DbHelper, this.CurrentTableName, entity.Id, entity.ModifiedUserId, entity.ModifiedOn))
            {
                // 数据已经被修改
                statusCode = Status.ErrorChanged.ToString();
            }
            else
            {
                // 检查名称是否重复
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldOrganizeId, entity.OrganizeId));
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, entity.RealName));
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
                if (this.Exists(parameters, entity.Id))
                {
                    // 名称已重复
                    statusCode = Status.ErrorNameExist.ToString();
                }
                else
                {
                    // 获取原始实体信息
                    var entityOld = this.GetObject(entity.Id);
                    // 保存修改记录
                    this.UpdateEntityLog(entity, entityOld);

                    result = this.UpdateObject(entity);
                    if (result == 1)
                    {
                        statusCode = Status.OKUpdate.ToString();
                    }
                    else
                    {
                        statusCode = Status.ErrorDeleted.ToString();
                    }
                }
            }

            return result;
        }
        #endregion

        #region public void UpdateEntityLog(BaseRoleEntity newEntity, BaseRoleEntity oldEntity, string tableName = null)
        /// <summary>
        /// 保存实体修改记录
        /// </summary>
        /// <param name="newEntity">修改前的实体对象</param>
        /// <param name="oldEntity">修改后的实体对象</param>
        /// <param name="tableName">表名称</param>
        public void UpdateEntityLog(BaseRoleEntity newEntity, BaseRoleEntity oldEntity, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                // tableName = this.CurrentTableName + "_LOG";
                tableName = BaseModifyRecordEntity.TableName;
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(this.UserInfo, tableName);
            foreach (var property in typeof(BaseRoleEntity).GetProperties())
            {
                var oldValue = Convert.ToString(property.GetValue(oldEntity, null));
                var newValue = Convert.ToString(property.GetValue(newEntity, null));
                var fieldDescription = property.GetCustomAttributes(typeof(FieldDescription), false).FirstOrDefault() as FieldDescription;
                //不记录创建人、修改人、没有修改的记录
                if (!fieldDescription.NeedLog || oldValue == newValue)
                {
                    continue;
                }
                var record = new BaseModifyRecordEntity();
                record.ColumnCode = property.Name.ToUpper();
                record.ColumnDescription = fieldDescription.Text;
                record.NewValue = newValue;
                record.OldValue = oldValue;
                record.TableCode = this.CurrentTableName.ToUpper();
                record.TableDescription = FieldExtensions.ToDescription(typeof(BaseRoleEntity), "TableName");
                record.RecordKey = oldEntity.Id.ToString();
                record.IPAddress = Utilities.GetIPAddress(true);
                manager.Add(record, true, false);
            }
        }
        #endregion
    }
}
