//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd .
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print.Utilities
{
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;
    using Newtonsoft.Json;
    using STO.Print.Model;

    /// <summary>
    /// BillPrintHelper
    /// 运单帮助类
    ///
    /// 修改纪录
    ///
    ///		2015-07-28 版本：1.0 YangHengLian 创建主键。
    ///     2015-09-21 调用接口使用阿里云的服务器来部署WebApi了，java的接口域名很多移动和联通的访问不了，导致提取大头笔失败
    ///     2015-11-21 把api接口去除掉了，那个域名不用了
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-28</date>
    /// </author>
    /// </summary>
    public class BillPrintHelper
    {
        /// <summary>`
        /// Grid++5.8加密狗key，不可以泄露出去，2015年12月22日10:42:45
        /// </summary>
        public static readonly string GridReportKey = "LYA4W3QN8JE3";

        /// <summary>
        /// 保存底单图片的文件夹路径
        /// 默认在“我的文档”文件夹中创建保存文件夹，按照日期来作为文件夹
        /// </summary>
        public static readonly string SaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ZTOPrint\\" + DateTime.Now.ToString(BaseSystemInfo.DateFormat);

        /// <summary>
        /// 备份数据库目录
        /// </summary>
        public static readonly string BackUpDataBaseFolder = BaseSystemInfo.StartupPath + "\\backup\\";

        /// <summary>
        /// 打印Sqlite数据库连接字符串
        /// </summary>
        public static readonly string BillPrintConnectionString = string.Format(@"Data Source={0}\DataBase\STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988", Application.StartupPath);

        /// <summary>
        /// 打印历史记录Sqlite数据库连接字符串
        /// </summary>
        public static readonly string BillPrintBackConnectionString = string.Format(@"Data Source={0}\DataBase\STO.Backup.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988", Application.StartupPath);

        /// <summary>
        /// 打印运单数据库连接实体
        /// </summary>
        public static readonly IDbHelper DbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintConnectionString);

        /// <summary>
        /// 打印运单数据库连接实体
        /// </summary>
        public static readonly IDbHelper BackupDbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintBackConnectionString);

        /// <summary>
        /// 系统参数管理类
        /// </summary>
        public static readonly BaseParameterManager ParameterManager = new BaseParameterManager(DbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);

        /// <summary>
        /// 大头笔管理类
        /// </summary>
        public static readonly BaseAreaProvinceMarkManager MarkManager = new BaseAreaProvinceMarkManager(DbHelper);

        /// <summary>
        /// 面单Sqlite数据库执行sql字符串
        /// </summary>
        public static string CmdStrForZtoBillPrinter =
                                                        @"SELECT 
                                                          Id,
                                                          RECEIVE_MAN AS 收件人姓名,
                                                          RECEIVE_PHONE AS 收件电话,
                                                          RECEIVE_PROVINCE AS 收件省份,
                                                          RECEIVE_CITY AS 收件城市,
                                                          RECEIVE_COUNTY AS 收件区县,
                                                          RECEIVE_ADDRESS AS 收件人详细地址,
                                                          BIG_PEN AS 大头笔,                                               
                                                          BILL_CODE AS 单号,
                                                          ORDER_NUMBER AS 订单号,
                                                          TOPAYMENT AS 到付款,
                                                          GOODS_PAYMENT AS 代收货款,
                                                          RECEIVE_COMPANY AS 收件人单位,
                                                          RECEIVE_DESTINATION AS 目的地,
                                                          RECEIVE_POSTCODE AS 收件人邮编,
                                                          SEND_MAN AS 发件人姓名,
                                                          SEND_PHONE AS 发件电话,
                                                          SEND_DEPARTURE AS 始发地,
                                                          SEND_PROVINCE AS 发件省份,
                                                          SEND_CITY AS 发件城市,
                                                          SEND_COUNTY AS 发件区县,
                                                          SEND_ADDRESS AS 发件详细地址,
                                                          SEND_DATE AS 发件日期,
                                                          SEND_SITE AS 发件网点,
                                                          SEND_COMPANY AS 发件单位,
                                                          SEND_DEPARTMENT AS 发件部门,
                                                          SEND_POSTCODE AS 发件邮编,
                                                          CREATEON AS 创建时间,
                                                          GOODS_NAME AS 物品类型,
                                                          LENGTH AS 长,
                                                          WIDTH AS 宽,
                                                          HEIGHT AS 高,
                                                          WEIGHT AS 重量,
                                                          PAYMENT_TYPE AS 付款方式,
                                                          TRAN_FEE AS 运费,
                                                          TOTAL_NUMBER AS 数量,
                                                          REMARK AS 备注,
                                                          PRINT_STATUS AS 打印状态
                                                          FROM ZTO_PRINT_BILL ORDER BY CREATEON";

        /// <summary>
        /// 根据中文获取数据库对应的字段名称，用来导入自定义Excel使用比较好
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFieldByName(string name)
        {
            string result = string.Empty;
            switch (name)
            {
                case "收件人姓名":
                    result = ZtoPrintBillEntity.FieldReceiveMan;
                    break;
                case "收件电话":
                    result = ZtoPrintBillEntity.FieldReceivePhone;
                    break;
                case "收件省份":
                    result = ZtoPrintBillEntity.FieldReceiveProvince;
                    break;
                case "收件城市":
                    result = ZtoPrintBillEntity.FieldReceiveCity;
                    break;
                case "收件区县":
                    result = ZtoPrintBillEntity.FieldReceiveCounty;
                    break;
                case "收件人详细地址":
                    result = ZtoPrintBillEntity.FieldReceiveAddress;
                    break;
                case "大头笔":
                    result = ZtoPrintBillEntity.FieldBigPen;
                    break;
                case "收件人单位":
                    result = ZtoPrintBillEntity.FieldReceiveCompany;
                    break;
                case "目的地":
                    result = ZtoPrintBillEntity.FieldReceiveDestination;
                    break;
                case "收件人邮编":
                    result = ZtoPrintBillEntity.FieldReceivePostcode;
                    break;
                case "单号":
                    result = ZtoPrintBillEntity.FieldBillCode;
                    break;
                case "发件人姓名":
                    result = ZtoPrintBillEntity.FieldSendMan;
                    break;
                case "发件电话":
                    result = ZtoPrintBillEntity.FieldSendPhone;
                    break;
                case "始发地":
                    result = ZtoPrintBillEntity.FieldSendDeparture;
                    break;
                case "发件省份":
                    result = ZtoPrintBillEntity.FieldSendProvince;
                    break;
                case "发件城市":
                    result = ZtoPrintBillEntity.FieldSendCity;
                    break;
                case "发件区县":
                    result = ZtoPrintBillEntity.FieldSendCounty;
                    break;
                case "发件详细地址":
                    result = ZtoPrintBillEntity.FieldSendAddress;
                    break;
                case "发件日期":
                    result = ZtoPrintBillEntity.FieldSendDate;
                    break;
                case "发件网点":
                    result = ZtoPrintBillEntity.FieldSendSite;
                    break;
                case "发件单位":
                    result = ZtoPrintBillEntity.FieldSendCompany;
                    break;
                case "发件部门":
                    result = ZtoPrintBillEntity.FieldSendDepartment;
                    break;
                case "发件邮编":
                    result = ZtoPrintBillEntity.FieldSendPostcode;
                    break;
                case "创建时间":
                    result = ZtoPrintBillEntity.FieldCreateOn;
                    break;
                case "物品类型":
                    result = ZtoPrintBillEntity.FieldGoodsName;
                    break;
                case "长":
                    result = ZtoPrintBillEntity.FieldLength;
                    break;
                case "宽":
                    result = ZtoPrintBillEntity.FieldWidth;
                    break;
                case "高":
                    result = ZtoPrintBillEntity.FieldHeight;
                    break;
                case "重量":
                    result = ZtoPrintBillEntity.FieldWeight;
                    break;
                case "付款方式":
                    result = ZtoPrintBillEntity.FieldPaymentType;
                    break;
                case "运费":
                    result = ZtoPrintBillEntity.FieldTranFee;
                    break;
                case "数量":
                    result = ZtoPrintBillEntity.FieldTotalNumber;
                    break;
                case "备注":
                    result = ZtoPrintBillEntity.FieldRemark;
                    break;
                case "订单号":
                    result = ZtoPrintBillEntity.FieldOrderNumber;
                    break;
                case "到付款":
                    result = ZtoPrintBillEntity.FieldToPayMent;
                    break;
                case "代收货款":
                    result = ZtoPrintBillEntity.FieldGoodsPayMent;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 根据中文获取数据库对应的字段名称，用来导入自定义Excel使用比较好
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetUserFieldByName(string name)
        {
            string result = string.Empty;
            switch (name)
            {
                case "姓名":
                    result = ZtoUserEntity.FieldRealname;
                    break;
                case "省份":
                    result = ZtoUserEntity.FieldProvince;
                    break;
                case "城市":
                    result = ZtoUserEntity.FieldCity;
                    break;
                case "区县":
                    result = ZtoUserEntity.FieldCounty;
                    break;
                case "详细地址":
                    result = ZtoUserEntity.FieldAddress;
                    break;
                case "单位":
                    result = ZtoUserEntity.FieldCompany;
                    break;
                case "部门":
                    result = ZtoUserEntity.FieldDepartment;
                    break;
                case "电话":
                    result = ZtoUserEntity.FieldTelePhone;
                    break;
                case "手机":
                    result = ZtoUserEntity.FieldMobile;
                    break;
                case "邮编":
                    result = ZtoUserEntity.FieldPostcode;
                    break;
                case "备注":
                    result = ZtoUserEntity.FieldRemark;
                    break;
                case "默认发件人":
                    result = ZtoUserEntity.FieldIssendorreceive;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 发件人Sqlite数据库执行sql字符串
        /// </summary>
        public static string CmdStrForSendUser = @"SELECT 
                                                          Id,
                                                          RealName AS 姓名,
                                                          PROVINCE AS 省份,
                                                          CITY AS 城市,
                                                          COUNTY AS 区县,
                                                          ADDRESS AS 详细地址,
                                                          COMPANY AS 单位,
                                                          DEPARTMENT AS 部门,
                                                          TELEPHONE AS 电话,
                                                          MOBILE AS 手机,
                                                          POSTCODE AS 邮编,
                                                          IsDefault AS 默认发件人,
                                                          REMARK AS 备注
                                                          FROM ZTO_USER WHERE ISSENDORRECEIVE = 1 ORDER BY ISDEFAULT DESC ";

        /// <summary>
        /// 收件人Sqlite数据库执行sql字符串
        /// </summary>
        public static string CmdStrForReceiveUser = @"SELECT 
                                                          Id,
                                                          RealName AS 姓名,
                                                          PROVINCE AS 省份,
                                                          CITY AS 城市,
                                                          COUNTY AS 区县,
                                                          ADDRESS AS 详细地址,
                                                          COMPANY AS 单位,
                                                          DEPARTMENT AS 部门,
                                                          TELEPHONE AS 电话,
                                                          MOBILE AS 手机,
                                                          POSTCODE AS 邮编,
                                                          IsDefault AS 默认收件人,
                                                          REMARK AS 备注
                                                          FROM ZTO_USER WHERE ISSENDORRECEIVE = 0 ORDER BY ISDEFAULT DESC ";

        #region public static string GetRemaike(string sendMarkes, string sendAddress, string receiveMarkes, string receiveAddress) 调用接口获取大头笔信息
        /// <summary>
        /// 从接口获取大头笔信息
        /// </summary>
        /// <param name="sendMarkes">发件人省市区（逗号隔开）</param>
        /// <param name="sendAddress">发件人详细地址</param>
        /// <param name="receiveMarkes">收件人省市区（逗号隔开）</param>
        /// <param name="receiveAddress">收件人详细地址</param>
        /// <returns></returns>
        public static string GetRemaike(string sendMarkes, string sendAddress, string receiveMarkes, string receiveAddress)
        {
            string value = string.Empty;
            var sb = new StringBuilder();
            string sendProvince = string.Empty;
            string sendCity = string.Empty;
            string sendDistrict = string.Empty;
            string receiveProvince = string.Empty;
            string receiveCity = string.Empty;
            string receiveDistrict = string.Empty;
            try
            {
                var postValues = new NameValueCollection();
                if (!string.IsNullOrEmpty(sendMarkes))
                {
                    string[] sendmaks = sendMarkes.Split(',');
                    if (sendmaks.Length >= 3)
                    {
                        sendProvince = sendmaks[0];
                        sendCity = sendmaks[1];
                        sendDistrict = sendmaks[2];
                    }
                    else if (sendmaks.Length == 2)
                    {
                        sendProvince = sendmaks[0];
                        sendCity = sendmaks[1];
                    }
                    else
                    {
                        sendProvince = sendmaks[0];
                    }
                }
                if (!string.IsNullOrEmpty(receiveMarkes))
                {
                    string[] receivemaks = receiveMarkes.Split(',');
                    if (receivemaks.Length >= 3)
                    {
                        receiveProvince = receivemaks[0];
                        receiveCity = receivemaks[1];
                        receiveDistrict = receivemaks[2];
                    }
                    else if (receivemaks.Length == 2)
                    {
                        receiveProvince = receivemaks[0];
                        receiveCity = receivemaks[1];
                    }
                    else
                    {
                        receiveProvince = receivemaks[0];
                    }
                }
                sb.Append("{\"send_province\":\"" + sendProvince + "\"");
                sb.Append(",\"send_city\":\"" + sendCity + "\"");
                sb.Append(",\"send_district\":\"" + sendDistrict + "\"");
                sb.Append(",\"send_address\":\"" + sendAddress + "\"");
                sb.Append(",\"receive_province\":\"" + receiveProvince + "\"");
                sb.Append(",\"receive_city\":\"" + receiveCity + "\"");
                sb.Append(",\"receive_district\":\"" + receiveDistrict + "\"");
                sb.Append(",\"receive_address\":\"" + receiveAddress + "\"}");
                postValues.Add("data", sb.ToString()); //消息内容
                const string dtbkey = "QTk2NjZCMjE5NTE4NjIzNTQ=";
                const string dtbtype = "EBill";
                string dtbsing = SecretUtil.md5(sb + dtbkey);
                postValues.Add("data_digest", dtbsing); //消息签名
                postValues.Add("msg_type", "GETMARK");
                postValues.Add("company_id", dtbtype);
                const string posturl = "http://japi.zto.cn/zto/api_utf8/mark";
                var webClient = new WebClient();
                byte[] responseArray = webClient.UploadValues(posturl, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                string returnMases = response;
                dynamic entity = JsonConvert.DeserializeObject(returnMases);
                if (entity != null)
                {
                    if ((bool)entity["status"])
                    {
                        var resultEntity = JsonConvert.DeserializeObject(entity["result"].ToString());
                        if (resultEntity != null)
                        {
                            // 手写大头笔
                            var mark = resultEntity["mark"].ToString();
                            // 机打大头笔
                            var printMark = resultEntity["print_mark"].ToString();
                            // 优先提取机打大头笔，如果提取不到就提取手写大头笔
                            if (string.IsNullOrEmpty(printMark))
                            {
                                value = mark;
                            }
                            else
                            {
                                // 没填写发件省市区，优先提取手写大头笔，否则提取机打大头笔
                                if (string.IsNullOrEmpty(sendProvince) && string.IsNullOrEmpty(sendCity) && string.IsNullOrEmpty(sendDistrict))
                                {
                                    value = mark;
                                }
                                else
                                {
                                    value = printMark;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 采用Get请求的方式来获取
                string url = "http://japi.zto.cn/zto/api_utf8/mark?data={\"send_province\":\"江苏省\",\"send_city\":\"盐城市\",\"send_district\":\"射阳县\",\"send_address\":\"测试地址1\",\"receive_province\":\"江苏省\",\"receive_city\":\"苏州市\",\"receive_district\":\"吴中区\",\"receive_address\":\"测试地址2\"}&data_digest=51c8e1cff4abc51027bbf6c3a1ad92ef&msg_type=GETMARK&company_id=EBill";
                url = url.Replace("江苏省", sendProvince);
                url = url.Replace("盐城市", sendCity);
                url = url.Replace("射阳县", sendDistrict);
                url = url.Replace("测试地址1", sendAddress);
                url = url.Replace("江苏省", receiveProvince);
                url = url.Replace("苏州市", receiveCity);
                url = url.Replace("吴中区", receiveDistrict);
                url = url.Replace("测试地址2", receiveAddress);
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Timeout = 2000;
                httpRequest.Method = "GET";
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader sr = new StreamReader(httpResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                string result = sr.ReadToEnd();
                sr.Close();
                dynamic entity = JsonConvert.DeserializeObject(result);
                if (entity != null)
                {
                    if ((bool)entity["status"])
                    {
                        var resultEntity = JsonConvert.DeserializeObject(entity["result"].ToString());
                        if (resultEntity != null)
                        {
                            // 手写大头笔
                            var mark = resultEntity["mark"].ToString();
                            // 机打大头笔
                            var printMark = resultEntity["print_mark"].ToString();
                            // 优先提取机打大头笔，如果提取不到就提取手写大头笔
                            if (string.IsNullOrEmpty(printMark))
                            {
                                value = mark;
                            }
                            else
                            {
                                // 没填写发件省市区，优先提取手写大头笔，否则提取机打大头笔
                                if (string.IsNullOrEmpty(sendProvince) && string.IsNullOrEmpty(sendCity) && string.IsNullOrEmpty(sendDistrict))
                                {
                                    value = mark;
                                }
                                else
                                {
                                    value = printMark;
                                }
                            }
                        }
                    }
                }
            }
            return value;
        }
        #endregion

        /// <summary>
        /// 调用阿里云服务器的WebApi来获取大头笔，移动和联通的网络得到解救
        /// </summary>
        /// <param name="sendMarkes"></param>
        /// <param name="sendAddress"></param>
        /// <param name="receiveMarkes"></param>
        /// <param name="receiveAddress"></param>
        /// <returns></returns>
        //public static string GetPrintMarkByApi(string sendMarkes, string sendAddress, string receiveMarkes, string receiveAddress)
        //{
        //    try
        //    {
        //        var webClient = new WebClient();
        //        var postValues = new NameValueCollection
        //    {
        //        {"sendMarkes", sendMarkes},
        //        {"sendAddress", sendAddress},
        //        {"receiveMarkes",receiveMarkes},
        //        {"receiveAddress",receiveAddress}
        //    };
        //        // 向服务器发送POST数据
        //        byte[] responseArray = webClient.UploadValues("http://YangHengLian.com/api/PrintMark/GetPrintMark", postValues);
        //        return Encoding.UTF8.GetString(responseArray);
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}

        #region public static string GetRemaike(string sendMarkes, string sendAddress, string receiveMarkes, string receiveAddress) 从接口获取大头笔信息
        /// <summary>
        /// 从接口获取大头笔信息
        /// </summary>
        /// <param name="sendMarkes">发件人省市区（逗号隔开）</param>
        /// <param name="sendAddress">发件人详细地址</param>
        /// <param name="receiveMarkes">收件人省市区（逗号隔开）</param>
        /// <param name="receiveAddress">收件人详细地址</param>
        /// <returns></returns>
        public static ZtoPrintMarkResult GetZtoPrintMark(string sendMarkes, string sendAddress, string receiveMarkes, string receiveAddress)
        {
            var sb = new StringBuilder();
            string sendProvince = string.Empty;
            string sendCity = string.Empty;
            string sendDistrict = string.Empty;
            string receiveProvince = string.Empty;
            string receiveCity = string.Empty;
            string receiveDistrict = string.Empty;
            try
            {
                var postValues = new NameValueCollection();
                if (!string.IsNullOrEmpty(sendMarkes))
                {
                    string[] sendmaks = sendMarkes.Split(',');
                    if (sendmaks.Length >= 3)
                    {
                        sendProvince = sendmaks[0];
                        sendCity = sendmaks[1];
                        sendDistrict = sendmaks[2];
                    }
                    else if (sendmaks.Length == 2)
                    {
                        sendProvince = sendmaks[0];
                        sendCity = sendmaks[1];
                    }
                    else
                    {
                        sendProvince = sendmaks[0];
                    }
                }
                if (!string.IsNullOrEmpty(receiveMarkes))
                {
                    string[] receivemaks = receiveMarkes.Split(',');
                    if (receivemaks.Length >= 3)
                    {
                        receiveProvince = receivemaks[0];
                        receiveCity = receivemaks[1];
                        receiveDistrict = receivemaks[2];
                    }
                    else if (receivemaks.Length == 2)
                    {
                        receiveProvince = receivemaks[0];
                        receiveCity = receivemaks[1];
                    }
                    else
                    {
                        receiveProvince = receivemaks[0];
                    }
                }
                sb.Append("{\"send_province\":\"" + sendProvince + "\"");
                sb.Append(",\"send_city\":\"" + sendCity + "\"");
                sb.Append(",\"send_district\":\"" + sendDistrict + "\"");
                sb.Append(",\"send_address\":\"" + sendAddress + "\"");
                sb.Append(",\"receive_province\":\"" + receiveProvince + "\"");
                sb.Append(",\"receive_city\":\"" + receiveCity + "\"");
                sb.Append(",\"receive_district\":\"" + receiveDistrict + "\"");
                sb.Append(",\"receive_address\":\"" + receiveAddress + "\"}");
                postValues.Add("data", sb.ToString()); //消息内容
                const string dtbkey = "QTk2NjZCMjE5NTE4NjIzNTQ=";
                const string dtbtype = "EBill";
                string dtbsing = SecretUtil.md5(sb + dtbkey);
                postValues.Add("data_digest", dtbsing); //消息签名
                postValues.Add("msg_type", "GETMARK");
                postValues.Add("company_id", dtbtype);
                const string posturl = "http://japi.zto.cn/zto/api_utf8/mark";
                var webClient = new WebClient();
                byte[] responseArray = webClient.UploadValues(posturl, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                string returnMases = response;
                ZtoPrintMarkResult entity = JsonConvert.DeserializeObject<ZtoPrintMarkResult>(returnMases);
                if (entity != null)
                {
                    return entity;
                }
            }
            catch (Exception ex)
            {
                // 采用Get请求的方式来获取
                string url = "http://japi.zto.cn/zto/api_utf8/mark?data={\"send_province\":\"江苏省\",\"send_city\":\"盐城市\",\"send_district\":\"射阳县\",\"send_address\":\"测试地址1\",\"receive_province\":\"江苏省\",\"receive_city\":\"苏州市\",\"receive_district\":\"吴中区\",\"receive_address\":\"测试地址2\"}&data_digest=51c8e1cff4abc51027bbf6c3a1ad92ef&msg_type=GETMARK&company_id=EBill";
                url = url.Replace("江苏省", sendProvince);
                url = url.Replace("盐城市", sendCity);
                url = url.Replace("射阳县", sendDistrict);
                url = url.Replace("测试地址1", sendAddress);
                url = url.Replace("江苏省", receiveProvince);
                url = url.Replace("苏州市", receiveCity);
                url = url.Replace("吴中区", receiveDistrict);
                url = url.Replace("测试地址2", receiveAddress);
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Timeout = 2000;
                httpRequest.Method = "GET";
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader sr = new StreamReader(httpResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                string result = sr.ReadToEnd();
                sr.Close();
                ZtoPrintMarkResult entity = JsonConvert.DeserializeObject<ZtoPrintMarkResult>(result);
                if (entity != null)
                {
                    return entity;
                }
            }
            return null;
        }
        #endregion

        #region public static DataTable GetArea() 绑定省市区，返回内存表
        /// <summary>
        /// 绑定省市区，返回内存表
        /// </summary>
        /// <remarks>读取本地sqlite的数据，梧桐登录会检查更新区域表</remarks>
        /// <returns></returns>
        public static DataTable GetArea(bool isGetId = false)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            if (cache["AreaDt"] != null)
            {
                return cache["AreaDt"] as DataTable;
            }
            var areaDt = new DataTable();
            areaDt.Columns.Add(BaseAreaEntity.FieldFullName, typeof(string));
            areaDt.Columns.Add(BaseAreaEntity.FieldSimpleSpelling, typeof(string));
            if (isGetId)
            {
                areaDt.Columns.Add(BaseAreaEntity.FieldId, typeof(string));
            }
            var areaManager = new BaseAreaManager(DbHelper);
            // 获取省份集合，先从缓存获取
            var provinceList = areaManager.GetProvince();
            foreach (var baseAreaEnty in provinceList)
            {
                // 每个市，先从缓存获取
                var cityList = areaManager.GetListByParent(baseAreaEnty.Id);
                foreach (var areaEntity in cityList)
                {
                    // 每个区县，先从缓存获取
                    var countyList = areaManager.GetListByParent(areaEntity.Id);
                    foreach (var entity in countyList)
                    {
                        var newRow = areaDt.NewRow();
                        newRow[BaseAreaEntity.FieldFullName] = string.Format("{0}-{1}-{2}", baseAreaEnty.FullName, areaEntity.FullName, entity.FullName);
                        newRow[BaseAreaEntity.FieldSimpleSpelling] = entity.SimpleSpelling;
                        if (isGetId)
                        {
                            newRow[BaseAreaEntity.FieldId] = string.Format("{0}-{1}-{2}", baseAreaEnty.Id, areaEntity.Id, entity.Id);
                        }
                        areaDt.Rows.Add(newRow);
                    }
                }
            }
            cache.Remove("AreaDt"); //先移除指定键对应的值，防止重复添加
            if (areaDt.Rows.Count > 0)
            {
                //放入缓存中，时间是60分钟，为了导出数据的时候使用
                cache.Add("AreaDt", areaDt, null, DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            }
            return areaDt;
        }
        #endregion

        #region public static List<BaseAreaEntity> GetProvince() 获取用户中心省份，请求远程服务器
        /// <summary>
        /// 获取用户中心省份，请求远程服务器
        /// </summary>
        /// <returns></returns>
        public static List<BaseAreaEntity> GetProvince()
        {
            try
            {
                const string url = "http://usercenter.zt-express.com/UserCenterV42/AreaService.ashx?Function=GetProvinceList";
                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection();
                byte[] responseArray = webClient.UploadValues(url, postValues);
                string result = Encoding.UTF8.GetString(responseArray);
                var list = JsonConvert.DeserializeObject<List<BaseAreaEntity>>(result);
                return list;
            }
            catch (Exception exception)
            {
                LogUtil.WriteException(exception);
                return new List<BaseAreaEntity>();
            }
        }
        #endregion

        #region public static List<BaseAreaEntity> GetCityOrDistrict(string parentId) 获取城市或者区县或者街道都可以用这个
        /// <summary>
        /// 获取城市或者区县
        /// </summary>
        /// <param name="parentId">省份或城市Id</param>
        /// <returns></returns>
        public static List<BaseAreaEntity> GetCityOrDistrict(string parentId)
        {
            try
            {
                string url = "http://usercenter.zt-express.com/UserCenterV42/AreaService.ashx?Function=GetListByParent&ParentId=" + parentId;
                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection();
                byte[] responseArray = webClient.UploadValues(url, postValues);
                string result = Encoding.UTF8.GetString(responseArray);
                var list = JsonConvert.DeserializeObject<List<BaseAreaEntity>>(result);

                return list;
            }
            catch (Exception exception)
            {
                LogUtil.WriteException(exception);
                return new List<BaseAreaEntity>();
            }
        }
        #endregion

        #region public static void SaveDefaultExcelPath(string filePath) 保存默认Excel导入文件路径
        /// <summary>
        /// 保存默认Excel导入文件路径
        /// </summary>
        /// <param name="filePath"></param>
        public static void SaveDefaultExcelPath(string filePath)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "ExcelPath", filePath);
        }
        #endregion

        #region public static void SaveDefaultPrinterName(string printerName) 保存默认打印机
        /// <summary>
        /// 保存默认打印机
        /// </summary>
        /// <param name="printerName"></param>
        public static void SaveDefaultPrinterName(string printerName)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "DefaultPrinter", printerName);
        }
        #endregion

        #region public static void SaveDefaultPrintTemplate(string printerName) 保存默认打印模板
        /// <summary>
        /// 保存默认打印模板
        /// </summary>
        /// <param name="templatePath"></param>
        public static void SaveDefaultPrintTemplate(string templatePath)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "DefaultPrintTemplate", templatePath);
        }
        #endregion

        #region public static void SaveDefaultExcelPath(string filePath) 获取默认Excel导入文件路径
        /// <summary>
        /// 获取默认Excel导入文件路径
        /// </summary>
        public static string GetDefaultExcelPath()
        {
            string excelFilepath = ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "ExcelPath");
            return excelFilepath;
        }
        #endregion

        #region public static string GetDefaultPrinter() 获取用户自己配置的默认打印机
        /// <summary>
        /// 获取用户自己配置的默认打印机
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultPrinter()
        {
            string excelFilepath = ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "DefaultPrinter");
            return excelFilepath;
        }
        #endregion

        #region public static void DownloadPrintTemplate(string tempName) 下载打印模板
        /// <summary>
        /// 下载打印模板
        /// </summary>
        /// <param name="tempName">文件名称</param>
        public static void DownloadPrintTemplate(string tempName)
        {
            try
            {
                string path = Application.StartupPath + @"\template\" + tempName + @".grf";
                FolderBrowserDialog foler = new FolderBrowserDialog { Description = @"选择保存模板的目录" };
                if (foler.ShowDialog() == DialogResult.OK)
                {
                    if (foler.SelectedPath != "")
                        if (File.Exists(path))
                        {
                            var fileName = foler.SelectedPath;
                            if (fileName.LastIndexOf(@"\", StringComparison.Ordinal) == (fileName.Length - 1))
                                fileName = foler.SelectedPath + tempName + @".grf";
                            else
                                fileName = foler.SelectedPath + @"\" + tempName + @".grf";

                            if (File.Exists(fileName))
                            {
                                MessageBox.Show(foler.SelectedPath + @"  已经存在文件" + tempName + @".grf", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            File.Copy(path, fileName);
                            MessageBox.Show("下载成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
                MessageBox.Show(ex.Message, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region public static string GetDefaultTemplatePath() 获取默认打印模板
        /// <summary>
        /// 获取默认打印模板
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultTemplatePath()
        {
            string excelFilepath = ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "DefaultPrintTemplate");
            return excelFilepath;
        }
        #endregion

        /// <summary>
        /// 设置系统同步基础资料完成时间
        /// </summary>
        public static void SetSyncTime()
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "SyncTime", DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat));
        }

        /// <summary>
        /// 设置是否加载系统默认发件人信息（新增运单信息界面）
        /// <param name="isLoad">true:加载 false：不加载</param>
        /// </summary>
        public static void SetLoadDefaultSendMan(bool isLoad = true)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "LoadDefaultSendMan", isLoad ? "1" : "0");
        }

        /// <summary>
        /// 设置是否今天发货（导入运单信息界面）
        /// <param name="isLoad"></param>
        /// </summary>
        public static void SetTodaySend(bool isLoad = true)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "TodaySend", isLoad ? "1" : "0");
        }

        /// <summary>
        /// 获取设置是否今天发货（导入运单信息界面）
        /// </summary>
        /// <returns></returns>
        public static string GetTodaySend()
        {
            return ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "TodaySend");
        }

        /// <summary>
        /// 获取是否加载系统默认发件人信息（新增运单信息界面）
        /// </summary>
        public static string GetLoadDefaultSendMan()
        {
            return ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "LoadDefaultSendMan");
        }

        /// <summary>
        /// 获取上次系统同步基础数据的时间
        /// </summary>
        /// <returns></returns>
        public static string GetSyncTime()
        {
            return ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "SyncTime");
        }

        /// <summary>
        /// 设置是否联网获取大头笔
        /// </summary>
        /// <param name="isLoad"></param>
        public static void SetPrintMarkFromServer(bool isLoad)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "GetPrintMarkFromServer", isLoad ? "1" : "0");
        }

        /// <summary>
        /// 获取是否联网获取大头笔
        /// </summary>
        public static string GetPrintMarkFromServer()
        {
            return ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "GetPrintMarkFromServer");
        }

        /// <summary>
        /// 设置是否打开本界面时自动粘贴剪贴板并识别地址
        /// </summary>
        /// <param name="isAutoPasteAndRecognition"></param>
        public static void SetAutoPasteAndRecognition(bool isAutoPasteAndRecognition)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "AutoPasteAndRecognition", isAutoPasteAndRecognition ? "1" : "0");
        }

        /// <summary>
        /// 获取是否打开本界面时自动粘贴剪贴板并识别地址
        /// </summary>
        public static string GetAutoPasteAndRecognition()
        {
            return ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "AutoPasteAndRecognition");
        }

        /// <summary>
        /// 获取默认新增打印数据的窗体
        /// </summary>
        /// <returns></returns>
        public static BaseForm GetDefaultAddBillForm()
        {
            BaseForm addBillForm = new FrmAddZtoMainBill();
            var defaultTemplate = GetDefaultTemplatePath();
            if (string.IsNullOrEmpty(defaultTemplate))
            {
                addBillForm = new FrmAddZtoMainBill();
            }
            else
            {
                if (defaultTemplate.Contains("ZTOBill"))
                {
                    addBillForm = new FrmAddZtoBill();
                }
                if (defaultTemplate.Contains("ZTOCODBill"))
                {
                    addBillForm = new FrmAddZtoCODBill();
                }
                if (defaultTemplate.Contains("ZTOTaoBaoBill"))
                {
                    addBillForm = new FrmAddZtoTaoBaoBill();
                }
                if (defaultTemplate.Contains("ZTOToPayMentBill"))
                {
                    addBillForm = new FrmAddZtoToPayMentBill();
                }
                if (defaultTemplate.Contains("STO"))
                {
                    addBillForm = new FrmAddStoBill();
                }
                if (defaultTemplate.Contains("YTO"))
                {
                    addBillForm = new FrmAddYtoBill();
                }
                // 顺丰2012年新的面单
                if (defaultTemplate.Contains("SFBill"))
                {
                    addBillForm = new FrmAddShunFeng2012Bill();
                }
                // 顺丰2015年新的面单
                if (defaultTemplate.Contains("SF2015Bill"))
                {
                    addBillForm = new FrmAddShunFeng2015Bill();
                }
                if (defaultTemplate.Contains("Zto180ElecBill") || defaultTemplate.Contains("Zto190ElecBill") || defaultTemplate.Contains("Zto200ElecBill"))
                {
                    addBillForm = new FrmAddZto180ElecBill();
                }
                if (defaultTemplate.Contains("Zto180ToPayMentElecBill") || defaultTemplate.Contains("Zto190ToPayMentElecBill") || defaultTemplate.Contains("Zto200ToPayMentElecBill"))
                {
                    addBillForm = new FrmAddZto180ToPayMentElecBill();
                }
                if (defaultTemplate.Contains("Zto180CODElecBill") || defaultTemplate.Contains("Zto190CODElecBill") || defaultTemplate.Contains("Zto200CODElecBill"))
                {
                    addBillForm = new FrmAddZto180CODElecBill();
                }
            }
            return addBillForm;
        }

        /// <summary>
        /// 申通电子面单加密信息存储
        /// </summary>
        /// <param name="encryptedInfomation">加密信息</param>
        public static void SetZtoCustomerInfo(string encryptedInfomation)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "ZtoElecCustomer", encryptedInfomation);
        }

        /// <summary>
        /// 设置是否开机启动
        /// </summary>
        /// <param name="staus">true:表示开机启动，false表示不启动</param>
        public static void SetAutoRunWhenStart(bool staus)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "AutoRunWhenStart", staus ? "1" : "0");
        }

        /// <summary>
        /// 获取打印专家是否设置了开机启动
        /// </summary>
        public static bool GetAutoRunWhenStart()
        {
            var result = ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "AutoRunWhenStart");
            if (string.IsNullOrEmpty(result))
            {
                return false;
            }
            return BaseBusinessLogic.ConvertToBoolean(result);
        }

        /// <summary>
        /// 获取绑定线下电子面单客户实体
        /// </summary>
        /// <returns></returns>
        public static ZtoElecUserInfoEntity GetElecUserInfoEntity()
        {
            var paramenterContent = ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "ZtoElecCustomer");
            if (!string.IsNullOrEmpty(paramenterContent))
            {
                // 注意需要解密信息
                return JsonConvert.DeserializeObject<ZtoElecUserInfoEntity>(SecretUtil.Decrypt(paramenterContent));
            }
            return null;
        }


        /// <summary>
        /// 申通电子面单加密信息存储
        /// </summary>
        /// <param name="encryptedInfomation">加密信息</param>
        public static void SetZtoCustomerExtendInfo(string encryptedInfomation)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "ZtoElecCustomerExtend", encryptedInfomation);
        }

        /// <summary>
        /// 获取绑定线下电子面单客户实体
        /// </summary>
        /// <returns></returns>
        public static ZtoElecUserInfoExtendEntity GetElecUserInfoExtendEntity()
        {
            var paramenterContent = ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "ZtoElecCustomerExtend");
            if (!string.IsNullOrEmpty(paramenterContent))
            {
                // 注意需要解密信息
                return JsonConvert.DeserializeObject<ZtoElecUserInfoExtendEntity>(SecretUtil.Decrypt(paramenterContent));
            }
            return null;
        }

        /// <summary>
        /// 删除线下电子面单客户实体
        /// </summary>
        /// <returns></returns>
        public static void DeleteElecUserInfoEntity()
        {
            ParameterManager.DeleteByParameterCode("System", "System", "ZtoElecCustomer");
        }

        /// <summary>
        /// 设置系统字体信息
        /// </summary>
        /// <param name="font"></param>
        public static void SetSystemFont(string font)
        {
            ParameterManager.SetParameter(BaseParameterEntity.TableName, "System", "System", "FontSize", font);
        }


        /// <summary>
        /// 获取系统字体信息
        /// </summary>
        public static string GetSystemFont()
        {
            return ParameterManager.GetParameter(BaseParameterEntity.TableName, "System", "System", "FontSize");
        }

        #region public static string FuzzyTelephone(string telephone) 模糊手机或者座机
        /// <summary>
        /// 模糊手机或者座机
        /// </summary>
        /// <param name="telephone">电话，可以是手机也可以是座机</param>
        /// <returns></returns>
        public static string FuzzyTelephone(string telephone)
        {
            if (telephone.Length == 11)
            {
                return telephone.Substring(0, 3) + "****" + telephone.Substring(7, 4);
            }
            if (telephone.Length > 4 && !telephone.Contains("-"))
            {
                return telephone.Substring(0, telephone.Length == 7 ? 3 : 4) + "****";
            }
            if (telephone.Contains("-") && telephone.Length > 4)
            {
                var startIndex = telephone.IndexOf('-');
                var phoneLast = telephone.Substring(startIndex + 1);
                if (phoneLast.Length > 4)
                {
                    if (phoneLast.Length - 4 == 3)//0214-6985212，模糊后是0214-***5212，这种情况
                    {
                        return telephone.Substring(0, startIndex + 1) + "***" + phoneLast.Substring(phoneLast.Length - 4);
                    }
                    //021-69787522，模糊后：021-****7522，这种情况
                    return telephone.Substring(0, startIndex + 1) + "****" + phoneLast.Substring(phoneLast.Length - 4);
                }
            }
            return null;
        }
        #endregion

        #region public static string FuzzyUserName(string userName) 模糊人名，暂时支持中文模糊，英文名称，暂且不模糊
        /// <summary>
        /// 模糊人名，暂时支持中文模糊，英文名称，暂且不模糊
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string FuzzyUserName(string userName)
        {
            string newValue;
            if (userName.Length > 4)
            {
                var regEnglish = new Regex("^[a-zA-Z]");
                if (regEnglish.IsMatch(userName))
                {
                    var valueChar = string.Empty;
                    for (int i = 0; i <= userName.Length - 4; i++)
                    {
                        valueChar += "*";
                    }
                    newValue = userName.Substring(0, 2) + valueChar + userName.Substring(4, userName.Length - 4);
                }
                else
                {
                    newValue = userName.Substring(0, 2) + "**" + userName.Substring(4, userName.Length - 4);
                }
            }
            else
            {
                switch (userName.Length)
                {
                    case 1:
                        newValue = userName;
                        break;
                    case 2:
                        newValue = userName.Substring(0, 1) + "*"; //陈*
                        break;
                    case 3:
                        newValue = userName.Substring(0, 1) + "*" + userName.Substring(2, 1); //马*腾
                        break;
                    case 4:
                        newValue = userName.Substring(0, 2) + "**"; //马克**
                        break;
                    default:
                        newValue = userName.Substring(0, 2) + userName.Substring(userName.Length - 2);
                        break;
                }
            }
            return newValue;
        }
        #endregion
    }
}
