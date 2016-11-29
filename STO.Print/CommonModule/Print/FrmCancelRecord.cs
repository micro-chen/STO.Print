//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Threading.Tasks;

namespace STO.Print
{
    using DevExpress.Utils;
    using DotNet.Utilities;
    using STO.Print.AddBillForm;
    using STO.Print.Utilities;

    /// <summary>
    /// 取消的订单回收站查询
    ///  
    /// 修改记录
    /// 
    ///     2016-07-05 版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-07-05</date>
    /// </author>
    /// </summary>
    public partial class FrmCancelRecord : BaseForm
    {

        protected delegate void UpdateControlText1();

        public FrmCancelRecord()
        {
            InitializeComponent();
        }

        #region public void GridDataBind() 打印数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void GridDataBind()
        {
            string commandText = "SELECT Id,RECEIVE_MAN AS 收件人姓名,RECEIVE_PHONE AS 收件电话,RECEIVE_ADDRESS AS 收件人详细地址,BILL_CODE AS 单号,ORDER_NUMBER AS 订单号,SEND_MAN AS 发件人姓名,SEND_PHONE AS 发件电话,SEND_ADDRESS AS 发件详细地址,CREATEON AS 创建时间,REMARK AS 备注 FROM ZTO_PRINT_CANCEL " + string.Format(" WHERE CREATEON BETWEEN '{0}' AND '{1}'", Convert.ToDateTime(barStartDate.EditValue).ToString(BaseSystemInfo.DateFormat), Convert.ToDateTime(barEndDate.EditValue).ToString(BaseSystemInfo.DateFormat)) + " ORDER BY CREATEON";
            if (barStartDate.EditValue == null || barEndDate.EditValue == null)
            {
                commandText = BillPrintHelper.CmdStrForZtoBillPrinter;
            }
            var dt = BillPrintHelper.DbHelper.Fill(commandText);
           
            // 设置gridview列头的字体大小
            gridViewBills.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);
            // 设置gridview列头居中
            gridViewBills.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gridControlBills.DataSource = dt;
            gridViewBills.OptionsView.ColumnAutoWidth = false;
            gridViewBills.Columns["Id"].Visible = false;
            gridViewBills.Columns["Id"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["收件人姓名"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridViewBills.Columns["收件人姓名"].SummaryItem.DisplayFormat = @"总计：{0}";
            gridViewBills.Columns["收件人姓名"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridViewBills.Columns["单号"].Width = 100;
            gridViewBills.Columns["订单号"].Width = 120;
            gridViewBills.Columns["创建时间"].Width = 160;
            gridViewBills.Columns["发件人姓名"].Width = 80;
            gridViewBills.Columns["发件电话"].Width = 100;
            gridViewBills.Columns["发件详细地址"].Width = 300;
            gridViewBills.Columns["收件人姓名"].Width = 80;
            gridViewBills.Columns["收件电话"].Width = 100;
            gridViewBills.Columns["收件人详细地址"].Width = 300;
            gridViewBills.Columns["备注"].Width = 100;
            gridViewBills.Columns["备注"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gridViewBills.Columns["创建时间"].DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss";
        }
        #endregion

        private void FrmPrintHistoryRecord_Load(object sender, EventArgs e)
        {
            try
            {
                txtEndDate.MaxValue = DateTime.Now;
                txtStartDate.MaxValue = DateTime.Now;
                barStartDate.EditValue = DateTime.Now.AddDays(-30);
                barEndDate.EditValue = DateTime.Now.AddDays(1);
                var t = new Task(BindPrintData);
                t.Start();
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

        private void UpdateControlText()
        {
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
        }

        /// <summary>
        /// 导出xls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport2003Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xls, gridControlBills, gridViewBills);
        }

        /// <summary>
        /// 导出xlsx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport2007Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gridControlBills, gridViewBills);
        }

        /// <summary>
        /// 导出CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportCSV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Csv, gridControlBills, gridViewBills);
        }

        /// <summary>
        /// 查询取消记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridDataBind();
        }

    }
}
