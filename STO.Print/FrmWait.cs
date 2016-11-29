//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

namespace STO.Print
{
    using DevExpress.XtraWaitForm;

    /// <summary>
    /// 等待窗体
    ///
    /// 修改纪录
    ///
    ///		  2015-06-29  版本：1.0 YangHengLian 创建文件。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-06-29</date>
    /// </author>
    /// </summary>
    
    public partial class FrmWait : WaitForm
    {
        public FrmWait()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }

        #endregion

        public enum WaitFormCommand
        {
        }
    }
}