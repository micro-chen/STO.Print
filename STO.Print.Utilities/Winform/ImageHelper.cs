using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 图片对象比较、缩放、缩略图、水印、压缩、转换、编码等操作辅助类
    /// </summary>
    public class ImageHelper
    {
        #region 图片比较

        /// <summary>
        /// 两张图片的对比结果
        /// </summary>
        public enum CompareResult
        {
            /// <summary>
            /// 图片一样
            /// </summary>
            CompareOk,
            /// <summary>
            /// 像素不一样
            /// </summary>
            PixelMismatch,
            /// <summary>
            /// 图片大小尺寸不一样
            /// </summary>
            SizeMismatch
        };

        /// <summary>
        /// 对比两张图片
        /// </summary>
        /// <param name="bmp1">The first bitmap image</param>
        /// <param name="bmp2">The second bitmap image</param>
        /// <returns>CompareResult</returns>
        public static CompareResult CompareTwoImages(Bitmap bmp1, Bitmap bmp2)
        {
            CompareResult cr = CompareResult.CompareOk;

            //Test to see if we have the same size of image
            if (bmp1.Size != bmp2.Size)
            {
                cr = CompareResult.SizeMismatch;
            }
            else
            {
                //Convert each image to a byte array
                System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
                byte[] btImage1 = new byte[1];
                btImage1 = (byte[])ic.ConvertTo(bmp1, btImage1.GetType());
                byte[] btImage2 = new byte[1];
                btImage2 = (byte[])ic.ConvertTo(bmp2, btImage2.GetType());

                //Compute a hash for each image
                SHA256Managed shaM = new SHA256Managed();
                byte[] hash1 = shaM.ComputeHash(btImage1);
                byte[] hash2 = shaM.ComputeHash(btImage2);

                //Compare the hash values
                for (int i = 0; i < hash1.Length && i < hash2.Length
                                  && cr == CompareResult.CompareOk; i++)
                {
                    if (hash1[i] != hash2[i])
                        cr = CompareResult.PixelMismatch;
                }
            }
            return cr;
        }

        #endregion

        #region 图片缩放

        public enum ScaleMode
        {
            /// <summary>
            /// 指定高宽缩放（可能变形）
            /// </summary>
            HW,
            /// <summary>
            /// 指定宽，高按比例
            /// </summary>
            W,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            H,
            /// <summary>
            /// 指定高宽裁减（不变形）
            /// </summary>
            Cut
        };

        /// <summary>
        /// Resizes the image by a percentage
        /// </summary>
        /// <param name="imgPhoto">The img photo.</param>
        /// <param name="Percent">The percentage</param>
        /// <returns></returns>
        public static Image ResizeImageByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                    imgPhoto.VerticalResolution);

            using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                grPhoto.DrawImage(imgPhoto,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }

            return bmPhoto;
        }

        /// <summary>
        /// 缩放、裁切图片
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static Image ResizeImageToAFixedSize(Image originalImage, int width, int height, ScaleMode mode)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case ScaleMode.HW:
                    break;
                case ScaleMode.W:
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ScaleMode.H:
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ScaleMode.Cut:
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画布
            using (Graphics g = System.Drawing.Graphics.FromImage(bitmap))
            {
                //设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);
            }

            return bitmap;
        }


        #endregion

        #region 创建缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ScaleMode mode)
        {
            //以jpg格式保存缩略图
            MakeThumbnail(originalImagePath, thumbnailPath, width, height, mode, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ScaleMode mode, System.Drawing.Imaging.ImageFormat format)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            System.Drawing.Image bitmap = ResizeImageToAFixedSize(originalImage, width, height, mode);

            try
            {
                bitmap.Save(thumbnailPath, format);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
            }
        }
        #endregion

        #region 图片水印

        /// <summary>
        /// 水印位置
        /// </summary>
        public enum WatermarkPosition
        {
            /// <summary>
            /// 左上角
            /// </summary>
            TopLeft,
            /// <summary>
            /// 右上角
            /// </summary>
            TopRight,
            /// <summary>
            /// 左下角
            /// </summary>
            BottomLeft,
            /// <summary>
            /// 右下角
            /// </summary>
            BottomRight,
            /// <summary>
            /// 中心
            /// </summary>
            Center
        }

        /// <summary>
        /// 字体
        /// </summary>
        private static int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

        /// <summary>
        /// 添加文字水印   默认字体：Verdana  12号大小  斜体  黑色    位置:右下角
        /// </summary>
        /// <param name="originalImage">Image</param>
        /// <param name="text">水印字</param>
        /// <returns></returns>
        public static Image WatermarkText(Image originalImage, string text)
        {
            return WatermarkText(originalImage, text, WatermarkPosition.BottomRight);
        }

        /// <summary>
        /// 添加文字水印   默认字体：Verdana  12号大小  斜体  黑色
        /// </summary>
        /// <param name="originalImage">Image</param>
        /// <param name="text">水印字</param>
        /// <param name="position">水印位置</param>
        /// <returns></returns>
        public static Image WatermarkText(Image originalImage, string text, WatermarkPosition position)
        {
            return WatermarkText(originalImage, text, position, new Font("Verdana", 12, FontStyle.Italic), new SolidBrush(Color.Black));
        }

        /// <summary>
        /// 添加文字水印
        /// </summary>
        /// <param name="originalImage">Image</param>
        /// <param name="text">水印字</param>
        /// <param name="position">水印位置</param>
        /// <param name="font">字体</param>
        /// <param name="brush">颜色</param>
        /// <returns></returns>
        public static Image WatermarkText(Image originalImage, string text, WatermarkPosition position, Font font, Brush brush)
        {
            using (Graphics g = Graphics.FromImage(originalImage))
            {
                Font tempfont = null;
                SizeF sizeF = new SizeF();
                sizeF = g.MeasureString(text, font);

                if (sizeF.Width >= originalImage.Width)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        tempfont = new Font(font.FontFamily, sizes[i], font.Style);

                        sizeF = g.MeasureString(text, tempfont);

                        if (sizeF.Width < originalImage.Width)
                            break;
                    }
                }
                else
                {
                    tempfont = font;
                }

                float x = (float)originalImage.Width * (float)0.01;
                float y = (float)originalImage.Height * (float)0.01;

                switch (position)
                {
                    case WatermarkPosition.TopLeft:
                        break;
                    case WatermarkPosition.TopRight:
                        x = (float)((float)originalImage.Width * (float)0.99 - sizeF.Width);
                        break;
                    case WatermarkPosition.BottomLeft:
                        y = (float)((float)originalImage.Height * (float)0.99 - sizeF.Height);
                        break;
                    case WatermarkPosition.BottomRight:
                        x = (float)((float)originalImage.Width * (float)0.99 - sizeF.Width);
                        y = (float)((float)originalImage.Height * (float)0.99 - sizeF.Height);
                        break;
                    case WatermarkPosition.Center:
                        x = (float)((float)originalImage.Width / 2 - sizeF.Width / 2);
                        y = (float)((float)originalImage.Height / 2 - sizeF.Height / 2);
                        break;
                    default:
                        break;
                }

                g.DrawString(text, tempfont, brush, x, y);

                return originalImage;
            }
        }

        /// <summary>
        /// 添加图片水印     位置默认右下角
        /// </summary>
        /// <param name="originalImage">Image</param>
        /// <param name="watermarkImage">水印Image</param>
        /// <returns></returns>
        public static Image WatermarkImage(Image originalImage, Image watermarkImage)
        {
            return WatermarkImage(originalImage, watermarkImage, WatermarkPosition.BottomRight);
        }

        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="originalImage">Image</param>
        /// <param name="watermarkImage">水印Image</param>
        /// <param name="position">位置</param>
        /// <returns></returns>
        public static Image WatermarkImage(Image originalImage, Image watermarkImage, WatermarkPosition position)
        {
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                             };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int x = 0;
            int y = 0;
            int watermarkWidth = 0;
            int watermarkHeight = 0;
            double bl = 1d;

            if ((originalImage.Width > watermarkImage.Width * 4) && (originalImage.Height > watermarkImage.Height * 4))
            {
                bl = 1;
            }
            else if ((originalImage.Width > watermarkImage.Width * 4) && (originalImage.Height < watermarkImage.Height * 4))
            {
                bl = Convert.ToDouble(originalImage.Height / 4) / Convert.ToDouble(watermarkImage.Height);

            }
            else if ((originalImage.Width < watermarkImage.Width * 4) && (originalImage.Height > watermarkImage.Height * 4))
            {
                bl = Convert.ToDouble(originalImage.Width / 4) / Convert.ToDouble(watermarkImage.Width);
            }
            else
            {
                if ((originalImage.Width * watermarkImage.Height) > (originalImage.Height * watermarkImage.Width))
                {
                    bl = Convert.ToDouble(originalImage.Height / 4) / Convert.ToDouble(watermarkImage.Height);

                }
                else
                {
                    bl = Convert.ToDouble(originalImage.Width / 4) / Convert.ToDouble(watermarkImage.Width);
                }
            }

            watermarkWidth = Convert.ToInt32(watermarkImage.Width * bl);
            watermarkHeight = Convert.ToInt32(watermarkImage.Height * bl);

            switch (position)
            {
                case WatermarkPosition.TopLeft:
                    x = 10;
                    y = 10;
                    break;
                case WatermarkPosition.TopRight:
                    y = 10;
                    x = originalImage.Width - watermarkWidth - 10;
                    break;
                case WatermarkPosition.BottomLeft:
                    x = 10;
                    y = originalImage.Height - watermarkHeight - 10;
                    break;
                case WatermarkPosition.BottomRight:
                    x = originalImage.Width - watermarkWidth - 10;
                    y = originalImage.Height - watermarkHeight - 10;
                    break;
                case WatermarkPosition.Center:
                    x = originalImage.Width / 2 - watermarkWidth / 2;
                    y = originalImage.Height / 2 - watermarkHeight / 2;
                    break;
                default:
                    break;
            }
            using (Graphics g = Graphics.FromImage(originalImage))
            {
                g.DrawImage(watermarkImage, new Rectangle(x, y, watermarkWidth, watermarkHeight), 0, 0, watermarkImage.Width, watermarkImage.Height, GraphicsUnit.Pixel, imageAttributes);
            }

            return originalImage;
        }

        #endregion

        #region 复制到剪切板
        /// <summary>
        /// 指定图像的位图数据复制到剪贴板
        /// </summary>
        /// <param name="img">被复制到剪贴板中的图像</param>
        public static void CopyToClipboard(Image img)
        {
            MemoryStream source = new MemoryStream();
            MemoryStream dest = new MemoryStream();

            img.Save(source, ImageFormat.Bmp);
            byte[] b = source.GetBuffer();
            dest.Write(b, 14, (int)source.Length - 14); // why the hell 14 ??
            source.Position = 0;

            IDataObject ido = new DataObject();
            ido.SetData(DataFormats.Dib, true, dest);
            Clipboard.SetDataObject(ido, true);
            dest.Close();
            source.Close();
        }
        #endregion

        #region 其他操作

        /// <summary>
        /// 保存JPG时用
        /// </summary>
        /// <param name="mimeType"> </param>
        /// <returns>得到指定mimeType的ImageCodecInfo </returns>
        public static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        /// <summary>
        /// 设置图片透明度
        /// </summary>
        /// <param name="imgPic"></param>
        /// <param name="imgOpac"></param>
        /// <returns></returns>
        public static Image SetImgOpacity(Image imgPic, float imgOpac)
        {
            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            ColorMatrix cmxPic = new ColorMatrix();
            cmxPic.Matrix33 = imgOpac;
            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic);//, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();
            return bmpPic;
        }

        /// <summary>
        /// 修改图片亮度
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Bitmap ChangeBrightness(Bitmap bmp, int value)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int nOffset = bmpData.Stride - bmp.Width * 3;
            int bmpWidth = bmp.Width * 3;
            int nVal;

            unsafe
            {
                byte* p = (byte*)(void*)bmpData.Scan0;

                for (int y = 0; y < bmp.Height; ++y)
                {
                    for (int x = 0; x < bmpWidth; ++x)
                    {
                        nVal = p[0] + value;
                        if (nVal < 0) nVal = 0;
                        else if (nVal > 255) nVal = 255;

                        p[0] = (byte)nVal;
                        ++p;
                    }
                    p += nOffset;
                }
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        #region 改变图片大小
        /// <summary>
        /// 改变图片大小
        /// </summary>
        /// <param name="img"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image ChangeImageSize(Image img, int width, int height)
        {
            Image bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }

            return bmp;
        }

        public static Image ChangeImageSize(Image img, int width, int height, bool preserveSize)
        {
            if (preserveSize)
            {
                width = Math.Min(img.Width, width);
                height = Math.Min(img.Height, height);
            }

            Image bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }

            return bmp;
        }

        public static Image ChangeImageSize(Image img, float percentage)
        {
            int width = (int)(percentage / 100 * img.Width);
            int height = (int)(percentage / 100 * img.Height);
            return ChangeImageSize(img, width, height, false);
        }
        #endregion

        /// <summary>
        /// 剪切图片
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Image CropImage(Image img, Rectangle rect)
        {
            Image bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        /// <summary>
        /// 剪裁 -- 用GDI+
        /// </summary>
        /// <param name="b">原始Bitmap</param>
        /// <param name="StartX">开始坐标X</param>
        /// <param name="StartY">开始坐标Y</param>
        /// <param name="iWidth">宽度</param>
        /// <param name="iHeight">高度</param>
        /// <returns>剪裁后的Bitmap</returns>
        public static Bitmap CropImage(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }

            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }

            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }

            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();

                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        public static Image AddCanvas(Image img, int size)
        {
            Image bmp = new Bitmap(img.Width + size * 2, img.Height + size * 2);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(size, size, img.Width, img.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            }

            return bmp;
        }

        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="img"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static Bitmap RotateImage(Image img, float theta)
        {
            Matrix matrix = new Matrix();
            matrix.Translate(img.Width / -2, img.Height / -2, MatrixOrder.Append);
            matrix.RotateAt(theta, new Point(0, 0), MatrixOrder.Append);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddPolygon(new Point[] { new Point(0, 0), new Point(img.Width, 0), new Point(0, img.Height) });
                gp.Transform(matrix);
                PointF[] pts = gp.PathPoints;

                Rectangle bbox = BoundingBox(img, matrix);
                Bitmap bmpDest = new Bitmap(bbox.Width, bbox.Height);

                using (Graphics gDest = Graphics.FromImage(bmpDest))
                {
                    Matrix mDest = new Matrix();
                    mDest.Translate(bmpDest.Width / 2, bmpDest.Height / 2, MatrixOrder.Append);
                    gDest.Transform = mDest;
                    gDest.CompositingQuality = CompositingQuality.HighQuality;
                    gDest.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gDest.DrawImage(img, pts);
                    return bmpDest;
                }
            }
        }

        private static Rectangle BoundingBox(Image img, Matrix matrix)
        {
            GraphicsUnit gu = new GraphicsUnit();
            Rectangle rImg = Rectangle.Round(img.GetBounds(ref gu));

            Point topLeft = new Point(rImg.Left, rImg.Top);
            Point topRight = new Point(rImg.Right, rImg.Top);
            Point bottomRight = new Point(rImg.Right, rImg.Bottom);
            Point bottomLeft = new Point(rImg.Left, rImg.Bottom);
            Point[] points = new Point[] { topLeft, topRight, bottomRight, bottomLeft };
            GraphicsPath gp = new GraphicsPath(points,
                new byte[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            gp.Transform(matrix);
            return Rectangle.Round(gp.GetBounds());
        }

        /// <summary>
        /// 获取所有屏幕的矩形宽度
        /// </summary>
        /// <returns></returns>
        public static Rectangle GetScreenBounds()
        {
            Point topLeft = new Point(0, 0);
            Point bottomRight = new Point(0, 0);
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.X < topLeft.X) topLeft.X = screen.Bounds.X;
                if (screen.Bounds.Y < topLeft.Y) topLeft.Y = screen.Bounds.Y;
                if ((screen.Bounds.X + screen.Bounds.Width) > bottomRight.X) bottomRight.X = screen.Bounds.X + screen.Bounds.Width;
                if ((screen.Bounds.Y + screen.Bounds.Height) > bottomRight.Y) bottomRight.Y = screen.Bounds.Y + screen.Bounds.Height;
            }
            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        public static Bitmap MakeBackgroundTransparent(IntPtr hWnd, Image image)
        {
            Region region;
            if (NativeMethods.GetWindowRegion(hWnd, out region))
            {
                Bitmap result = new Bitmap(image.Width, image.Height);

                using (Graphics g = Graphics.FromImage(result))
                {
                    if (!region.IsEmpty(g))
                    {
                        RectangleF bounds = region.GetBounds(g);
                        g.Clip = region;
                        g.DrawImage(image, new RectangleF(new PointF(0, 0), bounds.Size), bounds, GraphicsUnit.Pixel);

                        return result;
                    }
                }
            }

            return (Bitmap)image;
        }

        public static void SaveOneInchPic(Image image, Color transColor, float dpi, string path)
        {
            try
            {
                image = image.Clone() as Image;

                ((Bitmap)image).SetResolution(dpi, dpi);//设置图片打印的dpi
                ImageCodecInfo myImageCodecInfo;
                EncoderParameters myEncoderParameters;
                myImageCodecInfo = ImageHelper.GetCodecInfo("image/jpeg");
                myEncoderParameters = new EncoderParameters(2);
                myEncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);
                myEncoderParameters.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, (long)ColorDepth.Depth32Bit);

                //image.Save(path,ImageFormat.Jpeg,)
                image.Save(path, myImageCodecInfo, myEncoderParameters);
                image.Dispose();
            }
            catch (System.Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion

        #region BASE64编码转换
        public static Bitmap Base64StrToBmp(string ImgBase64Str)
        {
            byte[] ImgBuffer = Convert.FromBase64String(ImgBase64Str);
            MemoryStream MStream = new MemoryStream(ImgBuffer);
            Bitmap Bmp = new Bitmap(MStream);
            return Bmp;
        }

        public static string ImageToBase64Str(string ImgName)
        {
            Image Img = Image.FromFile(ImgName);
            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
            Img.Save(MStream, ImageFormat.Jpeg);
            byte[] ImgBuffer = MStream.GetBuffer();
            string ImgBase64Str = Convert.ToBase64String(ImgBuffer);
            //释放资源，让别的使用
            Img.Dispose();
            return ImgBase64Str;
        }
        public static string ImageToBase64Str(Image Img)
        {
            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
            Img.Save(MStream, ImageFormat.Jpeg);
            byte[] ImgBuffer = MStream.GetBuffer();
            string ImgBase64Str = Convert.ToBase64String(ImgBuffer);
            return ImgBase64Str;
        }
        #endregion

        #region 色彩处理
        /// <summary>  
        /// 设置图形颜色  边缘的色彩更换成新的颜色  
        /// </summary>  
        /// <param name="p_Image">图片</param>  
        /// <param name="p_OldColor">老的边缘色彩</param>  
        /// <param name="p_NewColor">新的边缘色彩</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns>清理后的图形</returns>  
        public static Image SetImageColorBrim(Image p_Image, Color p_OldColor, Color p_NewColor, int p_Float)
        {
            int _Width = p_Image.Width;
            int _Height = p_Image.Height;

            Bitmap _NewBmp = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb);
            Graphics _Graphics = Graphics.FromImage(_NewBmp);
            _Graphics.DrawImage(p_Image, new Rectangle(0, 0, _Width, _Height));
            _Graphics.Dispose();

            BitmapData _Data = _NewBmp.LockBits(new Rectangle(0, 0, _Width, _Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _Data.PixelFormat = PixelFormat.Format32bppArgb;
            int _ByteSize = _Data.Stride * _Height;
            byte[] _DataBytes = new byte[_ByteSize];
            Marshal.Copy(_Data.Scan0, _DataBytes, 0, _ByteSize);

            int _Index = 0;
            #region 列
            for (int z = 0; z != _Height; z++)
            {
                _Index = z * _Data.Stride;
                for (int i = 0; i != _Width; i++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);

                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                        _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                        _DataBytes[_Index] = (byte)p_NewColor.B;
                        _Index += 4;
                    }
                    else
                    {
                        break;
                    }
                }
                _Index = (z + 1) * _Data.Stride;
                for (int i = 0; i != _Width; i++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index - 1], _DataBytes[_Index - 2], _DataBytes[_Index - 3], _DataBytes[_Index - 4]);

                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index - 1] = (byte)p_NewColor.A;
                        _DataBytes[_Index - 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index - 3] = (byte)p_NewColor.G;
                        _DataBytes[_Index - 4] = (byte)p_NewColor.B;
                        _Index -= 4;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion

            #region 行

            for (int i = 0; i != _Width; i++)
            {
                _Index = i * 4;
                for (int z = 0; z != _Height; z++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);
                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                        _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                        _DataBytes[_Index] = (byte)p_NewColor.B;
                        _Index += _Data.Stride;
                    }
                    else
                    {
                        break;
                    }
                }
                _Index = (i * 4) + ((_Height - 1) * _Data.Stride);
                for (int z = 0; z != _Height; z++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);
                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                        _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                        _DataBytes[_Index] = (byte)p_NewColor.B;
                        _Index -= _Data.Stride;
                    }
                    else
                    {
                        break;
                    }
                }
            }


            #endregion
            Marshal.Copy(_DataBytes, 0, _Data.Scan0, _ByteSize);
            _NewBmp.UnlockBits(_Data);
            return _NewBmp;
        }
        /// <summary>  
        /// 设置图形颜色  所有的色彩更换成新的颜色  
        /// </summary>  
        /// <param name="p_Image">图片</param>  
        /// <param name="p_OdlColor">老的颜色</param>  
        /// <param name="p_NewColor">新的颜色</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns>清理后的图形</returns>  
        public static Image SetImageColorAll(Image p_Image, Color p_OdlColor, Color p_NewColor, int p_Float)
        {
            int _Width = p_Image.Width;
            int _Height = p_Image.Height;

            Bitmap _NewBmp = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb);
            Graphics _Graphics = Graphics.FromImage(_NewBmp);
            _Graphics.DrawImage(p_Image, new Rectangle(0, 0, _Width, _Height));
            _Graphics.Dispose();

            BitmapData _Data = _NewBmp.LockBits(new Rectangle(0, 0, _Width, _Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _Data.PixelFormat = PixelFormat.Format32bppArgb;
            int _ByteSize = _Data.Stride * _Height;
            byte[] _DataBytes = new byte[_ByteSize];
            Marshal.Copy(_Data.Scan0, _DataBytes, 0, _ByteSize);

            int _WhileCount = _Width * _Height;
            int _Index = 0;
            for (int i = 0; i != _WhileCount; i++)
            {
                Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);
                if (ScanColor(_Color, p_OdlColor, p_Float))
                {
                    _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                    _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                    _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                    _DataBytes[_Index] = (byte)p_NewColor.B;
                }
                _Index += 4;
            }
            Marshal.Copy(_DataBytes, 0, _Data.Scan0, _ByteSize);
            _NewBmp.UnlockBits(_Data);
            return _NewBmp;
        }
        /// <summary>  
        /// 设置图形颜色  坐标的颜色更换成新的色彩 （漏斗）  
        /// </summary>  
        /// <param name="p_Image">新图形</param>  
        /// <param name="p_Point">位置</param>  
        /// <param name="p_NewColor">新的色彩</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns>清理后的图形</returns>  
        public static Image SetImageColorPoint(Image p_Image, Point p_Point, Color p_NewColor, int p_Float)
        {
            int _Width = p_Image.Width;
            int _Height = p_Image.Height;

            if (p_Point.X > _Width - 1) return p_Image;
            if (p_Point.Y > _Height - 1) return p_Image;

            Bitmap _SS = (Bitmap)p_Image;
            Color _Scolor = _SS.GetPixel(p_Point.X, p_Point.Y);
            Bitmap _NewBmp = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb);
            Graphics _Graphics = Graphics.FromImage(_NewBmp);
            _Graphics.DrawImage(p_Image, new Rectangle(0, 0, _Width, _Height));
            _Graphics.Dispose();

            BitmapData _Data = _NewBmp.LockBits(new Rectangle(0, 0, _Width, _Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _Data.PixelFormat = PixelFormat.Format32bppArgb;
            int _ByteSize = _Data.Stride * _Height;
            byte[] _DataBytes = new byte[_ByteSize];
            Marshal.Copy(_Data.Scan0, _DataBytes, 0, _ByteSize);


            int _Index = (p_Point.Y * _Data.Stride) + (p_Point.X * 4);

            Color _OldColor = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);

            if (_OldColor.Equals(p_NewColor)) return p_Image;
            Stack<Point> _ColorStack = new Stack<Point>(1000);
            _ColorStack.Push(p_Point);

            _DataBytes[_Index + 3] = (byte)p_NewColor.A;
            _DataBytes[_Index + 2] = (byte)p_NewColor.R;
            _DataBytes[_Index + 1] = (byte)p_NewColor.G;
            _DataBytes[_Index] = (byte)p_NewColor.B;

            do
            {
                Point _NewPoint = (Point)_ColorStack.Pop();

                if (_NewPoint.X > 0) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X - 1, _NewPoint.Y, _OldColor, p_NewColor, p_Float);
                if (_NewPoint.Y > 0) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X, _NewPoint.Y - 1, _OldColor, p_NewColor, p_Float);

                if (_NewPoint.X < _Width - 1) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X + 1, _NewPoint.Y, _OldColor, p_NewColor, p_Float);
                if (_NewPoint.Y < _Height - 1) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X, _NewPoint.Y + 1, _OldColor, p_NewColor, p_Float);

            }
            while (_ColorStack.Count > 0);

            Marshal.Copy(_DataBytes, 0, _Data.Scan0, _ByteSize);
            _NewBmp.UnlockBits(_Data);
            return _NewBmp;
        }
        /// <summary>  
        /// SetImageColorPoint 循环调用 检查新的坐标是否符合条件 符合条件会写入栈p_ColorStack 并更改颜色  
        /// </summary>  
        /// <param name="p_DataBytes">数据区</param>  
        /// <param name="p_Stride">行扫描字节数</param>  
        /// <param name="p_ColorStack">需要检查的位置栈</param>  
        /// <param name="p_X">位置X</param>  
        /// <param name="p_Y">位置Y</param>  
        /// <param name="p_OldColor">老色彩</param>  
        /// <param name="p_NewColor">新色彩</param>  
        /// <param name="p_Float">溶差</param>  
        private static void SetImageColorPoint(byte[] p_DataBytes, int p_Stride, Stack<Point> p_ColorStack, int p_X, int p_Y, Color p_OldColor, Color p_NewColor, int p_Float)
        {

            int _Index = (p_Y * p_Stride) + (p_X * 4);
            Color _OldColor = Color.FromArgb(p_DataBytes[_Index + 3], p_DataBytes[_Index + 2], p_DataBytes[_Index + 1], p_DataBytes[_Index]);

            if (ScanColor(_OldColor, p_OldColor, p_Float))
            {
                p_ColorStack.Push(new Point(p_X, p_Y));

                p_DataBytes[_Index + 3] = (byte)p_NewColor.A;
                p_DataBytes[_Index + 2] = (byte)p_NewColor.R;
                p_DataBytes[_Index + 1] = (byte)p_NewColor.G;
                p_DataBytes[_Index] = (byte)p_NewColor.B;
            }
        }

        /// <summary>  
        /// 检查色彩(可以根据这个更改比较方式  
        /// </summary>  
        /// <param name="p_CurrentlyColor">当前色彩</param>  
        /// <param name="p_CompareColor">比较色彩</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns></returns>  
        private static bool ScanColor(Color p_CurrentlyColor, Color p_CompareColor, int p_Float)
        {
            int _R = p_CurrentlyColor.R;
            int _G = p_CurrentlyColor.G;
            int _B = p_CurrentlyColor.B;

            return (_R <= p_CompareColor.R + p_Float && _R >= p_CompareColor.R - p_Float) && (_G <= p_CompareColor.G + p_Float && _G >= p_CompareColor.G - p_Float) && (_B <= p_CompareColor.B + p_Float && _B >= p_CompareColor.B - p_Float);

        }
        #endregion

        #region 图像压缩
        /// <summary>
        /// 将Bitmap对象压缩为JPG图片类型
        /// </summary>
        /// </summary>
        /// <param name="bmp">源bitmap对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public static void CompressAsJPG(Bitmap bmp, string saveFilePath, int quality)
        {
            EncoderParameter p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality); ;
            EncoderParameters ps = new EncoderParameters(1);
            ps.Param[0] = p;
            bmp.Save(saveFilePath, GetCodecInfo("image/jpeg"), ps);
            bmp.Dispose();
        }

        /// <summary>
        /// 将inputStream中的对象压缩为JPG图片类型
        /// </summary>
        /// <param name="inputStream">源Stream对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public static void CompressAsJPG(Stream inputStream, string saveFilePath, int quality)
        {
            CompressAsJPG(new Bitmap(inputStream), saveFilePath, quality);
        }
        #endregion

        #region 图片转换
        /// <summary>
        /// 转换成一个字节数组中指定的 <see cref="System.Drawing.Image"/> into an array of bytes
        /// </summary>
        /// <param name="image"><see cref="System.Drawing.Image"/></param>
        /// <returns>字节数组</returns>
        public static byte[] ImageToBytes(System.Drawing.Image image)
        {
            byte[] bytes = null;
            if (image != null)
            {
                lock (image)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    using (ms)
                    {
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        bytes = ms.GetBuffer();
                    }
                }
            }
            return bytes;
        }

        /// <summary>
        /// 转换成一个字节数组 <see cref="System.Drawing.Image"/>
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>由此产生的<see cref="System.Drawing.Image"/></returns>
        public static System.Drawing.Image ImageFromBytes(byte[] bytes)
        {
            System.Drawing.Image image = null;
            try
            {
                if (bytes != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes, false);
                    using (ms)
                    {
                        image = ImageFromStream(ms);
                    }
                }
            }
            catch
            {
            }
            return image;
        }

        /// <summary>
        /// 转换成一个URL（文件系统或网络）<see cref="System.Drawing.Image"/>
        /// </summary>
        /// <param name="url">图像的URL路径</param>
        /// <returns>由此产生的 <see cref="System.Drawing.Image"/></returns>
        public static System.Drawing.Image ImageFromUrl(string url)
        {
            System.Drawing.Image image = null;
            try
            {
                if (!String.IsNullOrEmpty(url))
                {
                    Uri uri = new Uri(url);
                    if (uri.IsFile)
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(uri.LocalPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                        using (fs)
                        {
                            image = ImageFromStream(fs);
                        }
                    }
                    else
                    {
                        System.Net.WebClient wc = new System.Net.WebClient();   // TODO: consider changing this to WebClientEx
                        using (wc)
                        {
                            byte[] bytes = wc.DownloadData(uri);
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes, false);
                            using (ms)
                            {
                                image = ImageFromStream(ms);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return image;
        }

        private static System.Drawing.Image ImageFromStream(System.IO.Stream stream)
        {
            System.Drawing.Image image = null;
            try
            {
                stream.Position = 0;
                System.Drawing.Image tempImage = Image.FromStream(stream);
                // 但不要关闭流，首先要创建一个副本
                using (tempImage)
                {
                    image = new Bitmap(tempImage);
                }
            }
            catch
            {
                // 该文件可能是由Image类可用。ico文件，所以尝试，以防万一
                try
                {
                    stream.Position = 0;
                    Icon icon = new Icon(stream);
                    if (icon != null) image = icon.ToBitmap();
                }
                catch
                {
                    //放弃 - 我们无法打开此文件类型
                }
            }

            return image;
        }
        #endregion

    }
}
