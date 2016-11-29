using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ClipboardProxy = System.Windows.Forms.Clipboard;


namespace STO.Print.Utilities
{
    /// <summary>提供用于操作剪贴板的方法.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class ClipboardHepler
    {
        #region Methods

        /// <summary>清除“剪贴板”。</summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void Clear()
        {
            ClipboardProxy.Clear();
        }

        /// <summary>指示剪贴板中是否包含音频数据。</summary>
        /// <returns>True如果音频数据存储在剪贴板中，否则为false。</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static bool ContainsAudio()
        {
            return ClipboardProxy.ContainsAudio();
        }

        /// <summary>指示剪贴板中是否包含在指定的自定义格式的数据。</summary>
        /// <returns>true，如果在指定的自定义格式的数据存储在剪贴板中，否则为false。</returns>
        /// <param name="format">字符串。自定义格式的名称进行检查。必需的。 </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static bool ContainsData(string format)
        {
            return ClipboardProxy.ContainsData(format);
        }

        /// <summary>返回一个布尔值，指示剪贴板中是否包含一个文件下拉列表。</summary>
        /// <returns>如果一个文件下拉列表中真正存储在剪贴板中，否则为false。</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static bool ContainsFileDropList()
        {
            return ClipboardProxy.ContainsFileDropList();
        }


        /// <summary>返回一个布尔值，指示是否存储在剪贴板上的图像被。</summary>
        /// <returns>真正的图像存储在剪贴板上，否则为false。</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static bool ContainsImage()
        {
            return ClipboardProxy.ContainsImage();
        }


        /// <summary>确定是否存在剪贴板上的文字。</summary>
        /// <returns>true，如果剪贴板中包含文本，否则为false。</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static bool ContainsText()
        {
            return ClipboardProxy.ContainsText();
        }

        /// <summary>确定是否存在剪贴板上的文字。</summary>
        /// <returns>true，如果剪贴板中包含文本，否则为false。</returns>
        /// <param name="format"><see cref="T:System.Windows.Forms.TextDataFormat"></see>.如果指定，确定什么样的文本格式检查。必需的。</param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static bool ContainsText(TextDataFormat format)
        {
            return ClipboardProxy.ContainsText(format);
        }


        /// <summary>从剪贴板中检索音频流。</summary>
        /// <returns><see cref="T:System.IO.Stream"></see></returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static Stream GetAudioStream()
        {
            return ClipboardProxy.GetAudioStream();
        }


        /// <summary>在自定义格式从剪贴板检索数据。</summary>
        /// <returns>对象。</returns>
        /// <param name="format">字符串。数据格式的名称。必需的。 </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static object GetData(string format)
        {
            return ClipboardProxy.GetData(format);
        }


        /// <summary>从剪贴板作为检索数据 <see cref="T:System.Windows.Forms.IDataObject"></see>.</summary>
        /// <returns><see cref="T:System.Windows.Forms.IDataObject"></see></returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static IDataObject GetDataObject()
        {
            return ClipboardProxy.GetDataObject();
        }


        /// <summary>检索代表从剪贴板中的文件名的字符串的集合。</summary>
        /// <returns><see cref="T:System.Collections.Specialized.StringCollection"></see></returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static StringCollection GetFileDropList()
        {
            return ClipboardProxy.GetFileDropList();
        }


        /// <summary>从剪贴板中检索图像。</summary>
        /// <returns><see cref="T:System.Drawing.Image"></see></returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static Image GetImage()
        {
            return ClipboardProxy.GetImage();
        }


        /// <summary>从剪贴板中检索文本。</summary>
        /// <returns>String.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static string GetText()
        {
            return ClipboardProxy.GetText();
        }

        /// <summary>从剪贴板中检索文本。</summary>
        /// <returns>String.</returns>
        /// <param name="format"><see cref="T:System.Windows.Forms.TextDataFormat"></see>. If specified, identifies what text format should be retrieved. Default is <see cref="F:System.Windows.Forms.TextDataFormat.CommaSeparatedValue"></see>. Required. </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static string GetText(TextDataFormat format)
        {
            return ClipboardProxy.GetText(format);
        }

        /// <summary>将音频数据写入到剪贴板。</summary>
        /// <param name="audioStream"><see cref="T:System.IO.Stream"></see> 音频数据被写入到剪贴板。必需的。 </param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void SetAudio(Stream audioStream)
        {
            ClipboardProxy.SetAudio(audioStream);
        }

        /// <summary>将音频数据写入到剪贴板。</summary>
        /// <param name="audioBytes">字节数组。音频数据被写入到剪贴板。必需的。 </param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void SetAudio(byte[] audioBytes)
        {
            ClipboardProxy.SetAudio(audioBytes);
        }



        /// <summary>将自定义格式的数据写入到剪贴板。</summary>
        /// <param name="data">对象。数据对象被写入到剪贴板。必需的。 </param>
        /// <param name="format">字符串。数据格式。必需的。</param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void SetData(string format, object data)
        {
            ClipboardProxy.SetData(format, data);
        }

        /// <summary>写入<see cref="T:System.Windows.Forms.DataObject"></see> 到剪贴板。</summary>
        /// <param name="data"><see cref="T:System.Windows.Forms.DataObject"></see>.数据对象被写入到剪贴板。必需的。 </param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static void SetDataObject(DataObject data)
        {
            ClipboardProxy.SetDataObject(data);
        }


        /// <summary>写一个代表文件路径到剪贴板的字符串的集合。</summary>
        /// <param name="filePaths"><see cref="T:System.Collections.Specialized.StringCollection"></see>.文件名列表。必需的。 </param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void SetFileDropList(StringCollection filePaths)
        {
            ClipboardProxy.SetFileDropList(filePaths);
        }

        /// <summary>写入剪贴板形象。</summary>
        /// <param name="image"><see cref="T:System.Drawing.Image"></see>.要写入的图像。必需的。 </param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void SetImage(Image image)
        {
            ClipboardProxy.SetImage(image);
        }

        /// <summary>写入到剪贴板中的文本。</summary>
        /// <param name="text">字符串。要写入的文字。必需的。</param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void SetText(string text)
        {
            ClipboardProxy.SetText(text);
        }


        /// <summary>将文本写入到剪贴板.</summary>
        /// <param name="format"><see cref="T:System.Windows.Forms.TextDataFormat"></see>. 格式为文字时使用。默认为 <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText"></see>. Required. </param>
        /// <param name="text">字符串。要写入的文字。必需的。</param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public static void SetText(string text, TextDataFormat format)
        {
            ClipboardProxy.SetText(text, format);
        }

        #endregion

        [ThreadStatic]
        static int SafeSetClipboardDataVersion;

        /// <summary>
        /// 线程安全的设置内容
        /// </summary>
        /// <param name="dataObject"></param>
        public static void SafeSetClipboard(object dataObject)
        {
            // 解决ExternalException错误。 （SD2-426）
            //虚拟PC内的最佳重复性。
            int version = unchecked(++SafeSetClipboardDataVersion);
            try
            {
                Clipboard.SetDataObject(dataObject, true);
            }
            catch (ExternalException)
            {
                Timer timer = new Timer();
                timer.Interval = 100;
                timer.Tick += delegate
                {
                    timer.Stop();
                    timer.Dispose();
                    if (SafeSetClipboardDataVersion == version)
                    {
                        try
                        {
                            Clipboard.SetDataObject(dataObject, true, 10, 50);
                        }
                        catch (ExternalException) { }
                    }
                };
                timer.Start();
            }
        }
    }
}
