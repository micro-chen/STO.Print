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
    /// HRCheckInManager
    /// 上班签到 业务
    ///
    /// 修改记录
    ///
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///     2015-08-05 版本：2.0 SongBiao 改造成最新的版本
    ///
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    public partial class HRCheckInManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HRCheckInManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            base.CurrentTableName = HRCheckInEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public HRCheckInManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public HRCheckInManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public HRCheckInManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public HRCheckInManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public HRCheckInManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(HRCheckInEntity entity)
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
        public string Add(HRCheckInEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(HRCheckInEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public HRCheckInEntity GetObject(string id)
        {
            return BaseEntity.Create<HRCheckInEntity>(this.ExecuteReader(new KeyValuePair<string, object>(HRCheckInEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(HRCheckInEntity entity)
        {
            string key = string.Empty;
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = System.Guid.NewGuid().ToString("N");
            }
            key = entity.Id;
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!string.IsNullOrEmpty(entity.Id))
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
            }
            else
            {
                if ((DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_BASE_LOGINLOG.NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    entity.Id = System.Guid.NewGuid().ToString("N");
                    sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(HRCheckInEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(HRCheckInEntity.FieldCreateBy, UserInfo.RealName);
                sqlBuilder.SetValue(HRCheckInEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(HRCheckInEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(HRCheckInEntity.FieldCreateOn);
            sqlBuilder.SetDBNow(HRCheckInEntity.FieldModifiedOn);
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.Access))
            {
                key = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
            }
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
            {
                return entity.Id.ToString();
            }
            return key;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(HRCheckInEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(HRCheckInEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(HRCheckInEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(HRCheckInEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(HRCheckInEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, HRCheckInEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="entity"></param>
        private void SetObject(SQLBuilder sqlBuilder, HRCheckInEntity entity)
        {
            sqlBuilder.SetValue(HRCheckInEntity.FieldCompanyId, entity.CompanyId);
            sqlBuilder.SetValue(HRCheckInEntity.FieldCompanyName, entity.CompanyName);
            sqlBuilder.SetValue(HRCheckInEntity.FieldDepartmentId, entity.DepartmentId);
            sqlBuilder.SetValue(HRCheckInEntity.FieldDepartmentName, entity.DepartmentName);
            sqlBuilder.SetValue(HRCheckInEntity.FieldUserId, entity.UserId);
            sqlBuilder.SetValue(HRCheckInEntity.FieldUserName, entity.UserName);
            sqlBuilder.SetValue(HRCheckInEntity.FieldCheckInDay, entity.CheckInDay);
            sqlBuilder.SetValue(HRCheckInEntity.FieldAMStartTime, entity.AMStartTime);
            sqlBuilder.SetValue(HRCheckInEntity.FieldAMStartIp, entity.AMStartIp);
            sqlBuilder.SetValue(HRCheckInEntity.FieldAMEndTime, entity.AMEndTime);
            sqlBuilder.SetValue(HRCheckInEntity.FieldAMEndIp, entity.AMEndIp);
            sqlBuilder.SetValue(HRCheckInEntity.FieldPMStartTime, entity.PMStartTime);
            sqlBuilder.SetValue(HRCheckInEntity.FieldPMStartIp, entity.PMStartIp);
            sqlBuilder.SetValue(HRCheckInEntity.FieldPMEndTime, entity.PMEndTime);
            sqlBuilder.SetValue(HRCheckInEntity.FieldPMEndIp, entity.PMEndIp);
            sqlBuilder.SetValue(HRCheckInEntity.FieldNightStartTime, entity.NightStartTime);
            sqlBuilder.SetValue(HRCheckInEntity.FieldNightStartIp, entity.NightStartIp);
            sqlBuilder.SetValue(HRCheckInEntity.FieldNightEndTime, entity.NightEndTime);
            sqlBuilder.SetValue(HRCheckInEntity.FieldNightEndIp, entity.NightEndIp);
            sqlBuilder.SetValue(HRCheckInEntity.FieldDescription, entity.Description);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return Delete(new KeyValuePair<string, object>(HRCheckInEntity.FieldId, id));
        }

        public void UpdateModifyRecord(HRCheckInEntity oldShow, HRCheckInEntity newShow, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(DbHelper, this.UserInfo, tableName);
            foreach (var property in typeof(HRCheckInEntity).GetProperties())
            {
                var fieldDescription = property.GetCustomAttributes(typeof(FieldDescription), false).FirstOrDefault() as FieldDescription;
                var oldValue = Convert.ToString(property.GetValue(oldShow, null));
                var newValue = Convert.ToString(property.GetValue(newShow, null));

                if (!fieldDescription.NeedLog || oldValue == newValue)
                {
                    continue;
                }
                var record = new BaseModifyRecordEntity();
                record.ColumnCode = property.Name.ToUpper();
                record.ColumnDescription = fieldDescription.Text;
                record.NewValue = newValue;
                record.OldValue = oldValue;
                record.TableCode = HRCheckInEntity.TableName.ToUpper();
                record.TableDescription = FieldExtensions.ToDescription(typeof(HRCheckInEntity), "TableName");
                record.RecordKey = oldShow.Id.ToString();
                record.IPAddress = Utilities.GetIPAddress(true);
                record.CreateBy = UserInfo.RealName;
                record.CreateOn = DateTime.Now;
                BaseSequenceManager sequenceManager = new BaseSequenceManager(UserInfo);
                // 序列产生的ID  添加到TAB_EMPLOYEE表
                record.Id = int.Parse(sequenceManager.GetOracleSequence("ZTOA"));
                manager.Add(record, false, false);
            }
        }
    }
}