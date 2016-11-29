//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.04.23 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.04.23</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 离职处理
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Leave(BaseUserEntity userEntity, BaseUserLogOnEntity userLogOnEntity, string comment)
        {
            int result = 0;

            if (userEntity != null)
            {
                // 更新用户实体
                this.UpdateObject(userEntity);
            }
            
            // 更新登录信息
            if (userLogOnEntity != null)
            {
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.UserInfo);
                userLogOnManager.UpdateObject(userLogOnEntity);
            }

            // 填写评论
            if (!string.IsNullOrWhiteSpace(comment))
            {
                SQLBuilder sqlBuilder = new SQLBuilder(BaseSystemInfo.ServerDbType);
                sqlBuilder.BeginInsert(BaseCommentEntity.TableName);
                sqlBuilder.SetValue(BaseCommentEntity.FieldId, System.Guid.NewGuid().ToString("N"));
                sqlBuilder.SetValue(BaseCommentEntity.FieldCategoryCode, BaseUserEntity.TableName);
                sqlBuilder.SetValue(BaseCommentEntity.FieldObjectId, userEntity.Id);
                sqlBuilder.SetValue(BaseCommentEntity.FieldContents, comment);
                sqlBuilder.SetValue(BaseCommentEntity.FieldWorked, 1);
                sqlBuilder.SetValue(BaseCommentEntity.FieldDepartmentId, userEntity.DepartmentId);
                sqlBuilder.SetValue(BaseCommentEntity.FieldDepartmentName, userEntity.DepartmentName);
                sqlBuilder.SetValue(BaseCommentEntity.FieldCreateUserId, this.UserInfo.Id);
                sqlBuilder.SetValue(BaseCommentEntity.FieldCreateBy, this.UserInfo.RealName);
                sqlBuilder.SetDBNow(BaseCommentEntity.FieldCreateOn);
                sqlBuilder.SetValue(BaseCommentEntity.FieldIPAddress, this.UserInfo.IPAddress);
                sqlBuilder.SetValue(BaseCommentEntity.FieldEnabled, 1);
                sqlBuilder.SetValue(BaseCommentEntity.FieldDeletionStateCode, 0);
                sqlBuilder.EndInsert();
            }

            // 2016-03-17 吉日嘎拉 停止吉信的号码
            if (userEntity != null && !string.IsNullOrEmpty(userEntity.NickName))
            {
                AfterLeaveStopIM(userEntity);
            }

            // 2016-03-17 吉日嘎拉 停止吉信的号码
            if (userEntity != null && !string.IsNullOrEmpty(userEntity.Id))
            {
                BaseUserContactEntity userContactEntity = BaseUserContactManager.GetObjectByCache(userEntity.Id);
                {
                    if (userContactEntity != null && !string.IsNullOrEmpty(userContactEntity.CompanyMail))
                    {
                        ChangeUserMailStatus(userContactEntity.CompanyMail, true);
                    }
                }
            }

            return result;
        }

        private string GetStatus(string response)
        {
            /*
             * 返回：
                0 - 更改用户邮箱状态成功；
                1 - 参数不正确或参数格式无效；
                2 - 邮箱用户不存在或读取用户信息失败；
                3 - 更改用户邮箱状态失败。
                */
            string result = "更改用户邮箱状态成功";
            if (response.Contains("1"))
            {
                result = "参数不正确或参数格式无效";
            }
            if (response.Contains("2"))
            {
                result = "邮箱用户不存在或读取用户信息失败";
            }
            if (response.Contains("3"))
            {
                result = "更改用户邮箱状态失败";
            }
            return result;
        }

        /// <summary>
        /// 改变用户邮箱状态
        /// </summary>
        /// <param name="email">公司邮箱</param>
        /// <param name="stop">1：禁止，0：启用</param>
        /// <returns></returns>
        public BaseResult ChangeUserMailStatus(string email, bool stop = true)
        {
            var result = new BaseResult();
            try
            {
                // 停止邮箱内网请求地址：http://192.168.0.201:6080/roperate.php?optcmd=modifyuserstatus&user=xumingyue&domain=zto.cn&status=1
                // 开启邮箱内网请求地址：http://192.168.0.201:6080/roperate.php?optcmd=modifyuserstatus&user=xumingyue&domain=zto.cn&status=0
                if (!string.IsNullOrEmpty(email))
                {
                    var array = email.Split('@');
                    var status = stop ? 1 : 0;
                    // 1：表示停止，0：表示启用
                    string requestUrl = string.Format("http://192.168.0.201:6080/roperate.php?optcmd=modifyuserstatus&user={0}&domain={1}&status={2}", array[0], array[1], status);
                    var webClient = new WebClient();
                    byte[] responseArray = webClient.UploadValues(requestUrl, new NameValueCollection());
                    string response = Encoding.UTF8.GetString(responseArray);
                    if (!string.IsNullOrEmpty(response))
                    {
                        if (response.ToUpper() == "OK")
                        {
                            result.Status = true;
                            result.StatusCode = "success";
                            result.StatusMessage = string.Format("{0}用户邮箱状态成功", stop ? "禁止" : "启用");
                            result.RecordCount = 1;
                        }
                        else
                        {
                            result.Status = false;
                            result.StatusCode = "failed";
                            result.StatusMessage = GetStatus(response);
                            result.RecordCount = 0;
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.StatusCode = "failed";
                        result.StatusMessage = "返回值为空";
                        result.RecordCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                string writeMessage = "BaseUserManager.AfterLeave:发生时间:" + DateTime.Now
                     + System.Environment.NewLine + "Message:" + ex.Message
                     + System.Environment.NewLine + "Source:" + ex.Source
                     + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                     + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                     + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");

                result.Status = false;
                result.StatusCode = "failed";
                result.StatusMessage = ex.Source;
                result.RecordCount = 0;
            }

            return result;
        }

        public bool AfterLeaveStopIM(BaseUserEntity userEntity)
        {
            bool result = false;

            // 2016-03-17 吉日嘎拉 停止吉信的号码
            if (userEntity != null && !string.IsNullOrEmpty(userEntity.NickName))
            {
                //{"a":"structure-delete-user","v":0.1,"t":"loginname"}
                //返回：{"ret":0} 表示成功
                try
                {
                    string url = "http://jixin.zt-express.com:8280/mng/im.service";
                    WebClient webClient = new WebClient();
                    NameValueCollection postValues = new NameValueCollection();
                    Hashtable ht = new Hashtable();
                    ht.Add("a", "structure-delete-user");
                    ht.Add("v", "0.1");
                    ht.Add("t", userEntity.NickName);
                    string data = new JavaScriptSerializer().Serialize(ht);
                    data = SecretUtil.EncodeBase64("utf-8", data);
                    postValues.Add("data", data);
                    byte[] responseArray = webClient.UploadValues(url, postValues);
                    data = Encoding.UTF8.GetString(responseArray);
                    data = SecretUtil.DecodeBase64("utf-8", data);
                    JObject o = JObject.Parse(data);
                    JToken jToken = o["ret"];
                    if (string.Equals("0", jToken.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                catch (System.Exception ex)
                {
                    string writeMessage = "BaseUserManager.AfterLeaveStopIM:发生时间:" + DateTime.Now
                        + System.Environment.NewLine + "Message:" + ex.Message
                        + System.Environment.NewLine + "Source:" + ex.Source
                        + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                        + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                        + System.Environment.NewLine;

                    FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                }
            }

            return result;
        }
    }
}