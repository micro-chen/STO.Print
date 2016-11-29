//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;
    using Newtonsoft.Json;

    /// <summary>
    /// CommentUtilities 
    /// 评论服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2016.04.07 版本：1.0 YangHengLian  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016.04.07</date>
    /// </author>
    public class CommentUtilities
    {
        /// <summary>
        /// 请求Url
        /// </summary>
        private static readonly string Url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/CommentService.ashx";

        /// <summary>
        /// 分页获取评论信息
        /// </summary>
        /// <param name="userInfo">登录用户实体</param>
        /// <param name="categoryCode">评论类型，是网点评论，还是用户评论</param>
        /// <param name="objectId">评论对象Id，比如对哪个网点评论的，哪个人评论的</param>
        /// <param name="currentPageIndex">页码</param>
        /// <param name="pageSize">每页获取数量</param>
        /// <returns></returns>
        public static JsonResult<List<BaseCommentEntity>> GetListByPage(BaseUserInfo userInfo, string categoryCode = null, string objectId = null, int currentPageIndex = 1, int pageSize = 10)
        {
            var webClient = new WebClient();
            var postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetListByPage");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("pageIndex", currentPageIndex.ToString());
            postValues.Add("pageSize", pageSize.ToString());
            if (categoryCode != null)
            {
                postValues.Add("categoryCode", categoryCode);
            }
            if (!string.IsNullOrEmpty(objectId))
            {
                postValues.Add("objectId", objectId);
            }
            byte[] responseArray = webClient.UploadValues(Url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<JsonResult<List<BaseCommentEntity>>>(response);
            }
            return null;
        }

        /// <summary>
        /// 获取评论回复记录
        /// </summary>
        /// <param name="userInfo">登录用户实体</param>
        /// <param name="id">评论Id</param>
        /// <param name="categoryCode">评论类型，是网点评论，还是用户评论</param>
        /// <returns></returns>
        public static JsonResult<List<BaseCommentEntity>> GetReplyList(BaseUserInfo userInfo, string id, string categoryCode = null)
        {
            var webClient = new WebClient();
            var postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetReplyList");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", id);
            if (categoryCode != null)
            {
                postValues.Add("categoryCode", categoryCode);
            }
            byte[] responseArray = webClient.UploadValues(Url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<JsonResult<List<BaseCommentEntity>>>(response);
            }
            return null;
        }

        /// <summary>
        /// 根据Id删除评论
        /// </summary>
        /// <param name="userInfo">登录用户信息</param>
        /// <param name="id">评论Id</param>
        /// <returns></returns>
        public static BaseResult SetDelete(BaseUserInfo userInfo, string id)
        {
            var webClient = new WebClient();
            var postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "SetDelete");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", id);
            byte[] responseArray = webClient.UploadValues(Url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<BaseResult>(response);
            }
            return null;
        }

        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="userInfo">登录用户信息</param>
        /// <param name="commentEntity">评论实体</param>
        /// <returns></returns>
        public static BaseResult Add(BaseUserInfo userInfo, BaseCommentEntity commentEntity)
        {
            var webClient = new WebClient();
            var postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "Add");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("commentEntity", JsonConvert.SerializeObject(commentEntity));
            byte[] responseArray = webClient.UploadValues(Url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<BaseResult>(response);
            }
            return null;
        }
    }
}
