//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;
    using Manager;
    using Model;
    using Model.ZtoOrderBatchEntityJsonTypes;
    using Newtonsoft.Json;

    /// <summary>
    /// 申通电子面单获取订单请求接口
    /// 
    /// 修改记录
    /// 
    ///     2015-08-03  版本：1.0 YangHengLian 创建 http://testpartner.zto.cn/
    ///     2016-1-8   乐工说把订单号提前生成，这样就可以防止浪费单号的情况发生了。根据订单号返回还是一样的，所以这样就放心的了，非常牛逼的思想，五颗星
    ///     
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-03</date>
    /// </author>
    /// </summary>
    public partial class ZtoElecBillHelper
    {
        /// <summary>
        /// 电子面单请求URL
        /// </summary>
        private const string ElecUrl = "http://partner.zto.cn/client/interface.php";

        /// <summary>
        /// 获取申通电子面单
        /// </summary>
        /// <param name="ztoPrintBillEntities">打印实体泛型集合</param>
        /// <param name="ztoElecUserInfoEntity">电子面单线下商家ID实体信息</param>
        /// <returns></returns>
        public static List<ZtoPrintBillEntity> BindElecBillByCustomerId(List<ZtoPrintBillEntity> ztoPrintBillEntities, ZtoElecUserInfoEntity ztoElecUserInfoEntity)
        {
            var errorList = new List<object>();
            // 全部生成订单号，然后重复获取都没关系了不怕
            var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
            foreach (var ztoPrintBillEntity in ztoPrintBillEntities)
            {
                // 订单号 特殊处理，如果没有填写需要生成一个唯一订单号给接口
                if (string.IsNullOrEmpty(ztoPrintBillEntity.OrderNumber))
                {
                    var orderNumber = Guid.NewGuid().ToString("N").ToLower();
                    ztoPrintBillEntity.OrderNumber = orderNumber;
                    var updateParameters = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldOrderNumber,ztoPrintBillEntity.OrderNumber),
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                    };
                    var whereParameters = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, ztoPrintBillEntity.Id)
                    };
                    printBillManager.SetProperty(whereParameters, updateParameters);
                }
            }
            // 需要提交接口次数
            var submitCount = Math.Ceiling(ztoPrintBillEntities.Count / 10.0);
            // 如果提交的记录数超过10条，需要分几批提交接口，10条一次最好，防止接口挂了
            if (submitCount > 1)
            {
                var list = new List<ZtoPrintBillEntity>();
                for (int i = 0; i < submitCount; i++)
                {
                    List<ZtoPrintBillEntity> t = ztoPrintBillEntities.Skip(10 * i).Take(10).ToList();
                    List<ZtoPrintBillEntity> tempList = Request(t, ztoElecUserInfoEntity, ref errorList);
                    if (tempList == null)
                    {
                        if (list.Any())
                        {
                            return list;
                        }
                        return null;
                    }
                    list.AddRange(tempList);
                }
                if (errorList.Any())
                {
                    var frmImportError = new FrmImportError() { errorList = errorList };
                    frmImportError.ShowDialog();
                }
                return list;
            }
            // 提交一次接口就可以了
            var resultList = Request(ztoPrintBillEntities, ztoElecUserInfoEntity, ref errorList);
            if (errorList.Any())
            {
                var frmImportError = new FrmImportError() { errorList = errorList };
                frmImportError.ShowDialog();
            }
            return resultList;
        }

        /// <summary>
        /// 请求电子面单单号（十条请求一次）
        /// </summary>
        /// <param name="ztoPrintBillEntities">打印实体集合</param>
        /// <param name="ztoElecUserInfoEntity">申通电子面单商家实体</param>
        /// <returns></returns>
        public static List<ZtoPrintBillEntity> Request(List<ZtoPrintBillEntity> ztoPrintBillEntities, ZtoElecUserInfoEntity ztoElecUserInfoEntity, ref List<object> errorList)
        {
            // http://testpartner.zto.cn/ 接口文档
            // 构建订单请求实体集合（现在是10条请求一次）
            var orderBatchEntities = new List<ZtoOrderBatchEntity>();
            // {"id":"778718821067/10/9","type":"","tradeid":"","mailno":"","seller":"王先生","buyer":"深圳机场vip","sender":{"id":"","name":"1000043696","company":"75578","mobile":null,"phone":"13726273582","area":"","city":"广东省,深圳市,宝安区","address":"广东省深圳市宝安区沙井街道","zipcode":"","email":"","im":"","starttime":"","endtime":"","isremark":"true"},"receiver":{"id":"","name":"陈小姐","company":"","mobile":"0755-27263516","phone":"0755-27263516","area":"","city":"广东省,深圳市,宝安区","address":"沙井街道南环路马鞍山耗三工业区第三栋（凯强电子）","zipcode":"","email":"","im":""},"items":null}
            foreach (var ztoPrintEntity in ztoPrintBillEntities)
            {
                var orderBatchEntity = new ZtoOrderBatchEntity { Id = ztoPrintEntity.OrderNumber };
                ztoPrintEntity.CreateSite = orderBatchEntity.Id;
                orderBatchEntity.Seller = ztoPrintEntity.SendMan;
                orderBatchEntity.Buyer = ztoPrintEntity.ReceiveMan;
                // 构建订单发件人
                var sender1 = new Sender
                {
                    Name = ztoPrintEntity.SendMan,
                    Mobile = ztoPrintEntity.SendPhone,
                    Phone = ztoPrintEntity.SendPhone,
                    City = string.Format("{0},{1},{2}", ztoPrintEntity.SendProvince, ztoPrintEntity.SendCity, ztoPrintEntity.SendCounty),
                    Address = ztoPrintEntity.SendAddress,
                    Isremark = "true"
                };
                // 构建订单收件人
                var receiver = new Receiver
                {
                    Name = ztoPrintEntity.ReceiveMan,
                    Phone = ztoPrintEntity.ReceivePhone,
                    Mobile = ztoPrintEntity.ReceivePhone,
                    City = string.Format("{0},{1},{2}", ztoPrintEntity.ReceiveProvince, ztoPrintEntity.ReceiveCity, ztoPrintEntity.ReceiveCounty),
                    Address = ztoPrintEntity.ReceiveAddress
                };
                orderBatchEntity.Sender = sender1;
                orderBatchEntity.Receiver = receiver;
                orderBatchEntities.Add(orderBatchEntity);
            }
            // 实体构建完成了，下面开始请求动作
            string content = SecretUtil.EncodeBase64("UTF-8", JsonConvert.SerializeObject(orderBatchEntities));
            //if (string.IsNullOrEmpty(content))
            //{
            //    LogUtil.WriteInfo("发现content内容是空的" + ztoElecUserInfoEntity.Kehuid);
            //    //foreach (var ztoOrderBatchEntity in orderBatchEntities)
            //    //{
            //    //    errorList.Add(new { 订单号 = ztoOrderBatchEntity.Id, 错误信息 = "请求构建的电子面单实体信息是：" + JsonConvert.SerializeObject(ztoOrderBatchEntity) });
            //    //}
            //}
            //else
            //{
            //    LogUtil.WriteInfo("发现content内容不是空的" + ztoElecUserInfoEntity.Kehuid+Environment.NewLine+content);
            //}
            // 正式地址 http://partner.zto.cn//client/interface.php
            // 测试地址 http://testpartner.zto.cn/client/interface.php

            var webClient = new WebClient();
            string date = DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
            var postValues = new NameValueCollection
            {
                {"style", "json"},
                {"func", "order.batch_submit"},
                {"content", content},
                {"partner",  ztoElecUserInfoEntity.Kehuid}, 
                {"datetime", date},
                {"verify",System.Web.HttpUtility.UrlEncode(SecretUtil.md5(ztoElecUserInfoEntity.Kehuid +date + content + ztoElecUserInfoEntity.Pwd))}
            };
            byte[] responseArray = webClient.UploadValues(ElecUrl, postValues);
            string response = Encoding.UTF8.GetString(responseArray);

            //if (response.Contains("s51") || response.Contains("可用单号不足"))
            //{
            //    XtraMessageBox.Show("可用单号不足，请在申通物料系统购买电子面单，点击确定跳转到物料系统。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    ToolHelper.OpenBrowserUrl("https://sso.zt-express.com/?SystemCode=WULIAO&openId=false");
            //    return null;
            //}
            // 这是正常的json {"result": "true","keys": [{"result":"true","id": "91ebc0f5bb8e48bba16fba4ad47e6832","orderid": "91ebc0f5bb8e48bba16fba4ad47e6832","mailno": "530557076165","mark":"南昌转吉安县","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "a8a8e5c047304b6ea9f84cd8d87c86fa","orderid": "a8a8e5c047304b6ea9f84cd8d87c86fa","mailno": "530557076172","mark":"长沙转冷水江","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "4eb5de495ba546f6aa7563ab1cd98c8c","orderid": "4eb5de495ba546f6aa7563ab1cd98c8c","mailno": "530557076180","mark":"北京 43-04","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "d63729ba2a1640ce9d09dfa62b0b2c36","orderid": "d63729ba2a1640ce9d09dfa62b0b2c36","mailno": "530557076197","mark":"长沙转冷水江","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "6c471fafb9824a2bbb60773d3f1b020b","orderid": "6c471fafb9824a2bbb60773d3f1b020b","mailno": "530557076202","mark":"长沙转冷水江","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "07ded9b31eb4427394c71fdd970f23d6","orderid": "07ded9b31eb4427394c71fdd970f23d6","mailno": "530557076210","mark":"深圳转 03-06","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "a0f692c695054ffb833cef7b8f144445","orderid": "a0f692c695054ffb833cef7b8f144445","mailno": "530557076227","mark":"长沙转冷水江","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "93223e4330f5420fb007588183ff23ef","orderid": "93223e4330f5420fb007588183ff23ef","mailno": "530557076234","mark":"珠海香洲 02-01","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "02251d35561548319378e9e4815c8bf1","orderid": "02251d35561548319378e9e4815c8bf1","mailno": "530557076241","mark":"成都转宜宾","sitecode":"41610","sitename":"锦州"},{"result":"true","id": "06961e5c671444cb8132d89954190083","orderid": "06961e5c671444cb8132d89954190083","mailno": "530557076259","mark":"佛山 05-06","sitecode":"41610","sitename":"锦州"}]}
            // 这是错误的json {"result": "true","keys": [{"result": "false","id": "2b736e2e291d49c689d80a62b693d71a","code": "s17","keys": "sender->mobile","remark": "数据内容太长"}]}
            var orderBatchResponseEntity = JsonConvert.DeserializeObject<ZtoOrderBatchResponseEntity>(response);
            if (orderBatchResponseEntity != null)
            {
                // 绑定大头笔和单号
                // 整个请求的返回值是ok的（就是调用接口校验通过的意思）
                #region 返回值不为空，需要绑定单号到数据库
                if (orderBatchResponseEntity.Result.ToUpper() == "TRUE")
                {
                    var backZtoPrintBillEntities = new List<ZtoPrintBillEntity>();
                    foreach (ZtoPrintBillEntity ztoPrintBillEntity in ztoPrintBillEntities)
                    {
                        foreach (var keyse in orderBatchResponseEntity.Keys)
                        {
                            if (keyse.Id == ztoPrintBillEntity.OrderNumber)
                            {
                                if (!string.IsNullOrEmpty(keyse.Mailno))
                                {
                                    ztoPrintBillEntity.BigPen = keyse.Mark;
                                    ztoPrintBillEntity.BillCode = keyse.Mailno;
                                    backZtoPrintBillEntities.Add(ztoPrintBillEntity);
                                    break;
                                }
                                // 为什么失败需要告知给用户
                                errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = response });
                            }
                        }
                    }
                    return backZtoPrintBillEntities;
                }
                else
                {
                    // 调用接口都没有通过，那么一般就是商家ID和密码不正确导致
                    foreach (ZtoPrintBillEntity ztoPrintBillEntity in ztoPrintBillEntities)
                    {
                        errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = response });
                    }
                }
                #endregion
                //var customerErrorEntity = JsonConvert.DeserializeObject<ZtoCustomerErrorEntity>(response);
                //if (customerErrorEntity != null)
                //{
                //    XtraMessageBox.Show(customerErrorEntity.Keys + customerErrorEntity.Remark + Environment.NewLine + "请在发件人表格中修改默认发件人的商家ID和密码，注意密码区分大小写。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    XtraMessageBox.Show(response, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            else
            {
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in ztoPrintBillEntities)
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "接口返回值为空，无法获取单号，尝试重新获取，只要订单号在就可以重新获取，不会浪费单号" });
                }
            }
            return null;
        }

        /// <summary>
        /// 订单取消接口
        /// </summary>
        /// <param name="orderNumber">订单号</param>
        /// <param name="ztoElecUserInfoEntity">商家ID实体</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static bool BackZtoElecBill(string orderNumber, ZtoElecUserInfoEntity ztoElecUserInfoEntity, ref string msg)
        {
            try
            {
                var entity = new
                {
                    id = orderNumber,
                    remark = BaseSystemInfo.SoftFullName + "取消，商家ID:" + ztoElecUserInfoEntity.Kehuid
                };
                // 实体构建完成了，下面开始请求动作
                string content = SecretUtil.EncodeBase64("UTF-8", JsonConvert.SerializeObject(entity));
                // 正式地址 http://partner.zto.cn//client/interface.php
                // 测试地址 http://testpartner.zto.cn/client/interface.php
                var webClient = new WebClient();
                string date = DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
                var postValues = new NameValueCollection
                {
                    {"style", "json"},
                    {"func", "order.cancel"},
                    {"content", content},
                    {"partner",  ztoElecUserInfoEntity.Kehuid}, 
                    {"datetime", date},
                    {"verify",System.Web.HttpUtility.UrlEncode(SecretUtil.md5(ztoElecUserInfoEntity.Kehuid +date + content + ztoElecUserInfoEntity.Pwd))}
                };
                byte[] responseArray = webClient.UploadValues(ElecUrl, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                if (string.IsNullOrEmpty(response))
                {
                    return false;
                }
                // {"result":"true","id":"2b102d1b3e5542c9b6ece98cdfa82b09"}
                dynamic resultEntity = JsonConvert.DeserializeObject(response);
                if (resultEntity["result"].ToString() == "true" && resultEntity["id"].ToString() == orderNumber)
                {
                    msg = "取消成功";
                    return true;
                }
                msg = response;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取商家ID的可用电子面单数量
        /// </summary>
        /// <param name="ztoElecUserInfoEntity">申通电子面单线下商家ID实体信息</param>
        /// <returns></returns>
        public static string GetElecBillCount(ZtoElecUserInfoEntity ztoElecUserInfoEntity)
        {
            // http://testpartner.zto.cn/#mail.counter
            var entity = new
            {
                //  已申请过的最后一个运单号码。 （如提供此单号就以此单号开始统计未使用单号， 如不提供就查询所有未使用单号）。
                // lastno	string	否	100000000016	已申请过的最后一个运单号码。 （如提供此单号就以此单号开始统计未使用单号， 如不提供就查询所有未使用单号）。
                lastno = ""
            };
            // 实体构建完成了，下面开始请求动作
            string content = SecretUtil.EncodeBase64("UTF-8", JsonConvert.SerializeObject(entity));
            // 正式地址 http://partner.zto.cn//client/interface.php
            // 测试地址 http://testpartner.zto.cn/client/interface.php
            var webClient = new WebClient();
            string date = DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
            var postValues = new NameValueCollection
                {
                    {"style", "json"},
                    {"func", "mail.counter"},
                    {"content", content},
                    {"partner",  ztoElecUserInfoEntity.Kehuid}, 
                    {"datetime", date},
                    {"verify",System.Web.HttpUtility.UrlEncode(SecretUtil.md5(ztoElecUserInfoEntity.Kehuid +date + content + ztoElecUserInfoEntity.Pwd))}
                };
            byte[] responseArray = webClient.UploadValues(ElecUrl, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (string.IsNullOrEmpty(response))
            {
                return "请求失败，返回值为空，可以尝试重新查询。";
            }
            if (response.Contains("非法的数据签名") || response.Contains("s04"))
            {
                return response + Environment.NewLine + "请右击修改商家ID信息，也可以修改默认发件人的商家ID信息，密码区分大小写，一定要看清楚" + Environment.NewLine + "商家ID和密码可以从申通物料系统通过短信方式获取到，短信上面是什么就填写什么。";
            }
            // 返回值 {"result":"true","counter":{"available":"568"}}
            // response = "{\"result\": \"false\"   ,\"code\": \"s05\",\"remark\": \"缺少必要的参数\"}";
            // response = "-Via: 1.1 jq50:88 (Cdn Cache Server V2.0)" + Environment.NewLine + "Connection: keep-alive" +Environment.NewLine + response;
            // 判断一下返回的是不是json格式的数据
            if (JsonSplitHelper.IsJson(response))
            {
                var ztoElecBillCountJsonEntity = JsonConvert.DeserializeObject<ZTOElecBillCountJsonEntity>(response);
                if (ztoElecBillCountJsonEntity != null)
                {
                    if (ztoElecBillCountJsonEntity.counter != null)
                    {
                        return ztoElecBillCountJsonEntity.counter.available;
                    }
                    return response;
                }
                return response;
            }
            return response;
        }

        /// <summary>
        /// 签收图片接口 (mail.billimg)
        /// </summary>
        /// <param name="billCode"></param>
        /// <returns></returns>
        public static string GetSignImage(string billCode)
        {
            return null;
        }

        /// <summary>
        /// 查询申通快件跟踪
        /// </summary>
        /// <param name="billCode"></param>
        /// <param name="ztoElecUserInfoEntity"></param>
        /// <returns></returns>
        public static string GetBillRecord(string billCode, ZtoElecUserInfoEntity ztoElecUserInfoEntity)
        {
            var entity = new
            {
                mailno = billCode
            };
            string content = SecretUtil.EncodeBase64("UTF-8", JsonConvert.SerializeObject(entity));
            var webClient = new WebClient();
            string date = DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
            var postValues = new NameValueCollection
                {
                    {"style", "json"},
                    {"func", "mail.trace"},
                    {"content", content},
                    {"partner",  ztoElecUserInfoEntity.Kehuid}, 
                    {"datetime", date},
                    {"verify",System.Web.HttpUtility.UrlEncode(SecretUtil.md5(ztoElecUserInfoEntity.Kehuid +date + content + ztoElecUserInfoEntity.Pwd))}
                };
            byte[] responseArray = webClient.UploadValues(ElecUrl, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            return response;
        }
    }
}
