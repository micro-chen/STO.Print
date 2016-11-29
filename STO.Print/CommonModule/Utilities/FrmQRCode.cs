//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DotNet.Utilities;
    using ThoughtWorks.QRCode.Codec;
    using ThoughtWorks.QRCode.Codec.Data;

    /// <summary>
    /// 二维码生成和解码工具
    /// 
    /// 修改记录
    ///  
    ///     2015-10-24  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>		<name>YangHengLian</name>
    ///		<date>2015-10-24</date>
    /// </author>
    /// </summary>
    public partial class FrmQRCode : BaseForm
    {
        private Bitmap _bimg = null; //保存生成的二维码，方便后面保存
        private string _logoImagepath = string.Empty; //存储Logo的路径

        public FrmQRCode()
        {
            InitializeComponent();
        }

        private void FrmQRCode_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            for (int i = 1; i <= 30; i++)
            {
                cboVersion.Properties.Items.Add(i);
            }
            cboVersion.SelectedIndex = 6;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateQRCode_Click(object sender, EventArgs e)
        {
            try
            {
                ShowQRCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成二维码出错！", AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 显示生成的二维码
        /// </summary>
        public void ShowQRCode()
        {
            if (txtQRCodeContent.Text.Trim().Length <= 0)
            {
                MessageBox.Show("二维码内容不能为空，请输入内容！", AppMessage.MSG0000);
                txtQRCodeContent.Focus();
                return;
            }
            _bimg = CreateQRCode(txtQRCodeContent.Text);
            picQRcode.Image = _bimg;
            ResetImageStrethch(picQRcode, _bimg);
        }


        /// <summary>
        /// 重置Image的Strethch属性
        /// 当图片小于size时显示原始大小
        /// 当图片大于size时，缩小图片比例，让图片全部显示出来
        /// </summary>
        /// <param name="img"></param>
        /// <param name="bImg"></param>
        private void ResetImageStrethch(PictureEdit img, Bitmap bImg)
        {
            if (bImg.Width <= img.Width)
            {
                picQRcode.Properties.SizeMode = PictureSizeMode.Clip;
            }
            else
            {
                picQRcode.Properties.SizeMode = PictureSizeMode.Stretch;
            }
        }

        /// <summary>
        /// 生成二维码，如果有Logo，则在二维码中添加Logo
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Bitmap CreateQRCode(string content)
        {
            QRCodeEncoder qrEncoder = new QRCodeEncoder
                                          {
                                              QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                                              QRCodeScale = Convert.ToInt32(txtSize.Text),
                                              QRCodeVersion = Convert.ToInt32(cboVersion.SelectedItem.ToString()),
                                              QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
                                          };
            try
            {
                Bitmap qrcode = qrEncoder.Encode(content, Encoding.UTF8);
                if (!_logoImagepath.Equals(string.Empty))
                {
                    Graphics g = Graphics.FromImage(qrcode);
                    Bitmap bitmapLogo = new Bitmap(_logoImagepath);
                    int logoSize = Convert.ToInt32(txtLogoSize.Text);
                    bitmapLogo = new Bitmap(bitmapLogo, new System.Drawing.Size(logoSize, logoSize));
                    PointF point = new PointF(qrcode.Width / 2 - logoSize / 2, qrcode.Height / 2 - logoSize / 2);
                    g.DrawImage(bitmapLogo, point);
                }
                return qrcode;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("超出当前二维码版本的容量上限，请选择更高的二维码版本！", AppMessage.MSG0000);
                return new Bitmap(100, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成二维码出错！", AppMessage.MSG0000);
                return new Bitmap(100, 100);
            }
        }

        /// <summary>
        /// 保存二维码图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Png文件(*.Png)|*.png|All files(*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveQRCode(saveDialog.FileName);
                    MessageBox.Show("保存成功！", AppMessage.MSG0000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存二维码出错！", AppMessage.MSG0000);
                }
            }
        }

        /// <summary>
        /// 保存二维码，并为二维码添加白色背景。
        /// </summary>
        /// <param name="path"></param>
        public void SaveQRCode(string path)
        {
            if (_bimg != null)
            {
                Bitmap bitmap = new Bitmap(_bimg.Width + 30, _bimg.Height + 30);
                Graphics g = Graphics.FromImage(bitmap);
                g.FillRectangle(System.Drawing.Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
                g.DrawImage(_bimg, new PointF(15, 15));
                bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// 打开二维码图片，并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenQRCode_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "图片文件|*.jpg;*.png;*.gif|All files(*.*)|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                _bimg = new Bitmap(openDialog.FileName);
                picQRcode.Image = Image.FromFile(openDialog.FileName);
                ResetImageStrethch(picQRcode, _bimg);
            }
        }

        /// <summary>
        /// 解析二维码按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecodeQRCode_Click(object sender, EventArgs e)
        {
            try
            {
                tbDecodeResult.Text = "";
                DecodeQRCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("解析二维码出错！", AppMessage.MSG0000);
                return;
            }
        }

        /// <summary>
        /// 解析二维码
        /// </summary>
        public void DecodeQRCode()
        {
            if (_bimg == null)
            {
                MessageBox.Show("请先打开一张二维码图片！", AppMessage.MSG0000);
                return;
            }
            QRCodeDecoder qrDecoder = new QRCodeDecoder();
            QRCodeImage qrImage = new QRCodeBitmapImage(_bimg);
            tbDecodeResult.Text = qrDecoder.decode(qrImage, Encoding.UTF8);
        }

        /// <summary>
        /// 将验证码解析结果复制到剪切板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDecodeResult.Text))
            {
                Clipboard.SetText(tbDecodeResult.Text);
                MessageBox.Show("复制成功！", AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 添加Logo按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "图片文件|*.jpg;*.png;*.gif|All files(*.*)|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                _logoImagepath = openDialog.FileName;
                Bitmap bImg = new Bitmap(_logoImagepath);
                picLogo.Image = Image.FromFile(openDialog.FileName);
                ResetImageStrethch(picLogo, bImg);
            }
        }
    }
}