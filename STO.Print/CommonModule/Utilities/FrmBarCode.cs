//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using STO.Print.AddBillForm;
using Image = System.Drawing.Image;

namespace STO.Print
{
    using DotNet.Utilities;

    /// <summary>
    /// 条形码生成和解码工具
    /// 
    /// 修改记录
    ///  
    ///     2015-10-24  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-10-24</date>
    /// </author>
    /// </summary>
    public partial class FrmBarCode : BaseForm
    {
        public FrmBarCode()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateQRCode_Click(object sender, EventArgs e)
        {
            //Image image;
            //GetBarcode(148, 55, BarcodeLib.TYPE.CODE128, "20131025-136", out image);
            //picQRcode.Image = image;
            System.Drawing.Image image;
            int width = 148, height = 55;
            var barType = cboVersion.SelectedItem.ToString();
            var type = (BarcodeLib.TYPE)Enum.Parse(typeof(BarcodeLib.TYPE), barType, true);
            GetBarcode2(picQRcode.Height, picQRcode.Width, type, txtQRCodeContent.Text, out image);

            picQRcode.Image = image;
        }

        public static void GetBarcode2(int height, int width, BarcodeLib.TYPE type, string code, out System.Drawing.Image image)
        {
            try
            {
                image = null;
                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                b.BackColor = System.Drawing.Color.White;//图片背景颜色  
                b.ForeColor = System.Drawing.Color.Black;//条码颜色  
                b.IncludeLabel = true;
                b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                b.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;//图片格式  
                System.Drawing.Font font = new System.Drawing.Font("verdana", 10f);//字体设置  
                b.LabelFont = font;
                b.Height = height;//图片高度设置(px单位)  
                b.Width = width;//图片宽度设置(px单位)  

                image = b.Encode(type, code);//生成图片  
                //image.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

            }
            catch (Exception ex)
            {

                image = null;
            }
        }

        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="image"></param>
        public static void GetBarcode(int height, int width, BarcodeLib.TYPE type, string code, out Image image)
        {
            try
            {
                //BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                //b.BackColor = Color.White;//图片背景颜色
                //b.ForeColor = Color.Black;//条码颜色
                //b.IncludeLabel = true;
                //b.Alignment = BarcodeLib.AlignmentPositions.LEFT;
                //b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;//code的显示位置
                //b.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;//图片格式
                //Font font = new Font("verdana", 10f);//字体设置
                //b.LabelFont = font;
                //b.Height = height;//图片高度设置(px单位)
                //b.Width = width;//图片宽度设置(px单位)
                //image = b.Encode(type, code);//生成图片
                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                b.BackColor = System.Drawing.Color.White;//图片背景颜色  
                b.ForeColor = System.Drawing.Color.Black;//条码颜色  
                b.IncludeLabel = true;
                b.Alignment = BarcodeLib.AlignmentPositions.LEFT;
                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                b.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;//图片格式  
                System.Drawing.Font font = new System.Drawing.Font("verdana", 10f);//字体设置  
                b.LabelFont = font;
                b.Height = height;//图片高度设置(px单位)  
                b.Width = width;//图片宽度设置(px单位)  
                image = b.Encode(type, code);
                // image.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                image = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picQRcode.Image == null)
            {
                return;
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Jpg文件(*.Jpg)|*.jpg|All files(*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image image = picQRcode.Image;
                    image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MessageBox.Show("保存成功！", AppMessage.MSG0000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存条形码出错！", AppMessage.MSG0000);
                }
            }
        }

        private void FrmBarCode_Load(object sender, EventArgs e)
        {
            cboVersion.Properties.Items.Clear();
            foreach (int i in Enum.GetValues(typeof(BarcodeLib.TYPE)))
            {
                ListItem listitem = new ListItem(Enum.GetName(typeof(BarcodeLib.TYPE), i), i.ToString());
                cboVersion.Properties.Items.Add(listitem);
            }
            cboVersion.SelectedIndex = 0;
            txtInfo.Text = "京东单号条码类型：CODE128，单号：12077416310" + Environment.NewLine + "申通面单条码类型：CODE128C，单号：751811111111";
        }

    }
}