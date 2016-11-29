//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO , Ltd .
//-------------------------------------------------------------------------------------

using DotNet.Business;
using grproLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STO.Print.UserControl
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DotNet.Model;
    using DotNet.Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 录单自定义控件，用于电子面单的
    /// 
    /// 修改记录
    /// 
    ///     2015-07-10  版本：1.0 YangHengLian 创建
    ///     2016-2-24   这个qq：zto  2993064821提出来需求：同一个收件人需要打印很多份电子面单，面单的信息都是一样的，只是单号不一样就可以了，这种发货方式也是存在的
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-18</date>
    /// </author>
    /// </summary>
    public partial class MyEnterBillControl : XtraUserControl
    {
        /// <summary>
        /// 打印记录ID
        /// </summary>
        public string PrintBillId { get; set; }

        /// <summary>
        /// 定义Grid++Report报表主对象
        /// </summary>
        readonly GridppReport _report = new GridppReport();

        /// <summary>
        /// 打印实体集合
        /// </summary>
        List<ZtoPrintBillEntity> _list = new List<ZtoPrintBillEntity>();


        public MyEnterBillControl()
        {
            InitializeComponent();
            Load += MyEnterBillControl_Load;
        }

        #region private void MyEnterBillControl_Load(object sender, EventArgs e) 页面加载业务处理

        /// <summary>
        /// 页面加载业务处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyEnterBillControl_Load(object sender, EventArgs e)
        {
            try
            {
                // 一定要注册，否则发布后会有水印
                _report.Title = BaseSystemInfo.SoftFullName;
                _report.Register(BillPrintHelper.GridReportKey);
                _report.Initialize += ReportInitialize;
                _report.FetchRecord += ReportFetchRecord;
                _report.PrintBegin += ReportPrintBegin;
                _report.PrintEnd += ReportPrintEnd;
                // 最大寄件选择日期
                txtSendDate.Properties.MaxValue = DateTime.Now;
                // 最小寄件选择日期
                //txtSendDate.Properties.MinValue = DateTime.Now.AddDays(-6);
                txtSendDate.Text = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                var result1 = BillPrintHelper.GetLoadDefaultSendMan();
                if (string.IsNullOrEmpty(result1))
                {
                    ckGetDefaultSendMan.Checked = false;
                }
                else
                {
                    ckGetDefaultSendMan.Checked = result1 == "1";
                }
                BindArea();
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }

        #endregion

        #region private void ReportInitialize() 初始化报表列字段对象
        private IGRField _billCode, _senderName, _senderAddress, _senderCompany, _senderPhone, _departure,
                         _receiverName, _receiverAddress, _receiverCompany, _receiverPhone, _destination,
                         _description, _amount, _remarks, _sendTime, _weight, _totalMoney, _bigPen, _pageField, _countField, _qRCodeField, _orderNumberField, _toPayMentField, _goodsPayMentField, _sendSiteField;
        /// <summary>
        /// 初始化报表列字段对象
        /// </summary>
        private void ReportInitialize()
        {
            _billCode = _report.FieldByName("单号");
            _senderName = _report.FieldByName("寄件人姓名");
            _departure = _report.FieldByName("始发地");
            _senderAddress = _report.FieldByName("寄件人详址");
            _senderCompany = _report.FieldByName("寄件人公司");
            _senderPhone = _report.FieldByName("寄件人电话");
            _receiverName = _report.FieldByName("收件人姓名");
            _destination = _report.FieldByName("目的地");
            _receiverAddress = _report.FieldByName("收件人详址");
            _receiverCompany = _report.FieldByName("收件人公司");
            _receiverPhone = _report.FieldByName("收件人电话");
            _description = _report.FieldByName("品名");
            _amount = _report.FieldByName("数量");
            _remarks = _report.FieldByName("备注");
            _sendTime = _report.FieldByName("寄件时间");
            _weight = _report.FieldByName("重量");
            _totalMoney = _report.FieldByName("费用总计");
            _bigPen = _report.FieldByName("大头笔");
            _pageField = _report.FieldByName("Page");
            _countField = _report.FieldByName("Count");
            _qRCodeField = _report.FieldByName("二维码");
            _orderNumberField = _report.FieldByName("订单号");
            _toPayMentField = _report.FieldByName("到付款");
            _goodsPayMentField = _report.FieldByName("代收货款");
            _sendSiteField = _report.FieldByName("发件网点");
        }

        /// <summary>
        /// 报表字段和数据转换函数
        /// </summary>
        private void ReportFetchRecord()
        {
            if (_list != null && _list.Count > 0)
            {
                var elecExtendInfoEntity = BillPrintHelper.GetElecUserInfoExtendEntity();
                var tempCount = 0;
                foreach (ZtoPrintBillEntity billEntity in _list)
                {
                    ++tempCount;
                    _report.DetailGrid.Recordset.Append();
                    if (_billCode != null)
                    {
                        _billCode.AsString = billEntity.BillCode;
                    }
                    if (_senderName != null)
                    {
                        _senderName.AsString = billEntity.SendMan;
                    }
                    if (_senderAddress != null)
                    {
                        _senderAddress.AsString = billEntity.SendProvince + billEntity.SendCity + billEntity.SendCounty + billEntity.SendAddress;
                    }
                    if (_senderCompany != null)
                    {
                        _senderCompany.AsString = billEntity.SendCompany;
                    }
                    if (_senderPhone != null)
                    {
                        _senderPhone.AsString = billEntity.SendPhone;
                    }
                    if (_departure != null)
                    {
                        _departure.AsString = billEntity.SendDeparture;
                    }
                    if (_receiverName != null)
                    {
                        _receiverName.AsString = billEntity.ReceiveMan;
                    }
                    if (_receiverAddress != null)
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
                        _receiverAddress.AsString = billEntity.ReceiveProvince + billEntity.ReceiveCity + billEntity.ReceiveCounty + tempAddress;
                    }
                    if (_receiverCompany != null)
                    {
                        _receiverCompany.AsString = billEntity.ReceiveCompany;
                    }
                    if (_receiverPhone != null)
                    {
                        _receiverPhone.AsString = billEntity.ReceivePhone;
                    }
                    if (_destination != null)
                    {
                        _destination.AsString = billEntity.ReceiveDestination;
                    }
                    if (_description != null)
                    {
                        _description.AsString = billEntity.GoodsName;
                    }
                    if (_amount != null)
                    {
                        _amount.AsString = billEntity.TotalNumber;
                    }
                    if (_remarks != null)
                    {
                        _remarks.AsString = billEntity.Remark;
                    }
                    if (_sendTime != null)
                    {
                        _sendTime.AsString = billEntity.SendDate;
                    }
                    if (_weight != null)
                    {
                        _weight.AsString = billEntity.Weight;
                    }
                    //if (totalMoney != null)
                    //{
                    //    totalMoney.AsString = billEntity.TranFee;
                    //}
                    if (_bigPen != null)
                    {
                        _bigPen.AsString = billEntity.BigPen;
                    }
                    if (_countField != null)
                    {
                        _countField.AsString = _list.Count.ToString(CultureInfo.InvariantCulture);
                    }
                    if (_pageField != null)
                    {
                        _pageField.AsString = tempCount.ToString(CultureInfo.InvariantCulture);
                    }

                    if (_qRCodeField != null)
                    {

                    }
                    if (_orderNumberField != null)
                    {
                        _orderNumberField.AsString = billEntity.OrderNumber;
                    }
                    if (_toPayMentField != null)
                    {
                        _toPayMentField.AsString = billEntity.TOPAYMENT + "元";
                    }
                    if (_goodsPayMentField != null)
                    {
                        _goodsPayMentField.AsString = billEntity.GOODS_PAYMENT + "元";
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

        /// <summary>
        /// 打印机开始打印
        /// </summary>
        void ReportPrintBegin()
        {
            string alertInfo = _report.Printer.PrinterName + "正在打印" + _list.Count + "条打印记录，已经提交给打印机，提交时间：" + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
            // 检查打印服务是否启动了，很多的Ghost版本的windows操作系统把这服务关闭了，导致没办法打印，程序会异常
            var result = PrinterHelper.CheckServerState("RpcSs");
            var result1 = PrinterHelper.CheckServerState("Spooler");
            if (result != "服务已经启动" || result1 != "服务已经启动")
            {
                XtraMessageBox.Show("系统RpcSs或则Spooler两个打印服务未启动", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // 显示打印机状态
            var printerStatus = STO.Print.Utilities.PrinterHelperExtend.Printer.GetPrinterStatus(_report.Printer.PrinterName);
            string alertInfo1 = _report.Printer.PrinterName + "打印机状态：" + printerStatus;
            STO.Print.Utilities.PrinterHelper.OpenPrinterStatusWindow(_report.Printer.PrinterName);
        }

        /// <summary>
        /// 打印任务结束
        /// </summary>
        void ReportPrintEnd()
        {
            var t = new Task(() =>
            {
                var frmExportBillImage = new FrmExportBillImage(_list);
                frmExportBillImage.BuildImage();
            });
            t.Start();
            // 还需要加入备份库（打印历史记录表中,重复的数据打印不要重复创建到历史记录中）
            var filePath = BillPrintHelper.GetDefaultTemplatePath();
            var templateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);
            var templateList = templateManager.GetList<BaseTemplateEntity>(new KeyValuePair<string, object>(BaseTemplateEntity.FieldFilePath, filePath));
            var printBillManager = new ZtoPrintBillManager(BillPrintHelper.BackupDbHelper);
            var resultCount = printBillManager.AddHistory(_list, templateList.FirstOrDefault());
            if (resultCount > 0)
            {
                alertPrintInfo.Show(this.ParentForm, "提醒消息", string.Format("打印结束，可以在历史记录中查询打印数据，新增{0}条打印记录。", resultCount));

            }
            else
            {
                alertPrintInfo.Show(this.ParentForm, "提醒消息", string.Format("打印结束，新增{0}条打印记录，请检查单号是否都存在，否则不会新增到历史记录中。", resultCount));
            }
        }

        #region private void GreatReport() 生成Report内容
        /// <summary>
        /// 生成Report内容
        /// </summary>
        private void GreatReport()
        {
            var filePath = BillPrintHelper.GetDefaultTemplatePath();
            if (string.IsNullOrEmpty(filePath))
            {
                if (XtraMessageBox.Show(@"没有设置默认模板，是否设置？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    var frmTemplateSetting = new FrmTemplateSetting();
                    if (frmTemplateSetting.ShowDialog() == DialogResult.OK)
                    {
                        var defaultTemplate = BillPrintHelper.GetDefaultTemplatePath();
                        if (!string.IsNullOrEmpty(defaultTemplate))
                        {
                            if (File.Exists(defaultTemplate))
                            {
                                _report.LoadFromFile(defaultTemplate);
                            }
                            else
                            {
                                _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(@"没有设置默认模板，将采用系统默认模板", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                        }
                    }
                }
                else
                {
                    _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                }
            }
            else
            {
                if (File.Exists(filePath))
                {
                    _report.LoadFromFile(filePath);
                }
                else
                {
                    _report.LoadFromFile(Application.StartupPath + "\\Template\\ZTOBill.grf");
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            //构建实体
            ZtoPrintBillEntity printBillEntity = BuildPrintEntity();
            //if (!string.IsNullOrEmpty(PrintBillId))
            //{
            //    printBillEntity = new ZtoPrintBillManager(BillPrintHelper.DbHelper).GetObject(PrintBillId);
            //}
            //printBillEntity.SendMan = txtSendMan.Text;
            //printBillEntity.SendPhone = txtSendPhone.Text;
            //printBillEntity.SendCompany = txtSendCompany.Text;
            //printBillEntity.SendAddress = txtSendAddress.Text;
            //printBillEntity.SendDate = string.IsNullOrEmpty(txtSendDate.Text) ? DateTime.Now.ToString(BaseSystemInfo.DateFormat) : txtSendDate.Text;
            //if (!string.IsNullOrEmpty(dgvSendArea.Text))
            //{
            //    var sendAreaArray = dgvSendArea.Text.Split('-');
            //    printBillEntity.SendProvince = sendAreaArray[0];
            //    printBillEntity.SendCity = sendAreaArray[1];
            //    printBillEntity.SendCounty = sendAreaArray[2];
            //}
            //printBillEntity.ReceiveMan = txtReceiveMan.Text;
            //printBillEntity.ReceivePhone = txtReceivePhone.Text;
            //printBillEntity.ReceiveCompany = txtReceiveCompany.Text;
            //printBillEntity.ReceiveAddress = txtReceiveAddress.Text;
            //if (!string.IsNullOrEmpty(dgvReceiveArea.Text))
            //{
            //    var receiveAreaArray = dgvReceiveArea.Text.Split('-');
            //    printBillEntity.ReceiveProvince = receiveAreaArray[0];
            //    printBillEntity.ReceiveCity = receiveAreaArray[1];
            //    printBillEntity.ReceiveCounty = receiveAreaArray[2];
            //}
            //printBillEntity.GoodsName = cmbGoodsName.Text;
            //printBillEntity.Weight = txtGoodsWeight.Text;
            //printBillEntity.GOODS_PAYMENT = string.IsNullOrEmpty(txtGoodsPayment.Text) ? 0 : decimal.Parse(txtGoodsPayment.Text);
            //printBillEntity.TOPAYMENT = string.IsNullOrEmpty(txtToPayMent.Text) ? 0 : decimal.Parse(txtToPayMent.Text);
            //printBillEntity.Remark = txtRemark.Text;
            //printBillEntity.BigPen = txtBigPen.Text;
            var ztoPrintBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
            // 表示更新
            if (!string.IsNullOrEmpty(PrintBillId))
            {
                printBillEntity.Id = BaseBusinessLogic.ConvertToDecimal(PrintBillId);
                ztoPrintBillManager.Update(printBillEntity);
                XtraMessageBox.Show("更新成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _list.Clear();
                var saveCount = int.Parse(cmbPrintNumber.Text);
                if (saveCount == 0)
                {
                    cmbPrintNumber.Text = "1";
                    saveCount = 1;
                }
                // 这里要有订单号可以输入，不能系统生成，这样不科学，2016年6月15日22:33:11，杨恒连
                // 如果他大于1条的话，后面的订单号要系统生成了
                // 最好检查这个订单号有么有重复使用，重复使用就不好了，这样获取到用过的单号是要罚款的
                if (!string.IsNullOrEmpty(txtOrderNumber.Text))
                {
                    saveCount = saveCount - 1;
                    printBillEntity.OrderNumber = txtOrderNumber.Text;
                    ztoPrintBillManager.Add(printBillEntity, true);
                    _list.Add(printBillEntity);
                }
                for (int i = 0; i < saveCount; i++)
                {
                    printBillEntity.OrderNumber = Guid.NewGuid().ToString("N").ToLower();
                    ztoPrintBillManager.Add(printBillEntity, true);
                    _list.Add(printBillEntity);
                }
                XtraMessageBox.Show(string.Format("新增成功{0}条记录", cmbPrintNumber.Text), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region private void BindArea() 绑定省市区到控件

        /// <summary>
        /// 绑定省市区到内存中
        /// </summary>
        private void BindArea()
        {
            try
            {
                // 不要重复的省市区数据
                var cacheDataTable = dgvSendArea.Properties.DataSource as DataTable;
                if (cacheDataTable != null && cacheDataTable.Rows.Count > 0)
                {
                    return;
                }
                // 读取省份内存表
                var areaDt = BillPrintHelper.GetArea(true);
                if (areaDt.Rows.Count > 0)
                {
                    GridLookUpEditHelper.GridLookUpEditInit(dgvSendArea, areaDt, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldFullName);
                    GridLookUpEditHelper.GridLookUpEditInit(dgvReceiveArea, areaDt, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldFullName);
                    dgvSendArea.EditValueChanging += dgvSendArea_EditValueChanging;
                    dgvSendArea.KeyUp += dgvSendArea_KeyUp;
                    dgvSendArea.Enter += dgvSendArea_Enter;
                    dgvReceiveArea.KeyUp += dgvReceiveArea_KeyUp;
                    dgvReceiveArea.EditValueChanging += dgvReceiveArea_EditValueChanging;
                    dgvReceiveArea.Enter += dgvReceiveArea_Enter;
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }

        private void dgvReceiveArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvReceiveArea.Text))
            {
                dgvReceiveArea.ShowPopup();
                return;
            }
            GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling);
        }

        private void dgvSendArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSendArea.Text))
            {
                dgvSendArea.ShowPopup();
                return;
            }
            GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling);
        }

        private void dgvReceiveArea_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvReceiveArea.Text)) return;
            BeginInvoke(new MethodInvoker(() => GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling)));
        }

        private void dgvSendArea_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSendArea.Text)) return;
            BeginInvoke(new MethodInvoker(() => GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling)));
        }

        private void dgvReceiveArea_Enter(object sender, EventArgs e)
        {
            if (dgvReceiveArea.Text == "格式：江苏省-苏州市-吴中区")
            {
                dgvReceiveArea.ShowPopup();
            }
        }

        private void dgvSendArea_Enter(object sender, EventArgs e)
        {
            if (dgvSendArea.Text == "格式：江苏省-苏州市-吴中区")
            {
                dgvSendArea.ShowPopup();
            }
        }

        #endregion

        /// <summary>
        /// 收件区域光标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiveArea_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillCode.Text.Trim()))
            {
                return;
            }
            string tipMessage = null;
            if (txtGoodsPayment.Tag != null)
            {
                tipMessage += string.Format("收件区域支持代收货款，最大值为{0}元<br>", txtGoodsPayment.Tag);
            }
            else
            {
                tipMessage += "收件区域不支持代收货款<br>";
            }
            if (txtToPayMent.Tag != null)
            {
                tipMessage += string.Format("收件区域支持到付款，最大值为{0}元", txtToPayMent.Tag);
            }
            else
            {
                tipMessage += "收件区域不支持到付款";
            }
            dgvReceiveArea.ShowTip(tipMessage, ToolTipLocation.RightBottom, ToolTipType.SuperTip, 3000);
        }

        private void txtReceiveAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReceiveAddress.Text))
            {
                BindPrintMark();
            }
            // 如果收件的省市区存在的话就对的，不用提取收件省市区了。
            if (!string.IsNullOrEmpty(dgvReceiveArea.Text))
            {
                return;
            }
            // 根据收件的地址，获取到百度地图提供的省市区信息
            var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(txtReceiveAddress.Text);
            if (result != null)
            {
                var area = string.Format("{0}-{1}-{2}", result.Result.AddressComponent.Province.Trim(), result.Result.AddressComponent.City.Trim(), result.Result.AddressComponent.District.Trim());
                dgvReceiveArea.Text = area;
                if (string.IsNullOrEmpty(dgvReceiveArea.Text))
                {
                    BaseAreaManager areaManager = new BaseAreaManager(BillPrintHelper.DbHelper);
                    // 去数据库找
                    var countyEntity = areaManager.GetList<BaseAreaEntity>(new KeyValuePair<string, object>(BaseAreaEntity.FieldFullName, result.Result.AddressComponent.District.Trim())).FirstOrDefault();
                    if (countyEntity != null)
                    {
                        var cityEntity = areaManager.GetObject(countyEntity.ParentId);
                        var provinceEntity = areaManager.GetObject(cityEntity.ParentId);
                        dgvReceiveArea.Text = string.Format("{0}-{1}-{2}", provinceEntity.FullName, cityEntity.FullName, countyEntity.FullName);
                    }
                }
            }
        }

        private void BindPrintMark()
        {
            if (BaseSystemInfo.OnInternet)
            {
                if (dgvReceiveArea.Text.Trim().Length == 0) return;
                var sendAreaArray = dgvReceiveArea.Text.Split('-');
                if (sendAreaArray.Length != 3)
                {
                    sendAreaArray = new[] { "", "", "" };
                }
                var receiveAreaArray = dgvReceiveArea.Text.Split('-');
                if (receiveAreaArray.Length != 3)
                {
                    XtraMessageBox.Show(@"收件区域填写不正确，请重新填写", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvReceiveArea.Focus();
                    return;
                }
                var selectedRemark = new List<string>() { sendAreaArray[0], sendAreaArray[1], sendAreaArray[2] };
                var selectedReceiveMark = new List<string>() { receiveAreaArray[0], receiveAreaArray[1], receiveAreaArray[2] };
                var printMark = BillPrintHelper.GetRemaike(string.Join(",", selectedRemark), txtSendAddress.Text.Trim(), string.Join(",", selectedReceiveMark), dgvReceiveArea.Text.Replace("-", "") + txtReceiveAddress.Text);
                txtBigPen.Text = printMark;
                dgvReceiveArea.ShowTip(null, "机打大头笔是：" + printMark, ToolTipLocation.RightBottom, ToolTipType.SuperTip);
            }
            else
            {
                dgvReceiveArea.ShowTip(null, "未连接网络，无法获取大头笔", ToolTipLocation.RightBottom, ToolTipType.SuperTip, 3000);
            }
        }

        /// <summary>
        /// 加载默认发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckGetDefaultSendMan_CheckedChanged(object sender, EventArgs e)
        {
            if (ckGetDefaultSendMan.Checked)
            {
                // 加载默认发件人
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                if (userList.Any())
                {
                    var userEntity = userList.First();
                    txtSendMan.Text = userEntity.Realname;
                    dgvSendArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County);
                    txtSendAddress.Text = userEntity.Address;
                    txtSendPhone.Text = userEntity.Mobile + " " + userEntity.TelePhone;
                    txtSendCompany.Text = userEntity.Company;
                    txtReceiveMan.Focus();
                    txtReceiveMan.Select();
                    BillPrintHelper.SetLoadDefaultSendMan();
                }
                else
                {
                    if (XtraMessageBox.Show(@"未添加默认发件人信息，是否添加？", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        FrmAddSendMan addSendMan = new FrmAddSendMan();
                        addSendMan.ShowDialog();
                        addSendMan.Dispose();
                    }
                }
            }
            else
            {
                BillPrintHelper.SetLoadDefaultSendMan(false);
            }
        }

        /// <summary>
        /// 选择收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckChooseReceiveMan_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChooseReceiveMan.Checked)
            {
                FrmChooseReceiveMan chooseReceiveMan = new FrmChooseReceiveMan();
                if (chooseReceiveMan.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(chooseReceiveMan.receiveManControl1.ChooseId))
                    {
                        ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        ZtoUserEntity userEntity = userManager.GetObject(chooseReceiveMan.receiveManControl1.ChooseId);
                        if (userEntity != null)
                        {
                            // 赋值
                            txtReceiveMan.Text = userEntity.Realname;
                            // 湖北省-荆州市-公安县
                            dgvReceiveArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County).Replace(" ", "");
                            txtReceiveAddress.Text = userEntity.Address;
                            txtReceivePhone.Text = string.Format("{0} {1}", userEntity.Mobile, userEntity.TelePhone);
                            txtReceiveCompany.Text = userEntity.Company;
                            txtReceiveAddress_Leave(this, null);
                        }
                    }
                }
                chooseReceiveMan.Dispose();
            }
        }

        /// <summary>
        /// 识别发/收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckRecognitionAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (ckRecognitionAddress.Checked)
            {
                FrmRecognitionAddress recognitionTaoBaoFrm = new FrmRecognitionAddress() { Owner = this.ParentForm };
                if (recognitionTaoBaoFrm.ShowDialog() == DialogResult.OK)
                {
                    //实现绑定
                    var parentForm = this.ParentForm;
                    if (parentForm != null && parentForm.Tag != null)
                    {
                        ZtoUserEntity userEntity = parentForm.Tag as ZtoUserEntity;
                        if (userEntity != null)
                        {
                            //赋值
                            if (userEntity.Issendorreceive == "1")
                            {
                                InitSendMan(userEntity);
                            }
                            else
                            {
                                InitReceiveMan(userEntity);
                            }
                            parentForm.Tag = null;
                        }
                    }
                }
                recognitionTaoBaoFrm.Dispose();
            }
        }
        void InitSendMan(ZtoUserEntity userEntity)
        {
            txtSendMan.Text = userEntity.Realname;
            dgvSendArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County);
            txtSendAddress.Text = userEntity.Address;
            txtSendPhone.Text = userEntity.Mobile + " " + userEntity.TelePhone;
            txtSendCompany.Text = userEntity.Company;
            txtRemark.Text = userEntity.Remark;
        }

        void InitReceiveMan(ZtoUserEntity userEntity)
        {
            txtReceiveMan.Text = userEntity.Realname;
            dgvReceiveArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County);
            txtReceiveAddress.Text = userEntity.Address;
            txtReceivePhone.Text = userEntity.Mobile + " " + userEntity.TelePhone;
            txtSendCompany.Text = userEntity.Company;
            txtRemark.Text = userEntity.Remark;
            txtReceiveAddress_Leave(this, null);
        }

        /// <summary>
        /// 选择发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckChooseSendMan_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChooseSendMan.Checked)
            {
                var chooseSendMan = new FrmChooseSendMan();
                if (chooseSendMan.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(chooseSendMan.sendManControl1.ChooseId))
                    {
                        var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        ZtoUserEntity userEntity = userManager.GetObject(chooseSendMan.sendManControl1.ChooseId);
                        if (userEntity != null)
                        {
                            txtSendMan.Text = userEntity.Realname;
                            dgvSendArea.Text = string.Format("{0}-{1}-{2}", userEntity.Province, userEntity.City, userEntity.County);
                            txtSendAddress.Text = userEntity.Address;
                            txtSendPhone.Text = userEntity.Mobile + " " + userEntity.TelePhone;
                            txtSendCompany.Text = userEntity.Company;
                            txtReceiveMan.Focus();
                            txtReceiveMan.Select();
                            BillPrintHelper.SetLoadDefaultSendMan();
                        }
                    }
                }
                chooseSendMan.Dispose();
            }
        }

        /// <summary>
        /// 获取申通电子面单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetZtoElecBill_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                var printBillEntity = BuildPrintEntity();
                // 检查单号和订单号是不是都有了，都有了你还获取啥
                if (CheckBill())
                {
                    txtBillCode.ShowTip("已经有单号，不要重复获取");
                }
                else
                {
                    if (CheckOrderNumber())
                    {
                        // 使用默认的订单号
                        printBillEntity.OrderNumber = txtOrderNumber.Text;
                    }
                    else
                    {
                        // 系统自动生成一个订单号
                        txtOrderNumber.Text = Guid.NewGuid().ToString("N");
                        printBillEntity.OrderNumber = txtOrderNumber.Text;
                    }
                    _list.Clear();
                    _list.Add(printBillEntity);
                    CheckZtoElecInfo();
                }
            }
        }

        /// <summary>
        /// 查看底单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowImage_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BillPrintHelper.SaveFilePath))
            {
                Directory.CreateDirectory(BillPrintHelper.SaveFilePath);
            }
            Process.Start(BillPrintHelper.SaveFilePath);
        }

        /// <summary>
        /// 直接打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                _list.Clear();
                _list.Add(BuildPrintEntity());
                if (_list == null || _list.Count == 0)
                {
                    MessageUtil.ShowWarning("请认真填写好发件人和收件人的姓名、电话、省市区、地址");
                    return;
                }
                ZtoPrintBillManager printBillManager = new ZtoPrintBillManager(BillPrintHelper.BackupDbHelper);
                var tempResult = printBillManager.Exists(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode, txtBillCode.Text));
                if (tempResult)
                {
                    if (MessageUtil.ConfirmYesNo("当前单号已经打印过了，是否再次打印？"))
                    {
                        GreatReport();
                        _report.PrintEx(GRPrintGenerateStyle.grpgsPreviewAll, false);
                    }
                }
                else
                {
                    GreatReport();
                    _report.PrintEx(GRPrintGenerateStyle.grpgsPreviewAll, false);
                }
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintView_Click(object sender, EventArgs e)
        {
            // 呐喊  491035839这个qq提出来不要重复打印一个单号，这样浪费纸，打印前检查单号有没有打印了，打印了就弹出提醒让用户确认
            _list.Clear();
            _list.Add(BuildPrintEntity());
            if (_list == null || _list.Count == 0)
            {
                MessageUtil.ShowWarning("请认真填写好发件人和收件人的姓名、电话、省市区、地址");
                return;
            }
            ZtoPrintBillManager printBillManager = new ZtoPrintBillManager(BillPrintHelper.BackupDbHelper);
            var tempResult = printBillManager.Exists(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode, txtBillCode.Text));
            if (tempResult)
            {
                if (MessageUtil.ConfirmYesNo("当前单号已经打印过了，是否再次打印？"))
                {
                    GreatReport();
                    _report.PrintPreview(false);
                }
            }
            else
            {
                GreatReport();
                _report.PrintPreview(false);
            }
        }

        /// <summary>
        /// 复制单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyBillCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBillCode.Text))
            {
                ClipboardHepler.SetText(txtBillCode.Text);
                btnCopyBillCode.ShowTip("复制成功", ToolTipLocation.TopCenter);
            }
            else
            {
                btnCopyBillCode.ShowTip("单号为空，填写好发件人和收件人信息点击获取单号", ToolTipLocation.TopCenter, ToolTipType.Standard, 3000);
            }
        }

        /// <summary>
        /// 检查输入有效性
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtSendMan.Text))
            {
                txtSendMan.ShowTip("发件人姓名必填");
                txtSendMan.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSendPhone.Text))
            {
                txtSendPhone.Focus();
                txtSendPhone.ShowTip("发件人电话必填");
                return false;
            }
            if (string.IsNullOrEmpty(dgvSendArea.Text))
            {
                dgvSendArea.Focus();
                dgvSendArea.ShowTip("发件人省市区必填");
                return false;
            }
            if (string.IsNullOrEmpty(txtSendAddress.Text))
            {
                txtSendAddress.Focus();
                txtSendAddress.ShowTip("发件人详细地址必填");
                return false;
            }
            if (string.IsNullOrEmpty(txtReceiveMan.Text))
            {
                txtReceiveMan.Focus();
                txtReceiveMan.ShowTip("收件人姓名必填");
                return false;
            }
            if (string.IsNullOrEmpty(txtReceivePhone.Text))
            {
                txtReceivePhone.Focus();
                txtReceivePhone.ShowTip("收件人电话必填");
                return false;
            }
            if (string.IsNullOrEmpty(dgvReceiveArea.Text))
            {
                dgvReceiveArea.Focus();
                dgvReceiveArea.ShowTip("收件人省市区必填");
                return false;
            }
            if (string.IsNullOrEmpty(txtReceiveAddress.Text))
            {
                txtReceiveAddress.Focus();
                txtReceiveAddress.ShowTip("收件人详细地址必填");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查有没有填写单号，填写了返回true，否则false
        /// </summary>
        /// <returns></returns>
        private bool CheckBill()
        {
            if (!string.IsNullOrEmpty(txtBillCode.Text))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查有没有填写订单号，填写了返回true，否则false
        /// </summary>
        /// <returns></returns>
        private bool CheckOrderNumber()
        {
            if (!string.IsNullOrEmpty(txtOrderNumber.Text))
            {
                return true;
            }
            return false;
        }

        private ZtoPrintBillEntity BuildPrintEntity()
        {
            //构建实体
            ZtoPrintBillEntity printBillEntity = new ZtoPrintBillEntity();
            if (!string.IsNullOrEmpty(PrintBillId))
            {
                printBillEntity = new ZtoPrintBillManager(BillPrintHelper.DbHelper).GetObject(PrintBillId);
            }
            printBillEntity.SendMan = txtSendMan.Text;
            printBillEntity.SendPhone = txtSendPhone.Text;
            printBillEntity.SendCompany = txtSendCompany.Text;
            printBillEntity.SendAddress = txtSendAddress.Text;
            printBillEntity.SendDate = string.IsNullOrEmpty(txtSendDate.Text) ? DateTime.Now.ToString(BaseSystemInfo.DateFormat) : txtSendDate.Text;
            if (!string.IsNullOrEmpty(dgvSendArea.Text))
            {
                var sendAreaArray = dgvSendArea.Text.Split('-');
                printBillEntity.SendProvince = sendAreaArray[0];
                printBillEntity.SendCity = sendAreaArray[1];
                printBillEntity.SendCounty = sendAreaArray[2];
            }
            printBillEntity.ReceiveMan = txtReceiveMan.Text;
            printBillEntity.ReceivePhone = txtReceivePhone.Text;
            printBillEntity.ReceiveCompany = txtReceiveCompany.Text;
            printBillEntity.ReceiveAddress = txtReceiveAddress.Text;
            if (!string.IsNullOrEmpty(dgvReceiveArea.Text))
            {
                var receiveAreaArray = dgvReceiveArea.Text.Split('-');
                printBillEntity.ReceiveProvince = receiveAreaArray[0];
                printBillEntity.ReceiveCity = receiveAreaArray[1];
                printBillEntity.ReceiveCounty = receiveAreaArray[2];
            }
            printBillEntity.GoodsName = cmbGoodsName.Text;
            printBillEntity.Weight = txtGoodsWeight.Text;
            printBillEntity.GOODS_PAYMENT = string.IsNullOrEmpty(txtGoodsPayment.Text) ? 0 : decimal.Parse(txtGoodsPayment.Text);
            printBillEntity.TOPAYMENT = string.IsNullOrEmpty(txtToPayMent.Text) ? 0 : decimal.Parse(txtToPayMent.Text);
            printBillEntity.Remark = txtRemark.Text;
            printBillEntity.BigPen = txtBigPen.Text;
            // 2016年6月18日11:19:58 杨恒连加上
            printBillEntity.OrderNumber = txtOrderNumber.Text;
            printBillEntity.BillCode = txtBillCode.Text;
            return printBillEntity;
        }

        private void CheckZtoElecInfo()
        {
            ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
            if (elecUserInfoEntity != null)
            {
                if (_list == null || _list.Count == 0)
                {
                    MessageUtil.ShowWarning("请认真填写好发件人和收件人的姓名、电话、省市区、地址");
                    return;
                }
                var list = ZtoElecBillHelper.BindElecBillByCustomerId(_list, elecUserInfoEntity);
                if (list != null && list.Any())
                {
                    txtBillCode.Text = list.First().BillCode;
                    txtBigPen.Text = list.First().BigPen;
                    _list.First().BillCode = txtBillCode.Text;
                    _list.First().BigPen = txtBigPen.Text;
                    var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                    // 表示更新
                    if (!string.IsNullOrEmpty(PrintBillId))
                    {
                        list.First().Id = BaseBusinessLogic.ConvertToDecimal(PrintBillId);
                        printBillManager.Update(list.First());
                        MessageUtil.ShowTips("获取成功，已更新本地");
                    }
                    else
                    {
                        // 新增
                        printBillManager.Add(list.First(), true);
                        MessageUtil.ShowTips("获取成功，已保存本地");
                    }
                }
                else
                {
                    MessageUtil.ShowError("全部获取电子面单单号失败");
                }
            }
            else
            {
                // 在默认发件人那边修改个人的商家ID信息
                // 获取系统是否有默认发件人，如果有救修改，如果没有就新增
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                if (!userList.Any())
                {
                    XtraMessageBox.Show("系统未绑定默认发件人和商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmAddSendMan addSendMan = new FrmAddSendMan();
                    addSendMan.ShowDialog();
                    addSendMan.Dispose();
                }
                else
                {
                    XtraMessageBox.Show("默认发件人未绑定商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmAddSendMan frmSendMan = new FrmAddSendMan { Id = userList.First().Id.ToString() };
                    frmSendMan.ShowDialog();
                    frmSendMan.Dispose();
                }
            }
        }
        /// <summary>
        /// 清空收件人和单号和订单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearReceiveMan_Click(object sender, EventArgs e)
        {
            if (MessageUtil.ConfirmYesNo("确定清空收件人信息？"))
            {
                txtReceivePhone.Text = "";
                txtReceiveAddress.Text = "";
                txtReceiveCompany.Text = "";
                txtReceiveMan.Text = "";
                dgvReceiveArea.Text = "";
                txtReceiveMan.Focus();
            }
        }

        /// <summary>
        /// 清空单号和订单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearBillCodeAndOrderNumber_Click(object sender, EventArgs e)
        {
            if (MessageUtil.ConfirmYesNo("确定清空单号和订单号信息？"))
            {
                txtBillCode.Text = "";
                txtOrderNumber.Text = "";
            }
        }
    }
}
