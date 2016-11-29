//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace STO.Print.UserControl
{
    using DevExpress.XtraRichEdit.API.Native;

    /// <summary>
    /// 富文本框自定义控件
    ///
    /// 修改纪录
    ///
    ///		  2014-09-17  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-09-17</date>
    /// </author>
    /// </summary>
    public partial class MyRichEdit : DevExpress.XtraEditors.XtraUserControl
    {
        public Form Frm;
        public MyRichEdit()
        {
            InitializeComponent();
            Load += MyRichEdit_Load;
            myRich.DocumentLoaded += myRich_DocumentLoaded;
        }

        void myRich_DocumentLoaded(object sender, EventArgs e)
        {
            MyRichEdit_Load(sender, e);
        }

        void MyRichEdit_Load(object sender, EventArgs e)
        {
            DocumentRange range = myRich.Document.Range;
            CharacterProperties cp = myRich.Document.BeginUpdateCharacters(range);
            cp.FontName = "宋体";
            cp.FontSize = 12;
            myRich.Document.EndUpdateCharacters(cp);
        }

        /// <summary>
        /// 区域截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSnapScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm = this.ParentForm;
            if (Frm == null)
            {
                return; ;
            }
            Frm.Hide();
            var dialog = new FrmSnapScreen(myRich) { Owner = Frm }.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                Frm.Show();
            }
        }

        public Image Snap(int x, int y, int width, int height)
        {
            var image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            g.CopyFromScreen(x, y, 0, 0, new Size(width, height));
            return image;
        }

        private void btnSnapFullScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var img = Snap(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            myRich.Document.InsertImage(myRich.Document.CaretPosition, DocumentImageSource.FromImage(img));
        }
    }
}
