using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 获取一个对象，它提供用于访问经常引用的目录的属性。
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class SpecialDirectories
    {
        #region Properties
        /// <summary>Gets a path name pointing to the application's data in the 
        /// \Documents and Setting\All Users\ApplicationData directory.</summary>
        /// <returns>String.</returns>
        /// <remarks>If a path does not exist, one is created in the following format: 
        /// Base Path\ProductName\ProductVersion
        /// if Base Path Contains "Microsoft", incase of Add-In
        /// retuns Base Path\{0}\{1}
        /// Data stored in this path is part of user profile that is enabled for roaming.
        /// A roaming user works on more than one computer in a network. The user 
        /// profile for a roaming user is kept on a server on the network and is 
        /// loaded onto a system when the user logs on. For a user profile to be 
        /// considered for roaming, the operating system must support roaming 
        /// profiles and it must be enabled.
        /// A typical base path is C:\Documents and Settings\All Users\Application Data.
        /// </remarks>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string AllUsersApplicationData
        {
            get
            {
                return GetDataPath(GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "All users' application data"));

            }
        }

        /// <summary>Gets a path name pointing to the CurrentApplicationData directory.</summary>
        /// <returns>String.</returns>
        /// <remarks>If a path does not exist, one is created in the following format: 
        /// Base Path\ProductName\ProductVersion
        /// if Base Path Contains "Microsoft", incase of Add-In
        /// retuns Base Path\{0}\{1}
        /// Data stored in this path is part of user profile that is enabled for roaming.
        /// A roaming user works on more than one computer in a network. The user 
        /// profile for a roaming user is kept on a server on the network and is 
        /// loaded onto a system when the user logs on. For a user profile to be 
        /// considered for roaming, the operating system must support roaming 
        /// profiles and it must be enabled.
        /// A typical base path is C:\Documents and Settings\username\Application Data.
        /// </remarks>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string CurrentUserApplicationData
        {
            get
            {
                return GetDataPath(GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Current user's application data"));
            }
        }


        /// <summary>Gets a path name pointing to the Desktop directory.</summary>
        /// <returns>String.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string Desktop
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Desktop");
            }
        }

        /// <summary>Gets a path name pointing to the MyDocuments directory.</summary>
        /// <returns>String.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string MyDocuments
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "My Documents");
            }
        }

        /// <summary>Gets a path name pointing to the My Music directory.</summary>
        /// <returns>String.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string MyMusic
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "My Music");
            }
        }

        /// <summary>Gets a path name pointing to the My Pictures directory.</summary>
        /// <returns>String.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string MyPictures
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "My Pictures");
            }
        }

        /// <summary>Gets a path pointing to the Program Files directory.</summary>
        /// <returns>String.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string ProgramFiles
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Program Files");
            }
        }

        /// <summary>Gets a path name pointing to the Programs directory.</summary>
        /// <returns>String.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string Programs
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "Programs");
            }
        }

        /// <summary>Gets a path name pointing to the Temp directory.</summary>
        /// <returns>String.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string Temp
        {
            get
            {
                return GetDirectoryPath(Path.GetTempPath(), "Temporary directory");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the directory path.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="directoryNameResID">The directory name res ID.</param>
        /// <returns></returns>
        private static string GetDirectoryPath(string directory, string directoryName)
        {
            if (String.IsNullOrEmpty(directory))
            {
                throw new DirectoryNotFoundException(
                    String.Format(CultureInfo.InvariantCulture, 
                    "The given file path ends with a directory separator character.",
                    directoryName));

            }
            string path = GetLongPath(RemoveEndingSeparator(Path.GetFullPath(directory)));
            return path;
        }

        /// <summary>
        /// Gets the long path.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        private static string GetLongPath(string fullPath)
        {
            string text1;
            try
            {
                if (IsRoot(fullPath))
                {
                    return fullPath;
                } 
                DirectoryInfo info1 = new DirectoryInfo(GetParentPathInternal(fullPath));
                if (File.Exists(fullPath))
                {
                    return info1.GetFiles(Path.GetFileName(fullPath))[0].FullName;
                } 
                if (Directory.Exists(fullPath))
                {
                    return info1.GetDirectories(Path.GetFileName(fullPath))[0].FullName;
                } 
                text1 = fullPath;
            } 
            catch (Exception ex)
            {
                if ((((!(ex is ArgumentException) && !(ex is ArgumentNullException)) && !(ex is PathTooLongException)) &&
                     (!(ex is NotSupportedException) && !(ex is DirectoryNotFoundException))) &&
                    (!(ex is SecurityException) && !(ex is UnauthorizedAccessException)))
                {
                    throw;
                } 
                text1 = fullPath;
            } 
            return text1;
        }

        /// <summary>Returns the parent path of the provided path.</summary>
        /// <returns>String.</returns>
        /// <param name="path">String. Path to be examined. Required. </param>
        private static string GetParentPathInternal(string path)
        {
            Path.GetFullPath(path);
            if (IsRoot(path))
            {
                throw new ArgumentException("path", String.Format(CultureInfo.InvariantCulture,
                                                                  "Could not get parent path since the given path is a root directory: '{0}'.",
                                                                  new string[] { path }));
            } // if
            return Path.GetDirectoryName(path.TrimEnd(new char[]
                                                          {
                                                              Path.DirectorySeparatorChar,
                                                              Path.AltDirectorySeparatorChar
                                                          }));
        }

        /// <summary>
        /// Determines whether the specified path is absolute.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// 	<c>true</c> if the specified path is absolute; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsRoot(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                return false;
            } // if
            path = path.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            return (String.Compare(path, Path.GetPathRoot(path), StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        /// Removes the ending separator.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The path.without any ending seperator</returns>
        private static string RemoveEndingSeparator(string path)
        {
            if (Path.IsPathRooted(path) && path.Equals(Path.GetPathRoot(path), StringComparison.OrdinalIgnoreCase))
            {
                return path;
            } // if
            return path.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
        }

        /// <summary>
        /// Gets the data path.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <returns></returns>
        private static string GetDataPath(string basePath)
        {
            string text1 = @"{0}\{1}\{2}";
            string text2 = @"\{0}\{1}";
            string text3 = Application.CompanyName;
            string text4 = Application.ProductName;
            string text5 = Application.ProductVersion;
            string text6;
            if (text3.Contains("Microsoft Corporation"))
            {
                text6 = basePath + text2;
            }
            else
            {
                text6 = StringFormat(text1, basePath, text4, text5);
                if (!Directory.Exists(text6))
                {
                    Directory.CreateDirectory(text5);
                }
              
            }
            return text6;
        }

        /// <summary>
        /// Replaces the format item in a specified String with the text equivalent
        /// of the value of a corresponding Object instance in a specified array. 
        /// </summary>
        /// <param name="text">A String containing zero or more format items.</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the String equivalent of the corresponding instances of Object in args.</returns>
        private static string StringFormat(string text, params object[] args)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            if (args == null)
            {
                return text;
            }

            return String.Format(CultureInfo.CurrentCulture, text, args);
        }
 

        #endregion
    }
}
