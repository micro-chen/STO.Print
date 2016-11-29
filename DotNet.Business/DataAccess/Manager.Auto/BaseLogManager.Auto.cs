//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseLogManager
    /// 日志的基类（程序OK）
    /// 
    /// 想在这里实现访问记录、继承以前的比较好的思路。
    ///
    /// 修改记录
    /// 
    ///		2016.02.14 版本：1.0 JiRiGaLa   代码进行分离。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.14</date>
    /// </author> 
    /// </summary>
    public partial class BaseLogManager : BaseManager
    {
        public BaseLogManager()
        {
            // 用自增量的方式
            base.Identity = true;
            base.ReturnId = false;

            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterWriteDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseLogEntity.TableName;
            }
        }

        public BaseLogManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        public BaseLogManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        public BaseLogManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        #region public string AddObject(BaseLogEntity logEntity)
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="entity">日志对象</param>
        /// <returns>主键</returns>
        public string AddObject(BaseLogEntity entity)
        {
            // 2013-10-06 JiRiGala 改进为添加到消息队列里，提高系统的性能
            // System.Messaging.MessageQueue messageQueue = new System.Messaging.MessageQueue(".\\Private$\\DotNetLog");
            // System.Messaging.StatusMessage message = new System.Messaging.StatusMessage();
            // message.Body = entity;
            // message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
            // messageQueue.Send(message);

            if (!BaseSystemInfo.RecordLog)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = System.Guid.NewGuid().ToString("N");
            }

            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.Identity);
            sqlBuilder.SetValue(BaseLogEntity.FieldId, entity.Id);
            sqlBuilder.SetValue(BaseLogEntity.FieldTaskId, entity.TaskId);
            sqlBuilder.SetValue(BaseLogEntity.FieldService, entity.Service);
            sqlBuilder.SetValue(BaseLogEntity.FieldStartTime, entity.StartTime);
            sqlBuilder.SetValue(BaseLogEntity.FieldUserId, entity.UserId);
            sqlBuilder.SetValue(BaseLogEntity.FieldUserRealName, entity.UserRealName);
            sqlBuilder.SetValue(BaseLogEntity.FieldCompanyId, entity.CompanyId);
            sqlBuilder.SetValue(BaseLogEntity.FieldUrlReferrer, entity.UrlReferrer);
            sqlBuilder.SetValue(BaseLogEntity.FieldWebUrl, entity.WebUrl);
            sqlBuilder.SetValue(BaseLogEntity.FieldParameters, entity.Parameters);
            sqlBuilder.SetValue(BaseLogEntity.FieldClientIP, entity.ClientIP);
            sqlBuilder.SetValue(BaseLogEntity.FieldServerIP, entity.ServerIP);
            sqlBuilder.SetValue(BaseLogEntity.FieldElapsedTicks, entity.ElapsedTicks);
            sqlBuilder.SetValue(BaseLogEntity.FieldDescription, entity.Description);

            // sqlBuilder.SetDBNow(BaseLogEntity.FieldStartTime);
            sqlBuilder.EndInsert();

            return entity.Id;
        }
        #endregion
    }
}