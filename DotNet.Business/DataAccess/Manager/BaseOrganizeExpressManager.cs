//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseOrganizeExpressManager
    /// 组织机构、部门扩展表
    ///
    /// 修改记录
    ///     
    ///		2015.04.08 版本：1.0 潘齐民	添加记录修改日志方法。
    ///
    /// <author>
    ///		<name>潘齐民</name>
    ///		<date>2015.04.08</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeExpressManager : BaseManager //, IBaseOrganizeExpressManager
    {
        #region 实体修改记录 public void UpdateEntityLog(BaseOrganizeEntity newEntity, BaseOrganizeEntity oldEntity)

        /// <summary>
        /// 实体修改记录
        /// </summary>
        /// <param name="newEntity">修改前的实体对象</param>
        /// <param name="oldEntity">修改后的实体对象</param>
        public void UpdateEntityLog(BaseOrganizeExpressEntity newEntity, BaseOrganizeExpressEntity oldEntity, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(this.UserInfo, tableName);
            foreach (var property in typeof(BaseOrganizeExpressEntity).GetProperties())
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
                record.TableDescription = FieldExtensions.ToDescription(typeof(BaseOrganizeExpressEntity), "TableName");
                record.RecordKey = oldEntity.Id.ToString();
                record.IPAddress = Utilities.GetIPAddress(true);
                manager.Add(record, true, false);
            }
        }

        #endregion
    }
}