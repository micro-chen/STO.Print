//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    ///	BaseManager
    /// 通用基类部分
    /// 
    /// 总觉得自己写的程序不上档次，这些新技术也玩玩，也许做出来的东西更专业了。
    /// 修改记录
    /// 
    ///		2012.02.04 版本：1.5 JiRiGaLa 文件进行分割，简化处理。
    ///		2010.06.23 版本：1.4 JiRiGaLa 删除简化了一些重复的函数功能。
    ///		2007.11.22 版本：1.3 JiRiGaLa 创建没有BaseSystemInfo的构造函数。
    ///		2007.11.20 版本：1.2 JiRiGaLa 完善有数据库连接、当前用户信息的构造函数、增加NonSerialized。
    ///		2007.11.15 版本：1.1 JiRiGaLa 设置 SetParameter 函数功能。
    ///		2007.08.01 版本：1.0 JiRiGaLa 提炼了最基础的方法部分、觉得这些是很有必要的方法。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.02.04</date>
    /// </author> 
    /// </summary>
    public partial class BaseManager : IBaseManager
    {
        /// <summary>
        /// 任务标识
        /// </summary>
        public string TaskId = string.Empty;

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        // public void SetObjectExpand(SQLBuilder sqlBuilder);

        /// <summary>
        /// 数据表主键，需要用单一字段做为主键，建议默认为Id字段
        /// </summary>
        public string PrimaryKey = "Id";

        /// <summary>
        /// 是否自增量？大并发数据主键生成需要用这个方法
        /// 不是所有的情况下，都是在进行插入操作的，也不都是有Id字段的
        /// </summary>
        public bool Identity = true;

        /// <summary>
        /// 插入数据时，是否需要返回主键
        /// 默认都是需要插入操作时都要返回主键的
        /// </summary>
        public bool ReturnId = true;

        /// <summary>
        /// 通过远程接口调用
        /// </summary>
        public bool RemoteInterface = false;

        private string selectField = "*";
        /// <summary>
        /// 选取的字段
        /// </summary>
        public string SelectFields
        {
            get
            {
                return selectField;
            }
            set
            {
                selectField = value;
            }
        }

        /// <summary>
        /// 当前控制的表名
        /// </summary>
        public string CurrentTableName { get; set; }

        /// <summary>
        /// 当前索引
        /// </summary>
        public string CurrentIndex { get; set; }

        private static object locker = new Object();

        protected IDbHelper dbHelper = null;
        /// <summary>
        /// 当前的数据库连接
        /// </summary>
        public IDbHelper DbHelper
        {
            set
            {
                dbHelper = value;
            }
            get
            {
                if (dbHelper == null)
                {
                    lock (locker)
                    {
                        if (dbHelper == null)
                        {
                            dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterReadDbConnection);
                            // 是自动打开关闭数据库状态
                            dbHelper.MustCloseConnection = true;
                        }
                    }
                }
                return dbHelper;
            }
        }

        protected BaseUserInfo UserInfo = null;

        public BaseManager()
        {
        }

        public BaseManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        public BaseManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        public BaseManager(IDbHelper dbHelper, string tableName)
            : this(dbHelper)
        {
            CurrentTableName = tableName;
        }

        public BaseManager(string tableName)
            : this()
        {
            CurrentTableName = tableName;
        }

        public BaseManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        public BaseManager(BaseUserInfo userInfo, string tableName)
            : this(userInfo)
        {
            CurrentTableName = tableName;
        }

        public BaseManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            CurrentTableName = tableName;
        }

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public void SetConnection(IDbHelper dbHelper)
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 设置当前用户
        /// </summary>
        /// <param name="userInfo">当前用户</param>
        public void SetConnection(BaseUserInfo userInfo)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 设置数据库连接、当前用户
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">当前用户</param>
        public void SetConnection(IDbHelper dbHelper, BaseUserInfo userInfo)
        {
            this.SetConnection(dbHelper);
            UserInfo = userInfo;
        }

        public virtual void SetParameter(IDbHelper dbHelper)
        {
            DbHelper = dbHelper;
        }

        public virtual void SetParameter(BaseUserInfo userInfo)
        {
            UserInfo = userInfo;
        }

        public virtual void SetParameter(IDbHelper dbHelper, BaseUserInfo userInfo)
        {
            DbHelper = dbHelper;
            UserInfo = userInfo;
        }

        //
        // 类对应的数据库最终操作
        //
        public virtual string AddObject(object entity)
        {
            return string.Empty;
        }

        public virtual int UpdateObject(object entity)
        {
            return 0;
        }

        public virtual int DeleteObject(object id)
        {
            return DeleteObject(new KeyValuePair<string, object>(BaseBusinessLogic.FieldId, id));
        }

        public virtual int DeleteObject(params KeyValuePair<string, object>[] parameters)
        {
            List<KeyValuePair<string, object>> parametersList = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < parameters.Length; i++)
            {
                parametersList.Add(parameters[i]);
            }
			return MyDelete(parametersList);
			//return DbLogic.Delete(DbHelper, this.CurrentTableName, parametersList);
        }

        //
        // 对象事件触发器（编写程序的人员，可以不实现这些方法）
        //
        public virtual bool AddBefore()
        {
            // 这个函数需要覆盖
            return true;
        }

        public virtual bool AddAfter()
        {
            // 这个函数需要覆盖
            return true;
        }

        public virtual bool UpdateBefore()
        {
            // 这个函数需要覆盖
            return true;
        }

        public virtual bool UpdateAfter()
        {
            // 这个函数需要覆盖
            return true;
        }

        public virtual bool GetBefore(string id)
        {
            // 这个函数需要覆盖
            return true;
        }

        public virtual bool GetAfter(string id)
        {
            // 这个函数需要覆盖
            return true;
        }

        public virtual bool DeleteBefore(string id)
        {
            // 这个函数需要覆盖
            return true;
        }
        public virtual bool DeleteAfter(string id)
        {
            // 这个函数需要覆盖
            return true;
        }

        //
        // 批量操作保存
        //
        public virtual int BatchSave(DataTable dt)
        {
            return 0;
        }

        //
        // 状态码的获取
        //

        private string statusCode = Status.Error.ToString();
        /// <summary>
        /// 运行状态返回值
        /// </summary>
        public string StatusCode
        {
            get
            {
                return this.statusCode;
            }
            set
            {
                this.statusCode = value;
            }
        }

        private string statusMessage = string.Empty;
        /// <summary>
        /// 运行状态的信息
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }
            set
            {
                this.statusMessage = value;
            }
        }

        public string GetStateMessage()
        {
            this.StatusMessage = this.GetStateMessage(this.StatusCode);
            return this.StatusMessage;
        }

        public string GetStateMessage(string statusCode)
        {
            if (String.IsNullOrEmpty(statusCode))
            {
                return string.Empty;
            }
            Status status = (Status)Enum.Parse(typeof(Status), statusCode, true);
            return this.GetStateMessage(status);
        }

        #region public string GetStateMessage(StatusCode statusCode) 获得状态的信息
        /// <summary>
        /// 获得状态的信息
        /// </summary>
        /// <param name="statusCode">程序运行状态</param>
        /// <returns>返回信息</returns>
        public string GetStateMessage(Status statusCode)
        {
            string result = string.Empty;

            switch (statusCode)
            {
                case Status.AccessDeny:
                    result = AppMessage.MSG0800;
                    break;
                case Status.DbError:
                    result = AppMessage.MSG0002;
                    break;
                case Status.Error:
                    result = AppMessage.MSG0001;
                    break;
                case Status.OK:
                    result = AppMessage.MSG9965;
                    break;
                case Status.UserNotFound:
                    result = AppMessage.MSG9966;
                    break;
                case Status.PasswordError:
                    result = AppMessage.MSG9967;
                    break;
                case Status.LogOnDeny:
                    result = AppMessage.MSG9968;
                    break;
                case Status.ErrorOnLine:
                    result = AppMessage.MSG0048;
                    break;
                case Status.ErrorMacAddress:
                    result = AppMessage.MSG0049;
                    break;
                case Status.ErrorIPAddress:
                    result = string.Format(AppMessage.MSG0050, this.UserInfo.IPAddress);
                    break;
                case Status.ErrorOnLineLimit:
                    result = AppMessage.MSG0051;
                    break;
                case Status.PasswordCanNotBeNull:
                    result = AppMessage.Format(AppMessage.MSG0007, AppMessage.MSG9961);
                    break;
                case Status.PasswordCanNotBeRepeat:
                    result = AppMessage.Format(AppMessage.MSG0102);
                    break;
                case Status.ErrorDeleted:
                    result = AppMessage.MSG0005;
                    break;
                case Status.SetPasswordOK:
                    result = AppMessage.Format(AppMessage.MSG9963, AppMessage.MSG9964);
                    break;
                case Status.OldPasswordError:
                    result = AppMessage.Format(AppMessage.MSG0040, AppMessage.MSG9961);
                    break;
                case Status.ChangePasswordOK:
                    result = AppMessage.Format(AppMessage.MSG9962, AppMessage.MSG9964);
                    break;
                case Status.OKAdd:
                    result = AppMessage.MSG0009;
                    break;
                case Status.CanNotLock:
                    result = AppMessage.MSG0043;
                    break;
                case Status.LockOK:
                    result = AppMessage.MSG0044;
                    break;
                case Status.OKUpdate:
                    result = AppMessage.MSG0010;
                    break;
                case Status.OKDelete:
                    result = AppMessage.MSG0013;
                    break;
                case Status.Exist:
                    // "编号已存在,不可以重复."
                    result = AppMessage.Format(AppMessage.MSG0008, AppMessage.MSG9955);
                    break;
                case Status.ErrorCodeExist:
                    // "编号已存在,不可以重复."
                    result = AppMessage.Format(AppMessage.MSG0008, AppMessage.MSG9977);
                    break;
                case Status.ErrorNameExist:
                    // "名称已存在,不可以重复."
                    result = AppMessage.Format(AppMessage.MSG0008, AppMessage.MSG9978);
                    break;
                case Status.ErrorValueExist:
                    // "值已存在,不可以重复."
                    result = AppMessage.Format(AppMessage.MSG0008, AppMessage.MSG9800);
                    break;
                case Status.ErrorUserExist:
                    // "用户名已存在,不可以重复."
                    result = AppMessage.Format(AppMessage.MSG0008, AppMessage.MSG9957);
                    break;
                case Status.ErrorDataRelated:
                    result = AppMessage.MSG0033;
                    break;
                case Status.ErrorChanged:
                    result = AppMessage.MSG0006;
                    break;

                case Status.UserNotEmail:
                    result = AppMessage.MSG9910;
                    break;

                case Status.UserLocked:
                    result = AppMessage.MSG9911;
                    break;

                case Status.WaitForAudit:
                case Status.UserNotActive:
                    result = AppMessage.MSG9912;
                    break;

                case Status.UserIsActivate:
                    result = AppMessage.MSG9913;
                    break;

                case Status.NotFound:
                    result = AppMessage.MSG9956;
                    break;

                case Status.ErrorLogOn:
                    result = AppMessage.MSG9000;
                    break;

                case Status.UserDuplicate:
                    result = AppMessage.Format(AppMessage.MSG0008, AppMessage.MSG9957);
                    break;

                //// 开始审核
                //case AuditStatus.StartAudit:
                //    result = AppMessage.MSG0009;
                //    break;
                //// 审核通过
                //case AuditStatus.AuditPass:
                //    result = AppMessage.MSG0009;
                //    break;
                //// 待审核
                //case AuditStatus.WaitForAudit:
                //    result = AppMessage.MSG0010;
                //    break;
                //// 审核退回
                //case AuditStatus.AuditReject:
                //    result = AppMessage.MSG0009;
                //    break;
                //// 审核结束
                //case AuditStatus.AuditComplete:
                //    result = AppMessage.MSG0010;
                //    break;
                //// 提交成功。
                //case AuditStatus.SubmitOK:
                //    result = AppMessage.MSG0009;
                //    break;
            }
            this.StatusMessage = result;
            return result;
        }
        #endregion

        #region public int BatchSetCode(string[] ids, string[] codes) 重置编号
        /// <summary>
        /// 重置编号
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <param name="codes">编号数组</param>
        /// <returns>影响行数</returns>
        public int BatchSetCode(string[] ids, string[] codes)
        {
            int result = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                result += this.SetProperty(ids[i], new KeyValuePair<string, object>(BaseBusinessLogic.FieldCode, codes[i]));
            }
            return result;
        }
        #endregion

        ///
        /// 重新生成排序码
        ///

        #region public int BatchSetSortCode(string[] ids) 重置排序码
        /// <summary>
        /// 重置排序码
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        public int BatchSetSortCode(string[] ids)
        {
            int result = 0;
            BaseSequenceManager sequenceManager = new BaseSequenceManager(dbHelper);
            string[] sortCodes = sequenceManager.GetBatchSequence(this.CurrentTableName, ids.Length);
            for (int i = 0; i < ids.Length; i++)
            {
                result += this.SetProperty(ids[i], new KeyValuePair<string, object>(BaseBusinessLogic.FieldSortCode, sortCodes[i]));
            }
            return result;
        }
        #endregion

        #region public virtual int ResetSortCode() 重新设置表的排序码
        /// <summary>
        /// 重新设置表的排序码
        /// </summary>
        /// <returns>影响行数</returns>
        public virtual int ResetSortCode()
        {
            int result = 0;
            var dt = this.GetDataTable(0, BaseBusinessLogic.FieldSortCode);
            BaseSequenceManager sequenceManager = new BaseSequenceManager(dbHelper);
            string[] sortCode = sequenceManager.GetBatchSequence(this.CurrentTableName, dt.Rows.Count);
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                result += this.SetProperty(dr[BaseBusinessLogic.FieldId].ToString(), new KeyValuePair<string, object>(BaseBusinessLogic.FieldSortCode, sortCode[i]));
                i++;
            }
            return result;
        }
        #endregion

        #region public virtual int ChangeEnabled(object id) 变更有效状态
        /// <summary>
        /// 变更有效状态
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public virtual int ChangeEnabled(object id)
        {
            int result = 0;
            string sqlQuery = " UPDATE " + this.CurrentTableName
                            + " SET " + BaseBusinessLogic.FieldEnabled + " = (CASE " + BaseBusinessLogic.FieldEnabled + " WHEN 0 THEN 1 WHEN 1 THEN 0 END) "
                            + " WHERE (" + BaseBusinessLogic.FieldId + " = " + DbHelper.GetParameter(BaseBusinessLogic.FieldId) + ")";
            string[] names = new string[1];
            Object[] values = new Object[1];
            names[0] = BaseBusinessLogic.FieldId;
            values[0] = id;
            result = DbHelper.ExecuteNonQuery(sqlQuery, DbHelper.MakeParameters(names, values));
            return result;
        }
        #endregion
    }
}