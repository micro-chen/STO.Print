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
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2011.10.17 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.10.17</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        #region public int Update(BaseUserEntity entity, bool checkUserExist = false, bool checkCodeExist = false)
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="entity">用户实体</param>
        /// <param name="checkUserExist">检查用户名是否存在</param>
        /// <param name="checkCodeExist">检查编号是否存在</param>
        /// <returns>影响行数</returns>
        public int Update(BaseUserEntity entity, bool checkUserExist = false, bool checkCodeExist = false)
        {
            int result = 0;

            // 检查用户名是否重复
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, entity.UserName));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, entity.CompanyId));
            if (checkUserExist && this.Exists(parameters, entity.Id))
            {
                // 用户名已重复
                this.StatusCode = Status.ErrorUserExist.ToString();
            }
            else
            {
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCode, entity.Code));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, entity.CompanyId));

                if (checkCodeExist && !string.IsNullOrEmpty(entity.Code)
                    && entity.Code.Length > 0
                    && this.Exists(parameters, entity.Id))
                {
                    // 编号已重复
                    this.StatusCode = Status.ErrorCodeExist.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.QuickQuery))
                    {
                        // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                        entity.QuickQuery = StringUtil.GetPinyin(entity.RealName).ToLower();
                    }
                    if (string.IsNullOrEmpty(entity.SimpleSpelling))
                    {
                        // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                        entity.SimpleSpelling = StringUtil.GetSimpleSpelling(entity.RealName).ToLower();
                    }

                    // 获取原始实体信息
                    var entityOld = this.GetObject(entity.Id);
                    // 保存修改记录
                    this.UpdateEntityLog(entity, entityOld);

                    // 01：先更新自己的数据
                    result = this.UpdateObject(entity);
                    // 02：用户修改时，用户文件夹同步更新
                    // BaseFolderManager manager = new BaseFolderManager(this.DbHelper, this.UserInfo);
                    // manager.SetProperty(new KeyValuePair<string, object>(BaseFolderEntity.FieldFolderName, userEntity.RealName), new KeyValuePair<string, object>(BaseFolderEntity.FieldId, userEntity.Id));
                    
                    if (result == 0)
                    {
                        this.StatusCode = Status.ErrorDeleted.ToString();
                    }
                    else
                    {
                        AfterUpdate(entity);
                        this.StatusCode = Status.OKUpdate.ToString();
                    }
                }
            }

            return result;
        }
        #endregion

        #region public int UpdateEntityLog(BaseUserEntity newEntity, BaseUserEntity oldEntity, string tableName = null)
        /// <summary>
        /// 保存实体修改记录
        /// </summary>
        /// <param name="newEntity">修改前的实体对象</param>
        /// <param name="oldEntity">修改后的实体对象</param>
        /// <param name="tableName">表名称</param>
        /// <returns>影响行数</returns>
        public int UpdateEntityLog(BaseUserEntity newEntity, BaseUserEntity oldEntity, string tableName = null)
        {
            int result = 0;

            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(this.UserInfo, tableName);
            foreach (var property in typeof(BaseUserEntity).GetProperties())
            {
                var oldValue = Convert.ToString(property.GetValue(oldEntity, null));
                var newValue = Convert.ToString(property.GetValue(newEntity, null));
                var fieldDescription = property.GetCustomAttributes(typeof(FieldDescription), false).FirstOrDefault() as FieldDescription;
                // 不记录创建人、修改人、没有修改的记录
                if (!fieldDescription.NeedLog || oldValue == newValue)
                {
                    continue;
                }
                var record = new BaseModifyRecordEntity();
                record.TableCode = this.CurrentTableName.ToUpper();
                record.TableDescription = FieldExtensions.ToDescription(typeof(BaseUserEntity), "TableName");
                record.ColumnCode = property.Name.ToUpper();
                record.ColumnDescription = fieldDescription.Text;
                record.RecordKey = oldEntity.Id.ToString();
                record.NewValue = newValue;
                record.OldValue = oldValue;
                record.IPAddress = Utilities.GetIPAddress(true);
                manager.Add(record, true, false);
                result ++;
            }

            return result;
        }
        #endregion

        public int ChangeEnabled(string id)
        {
            BaseUserEntity userEntity = this.GetObject(id);
            if (userEntity.Enabled != 1)
            {
                // 若用户要生效了，那就需要修改锁定的时间了，否则被锁定的用户有效后也无法登录系统了
                BaseUserLogOnManager manager = new BaseUserLogOnManager(this.DbHelper, this.UserInfo);
                BaseUserLogOnEntity entity = manager.GetObject(id);
                entity.LockStartDate = null;
                entity.LockEndDate = null;
                manager.Update(entity);
                userEntity.AuditStatus = string.Empty;
                userEntity.DeletionStateCode = 0;
                userEntity.Enabled = 1;
            }
            else
            {
                // 若是有效的用户直接修改为无效的用户
                userEntity.Enabled = 0;
            }

            return this.UpdateObject(userEntity);
        }

        /// <summary>
        /// 设置对象，若不存在就增加，有存在就更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="userPassword">用户密码</param>
        /// <returns>更新、添加成功？</returns>
        public bool SetObject(BaseUserInfo userInfo, string userPassword)
        {
            bool result = false;

            BaseUserEntity userEntity = this.GetObject(userInfo.Id);
            if (userEntity == null)
            {
                userEntity = new BaseUserEntity();
                userEntity.Id = userInfo.Id;
                userEntity.Enabled = 1;
                userEntity.SortCode = int.Parse(userInfo.Id);
            }
            // 2015-07-14 吉日嘎拉 用户若没有排序码就会引起序列等问题，为了避免这样的问题，直接给他赋予排序码。
            if (!userEntity.SortCode.HasValue)
            {
                userEntity.SortCode = int.Parse(userEntity.Id);
            }
            userEntity.NickName = userInfo.NickName;
            userEntity.UserName = userInfo.UserName;
            userEntity.Code = userInfo.Code;
            userEntity.RealName = userInfo.RealName;
            userEntity.CompanyId = userInfo.CompanyId;
            userEntity.CompanyName = userInfo.CompanyName;
            userEntity.DepartmentId = userInfo.DepartmentId;
            userEntity.DepartmentName = userInfo.DepartmentName;
            userEntity.Enabled = 1;
            // userEntity.ManagerAuditDate = DateTime.Now;
            // userEntity.SubCompanyId = result.SubCompanyId;
            // userEntity.SubCompanyName = result.SubCompanyName;
            // userEntity.SubDepartmentId = result.SubDepartmentId;
            // userEntity.SubDepartmentName = result.SubDepartmentName;
            // userEntity.WorkgroupId = result.WorkgroupId;
            // userEntity.WorkgroupName = result.WorkgroupName;
            // 若有主键就是先更新，没主键就是添加
            if (!string.IsNullOrEmpty(userEntity.Id))
            {
                result = this.UpdateObject(userEntity) > 0;
                // 若不存在，就是添加的意思
                if (!result)
                {
                    // 更新不成功表示没数据，需要添加数据，这时候要注意主键不能出错
                    result = !string.IsNullOrEmpty(this.AddObject(userEntity));
                }
            }
            else
            {
                // 若没有主键就是添加数据
                result = !string.IsNullOrEmpty(this.AddObject(userEntity));
            }
            this.SetPassword(userInfo.Id, userPassword, true, true, false);

            /*
            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.DbHelper, this.UserInfo);
            BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(userInfo.Id);
            if (userLogOnEntity == null)
            {
                userLogOnEntity = new BaseUserLogOnEntity();
                userLogOnEntity.Id = userInfo.Id;
            }
            userLogOnEntity.OpenId = userInfo.OpenId;
            userLogOnEntity.IPAddress = userInfo.IPAddress;
            userLogOnEntity.MACAddress = userInfo.MACAddress;
            if (!string.IsNullOrEmpty(userLogOnEntity.Id))
            {
                result = userLogOnManager.Update(userLogOnEntity) > 0;
                // 若不存在，就是添加的意思
                if (!result)
                {
                    // 更新不成功表示没数据，需要添加数据，这时候要注意主键不能出错
                    result = !string.IsNullOrEmpty(userLogOnManager.Add(userLogOnEntity));
                }
            }
            else
            {
                // 若没有主键就是添加数据
                result = !string.IsNullOrEmpty(userLogOnManager.Add(userLogOnEntity));
            }
            */

            return result;
        }
    }
}