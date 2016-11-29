//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserLogOnManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///     2015.07.02 版本：1.0 JiRiGaLa 修改记录独立 
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.02</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserLogOnManager
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseUserLogOnEntity entity)
        {
            int result = 0;
            // 获取原始实体信息
            var entityOld = this.GetObject(entity.Id);
            // 保存修改记录
            this.UpdateEntityLog(entity, entityOld);
            // 更新数据
            result = this.UpdateObject(entity);
            // 重新缓存

            return result;
        }

        #region public void UpdateEntityLog(BaseUserLogOnEntity newEntity, BaseUserLogOnEntity oldEntity, string tableName = null)
        /// <summary>
        /// 保存实体修改记录
        /// </summary>
        /// <param name="newEntity">修改前的实体对象</param>
        /// <param name="oldEntity">修改后的实体对象</param>
        /// <param name="tableName">表名称</param>
        public void UpdateEntityLog(BaseUserLogOnEntity newEntity, BaseUserLogOnEntity oldEntity, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = BaseUserEntity.TableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(this.UserInfo, tableName);
            foreach (var property in typeof(BaseUserLogOnEntity).GetProperties())
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
                record.TableDescription = FieldExtensions.ToDescription(typeof(BaseUserLogOnEntity), "TableName");
                record.RecordKey = oldEntity.Id.ToString();
                record.IPAddress = Utilities.GetIPAddress(true);
                manager.Add(record, true, false);
            }
        }
        #endregion
    }
}