//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , STO , Ltd .
//-------------------------------------------------------------------------------------

using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DotNet.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using STO.Print.AddBillForm;

namespace STO.Print
{
    /// <summary>
    /// 根据单号生成对应的CODE128C条形码的
    ///
    /// 修改纪录
    ///
    ///		2014-5-29    版本：1.0 Yanghenglian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>Yanghenglian</name>
    ///		<date>2014-5-29</date>
    /// </author>
    /// </summary>
    public partial class FrmBuildBarCode : BaseForm
    {
        public IEnumerable<string> BillCodeList { get; set; }
        public FrmBuildBarCode()
        {
            InitializeComponent();
            Load += FrmPrintBarCode_Load;
        }

        void FrmPrintBarCode_Load(object sender, System.EventArgs e)
        {
            DocumentRange range = billCodeRich.Document.Range;
            CharacterProperties cp = billCodeRich.Document.BeginUpdateCharacters(range);
            cp.FontName = "新宋体";
            cp.FontSize = 12;
            billCodeRich.Document.EndUpdateCharacters(cp);
        }

        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuildCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBills.Text))
            {
                var billCodeArray = StringUtil.SplitMobile(txtBills.Text.Trim());
                foreach (var s in billCodeArray)
                {
                    Image img = GetBarcode2(100, billCodeRich.Width / 3, BarcodeLib.TYPE.CODE128C, s);
                    if (img != null)
                    {
                        billCodeRich.Document.InsertImage(billCodeRich.Document.CaretPosition, DocumentImageSource.FromImage(img));
                        // billCodeRich.Document.InsertText(billCodeRich.Document.CaretPosition, Environment.NewLine );
                    }
                }
            }
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Image GetBarcode2(int height, int width, BarcodeLib.TYPE type, string code)
        {
            Image image = null;
            try
            {
                var b = new BarcodeLib.Barcode
                            {
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                IncludeLabel = true,
                                Alignment = BarcodeLib.AlignmentPositions.CENTER,
                                LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER,
                                ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                            };
                var font = new Font("verdana", 10f);//字体设置  
                b.LabelFont = font;
                b.Height = height;//图片高度设置(px单位)  
                b.Width = width;//图片宽度设置(px单位)  
                image = b.Encode(type, code);//生成图片  
            }
            catch (Exception ex)
            {
                image = null;
            }
            return image;
        }

        /// <summary>
        /// 保存本地（word格式）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var saveFileDialog = new SaveFileDialog { Title = @"导出条形码", Filter = @"word文件(*.doc)|*.doc", OverwritePrompt = false }; 
            //var dialogResult = saveFileDialog.ShowDialog();
            //if (dialogResult != DialogResult.OK) return;
            //if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;

            //billCodeRich.SaveDocument(saveFileDialog.FileName, DocumentFormat.Doc);
            billCodeRich.SaveDocument("dd.doc", DocumentFormat.Doc);
        }

        /// <summary>
        /// 全屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFullScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnFullScreen.Caption == @"全屏")
            {
                spcContainer.PanelVisibility = SplitPanelVisibility.Panel2;
                spcContainer.SuspendLayout();
                btnFullScreen.Caption = @"退出全屏";
            }
            else
            {
                spcContainer.PanelVisibility = SplitPanelVisibility.Both;
                spcContainer.ResumeLayout();
                btnFullScreen.Caption = @"全屏";
            }
        }

        /// <summary>
        /// 快速打印
        ///  </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            billCodeRich.Print();
        }

        /// <summary>
        /// 预览打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreViewPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            billCodeRich.ShowPrintPreview();
        }

        /// <summary>
        /// 清除单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBillClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtBills.Text = string.Empty;
        }

        private void FrmBuildBarCode_Load(object sender, EventArgs e)
        {

        }
    }
}
