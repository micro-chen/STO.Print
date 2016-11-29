//-----------------------------------------------------------------------
// <copyright file="ZtoPrintCancelManager.Auto.cs" company="STO">
//     Copyright (c) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.Business;
using DotNet.Model;
using DotNet.Utilities;
using STO.Print.Model;

namespace STO.Print.Manager
{
    /// <summary>
    /// ZtoPrintCancelManager
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2016-07-05 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2016-07-05</date>
    /// </author>
    /// </summary>
    public partial class ZtoPrintCancelManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ZtoPrintCancelManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = ZtoPrintCancelEntity.TableName;
            }
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public ZtoPrintCancelManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public ZtoPrintCancelManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public ZtoPrintCancelManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public ZtoPrintCancelManager(BaseUserInfo userInfo, string tableName)
            : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public ZtoPrintCancelManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public ZtoPrintCancelManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加, 这里可以人工干预，提高程序的性能
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式，表主键是否采用自增的策略</param>
        /// <param name="returnId">返回主键，不返回程序允许速度会快，主要是为了主细表批量插入数据优化用的</param>
        /// <returns>主键</returns>
        public string Add(ZtoPrintCancelEntity entity, bool identity = true, bool returnId = true)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            this.AddObject(entity);
            return entity.Id.ToString();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(ZtoPrintCancelEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public ZtoPrintCancelEntity GetObject(string id)
        {
            return BaseEntity.Create<ZtoPrintCancelEntity>(this.ExecuteReader(new KeyValuePair<string, object>(ZtoPrintCancelEntity.FieldId, id)));
        }

        public ZtoPrintCancelEntity GetObject(int id)
        {
            return BaseEntity.Create<ZtoPrintCancelEntity>(this.ExecuteReader(new KeyValuePair<string, object>(ZtoPrintCancelEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(ZtoPrintCancelEntity entity)
        {
            string key = string.Empty;
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!this.Identity)
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper);
                        entity.Id = sequenceManager.Increment(this.CurrentTableName);
                        sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetDBNow(ZtoPrintCancelEntity.FieldCreateOn);
            sqlBuilder.SetDBNow(ZtoPrintCancelEntity.FieldModifiedOn);
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
        public int UpdateObject(ZtoPrintCancelEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetDBNow(ZtoPrintCancelEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, ZtoPrintCancelEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, ZtoPrintCancelEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldSendPhone, entity.SendPhone);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldReceiveMan, entity.ReceiveMan);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldCreateUserName, entity.CreateUserName);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldModifiedSite, entity.ModifiedSite);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldModifiedUserName, entity.ModifiedUserName);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldOrderNumber, entity.OrderNumber);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldBillCode, entity.BillCode);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldSendMan, entity.SendMan);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldRemark, entity.Remark);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldCreateSite, entity.CreateSite);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldReceiveAddress, entity.ReceiveAddress);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldReceivePhone, entity.ReceivePhone);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldSendAddress, entity.SendAddress);
            sqlBuilder.SetValue(ZtoPrintCancelEntity.FieldSendProvince, entity.SendProvince);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(this.PrimaryKey, id));
        }
        /// <summary>
        /// 更新修改记录
        /// </summary>
        /// <param name="oldShow"> </param>
        /// <param name="newShow"> </param>

        public void UpdateModifyRecord(ZtoPrintCancelEntity oldShow, ZtoPrintCancelEntity newShow, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(DbHelper, this.UserInfo, tableName);
            foreach (var property in typeof(ZtoPrintCancelEntity).GetProperties())
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
                record.TableCode = ZtoPrintCancelEntity.TableName.ToUpper();
                record.TableDescription = FieldExtensions.ToDescription(typeof(ZtoPrintCancelEntity), "TableName");
                record.RecordKey = oldShow.Id.ToString();
                record.IPAddress = DotNet.Business.Utilities.GetIPAddress(true);
                record.CreateBy = UserInfo.RealName;
                record.CreateOn = DateTime.Now;
                manager.Add(record, true, false);
            }
        }
    }
}
