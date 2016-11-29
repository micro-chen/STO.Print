//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using grproLib;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 设计模板界面
    ///  
    /// 修改记录
    /// 
    ///     2015-07-21  版本：1.0 YangHengLian 创建
    ///     2015-07-22  YangHengLian 支持用户导入模板设计和系统默认模板的设计
    ///     2015-07-23  YangHengLian 加入保存和另存为的功能
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-21</date>
    /// </author>
    /// </summary>
    public partial class FrmDesigner : BaseForm
    {
        private readonly GridppReport _report = new GridppReport();

        BaseExpressManager _expressManager = new BaseExpressManager(BillPrintHelper.DbHelper);

        BaseTemplateManager _baseTemplateManager = new BaseTemplateManager(BillPrintHelper.DbHelper);

        public FrmDesigner()
        {
            InitializeComponent();
        }

        #region private void FrmPreview_Load(object sender, System.EventArgs e) 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPreview_Load(object sender, System.EventArgs e)
        {
            FillTreeViewNodes();
        }
        #endregion

        #region private void FillTreeViewNodes() 构建节点
        /// <summary>
        /// 构建节点
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
                        rootNode.Nodes.Add(baseTemplateEntity.FilePath, baseTemplateEntity.Name);
                    }
                }
            }
            treeView1.ExpandAll();
            treeView1.SelectedNode = treeView1.Nodes[0];
            treeView1_AfterSelect(this, null);
        }
        #endregion

        #region private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        /// <summary>
        /// 选中节点
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
                _report.Clear();
                _report.LoadFromFile(GetSelectPath());
                axGRDesigner1.Report = _report;
                axGRDesigner1.DefaultAction = false;
            }
            else
            {
                XtraMessageBox.Show(@"未找到模板文件", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region private void btnOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 打开外部模板
        /// <summary>
        /// 打开外部模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = @"Grid++Report 报表模板(*.grf)|*.grf;" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _report.Clear();
                    _report.LoadFromFile(ofd.FileName);
                    axGRDesigner1.Report = _report;
                    axGRDesigner1.DefaultAction = false;
                    treeView1.Nodes.Clear();
                    FillTreeViewNodes();
                    TreeNode rootNode = treeView1.Nodes.Add("外部模板");
                    rootNode.Nodes.Add(ofd.FileName, Path.GetFileNameWithoutExtension(ofd.FileName));
                    treeView1.ExpandAll();
                    treeView1.SelectedNode = rootNode.FirstNode;
                }
            }
        }
        #endregion

        #region private void btnPrintSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 打印设置
        /// <summary>
        /// 打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _report.Printer.PrinterSetupDialog();
        }
        #endregion

        #region private void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 预览

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _report.PrintPreview(true);
        }
        #endregion

        #region private void axGRDesigner1_SaveReport(object sender, System.EventArgs e) 编辑器保存按钮事件
        /// <summary>
        /// 编辑器保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axGRDesigner1_SaveReport(object sender, System.EventArgs e)
        {
            var filePath = GetSelectPath();
            if (!string.IsNullOrEmpty(filePath))
            {
                axGRDesigner1.Post();
                _report.SaveToFile(GetSelectPath());
                XtraMessageBox.Show(@"保存成功。", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                axGRDesigner1.DefaultAction = false;
            }
        }
        #endregion

        #region private string GetSelectPath() 获取选中节点文件路径
        /// <summary>
        /// 获取选中节点文件路径
        /// </summary>
        /// <returns></returns>
        private string GetSelectPath()
        {
            //从对应文件中载入报表模板数据
            string path = treeView1.SelectedNode.Name;
            return path;
        }
        #endregion

        #region private void btnSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 另存为
        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>参考链接：http://blog.csdn.net/xiao_dan/article/details/4506361 </remarks>
        private void btnSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var filePath = GetSelectPath();
            if (!string.IsNullOrEmpty(filePath))
            {
                axGRDesigner1.Post();
                _report.SaveToFile(filePath);
                var saveFileDialog = new SaveFileDialog { Title = @"保存打印模板", Filter = @"grf文件(*.grf)|*.grf", OverwritePrompt = false };
                var dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult != DialogResult.OK) return;
                while (File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    if (dialogResult != DialogResult.Yes) return;
                }
                if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
                File.Copy(filePath, saveFileDialog.FileName, true);
                if (XtraMessageBox.Show("保存文件成功，是否打开？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    var process = new Process
                    {
                        StartInfo =
                        {
                            FileName = saveFileDialog.FileName,
                            Verb = "Open",
                            WindowStyle = ProcessWindowStyle.Normal
                        }
                    };
                    process.Start();
                }
            }
        }
        #endregion

        #region private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            axGRDesigner1_SaveReport(this, null);
        }
        #endregion

    }
}
