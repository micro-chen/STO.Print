//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Scrolling;

namespace STO.Print
{
    using AddBillForm;
    using DevExpress.Utils;
    using DevExpress.XtraBars.Alerter;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Columns;
    using DevExpress.XtraEditors.Drawing;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraEditors.ViewInfo;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;
    using DevExpress.XtraPrinting;
    using DotNet.Utilities;
    using grproLib;
    using Manager;
    using Model;
    using System.Diagnostics;
    using Utilities;

    /// <summary>
    /// 打印数据窗体
    ///
    /// 修改纪录
    ///
    ///	 2015-07-17  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///  2015-11-02  加入复制单元格值的小功能
    ///  2016-2-2    勾选单子不是前面有个框要打上勾才会获取单号 但是获取单号之后 这些打勾就会自动取消 然后我还要在重新勾选一遍单子在去获取大头笔 获取完了之后勾选的又没了 还要在勾一遍才可以打印单子
    ///  2016-2-2    要是能把市面上主流快递的面单模板都添加到模板里面就好了
    ///  2016-6-20   加上双击行就可以编辑的功能，功能比较隐蔽不太好，群里有人提出来的，好的建议都会采纳
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-17</date>
    /// </author>
    /// </summary>
    public partial class FrmPrintData : BaseForm
    {
        /// <summary>
        /// 定义Grid++Report报表主对象
        /// </summary>
        readonly GridppReport _report = new GridppReport();

        /// <summary>
        /// 打印实体集合
        /// </summary>
        List<ZtoPrintBillEntity> _list = new List<ZtoPrintBillEntity>();

        protected delegate void UpdateControlText1();

        protected void UpdateControlText()
        {
            //  gridControlBills.UseEmbeddedNavigator = true;
            //   gridControlBills.UseDisabledStatePainter = true;
            GridDataBind();
            for (int i = 1; i < gridViewBills.Columns.Count; i++)
            {
                if (gridViewBills.Columns[i].FieldName == "单号" || gridViewBills.Columns[i].FieldName == "订单号" || gridViewBills.Columns[i].FieldName == "创建时间")
                {
                    // 只读
                    gridViewBills.Columns[i].OptionsColumn.ReadOnly = true;
                    // 不可编辑
                    gridViewBills.Columns[i].AppearanceCell.Options.UseTextOptions = false;
                }
                else
                {
                    // 可以编辑
                    gridViewBills.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                }
            }
            gridViewBills.OptionsBehavior.Editable = true;
            gridViewBills.OptionsFind.AlwaysVisible = true;
            gridViewBills.FindPanelVisible = true;
            gridViewBills.ShowFindPanel();
            //gridViewBills.ShowCustomization();
            //gridViewBills.ShowEditor();
            //gridViewBills.ShowEditorByMouse();
        }

        public FrmPrintData()
        {
            InitializeComponent();
        }

        private void FrmPrintDataLoad(object sender, EventArgs e)
        {
            try
            {
                var t = new Task(BindPrintData);
                t.Start();
                // 一定要注册，否则发布后会有水印
                _report.Title = BaseSystemInfo.SoftFullName;
                _report.Register(BillPrintHelper.GridReportKey);
                _report.Initialize += ReportInitialize;
                _report.FetchRecord += ReportFetchRecord;
                _report.PrintBegin += ReportPrintBegin;
                _report.PrintEnd += ReportPrintEnd;
                alertElecUserInfo.Show(this, new AlertInfo(AppMessage.MSG0000, "填写默认发件人有利于提取申通大头笔", "填写默认发件人有利于提取申通大头笔，可以看看帮专家册和视频，还可以加界面上的三个群", null, "http://yd.zt-express.com/Help/Index2"));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void BindPrintData()
        {
            var updateControl = new UpdateControlText1(UpdateControlText);
            Invoke(updateControl);
        }

        /// <summary>
        /// 打印机开始打印
        /// </summary>
        void ReportPrintBegin()
        {
            string alertInfo = _report.Printer.PrinterName + "正在打印" + _list.Count + "条打印记录，已经提交给打印机，提交时间：" + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
            alertElecUserInfo.Show(this, AppMessage.MSG0000, alertInfo);
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
            alertElecUserInfo.Show(this, AppMessage.MSG0000, alertInfo1);
            STO.Print.Utilities.PrinterHelper.OpenPrinterStatusWindow(_report.Printer.PrinterName);
            //FrmPrintStatus frmPrintStatus = new FrmPrintStatus();
            //frmPrintStatus.Show(this);
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
                XtraMessageBox.Show(string.Format("打印任务成功提交给打印机，可以在历史记录中查询打印数据，新增{0}条打印记录。", resultCount), AppMessage.MSG0000, MessageBoxButtons.OK);
            }
            else
            {
                XtraMessageBox.Show(string.Format("打印任务成功提交给打印机，{0}条单号已经打印过了，单号重复打印不存入历史记录。", _list.Count), AppMessage.MSG0000, MessageBoxButtons.OK);
            }
        }

        #region private void btnAddBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form addBill = BillPrintHelper.GetDefaultAddBillForm();
            addBill.Owner = this;
            if (addBill.ShowDialog() == DialogResult.OK)
            {
                GridDataBind();
            }
            addBill.Dispose();
        }
        #endregion

        #region public void GridDataBind() 打印数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void GridDataBind()
        {
            DataTable dt = BillPrintHelper.DbHelper.Fill(BillPrintHelper.CmdStrForZtoBillPrinter);
            // 增加个CheckBox列
            dt.Columns.Add("Check", typeof(bool));
            // 设置选择列的位置
            dt.Columns["Check"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Check"] = false;
            }
            // 设置gridview列头的字体大小
            gridViewBills.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);
            // 设置gridview列头居中
            gridViewBills.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gridControlBills.DataSource = dt;
            gridViewBills.OptionsView.ColumnAutoWidth = false;
            gridViewBills.Columns["Id"].Visible = false;
            gridViewBills.Columns["Id"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["Check"].Width = 22;
            gridViewBills.Columns["Check"].OptionsColumn.ShowCaption = false;
            gridViewBills.Columns["Check"].OptionsColumn.AllowSort = DefaultBoolean.False;
            gridViewBills.Columns["Check"].OptionsColumn.AllowEdit = false;
            gridViewBills.Columns["Check"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["收件人姓名"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridViewBills.Columns["收件人姓名"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind_;
            gridViewBills.Columns["收件人姓名"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["收件人姓名"].AppearanceCell.BorderColor = Color.Transparent;
            // gridViewBills.Columns["收件人姓名"].AppearanceCell.Font = new Font("宋体", 9, FontStyle.Bold);
            gridViewBills.Columns["收件人姓名"].AppearanceCell.ForeColor = Color.DarkSlateGray;
            gridViewBills.Columns["单号"].Width = 100;
            gridViewBills.Columns["订单号"].Width = 120;
            // gridViewBill.Columns["单号"].Visible = false;
            gridViewBills.Columns["发件日期"].Width = 100;
            gridViewBills.Columns["创建时间"].Width = 160;
            gridViewBills.Columns["发件网点"].Width = 80;
            gridViewBills.Columns["发件人姓名"].Width = 80;
            gridViewBills.Columns["始发地"].Width = 80;
            gridViewBills.Columns["发件省份"].Width = 80;
            gridViewBills.Columns["发件城市"].Width = 80;
            gridViewBills.Columns["发件区县"].Width = 80;
            gridViewBills.Columns["发件单位"].Width = 200;
            gridViewBills.Columns["发件部门"].Width = 80;
            gridViewBills.Columns["发件电话"].Width = 100;
            gridViewBills.Columns["发件邮编"].Width = 80;
            gridViewBills.Columns["发件详细地址"].Width = 300;
            gridViewBills.Columns["收件人姓名"].Width = 80;
            gridViewBills.Columns["目的地"].Width = 80;
            gridViewBills.Columns["收件省份"].Width = 80;
            //this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            //this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            //// 
            //// repositoryItemSearchLookUpEdit1
            //// 
            //this.repositoryItemSearchLookUpEdit1.AutoHeight = false;
            //this.repositoryItemSearchLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            //new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            //this.repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
            //this.repositoryItemSearchLookUpEdit1.View = this.repositoryItemSearchLookUpEdit1View;
            //// 
            //// repositoryItemSearchLookUpEdit1View
            //// 
            //this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            //this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            //this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            //this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;

            //this.gridControlBills.RepositoryItems.Add(this.repositoryItemSearchLookUpEdit1);
            //gridViewBills.Columns["收件省份"].ColumnEdit = repositoryItemSearchLookUpEdit1;
            //((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            gridViewBills.Columns["收件城市"].Width = 80;
            gridViewBills.Columns["收件区县"].Width = 80;
            gridViewBills.Columns["收件电话"].Width = 100;
            gridViewBills.Columns["收件人单位"].Width = 200;
            gridViewBills.Columns["收件人邮编"].Width = 80;
            gridViewBills.Columns["收件人详细地址"].Width = 300;
            gridViewBills.Columns["物品类型"].Width = 80;
            gridViewBills.Columns["大头笔"].Width = 120;
            //gridViewBills.Columns["大头笔"].AppearanceCell.Font = new Font("宋体", 9, FontStyle.Bold);
            gridViewBills.Columns["大头笔"].AppearanceCell.ForeColor = Color.DarkSlateGray;
            gridViewBills.Columns["长"].Width = 30;
            gridViewBills.Columns["宽"].Width = 30;
            gridViewBills.Columns["高"].Width = 30;
            gridViewBills.Columns["重量"].Width = 60;
            gridViewBills.Columns["重量"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["重量"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind__0_;
            gridViewBills.Columns["付款方式"].Width = 60;
            gridViewBills.Columns["运费"].Width = 60;
            gridViewBills.Columns["运费"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["运费"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind__0_;
            gridViewBills.Columns["数量"].Width = 60;
            gridViewBills.Columns["数量"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["数量"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind__0_;
            gridViewBills.Columns["备注"].Width = 100;
            gridViewBills.Columns["备注"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gridViewBills.Columns["打印状态"].Width = 80;
            // gridViewBills.Columns["打印状态"].AppearanceCell.ForeColor = Color.Blue;
            gridViewBills.Columns["打印状态"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gridViewBills.Columns["创建时间"].DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss";
            gridViewBills.Columns["到付款"].Width = 60;
            gridViewBills.Columns["到付款"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["到付款"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind__0_;
            gridViewBills.Columns["代收货款"].Width = 60;
            gridViewBills.Columns["代收货款"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridViewBills.Columns["代收货款"].SummaryItem.DisplayFormat = STO.Print.Properties.Resources.FrmPrintData_GridDataBind__0_;
            // 选中之后的值要重新打上勾，不然总是打勾好烦人 2016年2月2日14:36:18
            // 网点反馈的建议： 不是 勾选单子不是前面有个框要打上勾才会获取单号 但是获取单号之后 这些打勾就会自动取消 然后我还要在重新勾选一遍单子在去获取大头笔 获取完了之后勾选的又没了 还要在勾一遍才可以打印单子
            // http://biancheng.dnbcw.info/net/438728.html 灵感来自这里
            // 网点不想每次都滚动到最上面，所以这里也要控制滚动条位置的问题，杨恒连，2016年7月2日11:18:03 
            List<int> focusRowIndex = new List<int>();
            if (_list.Any())
            {
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    for (int i = 0; i < gridViewBills.DataRowCount; i++)
                    {
                        if (gridViewBills.GetRowCellValue(i, "订单号").ToString() == ztoPrintBillEntity.OrderNumber)
                        {
                            gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], true);
                            focusRowIndex.Add(gridViewBills.GetRowHandle(i));
                        }
                    }
                }
                //如果是全选的就滚动到最后一个就行了
                if (focusRowIndex.Count == gridViewBills.RowCount)
                {
                    gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
                }
                else
                {
                    if (focusRowIndex.Count > 0)
                    {
                        gridViewBills.FocusedRowHandle = focusRowIndex.First();
                    }
                    else
                    {
                        gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
                    }
                }
            }
            else
            {
                // 首次加载如果有数据就滚动到最后一行了，没有就在第一行
                gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
            }
        }
        #endregion

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
                    var frmTemplateSetting = new FrmTemplateSetting { Owner = this };
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
                var tempCount = 0;
                var elecExtendInfoEntity = BillPrintHelper.GetElecUserInfoExtendEntity();
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
                    //if (_senderAddress != null)
                    //{
                    //    _senderAddress.AsString = billEntity.SendProvince + billEntity.SendCity + billEntity.SendCounty + billEntity.SendAddress;
                    //}
                    if (_senderAddress != null)
                    {
                        var tempAddress = billEntity.SendAddress;
                        if (!string.IsNullOrEmpty(tempAddress))
                        {
                            if (!string.IsNullOrEmpty(billEntity.SendProvince))
                            {
                                tempAddress = tempAddress.Replace(billEntity.SendProvince, "");
                            }
                            if (!string.IsNullOrEmpty(billEntity.SendCity))
                            {
                                tempAddress = tempAddress.Replace(billEntity.SendCity, "");
                            }
                            if (!string.IsNullOrEmpty(billEntity.SendCounty))
                            {
                                tempAddress = tempAddress.Replace(billEntity.SendCounty, "");
                            }
                        }
                        _senderAddress.AsString = billEntity.SendProvince + billEntity.SendCity + billEntity.SendCounty + tempAddress;
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
        /// 画GridControl首列复选框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="Checked"></param>
        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked)
        {
            ControlGraphicsInfoArgs args = null;
            var chkEdit = new RepositoryItemCheckEdit();
            var info = chkEdit.CreateViewInfo() as CheckEditViewInfo;
            var painter = chkEdit.CreatePainter() as CheckEditPainter;
            if (info != null)
            {
                info.EditValue = Checked;
                info.Bounds = r;
                info.PaintAppearance.ForeColor = Color.Black;
                info.CalcViewInfo(g);
                args = new ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            }
            painter.Draw(args);
            if (args != null) args.Cache.Dispose();
        }

        private void gridViewBills_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView != null && e.Column == gridView.Columns["Check"])
            {
                e.Info.InnerElements.Clear();
                e.Info.Appearance.ForeColor = Color.Blue;
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, GetCheckedCount(gridViewBills) == gridViewBills.DataRowCount);
                e.Handled = true;
            }
        }

        /// <summary>
        /// 获取到已选择的数量
        /// </summary>
        /// <returns></returns>
        public int GetCheckedCount(GridView gridViewBill)
        {
            int count = 0;
            for (int i = 0; i < gridViewBill.DataRowCount; i++)
            {
                if ((bool)gridViewBill.GetRowCellValue(i, gridViewBill.Columns["Check"]))
                {
                    count++;
                }
            }
            return count;
        }

        private void GridViewBillsCustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

        private void GridViewBillsMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                Point point = gridViewBills.GridControl.PointToClient(MousePosition);
                GridHitInfo gridHitInfo = gridViewBills.CalcHitInfo(point);

                if (gridHitInfo.InColumn && gridHitInfo.Column.FieldName == "Check")
                {
                    if (GetCheckedCount(gridViewBills) == gridViewBills.DataRowCount)
                    {
                        UnCheckAll(gridViewBills);
                    }
                    else
                    {
                        CheckAll(gridViewBills);
                    }
                }

                if (gridHitInfo.InRow && gridHitInfo.InRowCell && gridHitInfo.Column.FieldName == "Check")
                {
                    if (Convert.ToBoolean(gridViewBills.GetFocusedRowCellValue("Check")) == false)
                    {
                        CheckSingle(gridViewBills);
                    }
                    else
                    {
                        UnCheckSingle(gridViewBills);
                    }
                }
            }
        }

        /// <summary>
        /// 选择全部 
        /// </summary>
        private void CheckAll(GridView gridViewBill)
        {
            for (int i = 0; i < gridViewBill.DataRowCount; i++)
            {
                gridViewBill.SetRowCellValue(i, gridViewBill.Columns["Check"], true);
            }
        }

        /// <summary>
        /// 反选全部
        /// </summary>
        private void UnCheckAll(GridView gridViewBill)
        {
            for (int i = 0; i < gridViewBill.DataRowCount; i++)
            {
                gridViewBill.SetRowCellValue(i, gridViewBill.Columns["Check"], false);
            }
        }

        /// <summary>
        /// 选择一个
        /// </summary>
        private void CheckSingle(GridView gridViewBill)
        {
            gridViewBill.SetRowCellValue(gridViewBill.FocusedRowHandle, gridViewBill.Columns["Check"], true);
        }

        /// <summary>
        /// 反选一个
        /// </summary>
        private void UnCheckSingle(GridView gridViewBill)
        {
            gridViewBill.SetRowCellValue(gridViewBill.FocusedRowHandle, gridViewBill.Columns["Check"], false);
        }

        private void GridViewBillsRowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //gridViewBills.Appearance.OddRow.BackColor = Color.White; // 设置奇数行颜色 // 默认也是白色 可以省略 
            //gridViewBills.OptionsView.EnableAppearanceOddRow = true; // 使能 // 和和上面绑定 同时使用有效 
            //gridViewBills.Appearance.EvenRow.BackColor = Color.FromArgb(241, 241, 247); // 设置偶数行颜色 
            //gridViewBills.OptionsView.EnableAppearanceEvenRow = true; // 使能 // 和和上面绑定 同时使用有效
            // 单选
            if (e.RowHandle == gridViewBills.FocusedRowHandle)
            {
                e.Appearance.Font = new Font("宋体", 9, FontStyle.Bold);
                //e.Appearance.ForeColor = Color.Blue;
                e.Appearance.BackColor = Color.FromArgb(235, 235, 242);
            }
            else
            {
                // 多选的行也要明显标记出来，2016-7-2 16:24:45，杨恒连
                if (gridViewBills.GetRowCellValue(e.RowHandle, "Check").ToString().ToLower() == "true")
                {
                    e.Appearance.Font = new Font("宋体", 9, FontStyle.Bold);
                    // e.Appearance.ForeColor = Color.Blue;
                    e.Appearance.BackColor = Color.FromArgb(235, 235, 242);
                }
            }
        }

        #region private void btnEditBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 编辑打印信息
        /// <summary>
        /// 编辑打印信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                string selectedId = string.Empty;
                for (var rowIndex = 0; rowIndex < gridViewBills.RowCount; rowIndex++)
                {
                    var objValue = gridViewBills.GetRowCellValue(rowIndex, gridViewBills.Columns["Check"]);
                    if (objValue == null) continue;
                    bool isCheck;
                    bool.TryParse(objValue.ToString(), out isCheck);
                    if (isCheck)
                    {
                        selectedId = gridViewBills.GetRowCellValue(rowIndex, gridViewBills.Columns["Id"]).ToString();
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(selectedId))
                {
                    var addBill = BillPrintHelper.GetDefaultAddBillForm();
                    addBill.Owner = this;
                    addBill.SelectedId = selectedId;
                    if (addBill.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                    addBill.Dispose();
                }
                else
                {
                    XtraMessageBox.Show(@"请选择一条运单记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单信息。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void btnDeleteBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                var idList = new List<string>();
                for (var rowIndex = 0; rowIndex < gridViewBills.RowCount; rowIndex++)
                {
                    var objValue = gridViewBills.GetRowCellValue(rowIndex, gridViewBills.Columns["Check"]);
                    if (objValue == null) continue;
                    bool isCheck;
                    bool.TryParse(objValue.ToString(), out isCheck);
                    if (isCheck)
                    {
                        var id = gridViewBills.GetRowCellValue(rowIndex, "Id");
                        idList.Add(id.ToString());
                    }
                }
                if (idList.Any())
                {
                    if (XtraMessageBox.Show(string.Format("确定删除选中的{0}条记录吗？删除之前建议将数据导出Excel", idList.Count), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                    using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString))
                    {
                        try
                        {
                            dbHelper.BeginTransaction();
                            var manager = new ZtoPrintBillManager(dbHelper);
                            foreach (string id in idList)
                            {
                                var entity = manager.GetObject(id);
                                if (entity != null)
                                {
                                    manager.CurrentTableName = "ZTO_RECYCLE_BILL";
                                    manager.AddToRecycleBill(entity, true, false, this.Text);
                                    manager.CurrentTableName = ZtoPrintBillEntity.TableName;
                                }
                                manager.Delete(id);
                            }
                            dbHelper.CommitTransaction();
                            GridDataBind();
                        }
                        catch (Exception ex)
                        {
                            dbHelper.RollbackTransaction();
                            ProcessException(ex);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单信息。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void btnRefreshBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridDataBind();
        }
        #endregion

        #region private void btnDownloadExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 下载导入模板
        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownloadExcelItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownloadTemplate.DownloadExcelTemplate("ImportBill");
        }
        #endregion

        #region private void btnImportFreeExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导入任意Excel
        /// <summary>
        /// 导入任意Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportFreeExcelItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var importFreeExcel = new FrmImportFreeExcel();
            if (importFreeExcel.ShowDialog() == DialogResult.OK)
            {
                GridDataBind();
            }
            importFreeExcel.Dispose();
        }
        #endregion

        #region private void btnExport2003Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导出2003Excel
        /// <summary>
        /// 导出2003Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport2003ExcelItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xls, gridControlBills, gridViewBills);
            //try
            //{
            //    var targetDt = gridControlBills.DataSource as DataTable;
            //    if (targetDt != null)
            //    {
            //        if (targetDt.Columns.Contains("Id"))
            //        {
            //            targetDt.Columns.Remove("Id");
            //        }
            //        if (targetDt.Columns.Contains("Check"))
            //        {
            //            targetDt.Columns.Remove("Check");
            //        }
            //        ExcelHelper.ExportExcel(targetDt);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    XtraMessageBox.Show(exception.Message, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    GridDataBind();
            //}
        }
        #endregion

        #region private void btnExport2007Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导出2007Excel
        /// <summary>
        /// 导出2007Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExport2007ExcelItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gridControlBills, gridViewBills);
        }
        #endregion

        #region private void btnExportCSV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导出CSV
        /// <summary>
        /// 导出CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportCsvItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Csv, gridControlBills, gridViewBills);
        }
        #endregion

        #region private void tspCopyCellText_Click(object sender, EventArgs e) 复制选中单元格内容
        /// <summary>
        /// 复制选中单元格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspCopyCellTextClick(object sender, EventArgs e)
        {
            if (gridViewBills.GetFocusedDataRow() == null) return;
            var text = gridViewBills.GetFocusedRowCellValue(gridViewBills.FocusedColumn.GetCaption()).ToString();
            Clipboard.SetText(text);
        }
        #endregion

        #region private void btnExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导出HTML
        /// <summary>
        /// 导出HTML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportHtmlItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Html, gridControlBills, gridViewBills);
        }
        #endregion

        #region private void btnExportWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导出Word
        /// <summary>
        /// 导出Word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportWordItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Word, gridControlBills, gridViewBills);
        }
        #endregion

        #region private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导出PDF
        /// <summary>
        /// 导出PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportPdfItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Pdf, gridControlBills, gridViewBills);
        }
        #endregion

        #region private void btnPrintView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 打印预览
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintViewItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // 给个重复打印的提醒，2016-4-19 用户提出来的QQ：51255-千灯  84659651
                var printedBillCount = 0;
                List<ZtoPrintBillEntity> printedBillList = new List<ZtoPrintBillEntity>();
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    ZtoPrintBillManager printBillManager = new ZtoPrintBillManager(BillPrintHelper.BackupDbHelper);
                    var tempResult = printBillManager.Exists(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode, ztoPrintBillEntity.BillCode));
                    if (tempResult)
                    {
                        printedBillList.Add(ztoPrintBillEntity);
                        ++printedBillCount;
                    }
                }
                if (printedBillCount > 0)
                {
                    if (MessageUtil.ConfirmYesNo(string.Format("已经有{0}个单号打印过了，重复打印没必要，是否过滤掉？", printedBillCount)))
                    {
                        for (int i = 0; i < gridViewBills.DataRowCount; i++)
                        {
                            foreach (ZtoPrintBillEntity ztoPrintBillEntity in printedBillList)
                            {
                                if (gridViewBills.GetRowCellValue(i, gridViewBills.Columns["单号"]).ToString() == ztoPrintBillEntity.BillCode)
                                {
                                    gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], false);
                                    break;
                                }
                            }
                        }
                        _list = GetCheckedRecord(gridViewBills);
                        GreatReport();

                        _report.PrintPreview(true);
                    }
                    else
                    {
                        GreatReport();
                        _report.PrintPreview(true);
                    }
                }
                else
                {
                    GreatReport();
                    _report.PrintPreview(true);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region public List<ZtoPrintBillEntity> GetCheckedRecord(GridView gridView) 获取到选择的运单实体
        /// <summary>
        /// 获取到选择的运单实体
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        public List<ZtoPrintBillEntity> GetCheckedRecord(GridView gridView)
        {
            var list = new List<ZtoPrintBillEntity>();
            if (gridView.RowCount > 0)
            {
                for (var rowIndex = 0; rowIndex < gridView.RowCount; rowIndex++)
                {
                    var objValue = gridView.GetRowCellValue(rowIndex, gridView.Columns["Check"]);
                    if (objValue == null) continue;
                    bool isCheck;
                    bool.TryParse(objValue.ToString(), out isCheck);
                    if (isCheck)
                    {
                        var entity = new ZtoPrintBillEntity
                                         {
                                             BillCode = gridView.GetRowCellValue(rowIndex, "单号").ToString(),
                                             SendMan = gridView.GetRowCellValue(rowIndex, "发件人姓名").ToString(),
                                             SendAddress = gridView.GetRowCellValue(rowIndex, "发件详细地址").ToString(),
                                             SendCompany = gridView.GetRowCellValue(rowIndex, "发件单位").ToString(),
                                             SendPhone = gridView.GetRowCellValue(rowIndex, "发件电话").ToString()
                                         };
                        entity.SendDeparture = gridView.GetRowCellValue(rowIndex, "始发地").ToString();
                        entity.ReceiveMan = gridView.GetRowCellValue(rowIndex, "收件人姓名").ToString();
                        entity.ReceiveAddress = gridView.GetRowCellValue(rowIndex, "收件人详细地址").ToString();
                        entity.ReceiveCompany = gridView.GetRowCellValue(rowIndex, "收件人单位").ToString();
                        entity.ReceivePhone = gridView.GetRowCellValue(rowIndex, "收件电话").ToString();
                        entity.ReceiveProvince = gridView.GetRowCellValue(rowIndex, "收件省份").ToString();
                        entity.ReceiveCity = gridView.GetRowCellValue(rowIndex, "收件城市").ToString();
                        entity.ReceiveCounty = gridView.GetRowCellValue(rowIndex, "收件区县").ToString();
                        entity.ReceiveDestination = gridView.GetRowCellValue(rowIndex, "目的地").ToString();
                        entity.GoodsName = gridView.GetRowCellValue(rowIndex, "物品类型").ToString();
                        entity.TotalNumber = gridView.GetRowCellValue(rowIndex, "数量").ToString();
                        entity.SendDate = gridView.GetRowCellValue(rowIndex, "发件日期").ToString();
                        entity.SendSite = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "发件网点"));
                        entity.Weight = gridView.GetRowCellValue(rowIndex, "重量").ToString();
                        entity.TranFee = gridView.GetRowCellValue(rowIndex, "运费").ToString();
                        entity.SendProvince = gridView.GetRowCellValue(rowIndex, "发件省份").ToString();
                        entity.SendCity = gridView.GetRowCellValue(rowIndex, "发件城市").ToString();
                        entity.SendCounty = gridView.GetRowCellValue(rowIndex, "发件区县").ToString();
                        entity.BigPen = gridView.GetRowCellValue(rowIndex, "大头笔").ToString();
                        entity.Id = Convert.ToDecimal(gridView.GetRowCellValue(rowIndex, "Id").ToString());
                        entity.OrderNumber = gridView.GetRowCellValue(rowIndex, "订单号").ToString();
                        entity.TOPAYMENT = string.IsNullOrEmpty(gridView.GetRowCellValue(rowIndex, "到付款").ToString()) ? 0 : Convert.ToDecimal(gridView.GetRowCellValue(rowIndex, "到付款").ToString());
                        entity.GOODS_PAYMENT = string.IsNullOrEmpty(gridView.GetRowCellValue(rowIndex, "代收货款").ToString()) ? 0 : Convert.ToDecimal(gridView.GetRowCellValue(rowIndex, "代收货款").ToString());
                        entity.CreateOn = BaseBusinessLogic.ConvertToDateTime(gridView.GetRowCellValue(rowIndex, "创建时间"));
                        entity.ReceivePostcode = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "收件人邮编"));
                        entity.SendPostcode = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "发件邮编"));
                        entity.Length = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "长"));
                        entity.Width = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "宽"));
                        entity.Height = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "高"));
                        entity.PaymentType = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "付款方式"));
                        entity.Remark = BaseBusinessLogic.ConvertToString(gridView.GetRowCellValue(rowIndex, "备注"));
                        // 发件日期如果为空就用当前的时间作为发件日期
                        if (string.IsNullOrEmpty(entity.SendDate))
                        {
                            entity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                        }
                        list.Add(entity);
                    }
                }
            }
            return list;
        }
        #endregion

        #region private void btnQuickPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 快速打印
        /// <summary>
        /// 快速打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuickPrintItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                GreatReport();
                var defaultPrinter = BillPrintHelper.GetDefaultPrinter();
                if (!string.IsNullOrEmpty(defaultPrinter))
                {
                    _report.Printer.PrinterName = defaultPrinter;
                }
                else
                {
                    if (XtraMessageBox.Show(@"没有设置默认打印机，立刻设置", AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        var frmPrinterSetting = new FrmPrinterSetting { Owner = this };
                        if (frmPrinterSetting.ShowDialog() == DialogResult.OK)
                        {
                            var defaultPrinter1 = BillPrintHelper.GetDefaultPrinter();
                            if (!string.IsNullOrEmpty(defaultPrinter1))
                            {
                                _report.Printer.PrinterName = defaultPrinter;
                            }
                        }
                    }
                }
                if (XtraMessageBox.Show(string.Format("是否立刻打印{0}条记录？打印电子面单请一定检查是否有单号和大头笔，防止浪费热敏纸，普通面单检查是否有大头笔，防止浪费面单，土豪随意。", _list.Count), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    _report.PrintEx(GRPrintGenerateStyle.grpgsPreviewAll, false);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加打印数据", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void tspUpdateProvince_Click(object sender, EventArgs e) 智能识别收件省市区
        /// <summary>
        /// 智能识别收件省市区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int tempCount = 0;
        private void TspUpdateProvinceClick(object sender, EventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                tempCount = 0;
                prbMessage.Maximum = _list.Count;
                prbMessage.Value = 0;
                // 最大线程数可以让用户自己选择，如果提取不超过50就直接使用一个线程就行了，没必要浪费
                var maxThreadCount = _list.Count > 100 ? Convert.ToInt32(Math.Ceiling(_list.Count / 100.0)) : 1;
                if (maxThreadCount > 10)
                {
                    // 超过3000个就开启10个线程执行导入
                    if (_list.Count >= 3000)
                    {
                        maxThreadCount = 15;
                        if (_list.Count >= 10000)
                        {
                            maxThreadCount = 20;
                        }
                    }
                    else
                    {
                        // 开启5个就够了，防止电脑卡
                        maxThreadCount = 5;
                    }
                }
                //如果是N个线程，需要先对数据进行处理
                var listNo = _list.Count / maxThreadCount;
                // 先停止所有的线程，防止开启太多的线程
                for (int i = 1; i < maxThreadCount + 1; i++)
                {
                    var tempList = _list.Skip((i - 1) * listNo).Take(listNo + 1).ToList();
                    var t = new Task(() =>
                    {
                        foreach (ZtoPrintBillEntity ztoPrintBillEntity in tempList)
                        {
                            ++tempCount;
                            prbMessage.BeginInvoke(new Action(() =>
                            {
                                if (prbMessage.Value < prbMessage.Maximum)
                                {
                                    prbMessage.Value += 1;
                                }
                            }));
                            if (!string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveAddress))
                            {
                                var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(ztoPrintBillEntity.ReceiveAddress);
                                if (result != null)
                                {
                                    foreach (ZtoPrintBillEntity printBillEntity in _list)
                                    {
                                        if (printBillEntity.Id == ztoPrintBillEntity.Id)
                                        {
                                            printBillEntity.ReceiveProvince = result.Result.AddressComponent.Province.Trim();
                                            printBillEntity.ReceiveCity = result.Result.AddressComponent.City.Trim();
                                            printBillEntity.ReceiveCounty = result.Result.AddressComponent.District.Trim();
                                            //BeginInvoke(new Action(() =>
                                            //                           {
                                            //                               var tipMessage = printBillEntity.ReceiveMan + "的收件省市区是" + string.Format("{0}-{1}-{2}", printBillEntity.ReceiveProvince, printBillEntity.ReceiveCity, printBillEntity.ReceiveCounty);
                                            //                               prbMessage.ShowTip(tipMessage,ToolTipLocation.TopCenter);
                                            //                               //gridViewBills.SetRowCellValue(0, "收件省份", result.Result.AddressComponent.Province.Trim());
                                            //                               //gridViewBills.SetRowCellValue(0, "收件城市", result.Result.AddressComponent.City.Trim());
                                            //                               //gridViewBills.SetRowCellValue(0, "收件区县", result.Result.AddressComponent.District.Trim());
                                            //                           }));
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                    });
                    t.Start();
                }
                timerUpdateReceiveInfo.Start();
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void tspUpdateSendProvinceCityCounty_Click(object sender, EventArgs e) 智能更新发件省市区
        /// <summary>
        /// 智能更新发件省市区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspUpdateSendProvinceCityCountyClick(object sender, EventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                int tempCount = 0;
                prbMessage.Maximum = _list.Count;
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    ++tempCount;
                    prbMessage.Value = tempCount;
                    if (!string.IsNullOrEmpty(ztoPrintBillEntity.SendAddress))
                    {
                        var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(ztoPrintBillEntity.SendAddress);
                        if (result == null)
                        {
                            continue;
                        }
                        // 进行更新选中记录的（发件）省市区（包括省市区的ID）
                        var updateParameters = new List<KeyValuePair<string, object>>
                                                   {
                                                       new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendProvince,result.Result.AddressComponent.Province.Trim()),
                                                       new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCity,result.Result.AddressComponent.City.Trim()),
                                                       new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCounty,result.Result.AddressComponent.District.Trim()),
                                                       new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                                                   };
                        var whereParameters = new List<KeyValuePair<string, object>>
                                                  {
                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId,ztoPrintBillEntity.Id)
                                                  };
                        printBillManager.SetProperty(whereParameters, updateParameters);
                    }
                }
                if (tempCount > 0)
                {
                    GridDataBind();
                    XtraMessageBox.Show(@"更新成功" + tempCount + "条发件省市区，请再次检查一下发件省市区是否为空，也可以直接界面操作编写省市区信息，从而获取到大头笔。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(@"发件地址不详，更新失败，请检查打印数据发件人的发件地址，不要乱填写，也可以自己直接界面编写。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void tspSaveAsSendMan_Click(object sender, EventArgs e) 保存为常用发件人
        /// <summary>
        /// 保存为常用发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspSaveAsSendManClick(object sender, EventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                int tempCount = 0;
                prbMessage.Maximum = _list.Count;
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    var userEntity = new ZtoUserEntity
                                         {
                                             Realname = ztoPrintBillEntity.SendMan,
                                             TelePhone = ztoPrintBillEntity.SendPhone,
                                             Mobile = ztoPrintBillEntity.SendPhone,
                                             Province = ztoPrintBillEntity.SendProvince,
                                             City = ztoPrintBillEntity.SendCity,
                                             County = ztoPrintBillEntity.SendCounty,
                                             Address = ztoPrintBillEntity.SendAddress,
                                             Postcode = ztoPrintBillEntity.SendPostcode,
                                             CreateOn = DateTime.Now,
                                             Department = ztoPrintBillEntity.SendDepartment,
                                             Company = ztoPrintBillEntity.SendCompany,
                                             IsDefault = "0",
                                             Issendorreceive = "1"
                                         };
                    var result = userManager.Add(userEntity, true);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ++tempCount;
                    }
                }
                if (tempCount > 0)
                {
                    XtraMessageBox.Show(@"新增成功" + tempCount + "条发件人记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(@"新增失败。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void tspSaveAsReceiveMan_Click(object sender, EventArgs e) 保存为常用收件人
        /// <summary>
        /// 保存为常用收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspSaveAsReceiveManClick(object sender, EventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                int tempCount = 0;
                prbMessage.Maximum = _list.Count;
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    ZtoUserEntity userEntity = new ZtoUserEntity();
                    userEntity.Realname = ztoPrintBillEntity.ReceiveMan;
                    userEntity.TelePhone = ztoPrintBillEntity.ReceivePhone;
                    userEntity.Mobile = ztoPrintBillEntity.ReceivePhone;
                    userEntity.Province = ztoPrintBillEntity.ReceiveProvince;
                    userEntity.City = ztoPrintBillEntity.ReceiveCity;
                    userEntity.County = ztoPrintBillEntity.ReceiveCounty;
                    userEntity.Address = ztoPrintBillEntity.ReceiveAddress;
                    userEntity.Postcode = ztoPrintBillEntity.ReceivePostcode;
                    userEntity.CreateOn = DateTime.Now;
                    userEntity.Department = "";
                    userEntity.Company = ztoPrintBillEntity.ReceiveCompany;
                    userEntity.IsDefault = "0";
                    userEntity.Issendorreceive = "0";
                    var result = userManager.Add(userEntity, true);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ++tempCount;
                    }
                }
                if (tempCount > 0)
                {
                    XtraMessageBox.Show(@"新增成功" + tempCount + "条收件人记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(@"新增失败。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void tspSelectAll_Click(object sender, EventArgs e) 全选
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspSelectAllClick(object sender, EventArgs e)
        {
            CheckAll(gridViewBills);
        }
        #endregion

        #region private void tspUnSelectAll_Click(object sender, EventArgs e) 反选
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspUnSelectAllClick(object sender, EventArgs e)
        {
            UnCheckAll(gridViewBills);
        }
        #endregion

        #region private void tspCheckNetWork_Click(object sender, EventArgs e) 检查网络
        /// <summary>
        /// 检查网络
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspCheckNetWorkClick(object sender, EventArgs e)
        {
            if (BaseSystemInfo.OnInternet)
            {
                var netWorkText = NetworkHelper.GetIpInfo();
                XtraMessageBox.Show(netWorkText, AppMessage.MSG0000);
            }
            else
            {
                XtraMessageBox.Show("未连接网络", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void tspNotePad_Click(object sender, EventArgs e) 记事本
        /// <summary>
        /// 记事本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspNotePadClick(object sender, EventArgs e)
        {
            ToolHelper.OpenNotePad();
        }
        #endregion

        #region private void tspCalculator_Click(object sender, EventArgs e) 计算器
        /// <summary>
        /// 计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspCalculator_Click(object sender, EventArgs e)
        {
            ToolHelper.OpenCalculator();
        }
        #endregion

        #region private void btnUpdateZtoPrintMark_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 更新申通大头笔
        /// <summary>
        /// 更新申通大头笔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateZtoPrintMark_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gridViewBills.RowCount > 0)
                {
                    _list = GetCheckedRecord(gridViewBills);
                    if (_list == null || _list.Count == 0)
                    {
                        XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    int tempCount = 0;
                    prbMessage.Maximum = _list.Count;
                    prbMessage.Value = 0;
                    ZtoPrintBillManager printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                    Task t = new Task(() =>
                                      {
                                          //  gridViewBills.ShowLoadingPanel();
                                          foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                                          {
                                              prbMessage.BeginInvoke(new Action(() =>
                                                                                {
                                                                                    prbMessage.Value += 1;
                                                                                }));
                                              var selectedRemark = new List<string> { ztoPrintBillEntity.SendProvince, ztoPrintBillEntity.SendCity, ztoPrintBillEntity.SendCounty };
                                              var selectedReceiveMark = new List<string> { ztoPrintBillEntity.ReceiveProvince, ztoPrintBillEntity.ReceiveCity, ztoPrintBillEntity.ReceiveCounty };
                                              var printMark = BillPrintHelper.GetRemaike(string.Join(",", selectedRemark), ztoPrintBillEntity.SendAddress, string.Join(",", selectedReceiveMark), ztoPrintBillEntity.ReceiveAddress);
                                              if (string.IsNullOrEmpty(printMark))
                                              {
                                                  continue;
                                              }
                                              ++tempCount;
                                              // 根据记录的ID更新大头笔字段
                                              printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, ztoPrintBillEntity.Id), new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBigPen, printMark));
                                          }
                                          //gridViewBills.HideLoadingPanel();
                                          if (tempCount > 0)
                                          {
                                              this.Invoke(new Action(GridDataBind));
                                              XtraMessageBox.Show(@"更新成功" + tempCount + "申通大头笔条。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                          }
                                          else
                                          {
                                              XtraMessageBox.Show(@"发件和收件省市区信必须要有，没有就更新不了大头笔，可以右击更新收件省市区" + Environment.NewLine + "可以自己界面直接操作输入省市区等信息，和操作Excel一样操作方式操作表格", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                          }
                                      });
                    t.Start();
                }
                else
                {
                    XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }
        #endregion

        #region private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 导入指定Excel
        /// <summary>
        /// 导入指定Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frmImportData = new FrmImportExcel() { Owner = this };
            if (frmImportData.ShowDialog() == DialogResult.OK)
            {
                GridDataBind();
            }
            frmImportData.Dispose();
        }
        #endregion

        #region private void btnGetZtoElecBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 获取申通电子面单单号然后更新本地
        /// <summary>
        /// 获取申通电子面单单号然后更新本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetZtoElecBillItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
                if (elecUserInfoEntity != null)
                {
                    _list = GetCheckedRecord(gridViewBills);
                    if (_list == null || _list.Count == 0)
                    {
                        XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //  _list[0].OrderNumber = _list[1].OrderNumber;
                    #region 检查订单号重复
                    string repeatContent = "";
                    var repeatCount = 0;
                    var groupList = (from r in _list
                                     group r by r.OrderNumber into g
                                     where g.Count() > 1
                                     select g).ToList();
                    if (groupList.Any())
                    {
                        //遍历分组结果集
                        foreach (var item in groupList)
                        {
                            foreach (var u in item)
                            {
                                if (!string.IsNullOrEmpty(u.OrderNumber))
                                {
                                    ++repeatCount;
                                    repeatContent += ("姓名：" + u.ReceiveMan + "  -- 订单号：" + u.OrderNumber + Environment.NewLine);
                                }
                            }
                        }
                    }
                    if (repeatCount > 0)
                    {
                        XtraMessageBox.Show(string.Format("存在{0}个相同的订单号，请立刻右击重新生成订单号，明细如下:" + Environment.NewLine, repeatCount) + repeatContent, AppMessage.MSG0000);
                        return;
                    }
                    #endregion

                    #region 检查订单号和单号都有的情况，就不要获取，过滤掉
                    // 需要检查已经存在的单号的订单号（订单号和单号都有了就不要再去请求了，请求等于浪费带宽，增加接口的压力）
                    for (int k = 0; k < _list.Count; k++)
                    {
                        var ztoPrintBillEntity = _list[k];
                        if (!string.IsNullOrEmpty(ztoPrintBillEntity.OrderNumber) && !string.IsNullOrEmpty(ztoPrintBillEntity.BillCode))
                        {
                            _list.Remove(ztoPrintBillEntity);
                            k--;
                        }
                    }
                    if (_list.Count == 0)
                    {
                        MessageUtil.ShowTips("选中记录都已经获取到单号了，建议不要重复获取");
                        return;
                    }
                    #endregion

                    #region 检查电子面单余额，有的话就可以获取
                    var result = ZtoElecBillHelper.GetElecBillCount(elecUserInfoEntity);
                    int elecBillCount;
                    if (int.TryParse(result, out elecBillCount))
                    {
                        //if (elecBillCount < _list.Count)
                        //{
                        //    XtraMessageBox.Show(string.Format("当前可用电子面单数量为：{0}条，需要打印数量为：{1}条，可用单号不足，请到物料系统充值。", elecBillCount, _list.Count), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    ToolHelper.OpenBrowserUrl("https://sso.zt-express.com/?SystemCode=WULIAO&openId=false");
                        //    return;
                        //}
                    }
                    else
                    {
                        XtraMessageBox.Show(result, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    #endregion

                    if (XtraMessageBox.Show(string.Format("当前可用电子面单数量为：{1},确定获取{0}条电子面单吗？一次性获取不建议超过100条，网络也需要好", _list.Count, elecBillCount), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (_list.Any())
                    {
                        // 开线程拉单号更新本地
                        ZtoElecBillHelper.GetZtoBillAndBigPenByMultiThread(_list, elecUserInfoEntity, this, prbMessage, GridDataBind);
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
            catch (Exception exception)
            {
                ProcessException(exception);
                GridDataBind();
            }
        }
        #endregion

        #region private void btnExportBillImage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 生成底单
        /// <summary>
        /// 生成底单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportBillImage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                new FrmExportBillImage(_list).Show();
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void btnBackZTOElecBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 回收申通电子面单
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackZTOElecBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
                if (elecUserInfoEntity != null)
                {
                    _list = GetCheckedRecord(gridViewBills);
                    FrmBackElecBillByOrderNumber frmBackElecBill = null;
                    if (_list == null || _list.Count == 0)
                    {
                        frmBackElecBill = new FrmBackElecBillByOrderNumber();
                    }
                    else
                    {
                        if (XtraMessageBox.Show(string.Format("确定取消选中的{0}条记录吗？取消之前建议一定要将数据导出Excel", _list.Count), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        {
                            return;
                        }
                        frmBackElecBill = new FrmBackElecBillByOrderNumber(_list);
                    }
                    if (frmBackElecBill.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
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
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }
        #endregion

        #region private void tspbtnSetFont_Click(object sender, EventArgs e) 设置表格字体
        /// <summary>
        /// 设置表格字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspbtnSetFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.Font = AppearanceObject.DefaultFont;//系统字体
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                //BillPrintHelper.SetSystemFont(JsonConvert.SerializeObject(fontDlg.Font));
                //AppearanceObject.DefaultFont = fontDlg.Font;
                base.SetGridFont(gridViewBills, fontDlg.Font);
                // XtraMessageBox.Show("设置成功，建议重新打开。",AppMessage.MSG0000,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void tspBtnPrintExcel_Click(object sender, EventArgs e) 打印表格
        /// <summary>
        /// 打印表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspBtnPrintExcel_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = this.gridControlBills;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.CreateMarginalHeaderArea += Link_CreateMarginalHeaderArea;
            link.CreateDocument();
            link.ShowPreview();
        }
        #endregion

        #region private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            PageInfoBrick brick = e.Graph.DrawPageInfo(DevExpress.XtraPrinting.PageInfo.None, "面单打印数据", Color.DarkBlue, new RectangleF(0, 0, 100, 21), BorderSide.None);
            brick.LineAlignment = BrickAlignment.Center;
            brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = true;
            //brick.Font = new Font("宋体", 11f, FontStyle.Bold);
        }
        #endregion

        #region private void btnPrintHistoryRecord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 打印历史记录
        /// <summary>
        /// 打印历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintHistoryRecord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this.ParentForm, "FrmPrintHistoryRecord");
        }
        #endregion

        /// <summary>
        /// 打开保存电子面单底单文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TspOpenElecFolderClick(object sender, EventArgs e)
        {
            if (!Directory.Exists(BillPrintHelper.SaveFilePath))
            {
                Directory.CreateDirectory(BillPrintHelper.SaveFilePath);
            }
            Process.Start(BillPrintHelper.SaveFilePath);
        }

        /// <summary>
        /// 查看可用电子面单数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspGetElecBillCount_Click(object sender, EventArgs e)
        {
            ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
            try
            {
                if (elecUserInfoEntity != null)
                {
                    var result = ZtoElecBillHelper.GetElecBillCount(elecUserInfoEntity);
                    int elecBillCount = 0;
                    if (int.TryParse(result, out elecBillCount))
                    {
                        XtraMessageBox.Show(string.Format("商家ID为：{1}可用线下电子面单数量为：{0}条", result, elecUserInfoEntity.Kehuid), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(result, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }

        /// <summary>
        /// 单元格光标离开保存修改的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewBills_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Valid)
            {
                // 还有点小问题，不能获取到当前编辑的行的索引
                DataRow currentDataRow = gridViewBills.GetFocusedDataRow();
                // 获取选中行的索引，不然开启线程选中行变了，就更新错误了，这是大的bug不能有
                int rowHandler = gridViewBills.FocusedRowHandle;
                if (currentDataRow != null)
                {
                    // 进行更新动作
                    var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                    var updateColumnName = BillPrintHelper.GetFieldByName(gridViewBills.FocusedColumn.FieldName);
                    printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), new KeyValuePair<string, object>(updateColumnName, e.Value));
                    gridControlBills.ShowTip(gridViewBills.FocusedColumn.FieldName + "更新成功", ToolTipLocation.TopCenter);
                    #region 更新看看省市区和大头笔的信息
                    // 如果改了收件人的详细地址
                    if (gridViewBills.FocusedColumn.FieldName == "收件人详细地址" && currentDataRow["收件人详细地址"].ToString() != e.Value.ToString())
                    {
                        // 开线程来操作，这个动作还是比较耗时间的，不然有卡顿了，2016年1月30日16:37:11
                        var t = new Task(delegate
                        {
                            // 最好更新收件的省市区和大头笔，减少操作步骤
                            var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(e.Value.ToString());
                            if (result != null)
                            {
                                // 进行更新选中记录的（收件）省市区（包括省市区的ID）
                                var updateParameters = new List<KeyValuePair<string, object>>
                                                                                 {
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveProvince, result.Result.AddressComponent.Province.Trim()),
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveCity, result.Result.AddressComponent.City.Trim()),
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveCounty,result.Result.AddressComponent.District.Trim()),
                                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                                                                                 };
                                printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), updateParameters);
                                BeginInvoke(new Action(() =>
                                {
                                    gridViewBills.SetRowCellValue(rowHandler, "收件省份", result.Result.AddressComponent.Province.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "收件城市", result.Result.AddressComponent.City.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "收件区县", result.Result.AddressComponent.District.Trim());
                                }));
                                // 在更新大头笔
                                var selectedRemark = new List<string> { currentDataRow["发件省份"].ToString(), currentDataRow["发件城市"].ToString(), currentDataRow["发件区县"].ToString() };
                                var selectedReceiveMark = new List<string> { result.Result.AddressComponent.Province.Trim(), result.Result.AddressComponent.City.Trim(), result.Result.AddressComponent.District.Trim() };
                                var printMark = BillPrintHelper.GetRemaike(string.Join(",", selectedRemark), currentDataRow["发件详细地址"].ToString(), string.Join(",", selectedReceiveMark), e.Value.ToString());
                                // 根据记录的ID更新大头笔字段
                                printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBigPen, printMark));
                                BeginInvoke(new Action(() => gridViewBills.SetRowCellValue(rowHandler, "大头笔", printMark)));
                            }
                        });
                        t.Start();
                    }
                    // 如果改了发件人的详细地址
                    if (gridViewBills.FocusedColumn.FieldName == "发件详细地址" && currentDataRow["发件详细地址"].ToString() != e.Value.ToString())
                    {
                        var t = new Task(delegate
                        {
                            // 最好更新发件的省市区和大头笔，减少操作步骤
                            var result = BaiduMapHelper.GetProvCityDistFromBaiduMap(e.Value.ToString());
                            if (result != null)
                            {
                                // 进行更新选中记录的（发件）省市区（包括省市区的ID）
                                var updateParameters1 = new List<KeyValuePair<string, object>>
                                                                                  {
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendProvince, result.Result.AddressComponent.Province.Trim()),
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCity, result.Result.AddressComponent.City.Trim()),
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCounty, result.Result.AddressComponent.District.Trim()),
                                                                                      new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                                                                                  };
                                printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, currentDataRow["Id"]), updateParameters1);
                                BeginInvoke(new Action(() =>
                                {
                                    gridViewBills.SetRowCellValue(rowHandler, "发件省份", result.Result.AddressComponent.Province.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "发件城市", result.Result.AddressComponent.City.Trim());
                                    gridViewBills.SetRowCellValue(rowHandler, "发件区县", result.Result.AddressComponent.District.Trim());
                                }));
                            }
                        });
                        t.Start();
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 从京东购买热敏打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspBuyPrinterFromJD_Click(object sender, EventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://search.jd.com/Search?keyword=%E7%83%AD%E6%95%8F%E6%89%93%E5%8D%B0%E6%9C%BA&enc=utf-8&suggest=1.def.0&wq=remin&pvid=aurjn9ji.kdg1");
        }

        /// <summary>
        /// 从淘宝购买热敏打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspBuyPrinterFromTaoBao_Click(object sender, EventArgs e)
        {
            ToolHelper.OpenBrowserUrl("https://s.taobao.com/search?q=%E7%83%AD%E6%95%8F%E6%89%93%E5%8D%B0%E6%9C%BA&imgfile=&commend=all&ssid=s5-e&search_type=item&sourceId=tb.index&spm=a21bo.7724922.8452-taobao-item.1&ie=utf8&initiative_id=tbindexz_20160111");
        }

        /// <summary>
        /// 从申通优选购买热敏打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspBuyPrinterFromZtBest_Click(object sender, EventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://www.ztbest.com/index.php?app=search&keyword=%E6%89%93%E5%8D%B0%E6%9C%BA");
        }

        /// <summary>
        /// 发件日期改为今天(短日期）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspUpdateSendDate_Click(object sender, EventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    // 进行更新选中记录的（收件）省市区（包括省市区的ID）
                    var updateParameters = new List<KeyValuePair<string, object>>
                                                                             {
                                                                                 new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendDate,DateTime.Now.ToString(BaseSystemInfo.DateFormat)),
                                                                                 new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                                                                             };
                    printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, ztoPrintBillEntity.Id), updateParameters);
                }
                GridDataBind();
                XtraMessageBox.Show(@"更新成功" + _list.Count + "条。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选中区域改变事件（很牛逼）2016-1-20 16:00:35
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewBills_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UnCheckAll(gridViewBills);
            var t = gridViewBills.GetSelectedCells();
            foreach (GridCell gridCell in t)
            {
                gridViewBills.SetRowCellValue(gridCell.RowHandle, gridViewBills.Columns["Check"], true);
            }
        }

        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspBuildBarCode_Click(object sender, EventArgs e)
        {
            ChildFormManagementHelper.LoadMdiForm(this.ParentForm, "FrmBuildBarCode");
        }

        /// <summary>
        /// 修改申通线下商家ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspUpdateZtoElecBillInfo_Click(object sender, EventArgs e)
        {
            //var userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
            //var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
            //var addSendMan = new FrmAddSendMan();
            //if (userList.Any())
            //{
            //    addSendMan.Id = userList.First().Id.ToString();
            //}
            //else
            //{
            //    XtraMessageBox.Show("系统未绑定默认发件人和商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //addSendMan.ShowDialog();
            //addSendMan.Dispose();
            var frmBindZtoElecUserInfo = new FrmBindZtoElecUserInfo();
            frmBindZtoElecUserInfo.ShowDialog();
        }

        private void alertElecUserInfo_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
            if (e.Info.Tag != null)
            {
                ToolHelper.OpenBrowserUrl(e.Info.Tag.ToString());
            }
        }

        /// <summary>
        /// 选中未获取到单号的记录，这样就可以获取了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspChooseUnBillCodeRecord_Click(object sender, EventArgs e)
        {
            // DevExpress XtraGrid网格控件示例七：列过滤
            // 灵感来源：http://www.devexpresscn.com/Resources/CodeExamples-292.html
            ColumnView view = gridViewBills;
            view.ActiveFilter.Add(view.Columns["单号"], new ColumnFilterInfo("[单号] = ''", ""));
            if (gridViewBills.DataRowCount > 0)
            {
                int count = 0;
                for (int i = 0; i < gridViewBills.DataRowCount; i++)
                {
                    if (string.IsNullOrEmpty(gridViewBills.GetRowCellValue(i, "单号").ToString()))
                    {
                        gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], true);
                        ++count;
                    }
                    else
                    {
                        gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], false);
                    }
                }
                XtraMessageBox.Show(string.Format("选中{0}条记录，建议获取单号后打印。", count), AppMessage.MSG0000);
            }
            else
            {
                view.ClearColumnsFilter();
            }
        }

        /// <summary>
        /// 导入淘宝订单Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportTaoBaoExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frmImportExcelByTaoBao = new FrmImportExcelByTaoBao() { Owner = this };
            if (frmImportExcelByTaoBao.ShowDialog() == DialogResult.OK)
            {
                GridDataBind();
            }
            frmImportExcelByTaoBao.Dispose();
        }

        /// <summary>
        /// 导入京东订单Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportJingDongExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frmImportExcelByJingDong = new FrmImportExcelByJingDong() { Owner = this };
            if (frmImportExcelByJingDong.ShowDialog() == DialogResult.OK)
            {
                GridDataBind();
            }
            frmImportExcelByJingDong.Dispose();
        }

        /// <summary>
        /// 监控智能识别收件省市区的计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdateReceiveInfo_Tick(object sender, EventArgs e)
        {
            if (prbMessage.Value == prbMessage.Maximum)
            {
                timerUpdateReceiveInfo.Stop();
                var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    // 进行更新选中记录的（收件）省市区（包括省市区的ID）
                    var updateParameters = new List<KeyValuePair<string, object>>
                                                                 {
                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveProvince,ztoPrintBillEntity.ReceiveProvince),
                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveCity,ztoPrintBillEntity.ReceiveCity),
                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldReceiveCounty,ztoPrintBillEntity.ReceiveCounty),
                                                                     new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                                                                 };
                    printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, ztoPrintBillEntity.Id), updateParameters);
                }
                GridDataBind();
                XtraMessageBox.Show(@"更新成功" + tempCount + "条收件省市区，请再次检查一下收件省市区，这些信息必须准确才可以提取到大头笔，也可以自己直接界面编辑省市区信息。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 导入微店订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportWeiDianExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("导入微店订单开发中");
        }

        /// <summary>
        /// 导入阿里巴巴订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportAliBaBaExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("导入阿里巴巴订单开发中");
        }

        /// <summary>
        /// 复制选中记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspCopySelectedRow_Click(object sender, EventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list != null && _list.Any())
                {
                    var addPrintBySelectedRow = new FrmAddPrintBySelectedRow(_list);
                    if (addPrintBySelectedRow.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                    addPrintBySelectedRow.Dispose();
                }
            }
        }

        /// <summary>
        /// 生成新的订单号码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspBuildOrderNumber_Click(object sender, EventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list != null && _list.Any())
                {
                    var buildOrderNumber = new FrmBuildOrderNumber(_list);
                    if (buildOrderNumber.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                    buildOrderNumber.Dispose();
                }
                else
                {
                    var buildOrderNumber = new FrmBuildOrderNumber();
                    if (buildOrderNumber.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                    buildOrderNumber.Dispose();
                }
            }
        }

        private void tspChatRoom_Click(object sender, EventArgs e)
        {
            FrmAlert alert = new FrmAlert();
            alert.Show();
        }

        /// <summary>
        /// 双击行就可以编辑，2016年6月20日08:55:14 群里提出来的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewBills_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = this.gridViewBills.GetFocusedDataRow();
            if (dr != null)
            {
                // 联系系统管理员，反馈问题
                string selectedId = dr["Id"].ToString();
                if (!string.IsNullOrEmpty(selectedId))
                {
                    var addBill = BillPrintHelper.GetDefaultAddBillForm();
                    addBill.Owner = this;
                    addBill.SelectedId = selectedId;
                    if (addBill.ShowDialog() == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                    addBill.Dispose();
                }
            }
        }

        /// <summary>
        /// 补填单号【适合大面单打印】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputStartBillAndEndBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var tempList = new List<ZtoPrintBillEntity>();
                foreach (var ztoPrintBillEntity in _list)
                {
                    if (string.IsNullOrEmpty(ztoPrintBillEntity.BillCode))
                    {
                        tempList.Add(ztoPrintBillEntity);
                    }
                }
                if (tempList.Any())
                {
                    FrmInputStartBillAndEndBill inputStartBillAndEndBill = new FrmInputStartBillAndEndBill(_list);
                    if (inputStartBillAndEndBill.ShowDialog(this) == DialogResult.OK)
                    {
                        GridDataBind();
                    }
                }
                else
                {
                    MessageUtil.ShowTips("已经有单号的记录，不可以再次补填单号，补填单号只适合大面单打印，只能填写一次");
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 列数据格式设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewBills_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "打印状态")
            {
                e.DisplayText = "等待提交给打印机";
            }

            if (e.Column.FieldName == "发件电话")
            {
                var telephone = e.Value.ToString().Trim();
                e.DisplayText = BillPrintHelper.FuzzyTelephone(telephone);
            }

            if (e.Column.FieldName == "收件电话")
            {
                var telephone = e.Value.ToString().Trim();
                e.DisplayText = BillPrintHelper.FuzzyTelephone(telephone);
            }
            //if (e.Column.FieldName == "收件人姓名") 
            //{
            //    var receiveMan = e.Value.ToString();
            //    e.DisplayText = BillPrintHelper.FuzzyUserName(receiveMan);
            //}
            //if (e.Column.FieldName == "发件人姓名")
            //{
            //    var sendMan = e.Value.ToString();
            //    e.DisplayText = BillPrintHelper.FuzzyUserName(sendMan);
            //}
        }

        /// <summary>
        /// 切换发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeSendMan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewBills.RowCount > 0)
            {
                _list = GetCheckedRecord(gridViewBills);
                if (_list == null || _list.Count == 0)
                {
                    XtraMessageBox.Show(@"请至少选中一条运单记录", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var changeSendMan = new FrmChangeSendMan();
                changeSendMan.ZtoPrintBillEntities = _list;
                if (changeSendMan.ShowDialog(this) == DialogResult.OK)
                {
                    GridDataBind();
                }
            }
            else
            {
                XtraMessageBox.Show(@"请添加运单数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewBills_TopRowChanged(object sender, EventArgs e)
        {
            btnBackTop.Visible = false;
            if (gridViewBills.RowCount <= 10)
            {
                return;
            }
            var middleRowIndex = gridViewBills.RowCount / 2;
            //if (gridViewBills.IsRowVisible(gridViewBills.RowCount - 10) == RowVisibleState.Visible )
            //{
            //    // 到达底部了
            //    btnBackTop.Visible = true;
            //    btnBackTop.Text = "返回顶部";
            //}
            //else if (gridViewBills.IsRowVisible(0) == RowVisibleState.Visible)
            //{
            //    // 到达顶部了
            //    btnBackTop.Visible = true;
            //    btnBackTop.Text = "返回底部";
            //}
            //if (gridViewBills.IsRowVisible(gridViewBills.RowCount - middleRowIndex) == RowVisibleState.Hidden)
            //{
            //    // 到达底部了
            //    btnBackTop.Visible = true;
            //    btnBackTop.Text = "返回顶部";
            //}
            //else 
            //{
            //    // 到达顶部了
            //    btnBackTop.Visible = true;
            //    btnBackTop.Text = "返回底部";
            //}
            Type type = gridViewBills.GetType();

            System.Reflection.FieldInfo fi = type.GetField("fViewInfo", BindingFlags.NonPublic | BindingFlags.Instance);

            //    GridViewInfo info = gridViewBills.GetViewInfo() as GridViewInfo;

            fi = type.GetField("scrollInfo", BindingFlags.NonPublic | BindingFlags.Instance);

            ScrollInfo scrollInfo = fi.GetValue(gridViewBills) as ScrollInfo;
            //if (scrollInfo != null)
            //{
            //    btnBackTop.ShowTip(scrollInfo.VScrollPosition.ToString());
            //}
            if (scrollInfo != null && scrollInfo.VScrollPosition <= middleRowIndex)
            {
                btnBackTop.Visible = true;
                btnBackTop.Text = "返回底部";
            }
            else if (scrollInfo != null && scrollInfo.VScrollPosition > middleRowIndex)
            {
                btnBackTop.Visible = true;
                btnBackTop.Text = "返回顶部";
            }
        }


        /// <summary>
        /// 返回顶部
        /// 杨恒连，2016年7月24日13:27:51
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackTop_Click(object sender, EventArgs e)
        {
            var tt = gridViewBills.FocusedRowHandle;
            if (btnBackTop.Text == "返回顶部")
            {
                // 看下当前选中行是不是第一行
                if (tt == 0)
                {
                    // 有时候当前选中行就是第一行，但是滚动条已经往下走了，所以点击按钮就没反应
                    gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
                    gridViewBills.FocusedRowHandle = 0;
                    gridViewBills.SetRowCellValue(tt, gridViewBills.Columns["Check"], true);
                }
                else
                {
                    // 选中行不是第一行就直接设置第一行选中就可以了
                    gridViewBills.FocusedRowHandle = 0;
                    gridViewBills.SetRowCellValue(tt, gridViewBills.Columns["Check"], true);
                }
            }
            else
                if (btnBackTop.Text == "返回底部")
                {
                    // 看下当前选中行是不是最后一行
                    if (tt == gridViewBills.RowCount - 1)
                    {
                        // 有时候当前选中行就是第一行，但是滚动条已经往下走了，所以点击按钮就没反应
                        gridViewBills.FocusedRowHandle = 0;
                        gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
                        gridViewBills.SetRowCellValue(tt, gridViewBills.Columns["Check"], true);
                    }
                    else
                    {
                        // 选中行不是第一行就直接设置第一行选中就可以了
                        gridViewBills.FocusedRowHandle = gridViewBills.RowCount - 1;
                        gridViewBills.SetRowCellValue(tt, gridViewBills.Columns["Check"], true);
                    }
                }
        }

        /// <summary>
        /// 鼠标悬停在GridControl的序列号上，显示当前行数据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridViewBills.GridControl) return;
            ToolTipControlInfo info = null;
            //在当前鼠标的位置获取视图
            var view = gridViewBills.GridControl.GetViewAt(e.ControlMousePosition) as GridView;
            if (view == null) return;
            //获取视图的元信息驻留在当前位置
            GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
            //显示一行指示提示
            if (hi.HitTest == GridHitTest.RowIndicator)
            {
                //一个唯一标识行指示细胞对象
                object o = hi.HitTest.ToString() + hi.RowHandle.ToString(CultureInfo.InvariantCulture);
                var sb = new StringBuilder();
                sb.AppendLine("第" + (hi.RowHandle + 1) + "行打印数据信息：");
                foreach (GridColumn gridCol in view.Columns)
                {
                    if (!gridCol.Visible) continue; //不显示去除
                    if (gridCol.Caption == @"选择") continue; //去除最后一列值
                    if (gridCol.FieldName == @"Check") continue; //去除第一列选中列
                    if (view.GetRowCellDisplayText(hi.RowHandle, gridCol.FieldName) == "Checked")
                    {
                        sb.AppendFormat("    {0}：{1}\r\n", gridCol.ToolTip, "√");
                        continue;
                    }
                    if (view.GetRowCellDisplayText(hi.RowHandle, gridCol.FieldName) == "Unchecked")
                    {
                        sb.AppendFormat("    {0}：{1}\r\n", gridCol.ToolTip, "×");
                        continue;
                    }
                    sb.AppendFormat("    {0}：{1}\r\n", gridCol.FieldName, view.GetRowCellDisplayText(hi.RowHandle, gridCol.FieldName));
                }
                info = new ToolTipControlInfo(o, sb.ToString());
                info.ToolTipPosition = new Point(e.ControlMousePosition.X + 100, e.ControlMousePosition.Y);
            }
            //如果适用供应的提示信息，否则保留默认的提示（如果有）
            if (info != null)
            {
                e.Info = info;
            }
        }

        /// <summary>
        /// 隐藏收发件人的电话信息，特别是手机号码，这个很敏感的信息，截图的时候不能明文截图发出去
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspHideSendAndReceiveTelephone_Click(object sender, EventArgs e)
        {
            //if (gridViewBills.RowCount > 0)
            //{
            //    for (int i = 0; i < gridViewBills.DataRowCount; i++)
            //    {
            //        if (gridViewBills.GetRowCellValue(i, "订单号").ToString() == ztoPrintBillEntity.OrderNumber)
            //        {
            //            gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], true);
            //            focusRowIndex.Add(gridViewBills.GetRowHandle(i));
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 鼠标移动到GridView上，当前行背景色改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewBills_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);
            DataRow row = view.GetDataRow(info.RowHandle);
            if (row != null)
            {
                gridViewBills.FocusedRowHandle = info.RowHandle;
            }
        }
    }
}