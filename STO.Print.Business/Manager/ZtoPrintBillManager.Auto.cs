//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace STO.Print.Manager
{
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using Model;

    /// <summary>
    /// ZtoPrintBillManager
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-07-16 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2015-07-16</date>
    /// </author>
    ///  </summary>
    public partial class ZtoPrintBillManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ZtoPrintBillManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = ZtoPrintBillEntity.TableName;
            }
            base.PrimaryKey = ZtoPrintBillEntity.FieldId;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public ZtoPrintBillManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public ZtoPrintBillManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public ZtoPrintBillManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public ZtoPrintBillManager(BaseUserInfo userInfo, string tableName)
            : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public ZtoPrintBillManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public ZtoPrintBillManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
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
        public string Add(ZtoPrintBillEntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;

            return this.AddObject(entity);
        }

        /// <summary>
        /// 添加, 这里可以人工干预，提高程序的性能
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式，表主键是否采用自增的策略</param>
        /// <param name="returnId">返回主键，不返回程序允许速度会快，主要是为了主细表批量插入数据优化用的</param>
        /// <returns>主键</returns>
        public string AddToRecycleBill(ZtoPrintBillEntity entity, bool identity = false, bool returnId = false, string dataSource = "0")
        {
            this.Identity = identity;
            this.ReturnId = returnId;

            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(ZtoPrintBillEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public ZtoPrintBillEntity GetObject(string id)
        {
            return BaseEntity.Create<ZtoPrintBillEntity>(this.ExecuteReader(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(ZtoPrintBillEntity entity, string dataSource = "0")
        {
            string key = string.Empty;
            if (entity.Id != null)
            {
                key = entity.Id.ToString();
            }
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
                        // entity.Id = sequenceManager.Increment(this.CurrentTableName);
                        sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
                    }
                }
            }

            this.SetObject(sqlBuilder, entity, dataSource);
            //if (entity.CreateOn.Year != 1)
            //{
            //    sqlBuilder.SetValue(ZtoPrintBillEntity.FieldCreateOn, entity.CreateOn);
            //}
            //else
            //{
            sqlBuilder.SetDBNow(ZtoPrintBillEntity.FieldCreateOn);
            //}
            sqlBuilder.SetDBNow(ZtoPrintBillEntity.FieldModifiedOn);
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
        public int UpdateObject(ZtoPrintBillEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetDBNow(ZtoPrintBillEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, ZtoPrintBillEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, ZtoPrintBillEntity entity, string dataSource = "0")
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendPostcode, entity.SendPostcode);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceiveMan, entity.ReceiveMan);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceiveProvince, entity.ReceiveProvince);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldCreateUserName, entity.CreateUserName);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldModifiedSite, entity.ModifiedSite);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldModifiedUserName, entity.ModifiedUserName);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldPaymentType, entity.PaymentType);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldLength, entity.Length);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendMan, entity.SendMan);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceiveAddress, entity.ReceiveAddress);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceivePostcode, entity.ReceivePostcode);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceivePhone, entity.ReceivePhone);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendDepartment, entity.SendDepartment);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendDate, entity.SendDate);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendProvince, entity.SendProvince);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldTranFee, entity.TranFee);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendDeparture, entity.SendDeparture);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendPhone, entity.SendPhone);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendCity, entity.SendCity);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldGoodsName, entity.GoodsName);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldWeight, entity.Weight);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldHeight, entity.Height);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceiveCounty, entity.ReceiveCounty);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceiveDestination, entity.ReceiveDestination);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendCompany, entity.SendCompany);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldBillCode, entity.BillCode);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldTotalNumber, entity.TotalNumber);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldRemark, entity.Remark);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldCreateSite, entity.CreateSite);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendSite, entity.SendSite);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldWidth, entity.Width);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceiveCity, entity.ReceiveCity);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendAddress, entity.SendAddress);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldSendCounty, entity.SendCounty);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldReceiveCompany, entity.ReceiveCompany);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldBigPen, entity.BigPen);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldOrderNumber, entity.OrderNumber);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldExpressId, entity.ExpressId);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldGoodsPayMent, entity.GOODS_PAYMENT);
            sqlBuilder.SetValue(ZtoPrintBillEntity.FieldToPayMent, entity.TOPAYMENT);
            if (this.CurrentTableName == "ZTO_RECYCLE_BILL")
            {
                sqlBuilder.SetValue("DATA_SOURCE", dataSource);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            return this.Delete(new KeyValuePair<string, object>(this.PrimaryKey, id));
        }
        /// <summary>
        /// 更新修改记录
        /// </summary>
        /// <param name="oldShow"> </param>
        /// <param name="newShow"> </param>

        public void UpdateModifyRecord(ZtoPrintBillEntity oldShow, ZtoPrintBillEntity newShow, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(DbHelper, this.UserInfo, tableName);
            foreach (var property in typeof(ZtoPrintBillEntity).GetProperties())
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
                record.TableCode = ZtoPrintBillEntity.TableName.ToUpper();
                record.TableDescription = FieldExtensions.ToDescription(typeof(ZtoPrintBillEntity), "TableName");
                record.RecordKey = oldShow.Id.ToString();
                record.IPAddress = DotNet.Business.Utilities.GetIPAddress(true);
                record.CreateBy = UserInfo.RealName;
                record.CreateOn = DateTime.Now;
                manager.Add(record, true, false);
            }
        }
    }
}
