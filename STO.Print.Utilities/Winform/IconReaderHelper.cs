using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 提供静态方法来读取这两个文件夹和文件的系统图标。
    /// </summary>
    /// <example>
    /// <code>IconReaderHelper.GetFileIcon("c:\\general.xls");</code>
    /// </example>
    public class IconReaderHelper
    {
        /// <summary>
        /// 选项来指定图标的大小，返回。
        /// </summary>
        public enum IconSize
        {
            /// <summary>
            /// 指定大图标 - 32像素32像素。
            /// </summary>
            Large = 0,
            /// <summary>
            /// 指定小图标 - 16个像素，16像素。
            /// </summary>
            Small = 1
        }

        /// <summary>
        /// 选项来指定文件夹是否应该在开放或封闭的状态。
        /// </summary>
        public enum FolderType
        {
            /// <summary>
            /// 指定打开的文件夹。
            /// </summary>
            Open = 0,
            /// <summary>
            /// 指定关闭的文件夹。
            /// </summary>
            Closed = 1
        }

        /// <summary>
        /// 返回一个给定的文件图标 - 由name参数表示。
        /// </summary>
        /// <param name="name">文件的路径名。</param>
        /// <param name="size">或大或小</param>
        /// <param name="linkOverlay">是否包括链接图标</param>
        /// <returns>System.Drawing.Icon</returns>
        public static Icon GetFileIcon(string name, IconSize size, bool linkOverlay)
        {
            uint flags = Shell32.SHGFI_ICON | Shell32.SHGFI_USEFILEATTRIBUTES;

            if (true == linkOverlay) flags += Shell32.SHGFI_LINKOVERLAY;

            if (IconSize.Small == size)
            {
                flags += Shell32.SHGFI_SMALLICON;
            }
            else
            {
                flags += Shell32.SHGFI_LARGEICON;
            }

            Shell32.SHFILEINFO shfi = new Shell32.SHFILEINFO();
            Shell32.SHGetFileInfo(name, Shell32.FILE_ATTRIBUTE_NORMAL, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            // 复制（克隆）返回图标到一个新的对象，从而使我们能够清理正确
            Icon icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();
            User32.DestroyIcon(shfi.hIcon); // Cleanup
            return icon;
        }

        /// <summary>
        /// 用于访问系统文件夹图标。
        /// </summary>
        /// <param name="size">指定或大或小的图标。</param>
        /// <param name="folderType">指定打开或关闭FolderType。</param>
        /// <returns>System.Drawing.Icon</returns>
        public static Icon GetFolderIcon(IconSize size, FolderType folderType)
        {
            // 需要添加大小检查，虽然目前产生错误！
            uint flags = Shell32.SHGFI_ICON;// | Shell32.SHGFI_USEFILEATTRIBUTES;

            if (FolderType.Open == folderType)
            {
                flags += Shell32.SHGFI_OPENICON;
            }

            if (IconSize.Small == size)
            {
                flags += Shell32.SHGFI_SMALLICON;
            }
            else
            {
                flags += Shell32.SHGFI_LARGEICON;
            }

            Shell32.SHFILEINFO shfi = new Shell32.SHFILEINFO();
            Shell32.SHGetFileInfo(null, Shell32.FILE_ATTRIBUTE_DIRECTORY, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            Icon.FromHandle(shfi.hIcon); // 加载图标，从图标的句柄
            // 现在克隆的图标，因此，它可以成功地存储在一个ImageList
            Icon icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();

            User32.DestroyIcon(shfi.hIcon); // Cleanup
            return icon;
        }

        public static string GetDisplayName(string name, bool isDirectory)
        {
            uint flags = Shell32.SHGFI_TYPENAME | Shell32.SHGFI_USEFILEATTRIBUTES;
            Shell32.SHFILEINFO shfi = new Shell32.SHFILEINFO();
            uint fileType = isDirectory ? Shell32.FILE_ATTRIBUTE_DIRECTORY : Shell32.FILE_ATTRIBUTE_NORMAL;
            Shell32.SHGetFileInfo(name, fileType, ref shfi, (uint)Marshal.SizeOf(shfi), flags);
            return shfi.szTypeName;
        }


        #region 后缀名图标操作
        public static int GetIcon(ImageList images, string extension)
        {
            return GetIcon(images, extension, false);
        }

        public static int GetIcon(ImageList images, string extension, bool largeIcon)
        {
            for (int i = 0; i < images.Images.Count; i++)
            {
                if (images.Images.Keys[i] == extension)
                    return i;
            }

            images.Images.Add(extension, ExtractIconForExtension(extension, largeIcon));
            return images.Images.Count - 1;
        }

        public static Icon ExtractIconForExtension(string extension, bool large)
        {
            if (extension != null)
            {
                //let's just make up a file name with that extension
                string fictitiousFile = "0" + extension;
                //now get the icon for that file
                return GetAssociatedIcon(fictitiousFile, large);
            }
            else
            {
                throw new ArgumentException("Invalid file or extension.", "fileOrExtension");
            }
        }
        /// <summary>
        /// 获取后缀名的关联图标
        /// </summary>
        /// <param name="stubPath"></param>
        /// <param name="large"></param>
        /// <returns></returns>
        public static Icon GetAssociatedIcon(string stubPath, bool large)
        {
            Shell32.SHFILEINFO info = new Shell32.SHFILEINFO();
            int cbFileInfo = Marshal.SizeOf(info);
            uint flags;

            if (large)
                flags = Shell32.SHGFI_ICON | Shell32.SHGFI_LARGEICON | Shell32.SHGFI_USEFILEATTRIBUTES;
            else
                flags = Shell32.SHGFI_ICON | Shell32.SHGFI_SMALLICON | Shell32.SHGFI_USEFILEATTRIBUTES;


            Shell32.SHGetFileInfo(stubPath, 256, ref info, (uint)cbFileInfo, flags);
            return (Icon)Icon.FromHandle(info.hIcon);
        } 
        #endregion
    }


    // This code has been left largely untouched from that in the CRC example. The main changes have been moving
    // the icon reading code over to the IconReader type.
    /// <summary>
    /// Wraps necessary Shell32.dll structures and functions required to retrieve Icon Handles using SHGetFileInfo. Code
    /// courtesy of MSDN Cold Rooster Consulting case study.
    /// </summary> 
    internal class Shell32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHITEMID
        {
            public ushort cb;
            [MarshalAs(UnmanagedType.LPArray)]
            public byte[] abID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ITEMIDLIST
        {
            public SHITEMID mkid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszTitle;
            public uint ulFlags;
            public IntPtr lpfn;
            public int lParam;
            public IntPtr iImage;
        }

        // Browsing for directory.
        public const uint BIF_RETURNONLYFSDIRS = 0x0001;
        public const uint BIF_DONTGOBELOWDOMAIN = 0x0002;
        public const uint BIF_STATUSTEXT = 0x0004;
        public const uint BIF_RETURNFSANCESTORS = 0x0008;
        public const uint BIF_EDITBOX = 0x0010;
        public const uint BIF_VALIDATE = 0x0020;
        public const uint BIF_NEWDIALOGSTYLE = 0x0040;
        public const uint BIF_USENEWUI = (BIF_NEWDIALOGSTYLE | BIF_EDITBOX);
        public const uint BIF_BROWSEINCLUDEURLS = 0x0080;
        public const uint BIF_BROWSEFORCOMPUTER = 0x1000;
        public const uint BIF_BROWSEFORPRINTER = 0x2000;
        public const uint BIF_BROWSEINCLUDEFILES = 0x4000;
        public const uint BIF_SHAREABLE = 0x8000;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public const uint SHGFI_ICON = 0x000000100;     // get icon
        public const uint SHGFI_DISPLAYNAME = 0x000000200;     // get display name
        public const uint SHGFI_TYPENAME = 0x000000400;     // get type name
        public const uint SHGFI_ATTRIBUTES = 0x000000800;     // get attributes
        public const uint SHGFI_ICONLOCATION = 0x000001000;     // get icon location
        public const uint SHGFI_EXETYPE = 0x000002000;     // return exe type
        public const uint SHGFI_SYSICONINDEX = 0x000004000;     // get system icon index
        public const uint SHGFI_LINKOVERLAY = 0x000008000;     // put a link overlay on icon
        public const uint SHGFI_SELECTED = 0x000010000;     // show icon in selected state
        public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;     // get only specified attributes
        public const uint SHGFI_LARGEICON = 0x000000000;     // get large icon
        public const uint SHGFI_SMALLICON = 0x000000001;     // get small icon
        public const uint SHGFI_OPENICON = 0x000000002;     // get open icon
        public const uint SHGFI_SHELLICONSIZE = 0x000000004;     // get shell size icon
        public const uint SHGFI_PIDL = 0x000000008;     // pszPath is a pidl
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;     // use passed dwFileAttribute
        public const uint SHGFI_ADDOVERLAYS = 0x000000020;     // apply the appropriate overlays
        public const uint SHGFI_OVERLAYINDEX = 0x000000040;     // Get the index of the overlay

        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
    }

    /// <summary>
    /// Wraps necessary functions imported from User32.dll. Code courtesy of MSDN Cold Rooster Consulting example.
    /// </summary>
    internal class User32
    {
        /// <summary>
        /// Provides access to function required to delete handle. This method is used internally
        /// and is not required to be called separately.
        /// </summary>
        /// <param name="hIcon">Pointer to icon handle.</param>
        /// <returns>N/A</returns>
        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);
    }
}
