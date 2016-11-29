//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using grproLib;
    using STO.Print.AddBillForm;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 导出电子面单和纸质面单的底单图片
    ///  
    /// 修改记录
    /// 
    ///     2015-12-01  版本：1.0 YangHengLian 创建
    ///     2016-2-3 还需要优化一下界面，界面不是很好看
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-12-01</date>
    /// </author>
    /// </summary>
    public partial class FrmExportBillImage : BaseForm
    {
        private readonly GridppReport _report = new GridppReport();

        private readonly List<ZtoPrintBillEntity> _list;
        private readonly List<ZtoPrintBillEntity> _tempList = new List<ZtoPrintBillEntity>();
        private string _printFileName = "";
        private int _tempIndex = 0;
        protected delegate void UpdateControlText1(string str);
        //定义更新控件的方法
        protected void updateControlText(string str)
        {
            this.lblTotalBillNum.Text = str;
        }
        public static string BillWarningMessage
        {
            get
            {
                var messageList = new List<string>
                {
                    "底单可以用于梧桐的图片上传，这样就省去了高速扫描仪的工作步骤，省时省力高效",
                    "电子档底单可以方便做收发件，生成的图片文档打开，把枪对着电脑做收发件，方便快捷",
                    "生成的底单可以用于仲裁问题件提供的依据"
                };
                var messBuilder = new StringBuilder(Environment.NewLine);
                for (int i = 0; i < messageList.Count; i++)
                {
                    var tempIndex = i;
                    messBuilder.AppendLine(++tempIndex + "." + messageList[i] + "。" + Environment.NewLine);
                }
                return messBuilder.ToString();
            }
        }

        public FrmExportBillImage()
        {
            InitializeComponent();
        }

        public FrmExportBillImage(List<ZtoPrintBillEntity> list)
        {
            InitializeComponent();
            lblWarning.Visible = true;
            lblWarning.Text = BillWarningMessage;
            if (!Directory.Exists(BillPrintHelper.SaveFilePath))
            {
                Directory.CreateDirectory(BillPrintHelper.SaveFilePath);
            }
            txtWorkFolder.Text = BillPrintHelper.SaveFilePath;
            this._list = list;
            if (list.Any())
            {
                var total = 0;
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in list)
                {
                    if (!string.IsNullOrEmpty(ztoPrintBillEntity.BillCode))
                    {
                        txtBills.Text = txtBills.Text + ztoPrintBillEntity.BillCode + Environment.NewLine;
                        ++total;
                    }
                }
                lblTotalBillNum.Text = total.ToString();
            }
            var filePath = BillPrintHelper.GetDefaultTemplatePath();
            // 载入报表模板数据
            _report.LoadFromFile(filePath);
            _report.Initialize += ReportInitialize;
            _report.FetchRecord += ReportFetchRecord;
            _report.ExportBegin += ReportExportBegin;
        }


        /// <summary>
        /// 选择保存文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetWorkFloder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog { SelectedPath = BillPrintHelper.SaveFilePath };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                txtWorkFolder.Text = path;
            }
        }
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWorkFolder.Text))
                if (Directory.Exists(txtWorkFolder.Text))
                    Process.Start(txtWorkFolder.Text);
                else
                    MessageBox.Show(@"无效路径！");
            else
                MessageBox.Show(@"请选择保存位置！");
        }

        private void FrmExportBillImage_Load(object sender, EventArgs e)
        {
        }

        #region private void ReportInitialize() 初始化报表列字段对象
        private IGRField billCode, senderName, senderAddress, senderCompany, senderPhone, departure,
                         receiverName, receiverAddress, receiverCompany, receiverPhone, destination,
                         description, amount, remarks, sendTime, weight, totalMoney, bigPen, pageField, countField, qRCodeField, orderNumberField, toPayMentField, goodsPayMentField, _sendSiteField;
        /// <summary>
        /// 初始化报表列字段对象
        /// </summary>
        private void ReportInitialize()
        {
            billCode = _report.FieldByName("单号");
            senderName = _report.FieldByName("寄件人姓名");
            departure = _report.FieldByName("始发地");
            senderAddress = _report.FieldByName("寄件人详址");
            senderCompany = _report.FieldByName("寄件人公司");
            senderPhone = _report.FieldByName("寄件人电话");
            receiverName = _report.FieldByName("收件人姓名");
            destination = _report.FieldByName("目的地");
            receiverAddress = _report.FieldByName("收件人详址");
            receiverCompany = _report.FieldByName("收件人公司");
            receiverPhone = _report.FieldByName("收件人电话");
            description = _report.FieldByName("品名");
            amount = _report.FieldByName("数量");
            remarks = _report.FieldByName("备注");
            sendTime = _report.FieldByName("寄件时间");
            weight = _report.FieldByName("重量");
            totalMoney = _report.FieldByName("费用总计");
            bigPen = _report.FieldByName("大头笔");
            pageField = _report.FieldByName("Page");
            countField = _report.FieldByName("Count");
            qRCodeField = _report.FieldByName("二维码");
            orderNumberField = _report.FieldByName("订单号");
            toPayMentField = _report.FieldByName("到付款");
            goodsPayMentField = _report.FieldByName("代收货款");
            _sendSiteField = _report.FieldByName("发件网点");
        }

        /// <summary>
        /// 报表字段和数据转换函数
        /// </summary>
        private void ReportFetchRecord()
        {
            if (_tempList != null && _tempList.Count > 0)
            {
                var elecExtendInfoEntity = BillPrintHelper.GetElecUserInfoExtendEntity();
                foreach (ZtoPrintBillEntity billEntity in _tempList)
                {
                    _report.DetailGrid.Recordset.Append();
                    if (billCode != null)
                    {
                        billCode.AsString = billEntity.BillCode;
                    }
                    if (senderName != null)
                    {
                        senderName.AsString = billEntity.SendMan;
                    }
                    if (senderAddress != null)
                    {
                        senderAddress.AsString = billEntity.SendProvince + billEntity.SendCity + billEntity.SendCounty + billEntity.SendAddress;
                    }
                    if (senderCompany != null)
                    {
                        senderCompany.AsString = billEntity.SendCompany;
                    }
                    if (senderPhone != null)
                    {
                        senderPhone.AsString = billEntity.SendPhone;
                    }
                    if (departure != null)
                    {
                        departure.AsString = billEntity.SendDeparture;
                    }
                    if (receiverName != null)
                    {
                        receiverName.AsString = billEntity.ReceiveMan;
                    }
                    if (receiverAddress != null)
                    {
                        var tempAddress = billEntity.ReceiveAddress;
                        if (!string.IsNullOrEmpty(tempAddress))
                        {
                            if (!string.IsNullOrEmpty(billEntity.ReceiveProvince))
                            {
                                tempAddress = tempAddress.Replace(billEntity.ReceiveProvince, "");
                            }
                            if (!string.IsNullOrEmpty(billEntity.ReceiveCity))
                            {
                                tempAddress = tempAddress.Replace(billEntity.ReceiveCity, "");
                            }
                            if (!string.IsNullOrEmpty(billEntity.ReceiveCounty))
                            {
                                tempAddress = tempAddress.Replace(billEntity.ReceiveCounty, "");
                            }
                        }
                        receiverAddress.AsString = billEntity.ReceiveProvince + billEntity.ReceiveCity + billEntity.ReceiveCounty + tempAddress;
                    }
                    if (receiverCompany != null)
                    {
                        receiverCompany.AsString = billEntity.ReceiveCompany;
                    }
                    if (receiverPhone != null)
                    {
                        receiverPhone.AsString = billEntity.ReceivePhone;
                    }
                    if (destination != null)
                    {
                        destination.AsString = billEntity.ReceiveDestination;
                    }
                    if (description != null)
                    {
                        description.AsString = billEntity.GoodsName;
                    }
                    if (amount != null)
                    {
                        amount.AsString = billEntity.TotalNumber.ToString();
                    }
                    if (remarks != null)
                    {
                        remarks.AsString = billEntity.Remark;
                    }
                    if (sendTime != null)
                    {
                        sendTime.AsString = billEntity.SendDate;
                    }
                    if (weight != null)
                    {
                        weight.AsString = billEntity.Weight;
                    }
                    //if (totalMoney != null)
                    //{
                    //    totalMoney.AsString = billEntity.TranFee;
                    //}
                    if (bigPen != null)
                    {
                        bigPen.AsString = billEntity.BigPen;
                    }
                    if (countField != null)
                    {
                        countField.AsString = _list.Count.ToString();
                    }
                    if (pageField != null)
                    {
                        pageField.AsString = _tempIndex.ToString();
                    }
                    if (qRCodeField != null)
                    {

                    }
                    if (orderNumberField != null)
                    {
                        orderNumberField.AsString = billEntity.OrderNumber;
                    }
                    if (toPayMentField != null)
                    {
                        toPayMentField.AsString = billEntity.TOPAYMENT + "元";
                    }
                    if (goodsPayMentField != null)
                    {
                        goodsPayMentField.AsString = billEntity.GOODS_PAYMENT + "元";
                    }
                    if (_sendSiteField != null)
                    {
                        if (elecExtendInfoEntity != null)
                        {
                            // 上海(02100)
                            _sendSiteField.AsString = string.Format("{0}({1})", elecExtendInfoEntity.siteName, elecExtendInfoEntity.siteCode);
                        }
                    }
                    _report.DetailGrid.Recordset.Post();
                }
            }
        }
        #endregion


        void ReportExportBegin(IGRExportOption Sender)
        {
            //ExportBegin 事件在将报表导出之前会触发到，无论是调用 ExportDirect 与 Export 方法
            //还是从打印预览窗口等地方执行导出，都会触发到 ExportBegin 事件。
            //通常在 ExportBegin 事件中设置导出选项参数，改变默认导出行为

            //指定导出文件的完整路径与文件名称
            string fileName = txtWorkFolder.Text + "\\" + _printFileName + ".png";

            Sender.FileName = fileName;

            //根据导出类型设置其特有的选项参数，有关选项参数的具体信息清参考帮助文档。
            //IGRExportOption是导出选项的基类，其它具体导出选项的接口名称都以IGRE2为前缀
            Sender.AsE2IMGOption.DPI = 300;
            Sender.AsE2IMGOption.ImageType = GRExportImageType.greitPNG;
        }

        /// <summary>
        /// 一键生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuildImage_Click(object sender, EventArgs e)
        {
            btnBuildImage.Enabled = false;
            //var t = new Thread(BuildImage);
            //t.Start();
            var t = new Task(BuildImage);
            t.Start();
            btnBuildImage.Enabled = true;
        }

        /// <summary>
        /// 生成底单
        /// </summary>
        public void BuildImage()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            UpdateControlText1 updateControl = updateControlText;
            try
            {
                if (_list == null || _list.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < _list.Count; i++)
                {
                    if (string.IsNullOrEmpty(_list[i].BillCode))
                    {
                        continue;
                    }
                    _printFileName = !string.IsNullOrEmpty(_list[i].BillCode) ? _list[i].BillCode : DateTime.Now.Ticks.ToString();
                    _tempList.Add(_list[i]);
                    ++_tempIndex;
                    // http://blog.csdn.net/szstephenzhou/article/details/12838961
                    // 在创建窗口句柄之前,不能在控件上调用 Invoke 或 BeginInvoke。
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(updateControl, string.Format("{0}/{1}", _tempIndex, _list.Count));
                    }
                    //  2016-1-31下午 这一行代码一定要写，因为要导出模板的面单图片，所以一定要设置true，如果不想要就设置成false就行了，grid++的客服还是很不错的，qq：grid++ report  641243789
                   // _report.BackImagePrint = true;
                    // 直接调用ExportDirect方法执行导出任务，这里我只是导出图片哦
                    _report.ExportDirect(GRExportType.gretIMG, _printFileName, false, false);
                    _tempList.Clear();
                }
                if (_tempIndex > 0)
                {
                    if (ckAddSystemWaterMark.Checked)
                    {
                        string waterMarkFolder = BillPrintHelper.SaveFilePath + "\\水印底单";
                        if (!DirectoryUtil.IsExistDirectory(waterMarkFolder))
                        {
                            DirectoryUtil.CreateDirectory(waterMarkFolder);
                        }
                        var files = DirectoryUtil.GetFileNames(BillPrintHelper.SaveFilePath);
                        foreach (string file in files)
                        {
                            if (Utilities.FileUtil.FileIsExist(file))
                            {
                                // img对象一定要释放，不然内存上升，杨恒连，2016年7月24日15:25:35
                                using (var img = ImageHelper.WatermarkText(Image.FromFile(file), BaseSystemInfo.SoftFullName, ImageHelper.WatermarkPosition.BottomRight, new Font("Verdana", 10, FontStyle.Bold), new SolidBrush(Color.Blue)))
                                {
                                    string fileName = Utilities.FileUtil.GetFileNameNoExtension(file);
                                    img.Save(string.Format(waterMarkFolder + "\\" + fileName + ".png", fileName));
                                }
                            }
                        }
                    }
                    // alertBuildImageInfo.Show(this, "生成底单", string.Format("成功生成{0}张底单图片", _list.Count));
                    //   XtraMessageBox.Show(string.Format("成功生成{0}张底单图片，是否打开文件夹查看？", _list.Count), AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if ( == DialogResult.Yes)
                    //{
                    //    Process.Start(BillPrintHelper.SaveFilePath);
                    //}
                }
            }
            catch (Exception exception)
            {
             //   XtraMessageBox.Show(exception.Message, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtil.WriteException(exception);
            }
            finally
            {
                btnBuildImage.Enabled = true;
                _tempIndex = 0;
                if (_list != null)
                {
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = string.Format("{0}分{1}秒{2}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds);
                    // http://blog.csdn.net/szstephenzhou/article/details/12838961
                    // 在创建窗口句柄之前,不能在控件上调用 Invoke 或 BeginInvoke。
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(updateControl, string.Format("{0}/{1}", _tempIndex, _list.Count) + "  耗时：" + elapsedTime);
                    }
                }
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {

        }
    }
}