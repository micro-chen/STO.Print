//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

	/// <summary>
	/// BaseUserLogOnManager
	/// 系统用户表登录信息
	///
	/// 修改记录
	///
	///		2013-04-21 版本：1.0 JiRiGaLa 创建主键。
	///
	/// <author>
	///		<name>JiRiGaLa</name>
	///		<date>2013-04-21</date>
	/// </author>
	/// </summary>
	public partial class BaseUserLogOnManager : BaseManager, IBaseManager
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public BaseUserLogOnManager()
		{
			if (base.dbHelper == null)
			{
				base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
			}
			if (string.IsNullOrEmpty(base.CurrentTableName))
			{
				base.CurrentTableName = BaseUserLogOnEntity.TableName;
			}
			// 不是自增量添加
			this.Identity = false;
		}

		/// <summary>
		/// 构造函数
		/// <param name="tableName">指定表名</param>
		/// </summary>
		public BaseUserLogOnManager(string tableName): this()
		{
			base.CurrentTableName = tableName;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dbHelper">数据库连接</param>
		public BaseUserLogOnManager(IDbHelper dbHelper)
			: this()
		{
			DbHelper = dbHelper;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="userInfo">用户信息</param>
		public BaseUserLogOnManager(BaseUserInfo userInfo)
			: this()
		{
			UserInfo = userInfo;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dbHelper">数据库连接</param>
		/// <param name="userInfo">用户信息</param>
		public BaseUserLogOnManager(IDbHelper dbHelper, BaseUserInfo userInfo)
			: this(dbHelper)
		{
			UserInfo = userInfo;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="userInfo">用户信息</param>
		/// <param name="tableName">指定表名</param>
		public BaseUserLogOnManager(BaseUserInfo userInfo, string tableName)
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
		public BaseUserLogOnManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
			: this(dbHelper, userInfo)
		{
			base.CurrentTableName = tableName;
		}

		/// <summary>
		/// 添加
		/// </summary>
		/// <param name="entity">实体</param>
		/// <returns>主键</returns>
		public string Add(BaseUserLogOnEntity entity)
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
		public string Add(BaseUserLogOnEntity entity, bool identity, bool returnId)
		{
			this.Identity = identity;
			this.ReturnId = returnId;
			return this.AddObject(entity);
		}

		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="id">主键</param>
		public BaseUserLogOnEntity GetObject(int? id)
		{
            return BaseEntity.Create<BaseUserLogOnEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, id)));
            // return BaseEntity.Create<BaseUserLogOnEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, id)));
		}

		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="id">主键</param>
		public BaseUserLogOnEntity GetObject(string id)
		{
            return BaseEntity.Create<BaseUserLogOnEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, id)));
            // return BaseEntity.Create<BaseUserLogOnEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, id)));
		}

		/// <summary>
		/// 添加实体
		/// </summary>
		/// <param name="entity">实体</param>
		public string AddObject(BaseUserLogOnEntity entity)
		{
			string result = string.Empty;
			if (string.IsNullOrEmpty(entity.Id))
			{
				BaseSequenceManager manager = new BaseSequenceManager(DbHelper, this.Identity);
				result = manager.Increment(this.CurrentTableName);
				entity.Id = result;
			}
			SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
			sqlBuilder.BeginInsert(this.CurrentTableName, BaseUserLogOnEntity.FieldId);
			if (!this.Identity)
			{
				sqlBuilder.SetValue(BaseUserLogOnEntity.FieldId, entity.Id);
			}
			else
			{
				if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
				{
					if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
					{
						sqlBuilder.SetFormula(BaseUserLogOnEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
					}
					if (DbHelper.CurrentDbType == CurrentDbType.DB2)
					{
						sqlBuilder.SetFormula(BaseUserLogOnEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
					}
				}
				else
				{
					if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
					{
						if (entity.Id == null)
						{
							if (string.IsNullOrEmpty(result))
							{
								BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
								result = sequenceManager.Increment(this.CurrentTableName);
							}
							entity.Id = result;
						}
						sqlBuilder.SetValue(BaseUserLogOnEntity.FieldId, entity.Id);
					}
				}
			}
			this.SetObject(sqlBuilder, entity);
			if (UserInfo != null)
			{
				sqlBuilder.SetValue(BaseUserLogOnEntity.FieldCreateUserId, UserInfo.Id);
				sqlBuilder.SetValue(BaseUserLogOnEntity.FieldCreateBy, UserInfo.RealName);
			}
			sqlBuilder.SetDBNow(BaseUserLogOnEntity.FieldCreateOn);
			if (UserInfo != null)
			{
				sqlBuilder.SetValue(BaseUserLogOnEntity.FieldModifiedUserId, UserInfo.Id);
				sqlBuilder.SetValue(BaseUserLogOnEntity.FieldModifiedBy, UserInfo.RealName);
			}
			sqlBuilder.SetDBNow(BaseUserLogOnEntity.FieldModifiedOn);
			if (DbHelper.CurrentDbType == CurrentDbType.SqlServer && this.Identity)
			{
				result = sqlBuilder.EndInsert().ToString();
			}
			else
			{
     			sqlBuilder.EndInsert();
                result = entity.Id;
            }
			return result;
		}

		/// <summary>
		/// 更新实体
		/// </summary>
		/// <param name="entity">实体</param>
		public int UpdateObject(BaseUserLogOnEntity entity)
		{
			SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
			sqlBuilder.BeginUpdate(this.CurrentTableName);
			this.SetObject(sqlBuilder, entity);
			if (UserInfo != null)
			{
				sqlBuilder.SetValue(BaseUserLogOnEntity.FieldModifiedUserId, UserInfo.Id);
				sqlBuilder.SetValue(BaseUserLogOnEntity.FieldModifiedBy, UserInfo.RealName);
			}
			sqlBuilder.SetDBNow(BaseUserLogOnEntity.FieldModifiedOn);
			sqlBuilder.SetWhere(BaseUserLogOnEntity.FieldId, entity.Id);
			return sqlBuilder.EndUpdate();
		}

		/// <summary>
		/// 设置实体
		/// </summary>
		/// <param name="entity">实体</param>
		private void SetObject(SQLBuilder sqlBuilder, BaseUserLogOnEntity entity)
		{
            // 2016-03-02 吉日嘎拉 增加按公司可以区别数据的功能。
            if (this.DbHelper.CurrentDbType == CurrentDbType.MySql)
            {
                sqlBuilder.SetValue(BaseUserContactEntity.FieldCompanyId, entity.CompanyId);
            }
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldUserPassword, entity.UserPassword);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldPasswordErrorCount, entity.PasswordErrorCount);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldMultiUserLogin, entity.MultiUserLogin);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldOpenId, entity.OpenId);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldQuestion, entity.Question);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldAnswerQuestion, entity.AnswerQuestion);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldChangePasswordDate, entity.ChangePasswordDate);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldSystemCode, entity.SystemCode);
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldEnabled, entity.Enabled);
			//sqlBuilder.SetValue(BaseUserLogOnEntity.FieldCommunicationPassword, entity.CommunicationPassword);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldAllowStartTime, entity.AllowStartTime);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldAllowEndTime, entity.AllowEndTime);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldLockStartDate, entity.LockStartDate);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldLockEndDate, entity.LockEndDate);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldFirstVisit, entity.FirstVisit);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldPreviousVisit, entity.PreviousVisit);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldLastVisit, entity.LastVisit);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldCheckIPAddress, entity.CheckIPAddress);
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldShowCount, entity.ShowCount);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldLogOnCount, entity.LogOnCount);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldUserOnLine, entity.UserOnLine);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldIPAddress, entity.IPAddress);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldIPAddressName, entity.IPAddressName);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldMACAddress, entity.MACAddress);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldSalt, entity.Salt);
			sqlBuilder.SetValue(BaseUserLogOnEntity.FieldPasswordStrength, entity.PasswordStrength);
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldNeedModifyPassword, entity.NeedModifyPassword);
		}
	}
}
