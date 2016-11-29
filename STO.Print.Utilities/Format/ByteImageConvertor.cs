using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace STO.Print.Utilities
{
    /// <summary>
    ///这个类提供了实用方法的字节数组和图像之间的转换。
    /// </summary>
    public sealed class ByteImageConvertor
    {
        private ByteImageConvertor()
        {
        }

        /// <summary>
        /// 的PO的字节数组转换VO的形象。
        /// </summary>
        /// <param name="bytes">在PO字节数组。</param>
        /// <returns>图像对象。</returns>
        public static Image ByteToImage(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            Image image = null;
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                image = Image.FromStream(stream);
            }
            return image;
        }

        /// <summary>
        /// 宝的字节转换的VO的形象成员。
        /// </summary>
        /// <param name="image">在VO的Image对象。</param>
        /// <returns>字节数组。</returns>
        public static byte[] ImageToByte(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Jpeg);
                bytes = stream.ToArray();
            }
            return bytes;
        }
    }
}