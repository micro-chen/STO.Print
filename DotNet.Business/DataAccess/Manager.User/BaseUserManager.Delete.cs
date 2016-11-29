//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2011.10.17 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.10.17</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            int result = 0;

            result = this.Delete(new KeyValuePair<string, object>(BaseUserEntity.FieldId, id));

            // AfterDeleted(new object[] { id }, true, true);

            return result;
        }

        public override int SetDeleted(object[] ids, bool enabled = false, bool modifiedUser = false)
        {
            // FileUtil.WriteMessage("SetDeleted", System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");

            int result = 0;
            result = base.SetDeleted(ids, enabled, modifiedUser);

            // FileUtil.WriteMessage("AfterSetDeleted", System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            for (int i = 0; i < ids.Length; i++)
            {
                string id = ids[i].ToString();
                BaseUserEntity entity = GetObjectByCache(id);
                this.AfterUpdate(entity);
            }
           
            // result = AfterDeleted(ids, enabled, modifiedUser);
            return result;
        }
    }
}