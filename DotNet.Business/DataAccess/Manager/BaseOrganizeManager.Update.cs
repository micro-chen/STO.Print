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
    /// BaseOrganizeManager（程序OK）
    /// 组织机构、部门表
    ///
    /// 修改记录
    ///     
    ///		2015.07.02 版本：1.0 JiRiGaLa	更新方法独立出来。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.02</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager //, IBaseOrganizeManager
    {
        public int Update(BaseOrganizeEntity entity)
        {
            int result = 0;

            // 检查是否已被其他人修改            
            //if (DbLogic.IsModifed(DbHelper, BaseOrganizeEntity.TableName, entity.Id, entity.ModifiedUserId, entity.ModifiedOn))
            //{
            //    // 数据已经被修改
            //    statusCode = StatusCode.ErrorChanged.ToString();
            //}

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, entity.ParentId));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldFullName, entity.FullName));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

            if (this.Exists(parameters, entity.Id))
            {
                // 名称已重复
                this.StatusCode = Status.ErrorNameExist.ToString();
                this.StatusMessage = Status.ErrorNameExist.ToDescription();
            }
            else
            {
                // 检查编号是否重复
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldCode, entity.Code));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

                if (entity.Code.Length > 0 && this.Exists(parameters, entity.Id))
                {
                    // 编号已重复
                    this.StatusCode = Status.ErrorCodeExist.ToString();
                    this.StatusMessage = Status.ErrorCodeExist.ToDescription();
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

                    // 1:更新部门的信息
                    result = this.UpdateObject(entity);

                    // 2:组织机构修改时，用户表的公司，部门，工作组数据给同步更新。
                    BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);
                    userManager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, entity.Id), new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyName, entity.FullName));
                    // userManager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldDepartmentId, entity.Id), new KeyValuePair<string, object>(BaseUserEntity.FieldDepartmentName, entity.FullName));
                    // userManager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldWorkgroupId, entity.Id), new KeyValuePair<string, object>(BaseUserEntity.FieldWorkgroupName, entity.FullName));
                    // 03：组织机构修改时，文件夹同步更新
                    // BaseFolderManager folderManager = new BaseFolderManager(this.DbHelper, this.UserInfo);
                    // folderManager.SetProperty(new KeyValuePair<string, object>(BaseFolderEntity.FieldFolderName, entity.FullName), new KeyValuePair<string, object>(BaseFolderEntity.FieldId, entity.Id));
                    if (result == 1)
                    {
                        // AfterUpdate(entity);

                        // 2015-06-24 更新过后，需要进行刷新缓存，提高系统的准确性
                        // SetCache(entity);
                        SetCache(entity.Id.ToString());

                        this.StatusCode = Status.OKUpdate.ToString();
                        this.StatusMessage = Status.OKUpdate.ToDescription();
                    }
                    else
                    {
                        this.StatusCode = Status.ErrorDeleted.ToString();
                        this.StatusMessage = Status.ErrorDeleted.ToDescription();
                    }
                }
            }
            return result;
        }

        #region public void UpdateEntityLog(BaseOrganizeEntity newEntity, BaseOrganizeEntity oldEntity)
        /// <summary>
        /// 保存实体修改记录
        /// </summary>
        /// <param name="newEntity">修改前的实体对象</param>
        /// <param name="oldEntity">修改后的实体对象</param>
        /// <param name="tableName">表名称</param>
        public void UpdateEntityLog(BaseOrganizeEntity newEntity, BaseOrganizeEntity oldEntity, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(this.UserInfo, tableName);
            foreach (var property in typeof(BaseOrganizeEntity).GetProperties())
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
                record.TableDescription = FieldExtensions.ToDescription(typeof(BaseOrganizeEntity), "TableName");
                record.ColumnCode = property.Name.ToUpper();
                record.ColumnDescription = fieldDescription.Text;
                record.RecordKey = oldEntity.Id.ToString();
                record.NewValue = newValue;
                record.OldValue = oldValue;
                record.IPAddress = Utilities.GetIPAddress(true);
                manager.Add(record, true, false);
            }
        }
        #endregion
    }
}