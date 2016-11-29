//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserContactManager
    /// 用户管理
    /// 
    /// 修改纪录
    /// 
    ///		2014.05.08 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.05.08</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserContactManager : BaseManager
    {
        #region public virtual void AfterUpdate(BaseUserContactEntity entity) 用户添加后执行的方法
        /// <summary>
        /// 用户更新之后执行的方法
        /// </summary>
        /// <param name="entity">用户实体</param>
        public virtual void AfterUpdate(BaseUserContactEntity entity)
        {
            string connectionString = ConfigurationHelper.AppSettings("K8Connection", BaseSystemInfo.EncryptDbConnection);
            if (!string.IsNullOrEmpty(connectionString))
            {
                IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.Oracle, connectionString);
                string commandText = string.Format(@"UPDATE TAB_USER 
                                                        SET Mobile = {0} 
                                                            , Email = {1}
                                                            , QQ = {2}
                                                      WHERE Id = {3} "
                    , dbHelper.GetParameter("Mobile")
                    , dbHelper.GetParameter("Email")
                    , dbHelper.GetParameter("QQ")
                    , dbHelper.GetParameter("Id"));
                dbHelper.ExecuteNonQuery(commandText, new IDbDataParameter[] { 
                    dbHelper.MakeParameter("Mobile", entity.Mobile)
                    , dbHelper.MakeParameter("Email", entity.Email)
                    , dbHelper.MakeParameter("QQ", entity.QQ)
                    , dbHelper.MakeParameter("Id", entity.Id)
                });
            }
        }
        #endregion

        #region public void Synchronous() 数据同步
        /// <summary>
        /// 数据同步
        /// </summary>
        public void Synchronous()
        {
            string connectionString = ConfigurationHelper.AppSettings("K8Connection", BaseSystemInfo.EncryptDbConnection);
            if (!string.IsNullOrEmpty(connectionString))
            {
                BaseUserContactManager userContactManager = new Business.BaseUserContactManager();
                        
                string commandText = "SELECT Id FROM BaseUserContact WHERE (Mobile IS NOT NULL) ";
                using (IDataReader dataReader = DbHelper.ExecuteReader(commandText))
                {
                    while (dataReader.Read())
                    {
                        BaseUserContactEntity entity = userContactManager.GetObject(dataReader["ID"].ToString());
                        if (entity != null)
                        {
                            IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.Oracle, connectionString);
                            commandText = string.Format(@"UPDATE TAB_USER 
                                                        SET Mobile = {0} 
                                                            , Email = {1}
                                                            , QQ = {2}
                                                      WHERE Id = {3} "
                                , dbHelper.GetParameter("Mobile")
                                , dbHelper.GetParameter("Email")
                                , dbHelper.GetParameter("QQ")
                                , dbHelper.GetParameter("Id"));
                                dbHelper.ExecuteNonQuery(commandText, new IDbDataParameter[] { 
                                    dbHelper.MakeParameter("Mobile", entity.Mobile)
                                    , dbHelper.MakeParameter("Email", entity.Email)
                                    , dbHelper.MakeParameter("QQ", entity.QQ)
                                    , dbHelper.MakeParameter("Id", entity.Id)
                                });
                        }
                    }
                }
            }
        }
        #endregion
    }
}