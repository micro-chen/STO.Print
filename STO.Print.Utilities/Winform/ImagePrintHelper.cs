using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 说明的PrintHelper。
    /// </summary>
    public class ImagePrintHelper
    {
        private Image image;
        private PrintDocument printDocument = new PrintDocument();
        private PrintDialog printDialog = new PrintDialog();
        private CoolPrintPreviewDialog previewDialog = new CoolPrintPreviewDialog();

        #region 配置参数
        /// <summary>
        /// Align printouts centered on the page.
        /// </summary>
        public bool AllowPrintCenter = true;

        /// <summary>
        /// rotate the image if it fits the page better
        /// </summary>
        public bool AllowPrintRotate = true;
        /// <summary>
        /// scale the image to fit the page better
        /// </summary>
        public bool AllowPrintEnlarge = true;
        /// <summary>
        /// scale the image to fit the page better
        /// </summary>
        public bool AllowPrintShrink = true;
        #endregion

        public ImagePrintHelper(Image image)
            : this(image, "test.png")
        {
        }

        public ImagePrintHelper(Image image, string documentname) 
        {
            this.image = (Image)image.Clone();
            printDialog.UseEXDialog = true;
            printDocument.DocumentName = documentname;
            printDocument.PrintPage += GetImageForPrint;
            printDialog.Document = printDocument;

            previewDialog.Document = printDocument;
        }

        public PrinterSettings PrintWithDialog()
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
                return printDialog.PrinterSettings;
            }
            else
            {
                return null;
            }
        }

        public void PrintPreview()
        {
            if (previewDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void GetImageForPrint(object sender, PrintPageEventArgs e)
        {
            //PrintOptionsDialog pod = new PrintOptionsDialog();
            //pod.ShowDialog();

            ContentAlignment alignment = this.AllowPrintCenter ? ContentAlignment.MiddleCenter : ContentAlignment.TopLeft;

            RectangleF pageRect = e.PageSettings.PrintableArea;
            GraphicsUnit gu = GraphicsUnit.Pixel;
            RectangleF imageRect = image.GetBounds(ref gu);
            // rotate the image if it fits the page better
            if (this.AllowPrintRotate)
            {
                if ((pageRect.Width > pageRect.Height && imageRect.Width < imageRect.Height) ||
                   (pageRect.Width < pageRect.Height && imageRect.Width > imageRect.Height))
                {
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    imageRect = image.GetBounds(ref gu);
                    if (alignment.Equals(ContentAlignment.TopLeft)) alignment = ContentAlignment.TopRight;
                }
            }
            RectangleF printRect = new RectangleF(0, 0, imageRect.Width, imageRect.Height); ;
            // scale the image to fit the page better
            if (this.AllowPrintEnlarge || this.AllowPrintShrink)
            {
                SizeF resizedRect = ScaleHelper.GetScaledSize(imageRect.Size, pageRect.Size, false);
                if ((this.AllowPrintShrink && resizedRect.Width < printRect.Width) ||
                   this.AllowPrintEnlarge && resizedRect.Width > printRect.Width)
                {
                    printRect.Size = resizedRect;
                }
            }

            // align the image
            printRect = ScaleHelper.GetAlignedRectangle(printRect, new RectangleF(0, 0, pageRect.Width, pageRect.Height), alignment);
            e.Graphics.DrawImage(image, printRect, imageRect, GraphicsUnit.Pixel);
        }
    }

    /// <summary>
    /// Offers a few helper functions for scaling/aligning an element with another element
    /// </summary>
    public static class ScaleHelper
    {
        /// <summary>
        /// calculates the Size an element must be resized to, in order to fit another element, keeping aspect ratio
        /// </summary>
        /// <param name="currentSize">the size of the element to be resized</param>
        /// <param name="targetSize">the target size of the element</param>
        /// <param name="crop">in case the aspect ratio of currentSize and targetSize differs: shall the scaled size fit into targetSize (i.e. that one of its dimensions is smaller - false) or vice versa (true)</param>
        /// <returns>a new SizeF object indicating the width and height the element should be scaled to</returns>
        public static SizeF GetScaledSize(SizeF currentSize, SizeF targetSize, bool crop)
        {
            float wFactor = targetSize.Width / currentSize.Width;
            float hFactor = targetSize.Height / currentSize.Height;

            float factor = crop ? Math.Max(wFactor, hFactor) : Math.Min(wFactor, hFactor);
            //System.Diagnostics.Debug.WriteLine(currentSize.Width+"..."+targetSize.Width);
            //System.Diagnostics.Debug.WriteLine(wFactor+"..."+hFactor+">>>"+factor);
            return new SizeF(currentSize.Width * factor, currentSize.Height * factor);
        }

        /// <summary>
        /// calculates the position of an element depending on the desired alignment within a RectangleF
        /// </summary>
        /// <param name="currentRect">the bounds of the element to be aligned</param>
        /// <param name="targetRect">the rectangle reference for aligment of the element</param>
        /// <param name="alignment">the System.Drawing.ContentAlignment value indicating how the element is to be aligned should the width or height differ from targetSize</param>
        /// <returns>a new RectangleF object with Location aligned aligned to targetRect</returns>
        public static RectangleF GetAlignedRectangle(RectangleF currentRect, RectangleF targetRect, ContentAlignment alignment)
        {
            RectangleF newRect = new RectangleF(targetRect.Location, currentRect.Size);
            switch (alignment)
            {
                case ContentAlignment.TopCenter:
                    newRect.X = (targetRect.Width - currentRect.Width) / 2;
                    break;
                case ContentAlignment.TopRight:
                    newRect.X = (targetRect.Width - currentRect.Width);
                    break;
                case ContentAlignment.MiddleLeft:
                    newRect.Y = (targetRect.Height - currentRect.Height) / 2;
                    break;
                case ContentAlignment.MiddleCenter:
                    newRect.Y = (targetRect.Height - currentRect.Height) / 2;
                    newRect.X = (targetRect.Width - currentRect.Width) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    newRect.Y = (targetRect.Height - currentRect.Height) / 2;
                    newRect.X = (targetRect.Width - currentRect.Width);
                    break;
                case ContentAlignment.BottomLeft:
                    newRect.Y = (targetRect.Height - currentRect.Height);
                    break;
                case ContentAlignment.BottomCenter:
                    newRect.Y = (targetRect.Height - currentRect.Height);
                    newRect.X = (targetRect.Width - currentRect.Width) / 2;
                    break;
                case ContentAlignment.BottomRight:
                    newRect.Y = (targetRect.Height - currentRect.Height);
                    newRect.X = (targetRect.Width - currentRect.Width);
                    break;
            }
            return newRect;
        }

        /// <summary>
        /// calculates the Rectangle an element must be resized an positioned to, in ordder to fit another element, keeping aspect ratio
        /// </summary>
        /// <param name="currentRect">the rectangle of the element to be resized/repositioned</param>
        /// <param name="targetRect">the target size/position of the element</param>
        /// <param name="crop">in case the aspect ratio of currentSize and targetSize differs: shall the scaled size fit into targetSize (i.e. that one of its dimensions is smaller - false) or vice versa (true)</param>
        /// <param name="alignment">the System.Drawing.ContentAlignment value indicating how the element is to be aligned should the width or height differ from targetSize</param>
        /// <returns>a new RectangleF object indicating the width and height the element should be scaled to and the position that should be applied to it for proper alignment</returns>
        public static RectangleF GetScaledRectangle(RectangleF currentRect, RectangleF targetRect, bool crop, ContentAlignment alignment)
        {
            SizeF newSize = GetScaledSize(currentRect.Size, targetRect.Size, crop);
            RectangleF newRect = new RectangleF(new Point(0, 0), newSize);
            return GetAlignedRectangle(newRect, targetRect, alignment);
        }
    }
}
