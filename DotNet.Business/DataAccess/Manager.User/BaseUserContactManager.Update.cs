//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserContactManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.09.06 版本：2.0 JiRiGaLa	重新设置缓存。
    ///		2015.07.02 版本：1.0 JiRiGaLa	更新记录。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.09.06</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserContactManager : BaseManager
    {
        /// <summary>
        /// 设置对象，若不存在就增加，有存在就更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>更新、添加成功？</returns>
        public bool SetObject(BaseUserContactEntity entity)
        {
            bool result = false;

            // 若有主键就是先更新，没主键就是添加
            if (!string.IsNullOrEmpty(entity.Id))
            {
                result = this.Update(entity) > 0;
                // 若不存在，就是添加的意思
                if (!result)
                {
                    // 更新不成功表示没数据，需要添加数据，这时候要注意主键不能出错
                    result = !string.IsNullOrEmpty(this.Add(entity));
                }
            }
            else
            {
                // 若没有主键就是添加数据
                result = !string.IsNullOrEmpty(this.Add(entity));
            }

            // 2016-01-20 吉日嘎拉， 用户的联系方式修改后，要把用户的修改时间也修改，这样才会同步下载用户的信息，用户的联系方式
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginUpdate(BaseUserEntity.TableName);
            sqlBuilder.SetDBNow(BaseUserEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseUserEntity.FieldId, entity.Id);
            sqlBuilder.EndUpdate();

            return result;
        }

        #region public int Update(BaseUserContactEntity entity)
        /// <summary>
        /// 更新用户联系方式
        /// </summary>
        /// <param name="entity">用户联系方式实体</param>
        /// <returns>影响行数</returns>
        public int Update(BaseUserContactEntity entity)
        {
            int result = 0;

            // 获取原始实体信息
            var entityOld = this.GetObject(entity.Id);
            // 2015-12-26 吉日嘎拉 当新增时，需要判断这个是否为空
            if (entityOld != null)
            {
                // 保存修改记录
                this.UpdateEntityLog(entity, entityOld);
            }
            // 更新数据
            result = this.UpdateObject(entity);
            // 同步数据
            // AfterUpdate(entity);
            // 重新设置缓存
            if (result > 0)
            {
                BaseUserContactManager.SetCache(entity);
            }

            // 返回值
            return result;
        }
        #endregion

        #region public void UpdateEntityLog(BaseUserContactEntity newEntity, BaseUserContactEntity oldEntity, string tableName = null)
        /// <summary>
        /// 保存实体修改记录
        /// </summary>
        /// <param name="newEntity">修改前的实体对象</param>
        /// <param name="oldEntity">修改后的实体对象</param>
        /// <param name="tableName">表名称</param>
        public void UpdateEntityLog(BaseUserContactEntity newEntity, BaseUserContactEntity oldEntity, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = BaseUserEntity.TableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(this.UserInfo, tableName);
            foreach (var property in typeof(BaseUserContactEntity).GetProperties())
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
                record.TableCode = this.CurrentTableName.ToUpper();
                record.TableDescription = FieldExtensions.ToDescription(typeof(BaseUserContactEntity), "TableName");
                record.RecordKey = oldEntity.Id.ToString();
                record.ColumnCode = property.Name.ToUpper();
                record.ColumnDescription = fieldDescription.Text;
                record.NewValue = newValue;
                record.OldValue = oldValue;
                record.IPAddress = Utilities.GetIPAddress(true);
                manager.Add(record, true, false);
            }
        }
        #endregion
    }
}