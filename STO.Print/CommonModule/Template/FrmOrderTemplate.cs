//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using grproLib;

    /// <summary>
    /// 定制模板界面
    ///  
    /// 修改记录
    /// 
    ///     2015-08-11  版本：1.0 YangHengLian 创建
    ///     2015-10-08  去除一些无用的按钮，加一些提示的消息，告知用户如何操作，自己做一个自定义的模板
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-11</date>
    /// </author>
    /// </summary>
    public partial class FrmOrderTemplate : BaseForm
    {
        private readonly GridppReport _report = new GridppReport();

        public FrmOrderTemplate()
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
            TreeNode rootNode = treeView1.Nodes.Add("系统模板");
            rootNode.Nodes.Add("定制新模板");
            treeView1.ExpandAll();
            treeView1.SelectedNode = rootNode.FirstNode;
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
            var filePath = GetSelectPath();
            if (File.Exists(filePath))
            {
                _report.Clear();
                _report.LoadFromFile(filePath);
                axGRDesigner1.Report = _report;
                axGRDesigner1.DefaultAction = false;
            }
            else
            {
                XtraMessageBox.Show("定制参考模板文件不存在", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        #region private string GetSelectPath() 获取选中节点文件路径
        /// <summary>
        /// 获取选中节点文件路径
        /// </summary>
        /// <returns></returns>
        private string GetSelectPath()
        {
            //从对应文件中载入报表模板数据
            string path = treeView1.SelectedNode.Name;
            string defaultFolder = Application.StartupPath + "\\Template\\";
            if (treeView1.SelectedNode.Text.Equals("定制新模板"))
            {
                path = defaultFolder + "Order.grf";
            }
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
                if (File.Exists(filePath))
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
                else
                {
                    XtraMessageBox.Show("定制参考模板文件不存在", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
