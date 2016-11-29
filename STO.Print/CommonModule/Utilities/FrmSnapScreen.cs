//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.API.Native;

    /// <summary>
    /// 截图窗体
    ///
    /// 修改纪录
    ///
    ///		  2014-06-29  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-06-29</date>
    /// </author>
    /// </summary>
    public partial class FrmSnapScreen : Form
    {
        int _x, _y, _nowX, _nowY, _width, _height;
        bool _isMouthDown;
        Graphics _g;
        private static RichEditControl _myRich;

        public FrmSnapScreen(RichEditControl rich)
        {
            InitializeComponent();
            _myRich = rich;
        }

        public FrmSnapScreen()
        {
            InitializeComponent();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            _x = MousePosition.X;
            _y = MousePosition.Y;
            _isMouthDown = true;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouthDown)
            {
                _width = Math.Abs(MousePosition.X - _x);
                _height = Math.Abs(MousePosition.Y - _y);
                _g = CreateGraphics();
                _g.Clear(BackColor);
                _g.FillRectangle(Brushes.CornflowerBlue, _x < MousePosition.X ? _x : MousePosition.X, _y < MousePosition.Y ? _y : MousePosition.Y, _width + 1, _height + 1);
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            _nowX = MousePosition.X + 1;
            _nowY = MousePosition.Y + 1;
            var cutImg = Snap(_x < _nowX ? _x : _nowX, _y < _nowY ? _y : _nowY, Math.Abs(_nowX - _x), Math.Abs(_nowY - _y));
            Clipboard.SetImage(cutImg);
            if (_myRich != null)
            {
                _myRich.Document.InsertImage(_myRich.Document.CaretPosition, DocumentImageSource.FromImage(cutImg));
                _myRich = null;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        public Image Snap(int x1, int y1, int width1, int height1)
        {
            var image = new Bitmap(width1, height1);
            Graphics g = Graphics.FromImage(image);
            g.CopyFromScreen(x1, y1, 0, 0, new Size(_width, _height));
            return image;
        }
    }
}
