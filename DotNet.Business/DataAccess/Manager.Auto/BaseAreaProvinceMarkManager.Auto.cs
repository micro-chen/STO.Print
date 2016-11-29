//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseAreaProvinceMarkManager
    /// 地区表(省、市、县)
    ///
    /// 修改记录
    ///
    ///		2015-06-23 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-06-23</date>
    /// </author>
    /// </summary>
    public partial class BaseAreaProvinceMarkManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseAreaProvinceMarkManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseAreaProvinceMarkEntity.TableName;
            }
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseAreaProvinceMarkManager(string tableName) : this()
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseAreaProvinceMarkManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseAreaProvinceMarkManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseAreaProvinceMarkManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseAreaProvinceMarkManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseAreaProvinceMarkEntity entity)
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
        public string Add(BaseAreaProvinceMarkEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseAreaProvinceMarkEntity GetObject(int? id)
        {
            return GetObject(id.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseAreaProvinceMarkEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseAreaProvinceMarkEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseAreaProvinceMarkEntity.FieldId, id)));
            // return BaseEntity.Create<BaseAreaProvinceMarkEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseAreaProvinceMarkEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseAreaProvinceMarkEntity entity)
        {
            string sequence = string.Empty;
            
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseAreaProvinceMarkEntity.FieldId);
            if (!string.IsNullOrEmpty(entity.Id) || !this.Identity)
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseAreaProvinceMarkEntity.FieldId, "SEQ_" + BaseAreaEntity.TableName + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseAreaProvinceMarkEntity.FieldId, "NEXT VALUE FOR SEQ_" + BaseAreaEntity.TableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        if (entity.Id == null)
                        {
                            if (string.IsNullOrEmpty(sequence))
                            {
                                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                                sequence = sequenceManager.Increment(this.CurrentTableName);
                            }
                            entity.Id = sequence;
                        }
                        sqlBuilder.SetValue(BaseAreaEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);

            // 创建人信息
            if (!string.IsNullOrEmpty(entity.CreateUserId))
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldCreateUserId, entity.CreateUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldCreateUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.CreateBy))
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldCreateBy, entity.CreateBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldCreateBy, UserInfo.RealName);
                }
            }
            if (entity.CreateOn.HasValue)
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldCreateOn, entity.CreateOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseAreaProvinceMarkEntity.FieldCreateOn);
            }
            
            // 修改人信息
            if (!string.IsNullOrEmpty(entity.ModifiedUserId))
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedUserId, entity.ModifiedUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.ModifiedBy))
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedBy, entity.ModifiedBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedBy, UserInfo.RealName);
                }
            }
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedOn, entity.ModifiedOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseAreaEntity.FieldModifiedOn);
            }

            if (DbHelper.CurrentDbType == CurrentDbType.SqlServer && this.Identity)
            {
                sequence = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
            }
            return sequence;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(BaseAreaProvinceMarkEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedBy, UserInfo.RealName);
            }
            // 若有修改时间标示，那就按修改时间来，不是按最新的时间来
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldModifiedOn, entity.ModifiedOn.Value);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseAreaProvinceMarkEntity.FieldModifiedOn);
            }
            sqlBuilder.SetWhere(BaseAreaProvinceMarkEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseAreaProvinceMarkEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseAreaProvinceMarkEntity entity)
        {
            sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldArea, entity.Area);
            sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldAreaId, entity.AreaId);
            sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldProvince, entity.Province);
            sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldProvinceId, entity.ProvinceId);
            sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldMark, entity.Mark);
            sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseAreaProvinceMarkEntity.FieldDescription, entity.Description);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseAreaProvinceMarkEntity.FieldId, id));
        }
    }
}
