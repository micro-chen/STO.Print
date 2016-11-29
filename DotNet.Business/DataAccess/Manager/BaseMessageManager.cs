//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack.Redis;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseMessageManager（程序OK）
    /// 消息表
    ///
    /// 修改记录
    ///     
    ///     2015.10.09 版本：2.3 JiRiGaLa 消息发送接口进行简化、提高健壮性。
    ///     2015.09.26 版本：2.2 JiRiGaLa 增加缓存功能。
    ///     2009.03.16 版本：2.1 JiRiGaLa 已发信息查询功能整理。
    ///     2009.02.20 版本：2.0 JiRiGaLa 主键分类，表结构进行改进，主键重新整理。
    ///     2008.04.15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.04.15</date>
    /// </author>
    /// </summary>
    public partial class BaseMessageManager : BaseManager
    {
        private string Query = "SELECT * FROM " + BaseMessageEntity.TableName;

        /// <summary>
        /// 发送消息
        /// 20151009 吉日嘎拉 消息发送接口参数最少化，能少一个参数，算一个参数，让调用的人更简单一些
        /// </summary>
        /// <param name="receiverId">接收者主键</param>
        /// <param name="contents">消息内容</param>
        /// <param name="functionCode">消息类型</param>
        /// <returns>消息实体</returns>
        public BaseMessageEntity Send(string receiverId, string contents, string functionCode = null)
        {
            BaseMessageEntity result = new BaseMessageEntity();

            result.Id = Guid.NewGuid().ToString("N");
            if (string.IsNullOrEmpty(functionCode))
            {
                functionCode = MessageFunction.UserMessage.ToString();
            }
            result.CategoryCode = MessageCategory.Send.ToString();
            result.FunctionCode = functionCode;
            result.ReceiverId = receiverId;
            result.Contents = contents;
            result.IsNew = (int)MessageStateCode.New;
            result.ReadCount = 0;
            result.DeletionStateCode = 0;
            result.CreateOn = DateTime.Now;

            if (this.UserInfo != null)
            {
                result.CreateCompanyId = this.UserInfo.CompanyId;
                result.CreateCompanyName = this.UserInfo.CompanyName;
                result.CreateDepartmentId = this.UserInfo.DepartmentId;
                result.CreateDepartmentName = this.UserInfo.DepartmentName;
                result.CreateUserId = this.UserInfo.Id;
                result.CreateBy = this.UserInfo.RealName;
            }

            // 发送消息
            this.Send(result, true);
            // 最近联系人, 这个处理得不太好，先去掉
            // this.SetRecent(receiverId);

            // 进行缓存处理
            // CacheProcessing(result);

            return result;
        }

        #region public int Send(BaseMessageEntity entity, bool saveSend = true) 添加短信，只能发给一个人
        /// <summary>
        /// 添加一条短信，只能发给一个人，在数据库中加入两条记录
        /// </summary>
        /// <param name="entity">添加对象</param>
        /// <returns>影响行数</returns>
        public int Send(BaseMessageEntity entity, bool saveSend = true)
        {
            string[] receiverIds = new string[1];
            receiverIds[0] = entity.ReceiverId.ToString();
            return this.Send(entity, receiverIds, saveSend);
        }
        #endregion

        #region public int Send(BaseMessageEntity entity, string[] receiverIds, bool saveSend = true) 添加短信，可以发给多个人
        /// <summary>
        /// 添加短信，可以发给多个人
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="receiverIds">接收者主键组</param>
        /// <param name="saveSend">保存每个发送记录</param>
        /// <returns>影响行数</returns>
        public int Send(BaseMessageEntity entity, string[] receiverIds, bool saveSend = true)
        {
            int result = 0;

            using (var redisClient = PooledRedisHelper.GetMessageClient())
            {
                result = Send(redisClient, entity, receiverIds, saveSend);
            }

            return result;
        }
        #endregion

        #region public int Send(IRedisClient redisClient, BaseMessageEntity entity, string[] receiverIds, bool saveSend = true) 添加短信，可以发给多个人
        /// <summary>
        /// 添加短信，可以发给多个人
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="receiverIds">接收者主键组</param>
        /// <param name="saveSend">保存每个发送记录</param>
        /// <returns>影响行数</returns>
        public int Send(IRedisClient redisClient, BaseMessageEntity entity, string[] receiverIds, bool saveSend = true, DateTime? expireAt = null)
        {
            // 每发一条短信，数据库中需要记录两条记录，他们的CreateUserId都为创建者ID。
            // 接收者多人的话，不要重复设置创建人的记录了，即对发送者来说，只要记录一条记录就够了  
            int result = 0;

            entity.CategoryCode = MessageCategory.Receiver.ToString();
            entity.IsNew = (int)MessageStateCode.New;

            BaseUserEntity userEntity = null;

            for (int i = 0; i < receiverIds.Length; i++)
            {
                entity.ReceiverId = receiverIds[i];
                // 没必要给自己发了, 其实给自己也需要发，否则不知道是否发送成功了没
                //if (entity.ReceiverId.Equals(UserInfo.Id))
                //{
                //    entity.IsNew = (int)MessageStateCode.Old;
                //    continue;
                //}
                // messageEntity.ParentId = null;
                entity.Id = Guid.NewGuid().ToString("N");
                entity.CategoryCode = MessageCategory.Receiver.ToString();
                userEntity = BaseUserManager.GetObjectByCache(receiverIds[i]);
                if (userEntity != null && !string.IsNullOrEmpty(userEntity.Id))
                {
                    entity.ReceiverRealName = userEntity.RealName;
                    // 发给了哪个部门的人，意义不大，是来自哪个部门的人，意义更大一些
                    entity.ReceiverDepartmentId = userEntity.DepartmentId;
                    entity.ReceiverDepartmentName = userEntity.DepartmentName;
                }
                entity.IsNew = 1;
                // 接收信息
                //string parentId = this.Add(entity, this.Identity, false);
                string parentId = this.AddObject(entity);

# if Redis
                // 20151018 吉日嘎拉 进行缓存处理, 让程序兼容不用缓存也可以用
                CacheProcessing(redisClient, entity, expireAt);
#endif

                if (saveSend)
                {
                    // 已发送信息
                    entity.Id = Guid.NewGuid().ToString("N");
                    entity.ParentId = parentId;
                    entity.IsNew = (int)MessageStateCode.Old;
                    entity.CategoryCode = MessageCategory.Send.ToString();
                    entity.DeletionStateCode = 0;
                    //this.Add(entity, this.Identity, false);
                    this.AddObject(entity);
                }
                result++;
            }

            // 是群发的消息，还是需要保存已发消息的功能
            if (!saveSend && receiverIds != null && receiverIds.Length > 1)
            {
                // 已发送信息
                entity.Id = Guid.NewGuid().ToString("N");
                entity.IsNew = (int)MessageStateCode.Old;
                entity.CategoryCode = MessageCategory.Send.ToString();
                entity.DeletionStateCode = 0;
                //this.Add(entity, this.Identity, false);
                this.AddObject(entity);
            }

            return result;
        }
        #endregion

        #region public int Send(BaseMessageEntity messageEntity, string organizeId, bool saveSend = true) 按部门群发短信
        /// <summary>
        /// 按部门群发短信
        /// </summary>
        /// <param name="messageEntity">实体</param>
        /// <param name="organizeId">部门主键</param>
        /// <returns>影响行数</returns>
        public int Send(BaseMessageEntity messageEntity, string organizeId, bool saveSend = true)
        {
            int result = 0;
            //int i = 0;
            BaseUserManager userManager = new BaseUserManager(UserInfo);
            List<BaseUserEntity> entityList = userManager.GetChildrenUserList(organizeId);
            List<string> receiverIds = new List<string>();
            foreach (var entity in entityList)
            {
                receiverIds.Add(entity.Id);
            }
            result = this.Send(messageEntity, receiverIds.ToArray(), saveSend);
            return result;
        }
        #endregion

        public int BatchSend(string[] receiverIds, string organizeId, string roleId, BaseMessageEntity entity, bool saveSend = true)
        {
            string[] organizeIds = null;
            string[] roleIds = null;
            if (!string.IsNullOrEmpty(organizeId))
            {
                organizeIds = new string[] { organizeId };
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                roleIds = new string[] { roleId };
            }
            return BatchSend(receiverIds, organizeIds, roleIds, entity, saveSend);
        }

        public int BatchSend(string receiverId, string organizeId, string roleId, BaseMessageEntity entity, bool saveSend = true)
        {
            string[] receiverIds = null;
            string[] organizeIds = null;
            string[] roleIds = null;
            if (!string.IsNullOrEmpty(receiverId))
            {
                receiverIds = new string[] { receiverId };
            }
            if (!string.IsNullOrEmpty(organizeId))
            {
                organizeIds = new string[] { organizeId };
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                roleIds = new string[] { roleId };
            }
            return BatchSend(receiverIds, organizeIds, roleIds, entity, saveSend);
        }

        #region public int BatchSend(string[] receiverIds, string[] ids, string[] roleIds, BaseMessageEntity entity, bool saveSend  = true) 批量发送消息
        /// <summary>
        /// 批量发送消息
        /// </summary>
        /// <param name="receiverIds">接收者主键组</param>
        /// <param name="ids">组织机构主键组</param>
        /// <param name="roleIds">角色主键组</param>
        /// <param name="content">内容</param>
        /// <returns>影响行数</returns>
        public int BatchSend(string[] receiverIds, string[] organizeIds, string[] roleIds, BaseMessageEntity entity, bool saveSend = true)
        {
            BaseUserManager userManager = new BaseUserManager(UserInfo);
            receiverIds = userManager.GetUserIds(receiverIds, organizeIds, roleIds);
            return this.Send(entity, receiverIds, saveSend);
        }
        #endregion

        #region public string Remind(string receiverId, string contents) 发送系统提示消息
        /// <summary>
        /// 发送系统提示消息
        /// </summary>
        /// <param name="receiverId">接收者主键</param>
        /// <param name="contents">内容</param>
        /// <returns>主键</returns>
        public string Remind(string receiverId, string contents)
        {
            string result = string.Empty;

            BaseMessageEntity entity = new BaseMessageEntity();
            entity.Id = Guid.NewGuid().ToString("N");
            entity.CategoryCode = MessageCategory.Receiver.ToString();
            entity.FunctionCode = MessageFunction.Remind.ToString();
            entity.ReceiverId = receiverId;
            entity.Contents = contents;
            entity.IsNew = (int)MessageStateCode.New;
            entity.ReadCount = 0;
            entity.DeletionStateCode = 0;
            entity.CreateOn = DateTime.Now;
            this.Identity = true;
            result = this.Add(entity);

# if Redis
            // 20151120 吉日嘎拉 让程序兼容不用缓存也可以用
            // 进行缓存处理
            CacheProcessing(entity);
#endif

            return result;
        }
        #endregion

        #region public int GetNewCount() 获取新信息个数
        /// <summary>
        /// 获取新信息个数
        /// </summary>
        /// <returns>记录个数</returns>
        public int GetNewCount()
        {
            return this.GetNewCount(MessageFunction.Message);
        }
        #endregion

        #region public int GetNewCount(MessageFunction messageFunction) 获取新信息个数
        /// <summary>
        /// 获取新信息个数，类别应该是收的信息，不是发的信息
        /// </summary>
        /// <returns>记录个数</returns>
        public int GetNewCount(MessageFunction messageFunction)
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(1) "
                            + "   FROM " + BaseMessageEntity.TableName
                            + "  WHERE (" + BaseMessageEntity.FieldIsNew + " = " + ((int)MessageStateCode.New).ToString() + " ) "
                            + "        AND (" + BaseMessageEntity.FieldCategoryCode + " = 'Receiver' )"
                            + "        AND (" + BaseMessageEntity.FieldReceiverId + " = '" + UserInfo.Id + "' )"
                            + "        AND (" + BaseMessageEntity.FieldDeletionStateCode + " = 0 )"
                            + "        AND (" + BaseMessageEntity.FieldFunctionCode + " = '" + messageFunction.ToString() + "' )";
            object returnObject = DbHelper.ExecuteScalar(sqlQuery);
            if (returnObject != null)
            {
                result = int.Parse(returnObject.ToString());
            }
            return result;
        }
        #endregion

        #region public BaseMessageEntity GetNewOne() 获取最新一条信息
        /// <summary>
        /// 获取最新一条信息
        /// </summary>
        /// <returns>记录个数</returns>
        public BaseMessageEntity GetNewOne()
        {
            BaseMessageEntity messageEntity = new BaseMessageEntity();
            string sqlQuery = "SELECT * "
                            + "   FROM (SELECT * FROM " + BaseMessageEntity.TableName + " WHERE (" + BaseMessageEntity.FieldIsNew + " = " + ((int)MessageStateCode.New).ToString() + " ) "
                            + "         AND (" + BaseMessageEntity.FieldReceiverId + " = '" + UserInfo.Id + "') "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn + " DESC) "
                            + " WHERE ROWNUM = 1 ";
            var dt = DbHelper.Fill(sqlQuery);
            return (BaseMessageEntity)messageEntity.GetSingle(dt);
        }
        #endregion

        #region public string[] MessageChek() 检查信息状态
        /// <summary>
        /// 检查信息状态
        /// </summary>
        /// <returns>信息状态数组</returns>
        public string[] MessageChek()
        {
            string[] result = new string[6];
            // 0.新信息的个数
            int messageCount = this.GetNewCount();
            result[0] = messageCount.ToString();
            if (messageCount > 0)
            {
                BaseMessageEntity messageEntity = this.GetNewOne();
                DateTime lastChekDate = DateTime.MinValue;
                if (messageEntity.CreateOn != null)
                {
                    // 1.最后检查时间
                    lastChekDate = Convert.ToDateTime(messageEntity.CreateOn);
                    result[1] = lastChekDate.ToString(BaseSystemInfo.DateTimeFormat);
                }
                result[2] = messageEntity.CreateUserId; // 2.最新消息的发出者
                result[3] = messageEntity.CreateBy; // 3.最新消息的发出者名称
                result[4] = messageEntity.Id;            // 4.最新消息的主键
                result[5] = messageEntity.Contents;       // 5.最新信息的内容
            }
            return result;
        }
        #endregion

        #region public BaseMessageEntity Read(string id) 阅读短信
        /// <summary>
        /// 阅读短信
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>数据权限</returns>
        public BaseMessageEntity Read(string id, bool setOld = false)
        {
            // 这里需要改进一下，运行一个高性能的sql语句就可以了，效率会高一些
            var dt = this.GetDataTableById(id);
            BaseMessageEntity entity = BaseEntity.Create<BaseMessageEntity>(dt);
            if (entity != null)
            {
                OnRead(this.UserInfo, id, setOld);
                entity.ReadCount++;
            }
            return entity;
        }
        #endregion

        #region public static int OnRead(string id, bool setOld = false) 阅读短信后设置状态值和阅读次数
        /// <summary>
        /// 阅读短信后设置状态值和阅读次数
        /// 修改为静态方法、提高执行效率。
        /// </summary>
        /// <param name="id">短信主键</param>
        /// <param name="setOld">设置为已读</param>
        /// <returns>影响的条数</returns>
        public static int OnRead(BaseUserInfo userInfo, string id, bool setOld = false)
        {
            int result = 0;

            // 2015-09-26 吉日嘎拉 从内存里把缓存的消息去掉
            using (var redisClient = PooledRedisHelper.GetMessageClient())
            {
                // 删除有序集合中的消息, 把这个人接收到的消息删除掉。
                redisClient.RemoveItemFromSortedSet(userInfo.Id, id);

                // 把消息删除掉, 具体的消息删除掉。
                if (redisClient.Remove("m" + id))
                {
                    result++;
                }
            }

            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.MessageDbType, BaseSystemInfo.MessageDbConnection))
            {
                SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
                sqlBuilder.BeginUpdate(BaseMessageEntity.TableName);
                if (setOld)
                {
                    sqlBuilder.SetValue(BaseMessageEntity.FieldIsNew, ((int)MessageStateCode.Old).ToString());
                }
                sqlBuilder.SetDBNow(BaseMessageEntity.FieldReadDate);
                // 增加阅读次数
                sqlBuilder.SetFormula(BaseMessageEntity.FieldReadCount, BaseMessageEntity.FieldReadCount + " + 1");
                sqlBuilder.SetWhere(BaseMessageEntity.FieldId, id);
                result = sqlBuilder.EndUpdate();
            }

            return result;
        }
        #endregion

        #region public DataTable ReadFromReceiver(string receiverId) 获取当前即时聊天者的所有新信息
        /// <summary>
        /// 获取当前即时聊天者的所有新信息
        /// </summary>
        /// <param name="receiverID">目标聊天者</param>
        /// <returns>数据表</returns>
        public DataTable ReadFromReceiver(string receiverId)
        {
            // 读取发给我的信息
            string sqlQuery = this.Query
                            + " WHERE (" + BaseMessageEntity.FieldIsNew + " = " + ((int)MessageStateCode.New).ToString() + " ) "
                            + " AND (" + BaseMessageEntity.FieldReceiverId + " = '" + UserInfo.Id + "' ) "
                            + " AND (" + BaseMessageEntity.FieldCreateUserId + " = '" + receiverId + "' ) "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            var dt = DbHelper.Fill(sqlQuery);
            // 标记为已读
            string id = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                // 这是别人发过来的信息
                if (dr[BaseMessageEntity.FieldReceiverId].ToString() == UserInfo.Id)
                {
                    id = dr[BaseMessageEntity.FieldId].ToString();
                    this.SetProperty(id, new KeyValuePair<string, object>(BaseMessageEntity.FieldIsNew, (int)MessageStateCode.Old));
                }
            }
            return dt;
        }
        #endregion

        #region public DataTable GetDataTableNew() 获取我的未读短信列表
        /// <summary>
        /// 获取我的未读短信列表
        /// </summary>		
        /// <returns>数据表</returns>
        public DataTable GetDataTableNew()
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseMessageEntity.FieldReceiverId, this.UserInfo.Id));
            parameters.Add(new KeyValuePair<string, object>(BaseMessageEntity.FieldCategoryCode, MessageCategory.Receiver.ToString()));
            parameters.Add(new KeyValuePair<string, object>(BaseMessageEntity.FieldIsNew, (int)MessageStateCode.New));
            parameters.Add(new KeyValuePair<string, object>(BaseMessageEntity.FieldDeletionStateCode, 0));
            return this.GetDataTable(parameters, 20, BaseMessageEntity.FieldCreateUserId + "," + BaseMessageEntity.FieldCreateOn);

            /*
            string sqlQuery = "   SELECT TOP 10 * "
                            + "     FROM " + BaseMessageEntity.TableName
                            + "    WHERE " + BaseMessageEntity.FieldIsNew + " = " + ((int)MessageStateCode.New).ToString()
                            + "          AND " + BaseMessageEntity.FieldReceiverId + " = " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId)
                            + "          AND " + BaseMessageEntity.FieldDeletionStateCode + " = 0 "
                            + "          AND " + BaseMessageEntity.FieldEnabled + " = 1 "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateUserId
                            + "          ," + BaseMessageEntity.FieldCreateOn;
            var result = new DataTable(BaseMessageEntity.TableName);
            DbHelper.Fill(result, sqlQuery, new IDbDataParameter[] { DbHelper.MakeParameter(BaseMessageEntity.FieldReceiverId, UserInfo.Id) });
            return result;
            */
        }
        #endregion

        #region public DataTable SearchNewList(string searchValue) 查询我的未读短信列表
        /// <summary>
        /// 查询我的未读短信列表
        /// </summary>
        /// <param name="search">查询字符</param>
        /// <returns>数据权限</returns>
        public DataTable SearchNewList(string searchValue)
        {
            if (searchValue.Length == 0)
            {
                return this.GetDataTableNew();
            }
            var dt = new DataTable(BaseMessageEntity.TableName);
            string sqlQuery = this.Query
                            + " WHERE ((" + BaseMessageEntity.FieldContents + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldContents) + " ) "
                            + " OR ( " + BaseMessageEntity.FieldTitle + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId) + " ) "
                            + " OR ( " + BaseMessageEntity.FieldReceiverRealName + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId) + " )) "
                            + " AND (" + BaseMessageEntity.FieldIsNew + " = " + ((int)MessageStateCode.New).ToString() + " ) "
                            + " AND (" + BaseMessageEntity.FieldReceiverId + " = " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId) + " ) "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            string[] names = new string[4];
            Object[] values = new Object[4];
            for (int i = 0; i < 3; i++)
            {
                names[i] = BaseMessageEntity.FieldContents;
                values[i] = searchValue;
            }
            names[3] = BaseMessageEntity.FieldReceiverId;
            values[3] = UserInfo.Id;
            DbHelper.Fill(dt, sqlQuery, DbHelper.MakeParameters(names, values));
            return dt;
        }
        #endregion

        #region public DataTable GetOldDT() 获取我的已读短信列表
        /// <summary>
        /// 获取我的已读短信列表
        /// </summary>		
        /// <returns>数据权限</returns>
        public DataTable GetOldDT()
        {
            var dt = new DataTable(BaseMessageEntity.TableName);
            string sqlQuery = this.Query
                            + " WHERE (" + BaseMessageEntity.FieldIsNew + " = " + ((int)MessageStateCode.Old).ToString() + " ) "
                            + " AND (" + BaseMessageEntity.FieldCategoryCode + " = '" + MessageCategory.Receiver.ToString() + "' ) "
                            + " AND (" + BaseMessageEntity.FieldDeletionStateCode + " = 0 ) "
                            + " AND (" + BaseMessageEntity.FieldReceiverId + " = " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId) + " ) "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            string[] names = new string[1];
            Object[] values = new Object[1];
            names[0] = BaseMessageEntity.FieldReceiverId;
            values[0] = UserInfo.Id;
            DbHelper.Fill(dt, sqlQuery, DbHelper.MakeParameters(names, values));
            return dt;
        }
        #endregion

        #region public DataTable SearchOldDT(string searchValue) 查询我的已读短信列表
        /// <summary>
        /// 查询我的已读短信列表
        /// </summary>
        /// <param name="search">查询字符</param>
        /// <returns>数据权限</returns>
        public DataTable SearchOldDT(string searchValue)
        {
            if (searchValue.Length == 0)
            {
                return this.GetOldDT();
            }
            var dt = new DataTable(BaseMessageEntity.TableName);
            string sqlQuery = this.Query
                            + " WHERE ((" + BaseMessageEntity.FieldContents + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldContents) + " ) "
                            + " OR (" + BaseMessageEntity.FieldReceiverRealName + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId) + " ) "
                            + " OR (" + BaseMessageEntity.FieldCreateOn + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldCreateOn) + " )) "
                            + " AND (" + BaseMessageEntity.FieldIsNew + " = " + ((int)MessageStateCode.Old).ToString() + " ) "
                            + " AND (" + BaseMessageEntity.FieldReceiverId + " = " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId) + " ) "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            string[] names = new string[4];
            Object[] values = new Object[4];
            for (int i = 0; i < 3; i++)
            {
                names[i] = BaseMessageEntity.FieldContents;
                values[i] = searchValue;
            }
            names[3] = BaseMessageEntity.FieldReceiverId;
            values[3] = UserInfo.Id;
            DbHelper.Fill(dt, sqlQuery, DbHelper.MakeParameters(names, values));
            return dt;
        }
        #endregion

        #region public DataTable GetDataTableByFunction(string categoryId, string functionId, string userId, string createUserId = null) 按消息功能获取消息列表
        /// <summary>
        /// 按消息功能获取消息列表
        /// </summary>
        /// <param name="categoryCode">消息分类</param>
        /// <param name="functionCode">消息功能</param>
        /// <param name="userId">用户主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByFunction(string categoryCode, string functionCode, string userId, string createUserId = null)
        {
            string sqlQuery = this.Query
                            + "    WHERE (" + BaseMessageEntity.FieldDeletionStateCode + " = 0 ) "
                            + "          AND (" + BaseMessageEntity.FieldCategoryCode + " = '" + DbHelper.SqlSafe(categoryCode) + "') ";
            if (!String.IsNullOrEmpty(functionCode))
            {
                sqlQuery += string.Format("          AND ({0} = '{1}' ) ", BaseMessageEntity.FieldFunctionCode, DbHelper.SqlSafe(functionCode));
            }
            if (!String.IsNullOrEmpty(createUserId))
            {
                sqlQuery += string.Format("          AND ({0} = '{1}' ) ", BaseMessageEntity.FieldFunctionCode, DbHelper.SqlSafe(createUserId));
            }
            if (categoryCode.Equals(MessageCategory.Send.ToString()))
            {
                // 已经发送出去的信息
                sqlQuery += string.Format("          AND ({0} = {1}) ", BaseMessageEntity.FieldCreateUserId, DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId));
            }
            else
            {
                // 已收到的信息
                sqlQuery += string.Format("          AND ({0} = {1}) ", BaseMessageEntity.FieldReceiverId, DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId));
            }

            sqlQuery += " ORDER BY " + BaseMessageEntity.FieldIsNew + " DESC "
                     + "          ," + BaseMessageEntity.FieldCreateOn;
            var dt = new DataTable(BaseMessageEntity.TableName);
            DbHelper.Fill(dt, sqlQuery, new IDbDataParameter[] { DbHelper.MakeParameter(BaseMessageEntity.FieldReceiverId, userId) });
            return dt;
        }
        #endregion

        public DataTable GetMessageDT(string createUserId, string searchValue = null)
        {
            searchValue = StringUtil.GetSearchString(searchValue);
            string sqlQuery = this.Query
                            + "    WHERE (" + BaseMessageEntity.FieldDeletionStateCode + " = 0 ) ";
            sqlQuery += string.Format("          AND ({0} = '{1}' ) ", BaseMessageEntity.FieldFunctionCode, MessageFunction.Message.ToString());
            // 已收到的信息
            sqlQuery += string.Format("          AND (( {0} = '{1}' AND {2} = '{3}' AND {4} = '{5}') ", BaseMessageEntity.FieldCategoryCode, MessageCategory.Receiver.ToString(), BaseMessageEntity.FieldReceiverId, UserInfo.Id, BaseMessageEntity.FieldCreateUserId, DbHelper.SqlSafe(createUserId));
            // 已发出的消息
            sqlQuery += string.Format("               OR ({0} = '{1}' AND {2} = '{3}' AND {4} = '{5}')) ", BaseMessageEntity.FieldCategoryCode, MessageCategory.Send.ToString(), BaseMessageEntity.FieldReceiverId, DbHelper.SqlSafe(createUserId), BaseMessageEntity.FieldCreateUserId, UserInfo.Id);

            if (!string.IsNullOrEmpty(searchValue))
            {
                sqlQuery += " AND ((" + BaseMessageEntity.FieldTitle + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldTitle) + " ) "
                          + " OR (" + BaseMessageEntity.FieldContents + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldContents) + " )) ";
            }

            sqlQuery += " ORDER BY " + BaseMessageEntity.FieldCreateOn + " DESC ";

            // sqlQuery += " ORDER BY " + BaseMessageEntity.FieldIsNew + " DESC "
            //          + "          ," + BaseMessageEntity.FieldCreateOn + " DESC ";
            // + "          ," + BaseMessageEntity.FieldCreateUserId;
            var dt = new DataTable(BaseMessageEntity.TableName);

            string[] names = new string[2];
            Object[] values = new Object[2];
            names[0] = BaseMessageEntity.FieldTitle;
            values[0] = searchValue;
            names[1] = BaseMessageEntity.FieldContents;
            values[1] = searchValue;
            // names[2] = BaseMessageEntity.FieldCreateBy;
            // values[2] = UserInfo.Id;
            DbHelper.Fill(dt, sqlQuery, DbHelper.MakeParameters(names, values));
            return dt;
        }

        public DataTable GetWarningDT(string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = UserInfo.Id;
            }
            return this.GetDataTableByFunction(MessageCategory.Receiver.ToString(), MessageFunction.Warning.ToString(), userId);
        }

        public DataTable GetWarningDT(string userId, int topN)
        {
            return this.SearchWarningDT(string.Empty, userId, topN);
        }

        public DataTable SearchWarningDT(string search, string userId = null, int topN = 0)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = UserInfo.Id;
            }
            if (search.Length == 0 && topN == 0)
            {
                return this.GetWarningDT();
            }
            search = StringUtil.GetSearchString(search);

            string sqlQuery = "SELECT ";
            if (topN != 0)
            {
                sqlQuery += " TOP " + topN.ToString();
            }
            sqlQuery += " * FROM " + BaseMessageEntity.TableName

                            + "    WHERE (" + BaseMessageEntity.FieldDeletionStateCode + " = 0 ) "
                            + "          AND (" + BaseMessageEntity.FieldCategoryCode + " = '" + MessageCategory.Receiver.ToString() + "') ";
            sqlQuery += string.Format("          AND ({0} = '{1}' ) ", BaseMessageEntity.FieldFunctionCode, MessageFunction.Warning.ToString());
            // 已收到的信息
            sqlQuery += string.Format("          AND ({0} = {1}) ", BaseMessageEntity.FieldReceiverId, DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId));

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseMessageEntity.FieldReceiverId, userId));

            if (!String.IsNullOrEmpty(search))
            {
                sqlQuery += " AND ((" + BaseMessageEntity.FieldTitle + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldTitle) + " ) "
                          + " OR (" + BaseMessageEntity.FieldContents + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldContents) + " ) "
                          + " OR (" + BaseMessageEntity.FieldCreateBy + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldCreateBy) + " )) ";

                dbParameters.Add(DbHelper.MakeParameter(BaseMessageEntity.FieldTitle, search));
                dbParameters.Add(DbHelper.MakeParameter(BaseMessageEntity.FieldContents, search));
                dbParameters.Add(DbHelper.MakeParameter(BaseMessageEntity.FieldCreateBy, search));
            }

            sqlQuery += " ORDER BY " + BaseMessageEntity.FieldIsNew + " DESC "
                     + "          ," + BaseMessageEntity.FieldCreateOn + " DESC ";
            var dt = new DataTable(BaseMessageEntity.TableName);

            DbHelper.Fill(dt, sqlQuery, dbParameters.ToArray());
            return dt;
        }

        #region public DataTable GetSendDT() 获取我的已发送短信列表
        /// <summary>
        /// 获取我的已发送短信列表
        /// </summary>		
        /// <returns>数据权限</returns>
        public DataTable GetSendDT()
        {
            string sqlQuery = this.Query
                            + " WHERE (" + BaseMessageEntity.FieldCategoryCode + " = '" + (MessageCategory.Send).ToString() + "') "
                            + " AND (" + BaseMessageEntity.FieldDeletionStateCode + " = 0) "
                            + " AND (" + BaseMessageEntity.FieldCreateUserId + " = '" + UserInfo.Id + "') "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public DataTable SearchSendDT(string searchValue) 查询我的已发送短信列表
        /// <summary>
        /// 查询我的已发送短信列表
        /// </summary>
        /// <param name="search">查询字符</param>
        /// <returns>数据权限</returns>
        public DataTable SearchSendDT(string searchValue)
        {
            if (searchValue.Length == 0)
            {
                return this.GetSendDT();
            }
            searchValue = StringUtil.GetSearchString(searchValue);
            var dt = new DataTable(BaseMessageEntity.TableName);
            string sqlQuery = this.Query
                            + " WHERE ((" + BaseMessageEntity.FieldContents + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldContents) + " ) "
                            + " OR (" + BaseMessageEntity.FieldReceiverRealName + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverRealName) + " ) "
                            + " OR (" + BaseMessageEntity.FieldCreateOn + " LIKE " + DbHelper.GetParameter(BaseMessageEntity.FieldCreateOn) + " )) "
                            + " AND (" + BaseMessageEntity.FieldDeletionStateCode + " = 0) "
                            + " AND (" + BaseMessageEntity.FieldCategoryCode + " = '" + (MessageCategory.Send).ToString() + "') "
                            + " AND (" + BaseMessageEntity.FieldCreateUserId + " = " + DbHelper.GetParameter(BaseMessageEntity.FieldCreateUserId) + " ) "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            string[] names = new string[4];
            Object[] values = new Object[4];
            names[0] = BaseMessageEntity.FieldContents;
            values[0] = searchValue;
            names[1] = BaseMessageEntity.FieldReceiverRealName;
            values[1] = searchValue;
            names[2] = BaseMessageEntity.FieldCreateOn;
            values[2] = searchValue;
            names[3] = BaseMessageEntity.FieldCreateUserId;
            values[3] = UserInfo.Id;
            DbHelper.Fill(dt, sqlQuery, DbHelper.MakeParameters(names, values));
            return dt;
        }
        #endregion

        #region public DataTable GetDeletedDT() 获取我的删除的短信列表
        /// <summary>
        /// 获取我的删除的短信列表
        /// </summary>		
        /// <returns>数据权限</returns>
        public DataTable GetDeletedDT()
        {
            var dt = new DataTable(BaseMessageEntity.TableName);
            string sqlQuery = this.Query
                            + " WHERE (" + BaseMessageEntity.FieldDeletionStateCode + " = 1 ) "
                            + " AND ((" + BaseMessageEntity.FieldReceiverId + " = " + DbHelper.GetParameter(BaseMessageEntity.FieldReceiverId) + " ) "
                            + " OR (" + BaseMessageEntity.FieldCreateUserId + " = " + DbHelper.GetParameter(BaseMessageEntity.FieldCreateUserId) + " AND " + BaseMessageEntity.FieldCategoryCode + " = '" + MessageCategory.Send.ToString() + "')) "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            DbHelper.Fill(dt, sqlQuery, new IDbDataParameter[] { 
				DbHelper.MakeParameter(BaseMessageEntity.FieldReceiverId, UserInfo.Id), 
				DbHelper.MakeParameter(BaseMessageEntity.FieldCreateUserId, UserInfo.Id) 
			});
            return dt;
        }
        #endregion

        #region public DataTable SearchDeletedDT(string searchValue) 查询我的已删除短信列表
        /// <summary>
        /// 查询我的已删除短信列表
        /// </summary>
        /// <param name="search">查询字符</param>
        /// <returns>数据权限</returns>
        public DataTable SearchDeletedDT(string searchValue)
        {
            if (searchValue.Length == 0)
            {
                return this.GetDeletedDT();
            }
            var dt = new DataTable(BaseMessageEntity.TableName);
            string sqlQuery = this.Query
                            + " WHERE ((" + BaseMessageEntity.FieldContents + " LIKE ? ) "
                            + " OR ( " + BaseMessageEntity.FieldReceiverRealName + " LIKE ? ) "
                            + " OR (" + BaseMessageEntity.FieldCreateOn + " LIKE ? )) "
                            + " AND (" + BaseMessageEntity.FieldDeletionStateCode + " = 1 ) "
                            + " AND (" + BaseMessageEntity.FieldReceiverId + " = ? ) "
                            + " ORDER BY " + BaseMessageEntity.FieldCreateOn;
            string[] names = new string[4];
            Object[] values = new Object[4];
            for (int i = 0; i < 3; i++)
            {
                names[i] = BaseMessageEntity.FieldContents;
                values[i] = searchValue;
            }
            names[3] = BaseMessageEntity.FieldReceiverId;
            values[3] = UserInfo.Id;
            DbHelper.Fill(dt, sqlQuery, DbHelper.MakeParameters(names, values));
            return dt;
        }
        #endregion


        public int SetRecent(string receiverId)
        {
            int result = 0;
            // 1: 更新最新联系人表
            SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
            sqlBuilder.BeginUpdate(BaseMessageRecentEntity.TableName);
            sqlBuilder.SetDBNow(BaseMessageRecentEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseMessageRecentEntity.FieldUserId, receiverId);
            sqlBuilder.SetWhere(BaseMessageRecentEntity.FieldTargetId, UserInfo.Id);
            if (sqlBuilder.EndUpdate() == 0)
            {
                // 2: 若没找到数据，进行插入操作。
                sqlBuilder.BeginInsert(BaseMessageRecentEntity.TableName);
                if (dbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    sqlBuilder.SetFormula(BaseMessageRecentEntity.FieldId, "SEQ_" + BaseMessageRecentEntity.TableName.ToUpper() + ".NEXTVAL ");
                }
                sqlBuilder.SetValue(BaseMessageRecentEntity.FieldUserId, receiverId);
                sqlBuilder.SetValue(BaseMessageRecentEntity.FieldTargetId, UserInfo.Id);
                sqlBuilder.SetValue(BaseMessageRecentEntity.FieldRealName, UserInfo.RealName);
                sqlBuilder.SetValue(BaseMessageRecentEntity.FieldCompanyName, UserInfo.CompanyName);
                sqlBuilder.SetValue(BaseMessageRecentEntity.FieldDepartmentName, UserInfo.DepartmentName);
                result += sqlBuilder.EndInsert();
            }

            sqlBuilder.BeginUpdate(BaseMessageRecentEntity.TableName);
            sqlBuilder.SetDBNow(BaseMessageRecentEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseMessageRecentEntity.FieldUserId, UserInfo.Id);
            sqlBuilder.SetWhere(BaseMessageRecentEntity.FieldTargetId, receiverId);
            if (sqlBuilder.EndUpdate() == 0)
            {
                // 2: 若没找到数据，进行插入操作。
                // 这个可以考虑从缓存里读取
                BaseUserEntity userEntity = null;
                if (ValidateUtil.IsInt(receiverId))
                {
                    userEntity = new BaseUserManager(UserInfo).GetObject(receiverId);
                }
                sqlBuilder.BeginInsert(BaseMessageRecentEntity.TableName);
                if (dbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    sqlBuilder.SetFormula(BaseMessageRecentEntity.FieldId, "SEQ_" + BaseMessageRecentEntity.TableName.ToUpper() + ".NEXTVAL ");
                }
                sqlBuilder.SetValue(BaseMessageRecentEntity.FieldUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseMessageRecentEntity.FieldTargetId, receiverId);
                if (userEntity != null)
                {
                    sqlBuilder.SetValue(BaseMessageRecentEntity.FieldRealName, userEntity.RealName);
                    sqlBuilder.SetValue(BaseMessageRecentEntity.FieldCompanyName, userEntity.CompanyName);
                    sqlBuilder.SetValue(BaseMessageRecentEntity.FieldDepartmentName, userEntity.DepartmentName);
                }
                else
                {
                    /*
                    string realName = GetExternalUserName(receiverId);
                    sqlBuilder.SetValue(BaseMessageRecentEntity.FieldRealName, realName);
                    */
                }
                result += sqlBuilder.EndInsert();
            }
            return result;
        }
    }
}