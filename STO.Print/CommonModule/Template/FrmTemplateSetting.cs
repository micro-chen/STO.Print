//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------
//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using grproLib;
    using System.IO;
    using Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;

    /// <summary>
    /// 设置默认打印模板
    /// 
    /// 修改记录
    ///  
    ///     2015-07-22  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-22</date>
    /// </author>
    /// </summary>
    public partial class FrmTemplateSetting : BaseForm
    {
        private readonly GridppReport _report = new GridppReport();

        BaseExpressManager _expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);

        BaseTemplateManager _baseTemplateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);

        public FrmTemplateSetting()
        {
            InitializeComponent();
        }

        #region private void FrmTemplateSetting_Load(object sender, EventArgs e) 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTemplateSetting_Load(object sender, EventArgs e)
        {
            FillTreeViewNodes();
            var filePath = BillPrintHelper.GetDefaultTemplatePath();
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    txtFileFullPath.Text = filePath;
                    _report.Clear();
                    _report.LoadFromFile(filePath);
                    axGRDesigner1.Report = _report;
                    axGRDesigner1.DefaultAction = false;
                }
            }
        }
        #endregion

        #region private void FillTreeViewNodes() 加载节点数据
        /// <summary>
        /// 加载节点数据
        /// </summary>
        private void FillTreeViewNodes()
        {
            var expressList = _expressManager.GetList<BaseExpressEntity>();
            if (!expressList.Any())
            {
                XtraMessageBox.Show(@"未加载到任何快递公司信息。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (BaseExpressEntity baseExpressEntity in expressList)
            {
                TreeNode rootNode = treeView1.Nodes.Add(baseExpressEntity.Name);
                var list = _baseTemplateManager.GetList<BaseTemplateEntity>(new KeyValuePair<string, object>(BaseTemplateEntity.FieldExpressId, baseExpressEntity.Id));
                if (list.Any())
                {
                    foreach (BaseTemplateEntity baseTemplateEntity in list)
                    {
                        rootNode.Nodes.Add(baseTemplateEntity.Id.ToString(), baseTemplateEntity.Name);
                    }
                }
            }
            treeView1.ExpandAll();
            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
            treeView1_AfterSelect(this, null);
        }
        #endregion

        #region private void FrmTemplateSetting_FormClosing(object sender, FormClosingEventArgs e) 关闭窗体中事件
        /// <summary>
        /// 关闭窗体中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTemplateSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion

        #region private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) 节点选中事件
        /// <summary>
        /// 节点选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Level == 0)
            {
                return;
            }
            var path = GetSelectPath();
            if (File.Exists(path))
            {
                var imgPath = GetSelectImagePath();
                if (string.IsNullOrEmpty(imgPath))
                {
                    return;
                }
                if (STO.Print.Utilities.FileUtil.FileIsExist(imgPath))
                {
                    picBillBox.Image = Image.FromFile(imgPath);
                    lblPPI.Text = string.Format("分辨率：{0}*{1}", picBillBox.Image.Width, picBillBox.Image.Height);
                    picBillBox.Properties.ZoomPercent = 55;
                    trackPercent.Value = (int) picBillBox.Properties.ZoomPercent;
                }
                else
                {
                    MessageUtil.ShowTips("模板图片文件不存在");
                }
            }
            else
            {
                MessageUtil.ShowTips("模板文件不存在");
            }
        }
        #endregion

        #region private void trackPercent_EditValueChanged(object sender, EventArgs e) 刻度条值改变事件
        /// <summary>
        /// 刻度条值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackPercent_EditValueChanged(object sender, EventArgs e)
        {
            picBillBox.Properties.ZoomPercent = trackPercent.Value;
        }
        #endregion

        #region private void btnSave_Click(object sender, EventArgs e) 保存默认系统打印模板
        /// <summary>
        /// 保存默认系统打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var templateFilePath = GetSelectPath();
            if (string.IsNullOrEmpty(templateFilePath))
            {
                XtraMessageBox.Show(@"模板文件不存在，保存失败", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BillPrintHelper.SaveDefaultPrintTemplate(templateFilePath);
            XtraMessageBox.Show(@"保存成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region private void btnOpen_Click(object sender, EventArgs e) 打开外部模板文件
        /// <summary>
        /// 打开外部模板文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = @"grf文件|*.grf;" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFileFullPath.Text = ofd.FileName;
                    _report.Clear();
                    _report.LoadFromFile(txtFileFullPath.Text);
                    axGRDesigner1.Report = _report;
                    axGRDesigner1.DefaultAction = false;
                }
            }
        }
        #endregion

        #region private void btnSaveOther_Click(object sender, EventArgs e)
        /// <summary>
        /// 保存外部选择默认打印模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveOther_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileFullPath.Text.Trim()))
            {
                XtraMessageBox.Show(@"请先选择模板文件", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BillPrintHelper.SaveDefaultPrintTemplate(txtFileFullPath.Text);
            XtraMessageBox.Show(@"保存成功，请勿删除此模板。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region private void btnClose_Click(object sender, EventArgs e) 关闭窗体
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region private string GetSelectPath() 获取选中节点文件路径
        /// <summary>
        /// 获取选中节点文件路径
        /// </summary>
        /// <returns></returns>
        private string GetSelectPath()
        {
            BaseTemplateEntity templateEntity = _baseTemplateManager.GetList<BaseTemplateEntity>(new KeyValuePair<string, object>(BaseTemplateEntity.FieldId, treeView1.SelectedNode.Name)).FirstOrDefault();
            if (templateEntity != null)
            {
                return templateEntity.FilePath;
            }
            return "";
        }
        #endregion

        #region private string GetSelectImagePath() 获取选中节点图片路径
        /// <summary>
        /// 获取选中节点文件路径
        /// </summary>
        /// <returns></returns>
        private string GetSelectImagePath()
        {
            //从对应文件中载入报表模板数据
            BaseTemplateEntity templateEntity = _baseTemplateManager.GetList<BaseTemplateEntity>(new KeyValuePair<string, object>(BaseTemplateEntity.FieldId, treeView1.SelectedNode.Name)).FirstOrDefault();
            if (templateEntity != null)
            {
                return templateEntity.BackgroundImagePath;
            }
            return "";
        }
        #endregion
    }
}
