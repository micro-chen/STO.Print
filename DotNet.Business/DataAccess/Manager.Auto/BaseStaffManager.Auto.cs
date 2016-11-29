//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseStaffManager
    /// 员工（职员）表
    /// 
    /// 修改记录
    /// 
    /// 2012-07-07 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-07-07</date>
    /// </author>
    /// </summary>
    public partial class BaseStaffManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseStaffManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            base.CurrentTableName = BaseStaffEntity.TableName;
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseStaffManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseStaffManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseStaffManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseStaffManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public BaseStaffManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主键</param>
        /// <returns>主键</returns>
        public string Add(BaseStaffEntity entity, bool identity = true, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseStaffEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseStaffEntity GetObject(string id)
        {
            return GetObject(int.Parse(id));
        }

        public BaseStaffEntity GetObject(int id)
        {
            return BaseEntity.Create<BaseStaffEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseStaffEntity.FieldId, id)));
            // return BaseEntity.Create<BaseStaffEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseStaffEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseStaffEntity entity)
        {
            string sequence = string.Empty;
            if (entity.SortCode == null || entity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                entity.SortCode = int.Parse(sequence);
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseStaffEntity.FieldId);
            if (entity.Id != null || !this.Identity)
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(BaseStaffEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseStaffEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseStaffEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
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
                            entity.Id = int.Parse(sequence);
                        }
                        sqlBuilder.SetValue(BaseStaffEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseStaffEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseStaffEntity.FieldCreateBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseStaffEntity.FieldCreateOn);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseStaffEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseStaffEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseStaffEntity.FieldModifiedOn);
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.Access))
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
        public int UpdateObject(BaseStaffEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseStaffEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseStaffEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseStaffEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseStaffEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseStaffEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseStaffEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseStaffEntity.FieldUserId, entity.UserId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldUserName, entity.UserName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldRealName, entity.RealName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCode, entity.Code);
            sqlBuilder.SetValue(BaseStaffEntity.FieldGender, entity.Gender);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCompanyId, entity.CompanyId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCompanyName, entity.CompanyName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldSubCompanyId, entity.SubCompanyId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDepartmentId, entity.DepartmentId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDepartmentName, entity.DepartmentName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldWorkgroupId, entity.WorkgroupId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldWorkgroupName, entity.WorkgroupName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldQuickQuery, entity.QuickQuery);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDutyId, entity.DutyId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldIdentificationCode, entity.IdentificationCode);
            sqlBuilder.SetValue(BaseStaffEntity.FieldIDCard, entity.IDCard);
            sqlBuilder.SetValue(BaseStaffEntity.FieldBankCode, entity.BankCode);
            sqlBuilder.SetValue(BaseStaffEntity.FieldBankName, entity.BankName);
            // sqlBuilder.SetValue(BaseStaffEntity.FieldBankUserName, entity.BankUserName);
            sqlBuilder.SetValue(BaseStaffEntity.FieldRewarCard, entity.RewarCard);
            sqlBuilder.SetValue(BaseStaffEntity.FieldMedicalCard, entity.MedicalCard);
            sqlBuilder.SetValue(BaseStaffEntity.FieldUnionMember, entity.UnionMember);
            sqlBuilder.SetValue(BaseStaffEntity.FieldEmail, entity.Email);
            sqlBuilder.SetValue(BaseStaffEntity.FieldMobile, entity.Mobile);
            sqlBuilder.SetValue(BaseStaffEntity.FieldShortNumber, entity.ShortNumber);
            sqlBuilder.SetValue(BaseStaffEntity.FieldTelephone, entity.Telephone);
            sqlBuilder.SetValue(BaseStaffEntity.FieldExtension, entity.Extension);
            sqlBuilder.SetValue(BaseStaffEntity.FieldQQ, entity.QQ);
            sqlBuilder.SetValue(BaseStaffEntity.FieldOfficePhone, entity.OfficePhone);
            sqlBuilder.SetValue(BaseStaffEntity.FieldOfficePostCode, entity.OfficePostCode);
            sqlBuilder.SetValue(BaseStaffEntity.FieldOfficeAddress, entity.OfficeAddress);
            sqlBuilder.SetValue(BaseStaffEntity.FieldOfficeFax, entity.OfficeFax);
            sqlBuilder.SetValue(BaseStaffEntity.FieldAge, entity.Age);
            sqlBuilder.SetValue(BaseStaffEntity.FieldBirthday, entity.Birthday);
            sqlBuilder.SetValue(BaseStaffEntity.FieldHeight, entity.Height);
            sqlBuilder.SetValue(BaseStaffEntity.FieldWeight, entity.Weight);
            sqlBuilder.SetValue(BaseStaffEntity.FieldMarriage, entity.Marriage);
            sqlBuilder.SetValue(BaseStaffEntity.FieldEducation, entity.Education);
            sqlBuilder.SetValue(BaseStaffEntity.FieldSchool, entity.School);
            sqlBuilder.SetValue(BaseStaffEntity.FieldGraduationDate, entity.GraduationDate);
            sqlBuilder.SetValue(BaseStaffEntity.FieldMajor, entity.Major);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDegree, entity.Degree);
            sqlBuilder.SetValue(BaseStaffEntity.FieldTitleId, entity.TitleId);
            sqlBuilder.SetValue(BaseStaffEntity.FieldTitleDate, entity.TitleDate);
            sqlBuilder.SetValue(BaseStaffEntity.FieldTitleLevel, entity.TitleLevel);
            sqlBuilder.SetValue(BaseStaffEntity.FieldWorkingDate, entity.WorkingDate);
            sqlBuilder.SetValue(BaseStaffEntity.FieldJoinInDate, entity.JoinInDate);
            sqlBuilder.SetValue(BaseStaffEntity.FieldHomePostCode, entity.HomePostCode);
            sqlBuilder.SetValue(BaseStaffEntity.FieldHomeAddress, entity.HomeAddress);
            sqlBuilder.SetValue(BaseStaffEntity.FieldHomePhone, entity.HomePhone);
            sqlBuilder.SetValue(BaseStaffEntity.FieldHomeFax, entity.HomeFax);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCarCode, entity.CarCode);
            sqlBuilder.SetValue(BaseStaffEntity.FieldProvince, entity.Province);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCity, entity.City);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDistrict, entity.District);
            sqlBuilder.SetValue(BaseStaffEntity.FieldNativePlace, entity.NativePlace);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCurrentProvince, entity.CurrentProvince);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCurrentCity, entity.CurrentCity);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCurrentDistrict, entity.CurrentDistrict);
            sqlBuilder.SetValue(BaseStaffEntity.FieldParty, entity.Party);
            sqlBuilder.SetValue(BaseStaffEntity.FieldNation, entity.Nation);
            sqlBuilder.SetValue(BaseStaffEntity.FieldNationality, entity.Nationality);
            sqlBuilder.SetValue(BaseStaffEntity.FieldWorkingProperty, entity.WorkingProperty);
            sqlBuilder.SetValue(BaseStaffEntity.FieldCompetency, entity.Competency);
            sqlBuilder.SetValue(BaseStaffEntity.FieldEmergencyContact, entity.EmergencyContact);
            sqlBuilder.SetValue(BaseStaffEntity.FieldWeddingday, entity.Weddingday);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDivorce, entity.Divorce);
            sqlBuilder.SetValue(BaseStaffEntity.FieldChildbirthday1, entity.Childbirthday1);
            sqlBuilder.SetValue(BaseStaffEntity.FieldChildbirthday2, entity.Childbirthday2);
            sqlBuilder.SetValue(BaseStaffEntity.FieldIsDimission, entity.IsDimission);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDimissionDate, entity.DimissionDate);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDimissionCause, entity.DimissionCause);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDimissionWhither, entity.DimissionWhither);
            sqlBuilder.SetValue(BaseStaffEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseStaffEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseStaffEntity.FieldDescription, entity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseStaffEntity.FieldId, id));
        }
    }
}
