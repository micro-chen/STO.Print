//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace DotNet.Utilities
{
    [Serializable]
    public class UserLogOnResult
    {
        public BaseUserInfo UserInfo = null;

        public string StatusCode = Status.UserNotFound.ToString();

        public string StatusMessage = Status.UserNotFound.ToDescription();

        // 2015-07-14 吉日嘎拉 短信促销的通知
        public string Message = "中天系统冲刺全线上线、回馈网点短信大幅度优惠、4分一条、没必要眼花缭乱手机发送了，可以用系统高效率发送了。";

        /*
        public string Message = "1：系统有问题联系登录页面信息中心统一服务电话：021-3116 5566。" + "\r\n" + "\r\n"
                                 + "2：哪个业务模块有问题请在中天首页上找对应明确的模块负责人人反馈处理、提高效率。" + "\r\n" + "\r\n"
                                 + "3：加公司的QQ群设置好群名片否则会被踢出，方便别人快速找到您，提高工作效率。" + "\r\n" + "\r\n"
                                 + "4：欢迎大家使用“内部论坛”提出意见建议，反馈信息，成为与总部沟通交流的有效渠道。";
        */
    }
}