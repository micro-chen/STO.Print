//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using STO.Print.Model;

namespace STO.Print
{
    using AddBillForm;
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using Utilities;

    /// <summary>
    /// 网点录单每天统计报表
    ///
    /// 修改纪录
    ///
    ///		  2014-06-16  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2014-06-18  报表数据记录在本地Sqlite中，读取更快，查询服务器超级慢
    ///       2014-09-23  验证DataTable是否为null是否有行数据，不然绑定数据会报错
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-05-06</date>
    /// </author>
    /// </summary>
    public partial class FrmPrintBillChart : BaseForm
    {

        public FrmPrintBillChart()
        {
            InitializeComponent();
            ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
            if (elecUserInfoEntity != null)
            {
                chartBill.Titles[0].Text = string.Format("{0}商家的打印统计报表", elecUserInfoEntity.Kehuid);
            }
            else
            {
                chartBill.Titles[0].Text = string.Format("打印统计报表");
            }
            barEditItem1.EditValue = DateTime.Now.AddDays(-5).ToString(BaseSystemInfo.DateFormat);
            barEditItem2.EditValue = DateTime.Now.AddDays(1).ToString(BaseSystemInfo.DateFormat);
            InitData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void InitData()
        {
            try
            {
                if (string.IsNullOrEmpty(barEditItem1.EditValue.ToString()))
                {
                    XtraMessageBox.Show("请选择起始日期", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(barEditItem2.EditValue.ToString()))
                {
                    XtraMessageBox.Show("请选择截止日期", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DateTime beginTime, endTime;
                if (!DateTime.TryParse(barEditItem1.EditValue.ToString(), out beginTime))
                {
                    XtraMessageBox.Show("起始日期输入不正确，请重新输入", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!DateTime.TryParse(barEditItem2.EditValue.ToString(), out endTime))
                {
                    XtraMessageBox.Show("截止日期输入不正确，请重新输入", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Convert.ToDateTime(barEditItem1.EditValue.ToString()) > Convert.ToDateTime(barEditItem2.EditValue.ToString()))
                {
                    XtraMessageBox.Show("截止日期必须大于起始日期，请重新输入", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string commandText = string.Format("SELECT COUNT(1) AS BillCount,CREATEON AS EnterTime FROM ZTO_PRINT_BILL WHERE CREATEON BETWEEN '{0}' AND '{1}'  GROUP BY strftime('%Y-%m-%d',CREATEON)", barEditItem1.EditValue.ToString(), barEditItem2.EditValue.ToString());
                var sourceDt = BillPrintHelper.BackupDbHelper.Fill(commandText);
                if (sourceDt != null && sourceDt.Rows.Count > 0)
                {
                    chartBill.DataSource = sourceDt;
                    dgvEnteBill.DataSource = chartBill.DataSource;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitData();
            // 这是打印报表图片的一段代码
            //this.chartBill.ShowPrintPreview(DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom);
            //DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
            //DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();

            //compositeLink.PrintingSystem = ps;
            //compositeLink.Landscape = true;
            //compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;

            //DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
            //ps.PageSettings.Landscape = true;
            //link.Component = this.chartBill;
            //compositeLink.Links.Add(link);

            //link.CreateDocument();  //建立文档
            //ps.PreviewFormEx.Show();//进行预览
        }
        /// <summary>
        /// 导出Excel(xls)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcelForxls_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // ExportHelper.ChartExportXls(chartBill);
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xls, dgvEnteBill, gvEnterBill);
        }
        /// <summary>
        /// 导出Excel（xlsx）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelForxlsx_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, dgvEnteBill, gvEnterBill);
            //  ExportHelper.ChartExportXlsx(chartBill);
        }
        /// <summary>
        /// 导出图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportImage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.ChartExportImage(chartBill);
        }
    }
}
