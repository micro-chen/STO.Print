//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseRoleManager
    /// 角色表
    ///
    /// 修改记录
    ///
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    public partial class BaseRoleManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseRoleManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseRoleEntity.TableName;
            }
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseRoleManager(string tableName) : this()
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseRoleManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseRoleManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseRoleManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">指定表名</param>
        public BaseRoleManager(IDbHelper dbHelper, string tableName)
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
        public BaseRoleManager(BaseUserInfo userInfo, string tableName)
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
        public BaseRoleManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseRoleEntity entity)
        {
            return this.AddObject(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主鍵</param>
        /// <returns>主键</returns>
        public string Add(BaseRoleEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseRoleEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseRoleEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseRoleEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseRoleEntity.FieldId, id)));
            // return BaseEntity.Create<BaseRoleEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseRoleEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// 全局的角色不重复、多子系统之间的角色不重复，就厉害了，以后可以合并到一起也没关系了
        /// 通用基础子系统里的角色，可能在各子系统里也有权限需要设置的情况
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseRoleEntity entity)
        {
            string result = string.Empty;

            if (entity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                result = sequenceManager.Increment(BaseRoleEntity.TableName);
                entity.SortCode = int.Parse(result);
            }

            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseRoleEntity.FieldId);

            // 若是非空主键，表明已经指定了主键了
            if (!string.IsNullOrEmpty(entity.Id))
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.SetValue(BaseRoleEntity.FieldId, entity.Id);
                result = entity.Id;
            }
            else
            {
                if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    // 2015-12-23 吉日嘎拉 这里需要兼容一下以前的老的数据结构
                    sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + BaseRoleEntity.TableName.ToUpper() + ".NEXTVAL ");
                }
                else
                {
                    entity.Id = Guid.NewGuid().ToString("N");
                    result = entity.Id;
                    sqlBuilder.SetValue(BaseRoleEntity.FieldId, entity.Id);
                }
            }

            this.SetObject(sqlBuilder, entity);

            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseRoleEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseRoleEntity.FieldCreateBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseRoleEntity.FieldCreateOn);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseRoleEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseRoleEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseRoleEntity.FieldModifiedOn);

            if (DbHelper.CurrentDbType == CurrentDbType.SqlServer && this.Identity)
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
        public int UpdateObject(BaseRoleEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseRoleEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseRoleEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseRoleEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseRoleEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseRoleEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseRoleEntity entity)
        {
            sqlBuilder.SetValue(BaseRoleEntity.FieldOrganizeId, entity.OrganizeId);
            sqlBuilder.SetValue(BaseRoleEntity.FieldCode, entity.Code);
            sqlBuilder.SetValue(BaseRoleEntity.FieldRealName, entity.RealName);
            sqlBuilder.SetValue(BaseRoleEntity.FieldCategoryCode, entity.CategoryCode);
            sqlBuilder.SetValue(BaseRoleEntity.FieldAllowEdit, entity.AllowEdit);
            sqlBuilder.SetValue(BaseRoleEntity.FieldAllowDelete, entity.AllowDelete);
            sqlBuilder.SetValue(BaseRoleEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseRoleEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseRoleEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseRoleEntity.FieldIsVisible, entity.IsVisible);
            sqlBuilder.SetValue(BaseRoleEntity.FieldDescription, entity.Description);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return Delete(new KeyValuePair<string, object>(BaseRoleEntity.FieldId, id));
        }

		protected override List<KeyValuePair<string, object>> GetDeleteExtParam(List<KeyValuePair<string, object>> parameters)
		{
			var result = base.GetDeleteExtParam(parameters);
			result.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldAllowDelete, 1));
			return result;
		}
    }
}