//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , ZTO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ZTO.Print.Manager;

namespace ZTO.Print.Utilities
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using Model;
    using Model.ZtoOrderBatchEntityJsonTypes;
    using Newtonsoft.Json;

    /// <summary>
    /// 中通电子面单获取订单请求接口
    /// 
    /// 修改记录
    /// 
    ///     2015-08-03  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-03</date>
    /// </author>
    /// </summary>
    public class ZtoElecHelper
    {
        /// <summary>
        /// 获取中通电子面单
        /// </summary>
        /// <param name="ztoPrintBillEntities">打印实体泛型集合</param>
        /// <param name="ztoElecUserInfoEntity">电子面单线下商家ID实体信息</param>
        /// <returns></returns>
        public static List<ZtoPrintBillEntity> BindElecBillByCustomerId(List<ZtoPrintBillEntity> ztoPrintBillEntities, ZtoElecUserInfoEntity ztoElecUserInfoEntity)
        {
            // 需要提交接口次数
            var submitCount = Math.Ceiling(ztoPrintBillEntities.Count / 20.0);
            // 如果提交的记录数超过20条，需要分几批提交接口，20条一次最好，防止接口挂了
            if (submitCount > 1)
            {
                List<ZtoPrintBillEntity> list = new List<ZtoPrintBillEntity>();
                for (int i = 0; i < submitCount; i++)
                {
                    List<ZtoPrintBillEntity> t = ztoPrintBillEntities.Skip(20 * i).Take(20).ToList();
                    List<ZtoPrintBillEntity> tempList = Request(t, ztoElecUserInfoEntity);
                    list.AddRange(tempList);
                }
                return list;
            }
            // 提交一次接口就可以了
            return Request(ztoPrintBillEntities, ztoElecUserInfoEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztoPrintBillEntities"></param>
        /// <param name="ztoElecUserInfoEntity"></param>
        /// <returns></returns>
        public static List<ZtoPrintBillEntity> Request(List<ZtoPrintBillEntity> ztoPrintBillEntities, ZtoElecUserInfoEntity ztoElecUserInfoEntity)
        {
            // http://testpartner.zto.cn/ 接口文档
            List<ZtoOrderBatchEntity> orderBatchEntities = new List<ZtoOrderBatchEntity>();
            // {"id":"778718821067/10/9","type":"","tradeid":"","mailno":"","seller":"王先生","buyer":"深圳机场vip","sender":{"id":"","name":"1000043696","company":"75578","mobile":null,"phone":"13726273582","area":"","city":"广东省,深圳市,宝安区","address":"广东省深圳市宝安区沙井街道","zipcode":"","email":"","im":"","starttime":"","endtime":"","isremark":"true"},"receiver":{"id":"","name":"陈小姐","company":"","mobile":"0755-27263516","phone":"0755-27263516","area":"","city":"广东省,深圳市,宝安区","address":"沙井街道南环路马鞍山耗三工业区第三栋（凯强电子）","zipcode":"","email":"","im":""},"items":null}
            ZtoPrintBillManager printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
            foreach (var ztoPrintEntity in ztoPrintBillEntities)
            {
                ZtoOrderBatchEntity orderBatchEntity = new ZtoOrderBatchEntity();
                // 订单号 特殊处理，如果没有填写需要生成一个唯一订单号给接口
                if (!string.IsNullOrEmpty(ztoPrintEntity.OrderNumber))
                {
                    orderBatchEntity.Id = ztoPrintEntity.OrderNumber;
                }
                else
                {
                    orderBatchEntity.Id = Guid.NewGuid().ToString("N").ToLower();
                    ztoPrintEntity.OrderNumber = orderBatchEntity.Id;
                    printBillManager.SetProperty(ztoPrintEntity.OrderNumber, new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId,ztoPrintEntity.Id));
                }
                ztoPrintEntity.CreateSite = orderBatchEntity.Id;
                orderBatchEntity.Seller = ztoPrintEntity.SendMan;
                orderBatchEntity.Buyer = ztoPrintEntity.ReceiveMan;
                Sender sender1 = new Sender
                {
                    Name = ztoPrintEntity.SendMan,
                    Mobile = ztoPrintEntity.SendPhone,
                    Phone = ztoPrintEntity.SendPhone,
                    City = string.Format("{0},{1},{2}", ztoPrintEntity.SendProvince, ztoPrintEntity.SendCity, ztoPrintEntity.SendCounty),
                    Address = ztoPrintEntity.SendAddress,
                    Isremark = "true"
                };
                Receiver receiver = new Receiver
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
            string content = SecretUtil.EncodeBase64("UTF-8", JsonConvert.SerializeObject(orderBatchEntities).ToLower());
            // 正式地址 http://partner.zto.cn//client/interface.php
            // 测试地址 http://testpartner.zto.cn/client/interface.php
            WebClient webClient = new WebClient();
            string date = DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
            NameValueCollection postValues = new NameValueCollection
            {
                {"style", "json"},
                {"func", "order.batch_submit"},
                {"content", content},
                {"partner",  ztoElecUserInfoEntity.Kehuid}, 
                {"datetime", date},
                {"verify",System.Web.HttpUtility.UrlEncode(SecretUtil.md5(ztoElecUserInfoEntity.Kehuid +date + content + ztoElecUserInfoEntity.Pwd).ToLower())}
            };
            byte[] responseArray = webClient.UploadValues("http://partner.zto.cn//client/interface.php", postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (response.Contains("s51") || response.Contains("可用单号不足"))
            {
                XtraMessageBox.Show("可用单号不足，请在中通物料系统购买电子面单，点击确定跳转到物料系统。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ToolHelper.OpenIe("https://sso.zt-express.com/?SystemCode=WULIAO&openId=false");
                return null;
            }
            // {"result": "true","keys": [{"result":"true","id": "ztaa6c6dc3db3044b2b78844b73ec2013e","orderid": "ztaa6c6dc3db3044b2b78844b73ec2013e","mailno": "530253938633","mark":"长沙转常德","sitecode":"61201","sitename":"苏州园区六部"}]}

            ZtoOrderBatchResponseEntity orderBatchResponseEntity = JsonConvert.DeserializeObject<ZtoOrderBatchResponseEntity>(response);
            if (orderBatchResponseEntity != null)
            {
                // 绑定大头笔和单号
                if (orderBatchResponseEntity.Result.ToUpper() == "TRUE")
                {
                    List<ZtoPrintBillEntity> backZtoPrintBillEntities = new List<ZtoPrintBillEntity>();
                    foreach (ZtoPrintBillEntity ztoPrintBillEntity in ztoPrintBillEntities)
                    {
                        foreach (var keyse in orderBatchResponseEntity.Keys)
                        {
                            if (keyse.Id == ztoPrintBillEntity.OrderNumber)
                            {
                                ztoPrintBillEntity.BigPen = keyse.Mark;
                                ztoPrintBillEntity.BillCode = keyse.Mailno;
                                backZtoPrintBillEntities.Add(ztoPrintBillEntity);
                                break;
                            }
                        }
                    }
                    return backZtoPrintBillEntities;
                }
                ZtoCustomerErrorEntity customerErrorEntity = JsonConvert.DeserializeObject<ZtoCustomerErrorEntity>(response);
                if (customerErrorEntity != null)
                {
                    XtraMessageBox.Show(customerErrorEntity.Keys + customerErrorEntity.Remark + Environment.NewLine + "请在确认默认发件人的商家ID和密码是否正确。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(response, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(response, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return null;
        }

         
    }
}
