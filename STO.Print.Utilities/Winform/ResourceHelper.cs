using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 图片、光标、图标、位图等资源操作辅助类
    /// </summary>
    public class ResourceHelper
    {
        public static Cursor LoadCursor(Type assemblyType, string cursorName)
        {
            // 获取程序集包含位图资源
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            // 包含图像获取的资源流
            Stream iconStream = myAssembly.GetManifestResourceStream(cursorName);

            // 从流中加载的图标
            return new Cursor(iconStream);
        }

        public static Icon LoadIcon(Type assemblyType, string iconName)
        {
            // 获取程序集包含位图资源
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            // 包含图像获取的资源流
            Stream iconStream = myAssembly.GetManifestResourceStream(iconName);

            // 从流中加载的图标
            return new Icon(iconStream);
        }

        public static Icon LoadIcon(Type assemblyType, string iconName, Size iconSize)
        {
            // 负载所要求的整个图标（可能包括几个不同的图标大小）
            Icon rawIcon = LoadIcon(assemblyType, iconName);

            // 创建并返回一个新的图标，只包含所要求的大小
            return new Icon(rawIcon, iconSize);
        }

        public static Bitmap LoadBitmap(Type assemblyType, string imageName)
        {
            return LoadBitmap(assemblyType, imageName, false, new Point(0, 0));
        }

        public static Bitmap LoadBitmap(Type assemblyType, string imageName, Point transparentPixel)
        {
            return LoadBitmap(assemblyType, imageName, true, transparentPixel);
        }

        public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize)
        {
            return LoadBitmapStrip(assemblyType, imageName, imageSize, false, new Point(0, 0));
        }

        public static ImageList LoadBitmapStrip(Type assemblyType,
                                                string imageName,
                                                Size imageSize,
                                                Point transparentPixel)
        {
            return LoadBitmapStrip(assemblyType, imageName, imageSize, true, transparentPixel);
        }

        protected static Bitmap LoadBitmap(Type assemblyType,
                                           string imageName,
                                           bool makeTransparent,
                                           Point transparentPixel)
        {
            // 获取程序集包含位图资源
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            // 包含图像获取的资源流
            Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

            // 从流中加载位图
            Bitmap image = new Bitmap(imageStream);

            if (makeTransparent)
            {
                Color backColor = image.GetPixel(transparentPixel.X, transparentPixel.Y);

                // 使背景色透明位图
                image.MakeTransparent(backColor);
            }

            return image;
        }

        protected static ImageList LoadBitmapStrip(Type assemblyType,
                                                   string imageName,
                                                   Size imageSize,
                                                   bool makeTransparent,
                                                   Point transparentPixel)
        {
            //为点阵图带创建存储
            ImageList images = new ImageList();

            //定义图像的大小，我们提供
            images.ImageSize = imageSize;

            //获取程序集包含位图资源
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            //包含图像获取的资源流
            Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

            //从资源加载位图带
            Bitmap pics = new Bitmap(imageStream);

            if (makeTransparent)
            {
                Color backColor = pics.GetPixel(transparentPixel.X, transparentPixel.Y);

                // 使背景色透明位图
                pics.MakeTransparent(backColor);
            }

            //加载它们！
            images.Images.AddStrip(pics);

            return images;
        }
    }
}
