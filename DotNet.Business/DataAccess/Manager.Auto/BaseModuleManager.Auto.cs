//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseModuleManager
    /// 模块（菜单）表
    /// 
    /// 修改记录
    /// 
    /// 2012-05-22 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-05-22</date>
    /// </author>
    /// </summary>
    public partial class BaseModuleManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseModuleManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseModuleEntity.TableName;
            }
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseModuleManager(string tableName)
            : this()
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseModuleManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseModuleManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseModuleManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseModuleManager(BaseUserInfo userInfo, string tableName)
            : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseModuleManager(IDbHelper dbHelper, string tableName)
            : this(dbHelper)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseModuleManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseModuleEntity entity)
        {
            return this.AddObject(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主键</param>
        /// <returns>主键</returns>
        public string Add(BaseModuleEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseModuleEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseModuleEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseModuleEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseModuleEntity.FieldId, id)));
        }

        public BaseModuleEntity GetObject(int id)
        {
            return GetObject(id.ToString());
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseModuleEntity entity)
        {
            string result = string.Empty;

            if (entity.SortCode == null || entity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                result = sequenceManager.Increment(BaseModuleEntity.TableName);
                entity.SortCode = int.Parse(result);
            }

            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseModuleEntity.FieldId);

            // 若是非空主键，表明已经指定了主键了
            if (!string.IsNullOrEmpty(entity.Id))
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.SetValue(BaseModuleEntity.FieldId, entity.Id);
                result = entity.Id;
            }
            else
            {
                if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    // 2015-12-23 吉日嘎拉 这里需要兼容一下以前的老的数据结构
                    sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + BaseModuleEntity.TableName.ToUpper() + ".NEXTVAL ");
                }
                else
                {
                    entity.Id = Guid.NewGuid().ToString("N");
                    result = entity.Id;
                    sqlBuilder.SetValue(BaseModuleEntity.FieldId, entity.Id);
                }
            }

            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseModuleEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseModuleEntity.FieldCreateBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseModuleEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseModuleEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseModuleEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseModuleEntity.FieldModifiedOn);
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.Access))
            {
                result = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
            }

            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(BaseModuleEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseModuleEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseModuleEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseModuleEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseModuleEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseModuleEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseModuleEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseModuleEntity.FieldParentId, entity.ParentId);
            sqlBuilder.SetValue(BaseModuleEntity.FieldCode, entity.Code);
            sqlBuilder.SetValue(BaseModuleEntity.FieldFullName, entity.FullName);
            sqlBuilder.SetValue(BaseModuleEntity.FieldCategoryCode, entity.CategoryCode);
            sqlBuilder.SetValue(BaseModuleEntity.FieldImageIndex, entity.ImageIndex);
            sqlBuilder.SetValue(BaseModuleEntity.FieldSelectedImageIndex, entity.SelectedImageIndex);
            sqlBuilder.SetValue(BaseModuleEntity.FieldNavigateUrl, entity.NavigateUrl);
            sqlBuilder.SetValue(BaseModuleEntity.FieldImagUrl, entity.ImagUrl);
            sqlBuilder.SetValue(BaseModuleEntity.FieldTarget, entity.Target);
            sqlBuilder.SetValue(BaseModuleEntity.FieldFormName, entity.FormName);
            sqlBuilder.SetValue(BaseModuleEntity.FieldAssemblyName, entity.AssemblyName);
            // sqlBuilder.SetValue(BaseModuleEntity.FieldPermissionScopeTables, entity.PermissionScopeTables);            
            sqlBuilder.SetValue(BaseModuleEntity.FieldIsMenu, entity.IsMenu);
            sqlBuilder.SetValue(BaseModuleEntity.FieldIsPublic, entity.IsPublic);
            sqlBuilder.SetValue(BaseModuleEntity.FieldIsScope, entity.IsScope);
            sqlBuilder.SetValue(BaseModuleEntity.FieldIsVisible, entity.IsVisible);
            sqlBuilder.SetValue(BaseModuleEntity.FieldExpand, entity.Expand);
            sqlBuilder.SetValue(BaseModuleEntity.FieldAllowEdit, entity.AllowEdit);
            sqlBuilder.SetValue(BaseModuleEntity.FieldAllowDelete, entity.AllowDelete);
            sqlBuilder.SetValue(BaseModuleEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseModuleEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseModuleEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseModuleEntity.FieldDescription, entity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseModuleEntity.FieldId, id));
        }

		protected override List<KeyValuePair<string, object>> GetDeleteExtParam(List<KeyValuePair<string, object>> parameters)
		{
			var result = base.GetDeleteExtParam(parameters);
			result.Add(new KeyValuePair<string, object>(BaseModuleEntity.FieldAllowDelete, 1));
			return result;
		}
    }
}
