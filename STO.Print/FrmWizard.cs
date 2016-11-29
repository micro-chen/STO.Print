//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 初始化窗体（设置打印机和模板等信息）
    ///
    /// 修改纪录
    ///
    ///		2015-10-22  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-10-22</date>
    /// </author>
    /// </summary>
    public partial class FrmWizard : BaseForm
    {
        readonly BaseExpressManager _expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);

        readonly BaseTemplateManager _baseTemplateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);

        public FrmWizard()
        {
            InitializeComponent();
        }

        private void FrmWizard_Load(object sender, EventArgs e)
        {
            BindTemplate();
            BindPrinter();
        }
        /// <summary>
        /// 绑定打印机
        /// </summary>
        void BindPrinter()
        {
            // 加载系统默认打印机信息到系统中
            var list = PrinterHelper.GetLocalPrinters();
            lsbPrinter.Items.AddRange(list.ToArray());
        }

        /// <summary>
        /// 绑定打印模板
        /// </summary>
        void BindTemplate()
        {
            var expressList = _expressManager.GetList<BaseExpressEntity>();
            if (!expressList.Any())
            {
                return;
            }
            foreach (BaseExpressEntity baseExpressEntity in expressList)
            {
                var list = _baseTemplateManager.GetList<BaseTemplateEntity>(new KeyValuePair<string, object>(BaseTemplateEntity.FieldExpressId, baseExpressEntity.Id));
                if (list.Any())
                {
                    foreach (BaseTemplateEntity baseTemplateEntity in list)
                    {
                        lsbTemplate.Items.Add(baseExpressEntity.Name + "-" + baseTemplateEntity.Name);
                    }
                }
            }
        }

        /// <summary>
        /// 完成按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardControlInit_FinishClick(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// 下一步点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardControlInit_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (e.Page == wizardPageTemplateSetting)
            {
                if (lsbTemplate.SelectedItem == null && lsbTemplate.ItemCount > 0)
                {
                    e.Handled = true;
                }
                else
                {
                    if (lsbTemplate.SelectedItem != null)
                    {
                        var item = lsbTemplate.SelectedItem.ToString();
                        var tempName = item.Split('-');
                        var expressEntity =
                            _expressManager.GetList<BaseExpressEntity>(
                                new KeyValuePair<string, object>(BaseExpressEntity.FieldName, tempName[0])).First();
                        if (expressEntity != null)
                        {
                            var paramterList = new List<KeyValuePair<string, object>>
                                                   {
                                                       new KeyValuePair<string, object>(BaseTemplateEntity.FieldExpressId,
                                                                                        expressEntity.Id),
                                                       new KeyValuePair<string, object>(BaseTemplateEntity.FieldName,
                                                                                        tempName[1])
                                                   };
                            var list = _baseTemplateManager.GetList<BaseTemplateEntity>(paramterList);
                            if (list.Any())
                            {
                                BillPrintHelper.SaveDefaultPrintTemplate(list[0].FilePath);
                            }
                        }
                    }
                }
                lsbPrinter.SelectedIndex = 0;
            }
            if (e.Page == wizardPagePrinterSetting)
            {
                if (lsbPrinter.SelectedItem == null && lsbPrinter.ItemCount > 0)
                {
                    e.Handled = true;
                }
                else
                {
                    if (lsbPrinter.SelectedItem != null)
                    {
                        var item = lsbPrinter.SelectedItem.ToString();
                        BillPrintHelper.SaveDefaultPrinterName(item);
                        PrinterHelper.SetDefaultPrinter(item);
                    }
                }
            }
        }

        /// <summary>
        /// 打印测试页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintTestPage_Click(object sender, EventArgs e)
        {
            // printDocument1 为 打印控件
            // 设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 500, 300);
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            DialogResult result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
                this.printDocument1.Print();
        }
        /// <summary>
        /// 打印的格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*如果需要改变自己 可以在new Font(new FontFamily("黑体"),11）中的“黑体”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue , 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置 */
            e.Graphics.DrawString("淘宝订单发货清单", new Font(new FontFamily("黑体"), 11), System.Drawing.Brushes.Black, 170, 10);
            e.Graphics.DrawString("淘宝卖家:昔年家纺", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Blue, 10, 12);
            //信息的名称
            e.Graphics.DrawLine(Pens.Black, 8, 30, 480, 30);
            e.Graphics.DrawString("商品编号", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 35);
            e.Graphics.DrawString("商品名称", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 160, 35);
            e.Graphics.DrawString("数量", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 260, 35);
            e.Graphics.DrawString("单价", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 330, 35);
            e.Graphics.DrawString("总金额", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 400, 35);
            e.Graphics.DrawLine(Pens.Black, 8, 50, 480, 50);
            //产品信息
            e.Graphics.DrawString("R2011-01-2016:06:35", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 55);
            e.Graphics.DrawString("联想A460", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 160, 55);
            e.Graphics.DrawString("100", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 260, 55);
            e.Graphics.DrawString("200.00", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 330, 55);
            e.Graphics.DrawString("20000.00", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 400, 55);


            e.Graphics.DrawLine(Pens.Black, 8, 200, 480, 200);
            e.Graphics.DrawString("寄件地址:上海市青浦区华新镇华志路1685号", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 210);
            e.Graphics.DrawString("寄件人呢:xxxx", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 250, 210);
            e.Graphics.DrawString("客服热线:95311", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 350, 210);
            e.Graphics.DrawString("打印时间:" + DateTime.Now.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 9, 230);
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
