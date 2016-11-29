//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace STO.Print.UserControl
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;
    using DotNet.Utilities;
    using grproLib;
    using System.Linq;
    using System.Windows.Forms;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 收件人信息操作用户控件
    /// 
    /// 修改记录
    /// 
    ///     2015-08-03  版本：1.0 YangHengLian 创建
    ///     2016-1-25 网点想要导出收件人的功能
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-03</date>
    /// </author>
    /// </summary>
    public partial class ReceiveManControl : XtraUserControl
    {
        /// <summary>
        /// 定义Grid++Report报表主对象
        /// </summary>
        readonly GridppReport _report = new GridppReport();

        /// <summary>
        /// 打印实体集合
        /// </summary>
        List<ZtoPrintBillEntity> _list;

        /// <summary>
        /// 确认选中的用户ID（收件人）
        /// </summary>
        public string ChooseId;

        public ReceiveManControl()
        {
            InitializeComponent();
            Load += BillPanel_Load;
        }

        void BillPanel_Load(object sender, EventArgs e)
        {
            ReceiveManDataBind();
            for (int i = 1; i < gvReceiveMan.Columns.Count; i++)
            {
                // 可以编辑
                gvReceiveMan.Columns[i].AppearanceCell.Options.UseTextOptions = true;
            }
            gvReceiveMan.OptionsBehavior.Editable = true;
            gvReceiveMan.OptionsFind.AlwaysVisible = true;
            gvReceiveMan.OptionsFind.AlwaysVisible = true;
            gvReceiveMan.FindPanelVisible = true;
            // 查找面板
            gvReceiveMan.ShowFindPanel();
            _report.Initialize += ReportInitialize;
            _report.FetchRecord += ReportFetchRecord;
        }

        #region private void ReportInitialize() 初始化报表列字段对象
        private IGRField billCode, senderName, senderAddress, senderCompany, senderPhone, departure,
                         receiverName, receiverAddress, receiverCompany, receiverPhone, destination,
                         description, amount, remarks, sendTime, weight, totalMoney, bigPen, _sendSiteField;
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
                foreach (ZtoPrintBillEntity billEntity in _list)
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
                        amount.AsString = string.IsNullOrEmpty(billEntity.TotalNumber) ? "" : billEntity.TotalNumber;
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
                        weight.AsString = string.IsNullOrEmpty(billEntity.Weight) ? "" : billEntity.Weight;
                    }
                    if (totalMoney != null)
                    {
                        totalMoney.AsString = string.IsNullOrEmpty(billEntity.TranFee) ? "" : billEntity.TranFee;
                    }
                    if (bigPen != null)
                    {
                        bigPen.AsString = billEntity.BigPen;
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

        private void gvSendMan_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                Point point = gvReceiveMan.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo gridHitInfo = gvReceiveMan.CalcHitInfo(point);

                if (gridHitInfo.InColumn && gridHitInfo.Column.FieldName == "Check")
                {
                    if (GetCheckedCount(gvReceiveMan) == gvReceiveMan.DataRowCount)
                    {
                        UnCheckAll(gvReceiveMan);
                    }
                    else
                    {
                        CheckAll(gvReceiveMan);
                    }
                }

                if (gridHitInfo.InRow && gridHitInfo.InRowCell && gridHitInfo.Column.FieldName == "Check")
                {
                    if (Convert.ToBoolean(gvReceiveMan.GetFocusedRowCellValue("Check")) == false)
                    {
                        CheckSingle(gvReceiveMan);
                    }
                    else
                    {
                        UnCheckSingle(gvReceiveMan);
                    }
                }
            }
        }
        /// <summary>
        /// 获取到已选择的数量
        /// </summary>
        /// <returns></returns>
        public int GetCheckedCount(GridView gridViewBills)
        {
            int count = 0;
            for (int i = 0; i < gridViewBills.DataRowCount; i++)
            {
                if ((bool)gridViewBills.GetRowCellValue(i, gridViewBills.Columns["Check"]))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 选择全部 
        /// </summary>
        private void CheckAll(GridView gridViewBills)
        {
            for (int i = 0; i < gridViewBills.DataRowCount; i++)
            {
                gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], true);
            }
        }

        /// <summary>
        /// 反选全部
        /// </summary>
        private void UnCheckAll(GridView gridViewBills)
        {
            for (int i = 0; i < gridViewBills.DataRowCount; i++)
            {
                gridViewBills.SetRowCellValue(i, gridViewBills.Columns["Check"], false);
            }
        }

        /// <summary>
        /// 选择一个
        /// </summary>
        private void CheckSingle(GridView gridViewBills)
        {
            gridViewBills.SetRowCellValue(gridViewBills.FocusedRowHandle, gridViewBills.Columns["Check"], true);
        }

        /// <summary>
        /// 反选一个
        /// </summary>
        private void UnCheckSingle(GridView gridViewBills)
        {
            gridViewBills.SetRowCellValue(gridViewBills.FocusedRowHandle, gridViewBills.Columns["Check"], false);
        }

        private void gvSendMan_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

        private void gvSendMan_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == (sender as GridView).Columns["Check"])
            {
                e.Info.InnerElements.Clear();
                e.Info.Appearance.ForeColor = Color.Blue;
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, GetCheckedCount(gvReceiveMan) == gvReceiveMan.DataRowCount);
                e.Handled = true;
            }
        }

        /// <summary>
        /// 画GridControl首列复选框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="Checked"></param>
        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
            RepositoryItemCheckEdit chkEdit = new RepositoryItemCheckEdit();
            info = chkEdit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = chkEdit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.PaintAppearance.ForeColor = Color.Black;
            info.CalcViewInfo(g);
            args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }

        private void gvSendMan_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            gvReceiveMan.Appearance.OddRow.BackColor = Color.White; // 设置奇数行颜色 // 默认也是白色 可以省略 
            gvReceiveMan.OptionsView.EnableAppearanceOddRow = true; // 使能 // 和和上面绑定 同时使用有效 
            gvReceiveMan.Appearance.EvenRow.BackColor = Color.FromArgb(255, 250, 205); // 设置偶数行颜色 
            gvReceiveMan.OptionsView.EnableAppearanceEvenRow = true; // 使能 // 和和上面绑定 同时使用有效
            if (e.RowHandle == gvReceiveMan.FocusedRowHandle)
            {
                e.Appearance.Font = new Font("宋体", 9, FontStyle.Bold);
            }
        }

        #region public void SendManDataBind() 收件人数据绑定
        /// <summary>
        /// 收件人数据绑定
        /// </summary>
        public void ReceiveManDataBind()
        {
            DataTable dt = BillPrintHelper.DbHelper.Fill(BillPrintHelper.CmdStrForReceiveUser);
            // 增加个CheckBox列
            dt.Columns.Add("Check", typeof(bool));
            // 设置选择列的位置
            dt.Columns["Check"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Check"] = false;
                var value = dt.Rows[i]["默认收件人"].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (value == "1")
                    {
                        dt.Rows[i]["默认收件人"] = "是";
                    }
                    else
                    {
                        dt.Rows[i]["默认收件人"] = "否";
                    }
                }
            }
            // 设置gridview列头的字体大小
            gvReceiveMan.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);
            // 设置gridview列头居中
            gvReceiveMan.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gcReceiveMan.DataSource = dt;
            gvReceiveMan.OptionsView.ColumnAutoWidth = false;
            gvReceiveMan.Columns["Id"].Visible = false;
            gvReceiveMan.Columns["Check"].Width = 22;
            gvReceiveMan.Columns["Check"].OptionsColumn.ShowCaption = false;
            gvReceiveMan.Columns["Check"].OptionsColumn.AllowSort = DefaultBoolean.False;
            gvReceiveMan.Columns["Check"].OptionsColumn.AllowEdit = false;
            gvReceiveMan.Columns["姓名"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gvReceiveMan.Columns["姓名"].SummaryItem.DisplayFormat = @"总计：{0}";
            gvReceiveMan.Columns["姓名"].Width = 100;
            gvReceiveMan.Columns["省份"].Width = 100;
            gvReceiveMan.Columns["城市"].Width = 80;
            gvReceiveMan.Columns["区县"].Width = 80;
            gvReceiveMan.Columns["单位"].Width = 80;
            gvReceiveMan.Columns["部门"].Width = 80;
            gvReceiveMan.Columns["电话"].Width = 120;
            gvReceiveMan.Columns["手机"].Width = 120;
            gvReceiveMan.Columns["邮编"].Width = 80;
            gvReceiveMan.Columns["详细地址"].Width = 200;
            gvReceiveMan.Columns["备注"].Width = 150;
            // 列宽自定适应
            gvReceiveMan.BestFitColumns();
        }
        #endregion

        #region private void btnAddSendMan_Click(object sender, EventArgs e) 新增收件人
        /// <summary>
        /// 新增收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSendMan_Click(object sender, EventArgs e)
        {
            FrmAddSendMan frmSendMan = new FrmAddSendMan() { IsReceiveForm = true };
            if (frmSendMan.ShowDialog() == DialogResult.OK)
            {
                ReceiveManDataBind();
            }
            frmSendMan.Dispose();
        }
        #endregion

        #region private void btnDeleteSendMan_Click(object sender, EventArgs e) 删除收件人
        /// <summary>
        /// 删除收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteSendMan_Click(object sender, EventArgs e)
        {
            if (gvReceiveMan.RowCount == 0)
            {
                XtraMessageBox.Show(@"请添加收件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var list = GetCheckedUserRecord(gvReceiveMan);
            if (list.Any())
            {
                if (XtraMessageBox.Show(string.Format("确定删除选中的{0}条收件人记录吗？", list.Count), AppMessage.MSG0000, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, BillPrintHelper.BillPrintConnectionString))
                {
                    try
                    {
                        dbHelper.BeginTransaction();
                        var userManager = new ZtoUserManager(dbHelper);
                        var idArray = (from p in list select p.Id.ToString()).ToArray();
                        var result = userManager.Delete(idArray);
                        if (result > 0)
                        {
                            dbHelper.CommitTransaction();
                            ReceiveManDataBind();
                            XtraMessageBox.Show(string.Format("成功删除{0}条收件人记录。", result), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            dbHelper.RollbackTransaction();
                            XtraMessageBox.Show(@"删除失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch
                    {
                        dbHelper.RollbackTransaction();
                    }
                }
            }
        }
        #endregion

        #region public List<ZtoPrintBillEntity> GetCheckedRecord(GridView gridView) 获取到选择的收件人实体
        /// <summary>
        /// 获取到选择的收件人实体
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        public List<ZtoUserEntity> GetCheckedUserRecord(GridView gridView)
        {
            var list = new List<ZtoUserEntity>();
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
                        var entity = new ZtoUserEntity
                        {
                            Id = BaseBusinessLogic.ConvertToDecimal(gridView.GetRowCellValue(rowIndex, "Id").ToString()),
                            Realname = gridView.GetRowCellValue(rowIndex, "姓名").ToString(),
                            Province = gridView.GetRowCellValue(rowIndex, "省份").ToString(),
                            City = gridView.GetRowCellValue(rowIndex, "城市").ToString(),
                            County = gridView.GetRowCellValue(rowIndex, "区县").ToString(),
                            Address = gridView.GetRowCellValue(rowIndex, "详细地址").ToString(),
                            Company = gridView.GetRowCellValue(rowIndex, "单位").ToString(),
                            Department = gridView.GetRowCellValue(rowIndex, "部门").ToString(),
                            TelePhone = gridView.GetRowCellValue(rowIndex, "电话").ToString(),
                            Mobile = gridView.GetRowCellValue(rowIndex, "手机").ToString(),
                            Postcode = gridView.GetRowCellValue(rowIndex, "邮编").ToString()
                        };
                        list.Add(entity);
                    }
                }
            }
            return list;
        }
        #endregion

        #region private void btnEditSendMan_Click(object sender, EventArgs e) 更新收件人
        /// <summary>
        /// 更新收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSendMan_Click(object sender, EventArgs e)
        {
            var list = GetCheckedUserRecord(gvReceiveMan);
            if (list.Any())
            {
                if (list.Count > 1)
                {
                    XtraMessageBox.Show(@"请勿选择多条收件人记录。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                FrmAddSendMan frmSendMan = new FrmAddSendMan { Id = list.First().Id.ToString(), IsReceiveForm = true };
                if (frmSendMan.ShowDialog() == DialogResult.OK)
                {
                    ReceiveManDataBind();
                }
                frmSendMan.Dispose();
            }
            else
            {
                XtraMessageBox.Show(@"请添加收件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region private void btnRefreshSendMan_Click(object sender, EventArgs e) 刷新收件人信息
        /// <summary>
        /// 刷新收件人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshSendMan_Click(object sender, EventArgs e)
        {
            ReceiveManDataBind();
        }
        #endregion

        #region private void btnPrintSendMan_Click(object sender, EventArgs e) 批量打印收件人
        /// <summary>
        /// 批量打印收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintSendMan_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReceiveMan.RowCount > 0)
                {
                    var list = GetCheckedUserRecord(gvReceiveMan);
                    if (list.Any())
                    {
                        ZtoUserEntity defaultSendManEntity = new ZtoUserEntity();
                        if (ckUserDefaultSendMan.Checked)
                        {
                            ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                            var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                            defaultSendManEntity = userList.FirstOrDefault();
                        }
                        if (defaultSendManEntity == null)
                        {
                            if (XtraMessageBox.Show(@"未添加默认发件人信息，请添加默认发件人信息", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                var addSendMan = new FrmAddSendMan();
                                addSendMan.ShowDialog();
                                addSendMan.Dispose();
                            }
                            return;
                        }
                        _list = new List<ZtoPrintBillEntity>();
                        // 收件人集合
                        foreach (ZtoUserEntity userEntity in list)
                        {
                            ZtoPrintBillEntity printBillEntity = new ZtoPrintBillEntity
                            {
                                ReceiveMan = userEntity.Realname,
                                ReceiveProvince = userEntity.Province,
                                ReceiveCity = userEntity.City,
                                ReceiveCounty = userEntity.County,
                                ReceivePhone = userEntity.Mobile,
                                ReceiveAddress = userEntity.Address,
                                SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat)
                            };
                            var tempAddress = printBillEntity.ReceiveAddress;
                            if (!string.IsNullOrEmpty(tempAddress))
                            {
                                if (!string.IsNullOrEmpty(printBillEntity.ReceiveProvince))
                                {
                                    tempAddress = tempAddress.Replace(printBillEntity.ReceiveProvince, "");
                                }
                                if (!string.IsNullOrEmpty(printBillEntity.ReceiveCity))
                                {
                                    tempAddress = tempAddress.Replace(printBillEntity.ReceiveCity, "");
                                }
                                if (!string.IsNullOrEmpty(printBillEntity.ReceiveCounty))
                                {
                                    tempAddress = tempAddress.Replace(printBillEntity.ReceiveCounty, "");
                                }
                            }
                            printBillEntity.ReceiveAddress = printBillEntity.ReceiveProvince +
                                                            printBillEntity.ReceiveCity + printBillEntity.ReceiveCounty +
                                                            tempAddress;
                            printBillEntity.ReceiveCompany = userEntity.Company;
                            printBillEntity.ReceivePostcode = userEntity.Postcode;
                            if (string.IsNullOrEmpty(printBillEntity.ReceivePhone))
                            {
                                printBillEntity.ReceivePhone = userEntity.TelePhone;
                            }
                            if (ckUserDefaultSendMan.Checked)
                            {
                                printBillEntity.SendMan = defaultSendManEntity.Realname;
                                printBillEntity.SendPhone = defaultSendManEntity.Mobile + " " + defaultSendManEntity.TelePhone;
                                printBillEntity.SendProvince = defaultSendManEntity.Province;
                                printBillEntity.SendCity = defaultSendManEntity.City;
                                printBillEntity.SendCounty = defaultSendManEntity.County;
                                printBillEntity.SendAddress = defaultSendManEntity.Address;
                                printBillEntity.SendSite = "";
                                printBillEntity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                                printBillEntity.SendDeparture = defaultSendManEntity.Province;
                                printBillEntity.SendCompany = defaultSendManEntity.Company;
                                printBillEntity.SendDepartment = defaultSendManEntity.Department;
                                printBillEntity.SendPostcode = defaultSendManEntity.Postcode;
                                var selectedRemark = new List<string> { printBillEntity.SendProvince, printBillEntity.SendCity, printBillEntity.SendCounty };
                                var selectedReceiveMark = new List<string> { printBillEntity.ReceiveProvince, printBillEntity.ReceiveCity, printBillEntity.ReceiveCounty };
                                var printMark = BillPrintHelper.GetRemaike(string.Join(",", selectedRemark), printBillEntity.SendAddress, string.Join(",", selectedReceiveMark), printBillEntity.ReceiveAddress);
                                printBillEntity.BigPen = printMark;
                            }
                            _list.Add(printBillEntity);
                        }
                        GreatReport();
                        _report.PrintPreview(true);
                    }
                    else
                    {
                        XtraMessageBox.Show(@"请选择收件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show(@"请添加收件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.StackTrace, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    FrmTemplateSetting frmTemplateSetting = new FrmTemplateSetting();
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

        #region private void btnImportReceiveMan_Click(object sender, EventArgs e) 从Excel中导入收件人
        /// <summary>
        /// 从Excel中导入收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportReceiveMan_Click(object sender, EventArgs e)
        {
            //FrmImportReceiveManExcel importReceiveMan = new FrmImportReceiveManExcel();
            //if (importReceiveMan.ShowDialog() == DialogResult.OK)
            //{
            //    ReceiveManDataBind();
            //}
            //importReceiveMan.Dispose();
            var importExcelForSendManOrReceiveMan = new FrmImportExcelForReceiveMan();
            if (importExcelForSendManOrReceiveMan.ShowDialog() == DialogResult.OK)
            {
                ReceiveManDataBind();
            }
            importExcelForSendManOrReceiveMan.Dispose();
        }
        #endregion

        #region private void btnRecognitionAddress_Click(object sender, EventArgs e) 智能识别用户地址
        /// <summary>
        /// 智能识别用户地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecognitionAddress_Click(object sender, EventArgs e)
        {
            FrmRecognitionAddress recognitionTaoBaoFrm = new FrmRecognitionAddress();
            if (recognitionTaoBaoFrm.ShowDialog() == DialogResult.OK)
            {
                ReceiveManDataBind();
            }
            recognitionTaoBaoFrm.Dispose();
        }
        #endregion

        #region private void btnPrintMore_Click(object sender, EventArgs e) 打印多份
        /// <summary>
        /// 打印多份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintMore_Click(object sender, EventArgs e)
        {
            try
            {
                int printNumber;
                if (string.IsNullOrEmpty(cmbPrintNumber.Text.Trim()))
                {
                    printNumber = 1;
                }
                else
                {
                    var result = int.TryParse(cmbPrintNumber.Text, out printNumber);
                    if (!result)
                    {
                        printNumber = 1;
                    }
                }
                if (gvReceiveMan.RowCount > 0)
                {
                    ZtoUserEntity defaultSendManEntity = new ZtoUserEntity();
                    if (ckUserDefaultSendMan.Checked)
                    {
                        ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                        defaultSendManEntity = userList.FirstOrDefault();
                    }
                    if (defaultSendManEntity == null)
                    {
                        if (XtraMessageBox.Show(@"未添加默认发件人信息，请添加默认发件人信息", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            var addSendMan = new FrmAddSendMan();
                            addSendMan.ShowDialog();
                            addSendMan.Dispose();
                        }
                        return;
                    }
                    var list = GetCheckedUserRecord(gvReceiveMan);
                    if (list.Any())
                    {
                        _list = new List<ZtoPrintBillEntity>();
                        foreach (ZtoUserEntity userEntity in list)
                        {
                            ZtoPrintBillEntity printBillEntity = new ZtoPrintBillEntity
                            {
                                ReceiveMan = userEntity.Realname,
                                ReceiveProvince = userEntity.Province,
                                ReceiveCity = userEntity.City,
                                ReceiveCounty = userEntity.County,
                                ReceivePhone = userEntity.Mobile,
                                ReceiveAddress = userEntity.Address,
                                SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat)
                            };
                            var tempAddress = printBillEntity.ReceiveAddress;
                            if (!string.IsNullOrEmpty(tempAddress))
                            {
                                if (!string.IsNullOrEmpty(printBillEntity.ReceiveProvince))
                                {
                                    tempAddress = tempAddress.Replace(printBillEntity.ReceiveProvince, "");
                                }
                                if (!string.IsNullOrEmpty(printBillEntity.ReceiveCity))
                                {
                                    tempAddress = tempAddress.Replace(printBillEntity.ReceiveCity, "");
                                }
                                if (!string.IsNullOrEmpty(printBillEntity.ReceiveCounty))
                                {
                                    tempAddress = tempAddress.Replace(printBillEntity.ReceiveCounty, "");
                                }
                            }
                            printBillEntity.ReceiveAddress = printBillEntity.ReceiveProvince +
                                                            printBillEntity.ReceiveCity + printBillEntity.ReceiveCounty +
                                                            tempAddress;
                            printBillEntity.ReceiveCompany = userEntity.Company;
                            printBillEntity.ReceivePostcode = userEntity.Postcode;
                            if (string.IsNullOrEmpty(printBillEntity.ReceivePhone))
                            {
                                printBillEntity.ReceivePhone = userEntity.TelePhone;
                            }
                            if (ckUserDefaultSendMan.Checked)
                            {
                                printBillEntity.SendMan = defaultSendManEntity.Realname;
                                printBillEntity.SendPhone = defaultSendManEntity.Mobile + " " + defaultSendManEntity.TelePhone;
                                printBillEntity.SendProvince = defaultSendManEntity.Province;
                                printBillEntity.SendCity = defaultSendManEntity.City;
                                printBillEntity.SendCounty = defaultSendManEntity.County;
                                printBillEntity.SendAddress = defaultSendManEntity.Address;
                                printBillEntity.SendSite = "";
                                printBillEntity.SendDate = DateTime.Now.ToString(BaseSystemInfo.DateFormat);
                                printBillEntity.SendDeparture = defaultSendManEntity.Province;
                                printBillEntity.SendCompany = defaultSendManEntity.Company;
                                printBillEntity.SendDepartment = defaultSendManEntity.Department;
                                printBillEntity.SendPostcode = defaultSendManEntity.Postcode;
                                var selectedRemark = new List<string> { printBillEntity.SendProvince, printBillEntity.SendCity, printBillEntity.SendCounty };
                                var selectedReceiveMark = new List<string> { printBillEntity.ReceiveProvince, printBillEntity.ReceiveCity, printBillEntity.ReceiveCounty };
                                var printMark = BillPrintHelper.GetRemaike(string.Join(",", selectedRemark), printBillEntity.SendAddress, string.Join(",", selectedReceiveMark), printBillEntity.ReceiveAddress);
                                printBillEntity.BigPen = printMark;
                            }

                            for (int i = 0; i < printNumber; i++)
                            {
                                _list.Add(printBillEntity);
                            }
                        }
                        GreatReport();
                        _report.PrintPreview(true);
                    }
                    else
                    {
                        XtraMessageBox.Show(@"请选择收件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show(@"请添加收件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.StackTrace, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void btnChooseOk_Click(object sender, EventArgs e) 确认选择
        /// <summary>
        /// 确认选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseOk_Click(object sender, EventArgs e)
        {
            if (gvReceiveMan.RowCount == 0)
            {
                XtraMessageBox.Show("请添加收件人数据。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = gvReceiveMan.GetFocusedDataRow();
            ChooseId = row["Id"].ToString();
            if (this.ParentForm != null && this.ParentForm.Name == "FrmChooseReceiveMan")
            {
                this.ParentForm.Close();
            }
        }
        #endregion

        private void cmbPrintNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPrintNumber.Text == "0" || string.IsNullOrEmpty(cmbPrintNumber.Text))
            {
                cmbPrintNumber.Text = "1";
            }
        }

        /// <summary>
        /// 导出收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportReceiveMan_Click(object sender, EventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gcReceiveMan, gvReceiveMan);
        }
        /// <summary>
        /// 复制单元格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspCopyCellText_Click(object sender, EventArgs e)
        {
            if (gvReceiveMan.GetFocusedDataRow() == null) return;
            var text = gvReceiveMan.GetFocusedRowCellValue(gvReceiveMan.FocusedColumn.GetCaption()).ToString();
            Clipboard.SetText(text);
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspSelectAll_Click(object sender, EventArgs e)
        {
            CheckAll(gvReceiveMan);
        }
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspUnSelectAll_Click(object sender, EventArgs e)
        {
            UnCheckAll(gvReceiveMan);
        }
        /// <summary>
        /// 设置表格字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tspbtnSetFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.Font = AppearanceObject.DefaultFont;
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                foreach (AppearanceObject ap in gvReceiveMan.Appearance)
                {
                    ap.Font = fontDlg.Font;
                }
            }
        }

        /// <summary>
        /// 选中区域改变事件（很牛逼）2016-1-20 16:00:35
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReceiveMan_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UnCheckAll(gvReceiveMan);
            var t = gvReceiveMan.GetSelectedCells();
            foreach (GridCell gridCell in t)
            {
                gvReceiveMan.SetRowCellValue(gridCell.RowHandle, gvReceiveMan.Columns["Check"], true);
            }
        }

        /// <summary>
        /// 单元格光标离开保存修改的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReceiveMan_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Valid)
            {
                DataRow currentDataRow = gvReceiveMan.GetFocusedDataRow();
                if (currentDataRow != null)
                {
                    //gcReceiveMan.ShowTip("更新成功", ToolTipLocation.TopCenter);
                }
            }
        }

    }
}