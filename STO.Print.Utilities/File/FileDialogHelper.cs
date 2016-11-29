using System.Drawing;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    ///<summary>
    ///打开、保存文件对话框操作辅助类
    ///</summary>
    public class FileDialogHelper
    {
        private static string ExcelFilter = "Excel(*.xls)|*.xls|All File(*.*)|*.*";
        private static string ImageFilter = "Image Files(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png)|(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png)|All File(*.*)|*.*";
        private static string HtmlFilter = "HTML files (*.html;*.htm)|*.html;*.htm|All files (*.*)|*.*";
        private static string AccessFilter = "Access(*.mdb)|*.mdb|All File(*.*)|*.*";
        private static string ZipFillter = "Zip(*.zip)|*.zip|All files (*.*)|*.*";
        private const string ConfigFilter = "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*";
        private static string TxtFilter = "(*.txt)|*.txt|All files (*.*)|*.*";

        ///<summary>
        ///Initializes a new instance of the <see cref="FileDialogHelper"/> class.
        ///</summary>
        private FileDialogHelper()
        {
        }

        #region Txt相关对话框
        /// <summary>
        /// 打开Txt对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenText()
        {
            return Open("文本文件选择", TxtFilter);
        }

        /// <summary>
        /// 保存Excel对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveText()
        {
            return SaveText(string.Empty);
        }

        /// <summary>
        /// 保存Excel对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveText(string filename)
        {
            return Save("保存文本文件", TxtFilter, filename);
        } 

        /// <summary>
        /// 保存Excel对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveText(string filename, string initialDirectory)
        {
            return Save("保存文本文件", TxtFilter, filename, initialDirectory);
        }

        #endregion

        #region Excel相关对话框
        /// <summary>
        /// 打开Excel对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenExcel()
        {
            return Open("Excel选择", ExcelFilter);
        }

        /// <summary>
        /// 保存Excel对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveExcel()
        {
            return SaveExcel(string.Empty);
        }

        /// <summary>
        /// 保存Excel对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveExcel(string filename)
        {
            return Save("保存Excel", ExcelFilter, filename);
        }

        /// <summary>
        /// 保存Excel对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveExcel(string filename, string initialDirectory)
        {
            return Save("保存Excel", ExcelFilter, filename, initialDirectory);
        } 

        #endregion

        #region HTML相关对话框

        /// <summary>
        /// 打开Html对话框
        /// </summary>
        /// <returns></returns>
        public static string OpenHtml()
        {
            return Open("Html选择", HtmlFilter);
        }

        /// <summary>
        /// 保存Html对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveHtml()
        {
            return SaveHtml(string.Empty);
        }

        /// <summary>
        /// 保存Html对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveHtml(string filename)
        {
            return Save("保存Html", HtmlFilter, filename);
        } 
        
        /// <summary>
        /// 保存Html对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveHtml(string filename, string initialDirectory)
        {
            return Save("保存Html", HtmlFilter, filename, initialDirectory);
        } 

        #endregion

        #region 压缩文件相关
        /// <summary>
        /// Opens the Zip.
        /// </summary>
        /// <returns></returns>
        public static string OpenZip()
        {
            return Open("压缩文件选择", ZipFillter);
        }

        /// <summary>
        /// Opens the Zip.
        /// </summary>
        /// <returns></returns>
        public static string OpenZip(string filename)
        {
            return Open("压缩文件选择", ZipFillter, filename);
        }

        /// <summary>
        /// Save the Zip
        /// </summary>
        /// <returns></returns>
        public static string SaveZip()
        {
            return SaveZip(string.Empty);
        }

        /// <summary>
        /// Save the Zip
        /// </summary>
        /// <returns></returns>
        public static string SaveZip(string filename)
        {
            return Save("压缩文件保存", ZipFillter, filename);
        } 

        /// <summary>
        /// Save the Zip
        /// </summary>
        /// <returns></returns>
        public static string SaveZip(string filename, string initialDirectory)
        {
            return Save("压缩文件保存", ZipFillter, filename, initialDirectory);
        } 

        #endregion

        #region 图片相关
        /// <summary>
        /// Opens the image.
        /// </summary>
        /// <returns></returns>
        public static string OpenImage()
        {
            return Open("图片选择", ImageFilter);
        }


        /// <summary>
        /// 保存图片对话框,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveImage()
        {
            return SaveImage(string.Empty);
        } 

        /// <summary>
        /// 保存图片对话框并设置默认文件名,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveImage(string filename)
        {
            return Save("保存图片", ImageFilter, filename);
        }

        /// <summary>
        /// 保存图片对话框并设置默认文件名,并返回保存全路径
        /// </summary>
        /// <returns></returns>
        public static string SaveImage(string filename, string initialDirectory)
        {
            return Save("保存图片", ImageFilter, filename, initialDirectory);
        }

        #endregion

        #region 数据库备份还原

        /// <summary>
        /// 保存数据库备份对话框
        /// </summary>
        /// <returns>数据库备份路径</returns>
        public static string SaveAccessDb()
        {
            return Save("数据库备份", AccessFilter);
        }

        public static string SaveBakDb()
        {
            return Save("数据库备份", "Access(*.bak)|*.bak");
        }


        public static string OpenBakDb(string file)
        {
            return Open("数据库还原", "Access(*.bak)|*.bak", file);
        }

        /// <summary>
        /// 数据库还原对话框
        /// </summary>
        /// <returns>数据库还原路径</returns>
        public static string OpenAccessDb()
        {
            return Open("数据库还原", AccessFilter);
        } 
        #endregion

        #region 配置文件
        /// <summary>
        /// 保存配置文件备份对话框
        /// </summary>
        /// <returns>配置文件备份路径</returns>
        public static string SaveConfig()
        {
            return Save("配置文件备份", ConfigFilter);
        }

        /// <summary>
        /// 配置文件还原对话框
        /// </summary>
        /// <returns>配置文件还原路径</returns>
        public static string OpenConfig()
        {
            return Open("配置文件还原", ConfigFilter);
        } 
        #endregion

        #region 通用函数

        public static string OpenDir()
        {
            return OpenDir(string.Empty);
        }

        public static string OpenDir(string selectedPath)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择路径";
            dialog.SelectedPath = selectedPath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Opens the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static string Open(string title, string filter, string filename)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.Title = title;
            dialog.RestoreDirectory = true;
            dialog.FileName = filename;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Opens the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static string Open(string title, string filter)
        {
            return Open(title, filter, string.Empty);
        }

        /// <summary>
        /// Saves the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static string Save(string title, string filter, string filename)
        {
            return Save(title, filter, filename, "");
        }

        /// <summary>
        /// Saves the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static string Save(string title, string filter, string filename, string initialDirectory)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.Title = title;
            dialog.FileName = filename;
            dialog.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(initialDirectory))
            {
                dialog.InitialDirectory = initialDirectory;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }

        /// <summary>
        /// Saves the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static string Save(string title, string filter)
        {
            return Save(title, filter, string.Empty);
        } 

        #endregion

        #region 获取颜色对话框的颜色
        public static Color PickColor()
        {
            Color result = SystemColors.Control;
            ColorDialog form = new ColorDialog();
            if (DialogResult.OK == form.ShowDialog())
            {
                result = form.Color;
            }
            return result;
        }

        public static Color PickColor(Color color)
        {
            Color result = SystemColors.Control;
            ColorDialog form = new ColorDialog();
            form.Color = color;
            if (DialogResult.OK == form.ShowDialog())
            {
                result = form.Color;
            }
            return result;
        } 
        #endregion
    }
}
