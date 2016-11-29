//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// <author>
    ///		<name>Caihuajun</name>
    ///		<date>2008.09.16</date>
    /// </author>
    /// </summary>
    public partial class BaseNoteManager : BaseManager
    {
        public BaseNoteManager()
        {
            base.CurrentTableName = BaseNoteEntity.TableName;
        }

        public BaseNoteManager(IDbHelper dbHelper) : this()
        {
            DbHelper = dbHelper;
        }

        public BaseNoteManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        public BaseNoteManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseNoteEntity entity);

        #region private void SetObject(SQLBuilder sqlBuilder, BaseNoteEntity entity) 设置实体
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="sqlBuilder">SQL生成器</param>
        /// <param name="entity">实体对象</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseNoteEntity entity)
        {
            sqlBuilder.SetValue(BaseNoteEntity.FieldTitle, entity.Title);
            sqlBuilder.SetValue(BaseNoteEntity.FieldCategoryId, entity.CategoryId);
            sqlBuilder.SetValue(BaseNoteEntity.FieldCategoryFullName, entity.CategoryFullName);
            sqlBuilder.SetValue(BaseNoteEntity.FieldColor, entity.Color);
            sqlBuilder.SetValue(BaseNoteEntity.FieldContent, entity.Content);
            sqlBuilder.SetValue(BaseNoteEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseNoteEntity.FieldImportant, entity.Important);
            SetObjectExpand(sqlBuilder, entity);
        }
        #endregion

        public string Add(BaseNoteEntity entity)
        {
            return this.AddObject(entity);
        }

        #region public string AddObject(BaseNoteEntity entity) 添加一条记录
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>主键</returns>
        public string AddObject(BaseNoteEntity entity)
        {
            string id = Guid.NewGuid().ToString();
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginInsert(this.CurrentTableName);
            sqlBuilder.SetValue(BaseNoteEntity.FieldId, id);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseNoteEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetDBNow(BaseNoteEntity.FieldCreateOn);
            }
            sqlBuilder.SetDBNow(BaseNoteEntity.FieldModifiedOn);
            return sqlBuilder.EndInsert() > 0 ? id : string.Empty;
        }
        #endregion

        #region public int UpdateObject(BaseNoteEntity entity) 更新一条记录
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        public int UpdateObject(BaseNoteEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseNoteEntity.FieldModifiedUserId, UserInfo.Id);
            sqlBuilder.SetDBNow(BaseNoteEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseNoteEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }
        #endregion

        public int Update(BaseNoteEntity entity)
        {
            return this.UpdateObject(entity);
        }
    }
}