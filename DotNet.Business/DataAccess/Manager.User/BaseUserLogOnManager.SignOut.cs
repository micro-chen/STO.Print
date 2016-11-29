//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserLogOnManager
    /// 退出系统管理
    /// 
    /// 修改记录
    /// 
    ///		2016.02.17 版本：2.1 JiRiGaLa 优化退出时代码逻辑，减少执行没必要的数据库连接。
    ///		2015.12.14 版本：2.0 JiRiGaLa 检查代码质量。
    ///		2015.02.02 版本：1.0 JiRiGaLa 分离代码。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.17</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserLogOnManager : BaseManager
    {
        #region public bool SignOut(string openId, bool createOpenId = true, string systemCode = "Base", string ipAddress = null, string macAddress = null) 用户退出
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="openId">信令</param>
        /// <param name="createOpenId">重新生成令牌</param>
        /// <returns>影响行数</returns>
        public bool SignOut(string openId, bool createOpenId = true, string systemCode = "Base", string ipAddress = null, string macAddress = null)
        {
            int result = 0;

            // 应该进行一次日志记录
            // 从缓存读取、效率高
            string id = string.Empty;
            if (!string.IsNullOrWhiteSpace(openId))
            {
                BaseUserEntity userEntity = BaseUserManager.GetObjectByOpenIdByCache(openId);
                if (userEntity != null && !string.IsNullOrEmpty(userEntity.Id))
                {
                    string ipAddressName = string.Empty;
                    if (!string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
                    }

                    BaseLoginLogManager.AddLog(systemCode, userEntity, ipAddress, ipAddressName, macAddress, Status.SignOut.ToDescription());

                    // 是否更新访问日期信息
                    if (!BaseSystemInfo.UpdateVisit)
                    {
                        return result > 0;
                    }

                    string sqlQuery = string.Empty;
                    // 最后一次登录时间
                    sqlQuery = " UPDATE " + BaseUserLogOnEntity.TableName
                                + " SET " + BaseUserLogOnEntity.FieldPreviousVisit + " = " + BaseUserLogOnEntity.FieldLastVisit;
                    if (createOpenId)
                    {
                        // sqlQuery += " , " + BaseUserLogOnEntity.FieldOpenId + " = '" + System.Guid.NewGuid().ToString("N") + "'";
                    }
                    sqlQuery += " , " + BaseUserLogOnEntity.FieldUserOnLine + " = 0 "
                              + " , " + BaseUserLogOnEntity.FieldLastVisit + " = " + this.DbHelper.GetDbNow();

                    sqlQuery += "  WHERE " + BaseUserLogOnEntity.FieldId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldId);

                    List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
                    dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldId, userEntity.Id));
                    result = this.DbHelper.ExecuteNonQuery(sqlQuery, dbParameters.ToArray());
                }
            }

            return result > 0;
        }
        #endregion
    }
}