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
    /// BaseAreaManager 
    /// 地区表(省、市、县)
    ///
    /// 修改记录
    ///
    ///		2015.07.02 版本：1.0 JiRiGaLa  修改记录独立化。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.02</date>
    /// </author>
    /// </summary>
    public partial class BaseAreaManager : BaseManager
    {
        public int Update(BaseAreaEntity entity, out string statusCode)
        {
            int result = 0;
            // 检查是否已被其他人修改            
            //if (DbLogic.IsModifed(DbHelper, BaseOrganizeEntity.TableName, entity.Id, entity.ModifiedUserId, entity.ModifiedOn))
            //{
            //    // 数据已经被修改
            //    statusCode = StatusCode.ErrorChanged.ToString();
            //}
            //else
            //{

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (entity.ParentId != null)
            {
                parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, entity.ParentId));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, entity.FullName));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));

            if (this.Exists(parameters, entity.Id))
            {
                // 名称已重复
                statusCode = Status.ErrorNameExist.ToString();
            }
            else
            {
                // 检查编号是否重复
                parameters = new List<KeyValuePair<string, object>>();
                if (entity.ParentId != null)
                {
                    parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, entity.ParentId));
                }
                parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldCode, entity.Code));
                parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
                parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));

                if (entity.Code.Length > 0 && this.Exists(parameters, entity.Id))
                {
                    // 编号已重复
                    statusCode = Status.ErrorCodeExist.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.QuickQuery))
                    {
                        // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                        entity.QuickQuery = StringUtil.GetPinyin(entity.FullName).ToLower();
                    }
                    if (string.IsNullOrEmpty(entity.SimpleSpelling))
                    {
                        // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                        entity.SimpleSpelling = StringUtil.GetSimpleSpelling(entity.FullName).ToLower();
                    }

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
            //}
            return result;
        }

        #region public void UpdateEntityLog(BaseAreaEntity newEntity, BaseAreaEntity oldEntity)
        /// <summary>
        /// 实体修改记录
        /// </summary>
        /// <param name="newEntity">修改前的实体对象</param>
        /// <param name="oldEntity">修改后的实体对象</param>
        /// <param name="tableName">表名称</param>
        public void UpdateEntityLog(BaseAreaEntity newEntity, BaseAreaEntity oldEntity, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(this.UserInfo, tableName);
            foreach (var property in typeof(BaseAreaEntity).GetProperties())
            {
                var oldValue = Convert.ToString(property.GetValue(oldEntity, null));
                var newValue = Convert.ToString(property.GetValue(newEntity, null));
                //不记录创建人、修改人、没有修改的记录
                var fieldDescription = property.GetCustomAttributes(typeof(FieldDescription), false).FirstOrDefault() as FieldDescription;
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
                record.TableDescription = FieldExtensions.ToDescription(typeof(BaseAreaEntity), "TableName");
                record.RecordKey = oldEntity.Id.ToString();
                record.IPAddress = Utilities.GetIPAddress(true);
                manager.Add(record, true, false);
            }
        }
        #endregion
    }
}