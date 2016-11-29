//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using Newtonsoft.Json;
    using STO.Print.AddBillForm;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 查快递窗体，主要使用到了快递100的数据
    ///
    /// 修改纪录
    ///
    ///		2015-08-23  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2015-11-27  增加查询底单的功能，可以把打印的底单也显示出来
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-23</date>
    /// </author>
    /// </summary>
    public partial class FrmSearchBill : BaseForm
    {
        private readonly BaseExpressManager _expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);

        public FrmSearchBill()
        {
            InitializeComponent();
            if (!Directory.Exists(BillPrintHelper.SaveFilePath))
            {
                Directory.CreateDirectory(BillPrintHelper.SaveFilePath);
            }
        }


        /// <summary>
        /// GET模拟请求
        /// </summary>
        /// <param name="url">URL路径</param>
        /// <param name="postDataStr">参数</param>
        /// <returns>返回请求信息</returns>
        public static string HttpGet(string url, string postDataStr)
        {
            try
            {
                // http://www.kuaidi100.com/query?type=zhongtong&postid=761734957321&id=1&valicode=&temp=0.12421642546541989
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                var c = new CookieContainer();
                request.CookieContainer = c;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 返回指定JSON转成的字典
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>Dictionary`[string, object]</returns>
        public static Dictionary<string, object> SelectDictionary(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<Dictionary<string, object>>(json);
        }

        public void Search(string expressCode, string billCode)
        {
            try
            {
                ////请求的Url： http://www.kuaidi100.com/query?type=shunfeng&postid=201215344815&id=1&valicode=&temp=0.12316451570950449
                //string json = HttpGet("http://www.kuaidi100.com/query", "type=" + expressCode + "&postid=" + billCode + "&id=1&valicode=&temp=0.12316451570950449");
                //if (string.IsNullOrEmpty(json))
                //{
                //    return;
                //}
                //Dictionary<string, object> ht = SelectDictionary(json);
                //if (ht["status"].ToString() == "200")
                //{
                //    ArrayList aList = new ArrayList();
                //    aList = (ArrayList)ht["data"];
                //    List<BillResult> billResults = new List<BillResult>();
                //    for (int i = 0; i < aList.Count; i++)
                //    {
                //        Dictionary<string, object> di = (Dictionary<string, object>)aList[i];
                //        billResults.Add(new BillResult()
                //        {
                //            time = di["time"].ToString(),
                //            context = di["context"].ToString()
                //        });
                //    }
                //    gcBill.DataSource = billResults;
                //}
                //else
                //{
                //    XtraMessageBox.Show(ht["message"].ToString(), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
                if (elecUserInfoEntity == null)
                {
                    STO.Print.Utilities.MessageUtil.ShowTips("未绑定申通线下商家ID，无法查询，请在系统设置中进行绑定");
                    return;
                }
                var result = ZtoElecBillHelper.GetBillRecord(billCode, elecUserInfoEntity);
                if (!string.IsNullOrEmpty(result))
                {
                    var searchBillResponseEntity = JsonConvert.DeserializeObject<ZtoSearchBillResponseEntity>(result);
                    if (searchBillResponseEntity.Traces != null && searchBillResponseEntity.Traces.Length > 0)
                    {
                        var billResults = new List<BillResult>();
                        foreach (ZtoSearchBillResponseEntity.Trace trace in searchBillResponseEntity.Traces)
                        {
                            billResults.Add(new BillResult
                            {
                                time = trace.AcceptTime,
                                context = trace.Remark
                            });
                        }
                        gcBill.DataSource = billResults.OrderByDescending(p => p.time).ToList();
                    }
                }
                else
                {
                    btnSearch.ShowTip(result);
                }
                LoadBillImage(billCode);
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvBill_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            gvBill.Appearance.OddRow.BackColor = Color.White; // 设置奇数行颜色 // 默认也是白色 可以省略 
            gvBill.OptionsView.EnableAppearanceOddRow = true; // 使能 // 和和上面绑定 同时使用有效 
            gvBill.Appearance.EvenRow.BackColor = Color.FromArgb(255, 250, 205); // 设置偶数行颜色 
            gvBill.OptionsView.EnableAppearanceEvenRow = true; // 使能 // 和和上面绑定 同时使用有效
            if (e.RowHandle == gvBill.FocusedRowHandle)
            {
                e.Appearance.Font = new Font("宋体", 9, FontStyle.Bold);
            }
        }

        private void gvBill_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString(CultureInfo.InvariantCulture);
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle;
                }
            }
        }

        /// <summary>
        /// 单号改变重新获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBillCode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillCode.Text.Trim()))
            {
                return;
            }
            if (txtBillCode.Text.Trim().Length != 12)
            {
                return;
            }
            // http://www.kuaidi100.com/autonumber/autoComNum?text=778600778150
            // {"comCode":"","num":"778600778150","auto":[{"comCode":"zhongtong","id":"","noCount":874800,"noPre":"7786","startTime":""},{"comCode":"kuayue","id":"","noCount":14338,"noPre":"7786","startTime":""},{"comCode":"ucs","id":"","noCount":266,"noPre":"7786","startTime":""},{"comCode":"zhaijisong","id":"","noCount":13,"noPre":"7786","startTime":""}]}
            var response = HttpGet(string.Format("http://www.kuaidi100.com/autonumber/autoComNum?text={0}", txtBillCode.Text.Trim()), null);
            if (!string.IsNullOrEmpty(response))
            {
                var list = JsonConvert.DeserializeObject<KuaiDi100>(response);
                if (list != null && list.Auto.Length > 0)
                {
                    if (list.Num == txtBillCode.Text.Trim())
                    {
                        Search(list.Auto[0].ComCode, txtBillCode.Text.Trim());
                        try
                        {
                            cmbExpress.SelectedValue = list.Auto[0].ComCode;
                        }
                        catch (Exception ex)
                        {
                            ProcessException(ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string expressCode = cmbExpress.SelectedValue.ToString();
            string billCode = txtBillCode.Text;
            if (string.IsNullOrEmpty(expressCode))
            {
                XtraMessageBox.Show("请选择快递公司", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbExpress.Focus();
                return;
            }
            if (string.IsNullOrEmpty(billCode))
            {
                XtraMessageBox.Show("请输入快递单号", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBillCode.Focus();
                return;
            }
            Search(expressCode, billCode);
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSearchBill_Load(object sender, EventArgs e)
        {
            // 获取到快递公司的信息
            var list = _expressManager.GetList<BaseExpressEntity>();
            cmbExpress.DataSource = list;
            cmbExpress.ValueMember = BaseExpressEntity.FiledShortName;
            cmbExpress.DisplayMember = BaseExpressEntity.FieldName;
            if (list.Count > 0)
            {
                cmbExpress.SelectedIndex = 0;
            }
            txtBillCode.Focus();
            txtBillCode.Select();
        }

        /// <summary>
        /// 加载底单图片
        /// </summary>
        /// <param name="billCode">单号就是图片名称</param>
        void LoadBillImage(string billCode)
        {
            var fileName = BillPrintHelper.SaveFilePath + "\\" + billCode + ".png";
            if (File.Exists(fileName))
            {
                // http://blog.sina.com.cn/s/blog_9908653401014elg.html
                // （C#）Image.FromFile 方法会锁住文件的原因及可能的解决方法 
                Stream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                picBillBox.Image = Image.FromStream(fs);
                fs.Close();
                picBillBox.Properties.ZoomPercent = 30;
            }
            else
            {
                picBillBox.Image = null;
            }
        }

        /// <summary>
        /// 打开图片保存文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(BillPrintHelper.SaveFilePath);
        }

        /// <summary>
        /// 双击打开图片，用windows看图软件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBillBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBillCode.Text))
            {
                var fileName = BillPrintHelper.SaveFilePath + "\\" + txtBillCode.Text + ".png";
                if (Utilities.FileUtil.IsExistFile(fileName))
                {
                    // 这是画图软件打开的
                   //  Process.Start("mspaint.exe", fileName);
                    Process.Start(fileName);
                }
            }
        }

        /// <summary>
        /// 图片改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackPercent_EditValueChanged(object sender, EventArgs e)
        {
            picBillBox.Properties.ZoomPercent = trackPercent.Value;
        }
    }

    public class BillResult
    {
        public string time { get; set; }

        public string context { get; set; }
    }
}
