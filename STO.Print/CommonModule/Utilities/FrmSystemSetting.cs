//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using STO.Print.Synchronous;

namespace STO.Print
{
    using DotNet.Utilities;
    using STO.Print.AddBillForm;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 系统设置窗体
    ///
    /// 修改纪录
    ///
    ///	  2015-11-29  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-11-29</date>
    /// </author>
    /// </summary>
    public partial class FrmSystemSetting : BaseForm
    {
        readonly BaseExpressManager _expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);

        readonly BaseTemplateManager _baseTemplateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);

        public FrmSystemSetting()
        {
            InitializeComponent();
            backViewSetting.SelectedTab = backstageViewTabItemPrinter;
        }

        private void FrmSystemSetting_Load(object sender, EventArgs e)
        {
            BindTemplate();
            BindPrinter();
            var defaultPrinter = BillPrintHelper.GetDefaultPrinter();
            if (!string.IsNullOrEmpty(defaultPrinter))
            {
                lsbPrinter.SelectedValue = defaultPrinter;
                lblDefaultPrinter.Text = @"默认打印机：" + defaultPrinter;
            }
            var defaultPrintTemplate = BillPrintHelper.GetDefaultTemplatePath();
            if (!string.IsNullOrEmpty(defaultPrintTemplate))
            {
                var templateEntity = _baseTemplateManager.GetList<BaseTemplateEntity>(new KeyValuePair<string, object>(BaseTemplateEntity.FieldFilePath, defaultPrintTemplate)).FirstOrDefault();
                if (templateEntity != null)
                {
                    var expressEntity = _expressManager.GetObject(templateEntity.ExpressId.ToString());
                    lsbTemplate.SelectedValue = expressEntity.Name + "-" + templateEntity.Name;
                    lblDefaultPrintTemplate.Text = @"默认打印模板：" + expressEntity.Name + "-" + templateEntity.Name;
                }
            }
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
        /// 保存默认打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavePrinter_Click(object sender, EventArgs e)
        {
            if (lsbPrinter.SelectedItem != null)
            {
                var item = lsbPrinter.SelectedItem.ToString();
                BillPrintHelper.SaveDefaultPrinterName(item);
                PrinterHelper.SetDefaultPrinter(item);
                lblDefaultPrinter.Text = @"默认打印机：" + item;
                MessageBox.Show("默认打印机保存成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 保存默认模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTemplate_Click(object sender, EventArgs e)
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
                        lblDefaultPrintTemplate.Text = @"默认打印模板：" + item;
                        MessageBox.Show("默认打印模板保存成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackUpDataBase_Click(object sender, EventArgs e)
        {
            ZipFile();
            MessageBox.Show("数据库文件备份成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 压缩文件夹，防止数据库挂机
        /// </summary>
        static void ZipFile()
        {
            string fileName = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (!Directory.Exists(BillPrintHelper.BackUpDataBaseFolder))
            {
                Directory.CreateDirectory(BillPrintHelper.BackUpDataBaseFolder);
            }
            ZipHelper zipHelper = new ZipHelper();
            zipHelper.ZipDir(BaseSystemInfo.StartupPath + "\\DataBase\\", fileName);
            string zipFileFullName = BaseSystemInfo.StartupPath + "\\" + fileName + ".zip";
            if (File.Exists(zipFileFullName))
            {
                File.Copy(zipFileFullName, BillPrintHelper.BackUpDataBaseFolder + fileName + ".zip", true);
                File.Delete(zipFileFullName);
            }
        }

        /// <summary>
        /// 打开备份数据库目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBackUpFolder_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BillPrintHelper.BackUpDataBaseFolder))
            {
                Directory.CreateDirectory(BillPrintHelper.BackUpDataBaseFolder);
            }
            Process.Start(BillPrintHelper.BackUpDataBaseFolder);
        }

        /// <summary>
        /// 查看打印机正在打印什么
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookPrinting_Click(object sender, EventArgs e)
        {
            if (lsbPrinter.SelectedItem != null)
            {
                var item = lsbPrinter.SelectedItem.ToString();
                STO.Print.Utilities.PrinterHelper.OpenPrinterStatusWindow(item);
            }
        }

        /// <summary>
        /// 初始化系统模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitBillTemplate_Click(object sender, EventArgs e)
        {
            // 
            Synchronous.Synchronous.InitExpressData();
            STO.Print.Utilities.MessageUtil.ShowTips("初始化成功");
        }

        /// <summary>
        /// 打开同步基础资料窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBaseInfo_Click(object sender, EventArgs e)
        {
            new FrmSynchronous().ShowDialog();
        }
    }
}