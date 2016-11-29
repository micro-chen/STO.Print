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
    /// ZtoUserManager
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-08-11 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2015-08-11</date>
    /// </author>
    /// </summary>
    public partial class ZtoUserManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ZtoUserManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = ZtoUserEntity.TableName;
            }
            base.PrimaryKey = ZtoUserEntity.FieldId;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public ZtoUserManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public ZtoUserManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public ZtoUserManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public ZtoUserManager(BaseUserInfo userInfo, string tableName)
            : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public ZtoUserManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public ZtoUserManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
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
        public string Add(ZtoUserEntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(ZtoUserEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public ZtoUserEntity GetObject(string id)
        {
            return BaseEntity.Create<ZtoUserEntity>(this.ExecuteReader(new KeyValuePair<string, object>(this.PrimaryKey, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(ZtoUserEntity entity)
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
                        //entity.Id = sequenceManager.Increment(this.CurrentTableName);
                        sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetDBNow(ZtoUserEntity.FieldCreateOn);
            sqlBuilder.SetDBNow(ZtoUserEntity.FieldModifiedOn);
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
        public int UpdateObject(ZtoUserEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetDBNow(ZtoUserEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, ZtoUserEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, ZtoUserEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(ZtoUserEntity.FieldPostcode, entity.Postcode);
            sqlBuilder.SetValue(ZtoUserEntity.FieldRealname, entity.Realname);
            sqlBuilder.SetValue(ZtoUserEntity.FieldCreateUserName, entity.CreateUserName);
            sqlBuilder.SetValue(ZtoUserEntity.FieldCityId, entity.CityId);
            sqlBuilder.SetValue(ZtoUserEntity.FieldModifiedUserName, entity.ModifiedUserName);
            sqlBuilder.SetValue(ZtoUserEntity.FieldDepartment, entity.Department);
            sqlBuilder.SetValue(ZtoUserEntity.FieldProvince, entity.Province);
            sqlBuilder.SetValue(ZtoUserEntity.FieldCounty, entity.County);
            sqlBuilder.SetValue(ZtoUserEntity.FieldCountyId, entity.CountyId);
            sqlBuilder.SetValue(ZtoUserEntity.FieldRemark, entity.Remark);
            sqlBuilder.SetValue(ZtoUserEntity.FieldProvinceId, entity.ProvinceId);
            sqlBuilder.SetValue(ZtoUserEntity.FieldCity, entity.City);
            sqlBuilder.SetValue(ZtoUserEntity.FieldAddress, entity.Address);
            sqlBuilder.SetValue(ZtoUserEntity.FieldIssendorreceive, entity.Issendorreceive);
            sqlBuilder.SetValue(ZtoUserEntity.FieldTelePhone, entity.TelePhone);
            sqlBuilder.SetValue(ZtoUserEntity.FieldMobile, entity.Mobile);
            sqlBuilder.SetValue(ZtoUserEntity.FieldCompany, entity.Company);
            sqlBuilder.SetValue(ZtoUserEntity.FieldIsDefault, entity.IsDefault);
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

        public void UpdateModifyRecord(ZtoUserEntity oldShow, ZtoUserEntity newShow, string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName + "_LOG";
            }
            BaseModifyRecordManager manager = new BaseModifyRecordManager(DbHelper, this.UserInfo, tableName);
            foreach (var property in typeof(ZtoUserEntity).GetProperties())
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
                record.TableCode = ZtoUserEntity.TableName.ToUpper();
                record.TableDescription = FieldExtensions.ToDescription(typeof(ZtoUserEntity), "TableName");
                record.RecordKey = oldShow.Id.ToString();
                record.IPAddress = DotNet.Business.Utilities.GetIPAddress(true);
                record.CreateBy = UserInfo.RealName;
                record.CreateOn = DateTime.Now;
                manager.Add(record, true, false);
            }
        }
    }
}
