//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    public partial class Utilities
    {
        /// <summary>
        /// 上传文件的路径定义
        /// </summary>
        public static string UploadFiles = "UploadFiles";

        /// <summary>
        /// 是否显示提示信息
        /// </summary> 
        public static bool ShowInformation = true;

        /// <summary>
        /// 您确认要保存吗
        /// </summary>
        public static string LangSaveConfirm = " You confirm must preserve? ";

        /// <summary>
        /// 您确认要删除吗
        /// </summary>
        public static string LangDeleteConfirm = " You confirm must delete? ";

        /// <summary>
        /// 请仔细核对数据，确认输入的正确吗？
        /// </summary>
        public static string LangConfirm = " Please careful checkup data! The confirmation input is correct? ";

        /// <summary>
        /// 公司名称
        /// </summary>
        public static string CompanyName = string.Empty;
        /// <summary>
        /// 软件名称
        /// </summary>
        public static string SoftFullName = string.Empty;

        /// <summary>
        /// 版本号
        /// </summary>
        public static string Version = string.Empty;

        /// <summary>
        ///  IE下载地址
        /// </summary>
        public static string IEDownloadUrl = string.Empty;

        /// <summary>
        /// 设计者
        /// </summary>
        public static string Designed = string.Empty;

        /// <summary>
        /// 更新
        /// </summary>
        public static string Update = string.Empty;

        /// <summary>
        /// 默认页面
        /// </summary>
        public static string DefaultPage = @"~/Default.aspx";

        /// <summary>
        /// 内容未找到页面
        /// </summary>
        public static string NotFoundPage = @"~/Modules/Common/System/NotFound.aspx";

        /// <summary>
        /// 用户未登录页面
        /// </summary>
        public static string UserNotLogOn = @"Signin.aspx";

        /// <summary>
        /// 访问没有权限被拒绝页面
        /// </summary>
        public static string AccessDenyPage = @"~/Modules/Common/System/AccessDeny.aspx";

        /// <summary>
        /// 当前操作员不是系统管理员页面
        /// </summary>
        public static string UserIsNotAdminPage = @"~/Modules/Common/System/AccessDeny.aspx";

        /// <summary>
        /// 有效显示字符串定义
        /// </summary>
        public static string EnableState = "<font color=\"#CC0000\">√<font>";

        /// <summary>
        /// 无效显示字符串定义
        /// </summary>
        public static string DisableState = "<font color=\"#CC0000\">-<font>";

        /// <summary>
        /// 选择是简易管理模式，是否部门管理权限管理角色管理等页面很复杂？
        /// </summary>
        protected bool SimpleManagerMode = true;

        #region public static void GetConfiguration() 读取一些基本配置信息
        /// <summary>
        /// 读取一些基本配置信息
        /// </summary>
        public static void GetConfiguration()
        {
            // 获取一些显示信息
            Utilities.CompanyName = ConfigurationManager.AppSettings["CustomerCompanyName"];
            Utilities.SoftFullName = ConfigurationManager.AppSettings["SoftFullName"];
            Utilities.Version = ConfigurationManager.AppSettings["Version"];
            Utilities.IEDownloadUrl = ConfigurationManager.AppSettings["IEDownloadUrl"];
            Utilities.Designed = ConfigurationManager.AppSettings["Designed"];
            Utilities.Update = ConfigurationManager.AppSettings["Update"];
        }
        #endregion

        #region private string GetSession(string sessionName) 安全获取Session的值
        /// <summary>
        /// 安全获取Session的值
        /// </summary>
        /// <param name="sessionName">变量名</param>
        /// <returns>字符串</returns>
        private string GetSession(string sessionName)
        {
            string result = string.Empty;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[sessionName] != null)
            {
                result = HttpContext.Current.Session[sessionName].ToString();
            }
            return result;
        }
        #endregion

        #region protected string CheckCodeImage 登录验证码读取
        /// <summary>
        /// 登录验证码读取
        /// </summary>
        protected string CheckCodeImage
        {
            get
            {
                return this.GetSession("LogOnCheckCode");
            }
            set
            {
                HttpContext.Current.Session["LogOnCheckCode"] = value;
            }
        }
        #endregion

        public static string GetOpenId()
        {
            string openId = string.Empty;
            BaseUserInfo userInfo = GetUserInfo();
            if (userInfo != null)
            {
                openId = userInfo.OpenId;
            }
            return openId;
        }

        /// <summary>
        /// 获取客户端ip地址
        /// </summary>
        /// <param name="transparent">是否使用了代理</param>
        /// <returns>ip地址</returns>
        public static string GetIPAddress(bool transparent = false)
        {
            string result = string.Empty;

            if (System.Web.HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                if (transparent)
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    }
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    {
                        // 2015-12-07 吉日嘎拉 这里写错一样代码，会有异常发生，修正。
                        result = HttpContext.Current.Request.ServerVariables["HTTP_VIA"].ToString();
                    }
                    else
                    {
                        if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                        {
                            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                        }
                    }
                }

                // 2016-02-18 吉日嘎拉 优化程序的返回结果
                if (!string.IsNullOrEmpty(result) && result.Equals("::1"))
                {
                    result = "127.0.0.1";
                }
            }

            return result;
        }

        #region public static BaseUserInfo GetUserInfo() 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        public static BaseUserInfo GetUserInfo()
        {
            BaseUserInfo userInfo = null;
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session[SessionName] != null)
                {
                    userInfo = (BaseUserInfo)HttpContext.Current.Session[SessionName];
                }
                else
                {
                    userInfo = GetUserCookie();
                    // 从 Session 读取 当前操作员信息

                }
            }
            if (userInfo == null)
            {
                userInfo = new BaseUserInfo();
                //    result.Id = userIP;
                //    result.RealName = userIP;
                //    result.UserName = userIP;
                //    result.IPAddress = userIP;
            }
            if (userInfo != null)
            {
                userInfo.IPAddress = GetIPAddress();
            }
            return userInfo;
        }
        #endregion

        #region public DataTable DTPermission 当前操作员的权限数据，一个页面里只读取一次就可以了，不用反复读取权限，可以在 Session 里缓存起来
        /// <summary>
        /// 当前操作员的权限数据，一个页面里只读取一次就可以了，不用反复读取权限，可以在 Session 里缓存起来
        /// </summary>
        public DataTable DTPermission
        {
            get
            {
                return Utilities.GetFromSession("DTPermission") as DataTable;
            }
            set
            {
                Utilities.AddSession("DTPermission", value);
            }
        }
        #endregion

        #region public static void AddSession(string key, Object myObject) 添加Session
        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="myObject">值</param>
        public static void AddSession(string key, Object myObject)
        {
            HttpContext.Current.Session.Add(key, myObject);
        }
        #endregion

        #region public static Object GetFromSession(string key) 获取Session
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static Object GetFromSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
        #endregion

        //
        // 上传下载文件部分
        //

        #region public static string UpLoadFile(string categoryId, string objectId, System.Web.HttpPostedFile httpPostedFile, ref string loadDirectory, bool deleteFile) 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="paramCategoryId">分类代码</param>
        /// <param name="paramObjectID">实物代码</param>
        /// <param name="httpPostedFile">被上传的文件</param>
        /// <param name="paramLoadDirectory">上传目录</param>
        /// <param name="paramDeleteFile">是否删除原文件夹</param>
        /// <returns>上传的文件位置</returns>
        public static string UpLoadFile(string categoryId, string objectId, System.Web.HttpPostedFile httpPostedFile, ref string loadDirectory, bool deleteFile, string fileName = null)
        {
            // 服务器上的绝对路径
            string rootPath = HttpContext.Current.Server.MapPath("~/") + Utilities.UploadFiles + "\\";
            // 图片重新指定，这里主要是为了起备份的作用，按日期把新的照片备份好就可以了。
            if (loadDirectory.Length == 0)
            {
                // 当前日期
                // string dateTime = DateTime.Now.ToString(BaseSystemInfo.DateFormat).ToString();
                // loadDirectory = categoryId + "\\" + dateTime + "\\" + objectId;
                loadDirectory = categoryId + "\\" + objectId;
            }
            // 需要创建的目录，图片放在这里，为了保存历史纪录，使用了当前日期做为目录
            string makeDirectory = rootPath + loadDirectory;
            if (deleteFile)
            {
                // 删除原文件
                if (Directory.Exists(makeDirectory))
                {
                    // Directory.Delete(makeDirectory, true);
                }
            }
            Directory.CreateDirectory(makeDirectory);
            // 获得文件名
            string postedFileName = string.Empty;
            if (string.IsNullOrEmpty(fileName))
            {
                postedFileName = HttpContext.Current.Server.HtmlEncode(Path.GetFileName(httpPostedFile.FileName));
            }
            else
            {
                postedFileName = fileName;
            }
            // 图片重新指定，虚拟的路径
            // 这里还需要更新学生的最新照片
            string fileUrl = loadDirectory + "\\" + postedFileName;
            // 文件复制到相应的路径下
            string copyToFile = makeDirectory + "\\" + postedFileName;
            httpPostedFile.SaveAs(copyToFile);
            return fileUrl;
        }
        #endregion

        #region public static string UpLoadFile(string categoryId, string objectId, string loadDirectory, bool deleteFile) 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="categoryId">分类代码</param>
        /// <param name="objectId">实物代码</param>
        /// <param name="loadDirectory">上传目录</param>
        /// <param name="deleteFile">是否删除原文件夹</param>
        /// <returns>上传的文件位置</returns>
        public static string UpLoadFile(string categoryId, string objectId, string loadDirectory, bool deleteFile)
        {
            return UpLoadFile(categoryId, objectId, HttpContext.Current.Request.Files[0], ref loadDirectory, deleteFile);
        }
        #endregion

        #region public static string UpLoadFiles(string categoryId, string objectId, string upLoadDirectory) 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="paramCategoryId">分类代码</param>
        /// <param name="paramObjectID">实物代码</param>
        /// <param name="upLoadDirectory">上传的目录</param>
        /// <returns>上传目录</returns>
        public static string UpLoadFiles(string categoryId, string objectId, string upLoadDirectory)
        {
            // 上传文件的复制文件部分
            string upLoadFilePath = string.Empty;
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                if (HttpContext.Current.Request.Files[i].ContentLength > 0)
                {
                    // 获取文件名
                    string fileName = HttpContext.Current.Server.HtmlEncode(Path.GetFileName(HttpContext.Current.Request.Files[i].FileName));
                    upLoadFilePath = UpLoadFile(categoryId, objectId, HttpContext.Current.Request.Files[i], ref upLoadDirectory, false);
                }
            }
            return upLoadFilePath;
        }
        #endregion

        //
        // 表格选择记录功能部分 GridView
        //

        #region public static string[] GetSelecteIds(GridView gridView) 获得已选的表格行代码数组
        /// <summary>
        /// 获得已选的表格行代码数组
        /// </summary>
        /// <param name="gridView">表格</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(GridView gridView)
        {
            return GetSelecteIds(gridView, true);
        }
        #endregion

        #region public static string[] GetUnSelecteIds(GridView gridView) 获得未选的表格行代码数组
        /// <summary>
        /// 获得未选的表格行代码数组
        /// </summary>
        /// <param name="gridView">表格</param>
        /// <returns>代码数组</returns>
        public static string[] GetUnSelecteIds(GridView gridView)
        {
            return GetSelecteIds(gridView, false);
        }
        #endregion

        #region public static string[] GetSelecteIds(GridView gridView, bool paramChecked)
        /// <summary>
        /// 获得表格行代码数组
        /// </summary>
        /// <param name="gridView">表格</param>
        /// <param name="paramChecked">选中状态</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(GridView gridView, bool paramChecked)
        {
            return GetSelecteIds(gridView, paramChecked, "chkSelected");
        }
        #endregion

        #region public static string[] GetSelecteIds(GridView gridView, bool paramChecked, string paramControl) 获取表格行代码数组
        /// <summary>
        /// 获取表格行代码数组
        /// </summary>
        /// <param name="gridView">表格</param>
        /// <param name="paramChecked">选中状态</param>
        /// <param name="paramControl">控件名称</param>
        /// <returns></returns>
        public static string[] GetSelecteIds(GridView gridView, bool paramChecked, string paramControl)
        {
            return GetSelecteIds(gridView, paramChecked, paramControl, string.Empty);
        }
        #endregion

        #region public static string[] GetSelecteIds(GridView gridView, bool paramChecked, string paramControl, string key)
        /// <summary>
        /// 获得已选的表格行代码数组
        /// </summary>
        /// <param name="gridView">表格</param>
        /// <param name="paramChecked">选中状态</param>
        /// <param name="paramControl">控件名称</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(GridView gridView, bool paramChecked, string paramControl, string key)
        {
            string[] ids = new string[0];
            string idList = string.Empty;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                // 得到选中的ID
                if (gridView.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    TableCell tableCell = gridView.Rows[i].Cells[0];
                    CheckBox checkBox = (CheckBox)tableCell.FindControl(paramControl);
                    if (checkBox != null)
                    {
                        if (checkBox.Checked == paramChecked)
                        {
                            // 把选中的ID保存到字符串
                            string id = string.Empty;
                            if (string.IsNullOrEmpty(key))
                            {
                                id = gridView.DataKeys[gridView.Rows[i].RowIndex].Value.ToString();
                            }
                            else
                            {
                                id = gridView.DataKeys[gridView.Rows[i].RowIndex].Values[key].ToString();
                            }

                            if (id.Length > 0)
                            {
                                idList += id + ",";
                            }
                        }
                    }
                }
            }
            // 切分ID
            if (idList.Length > 1)
            {
                idList = idList.Substring(0, idList.Length - 1);
                ids = idList.Split(',').Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }
            return ids;
        }
        #endregion

        //
        // 表格选择记录功能部分 repeater
        //

        #region public static string[] GetSelecteIds(Repeater repeater) 获得已选的表格行代码数组
        /// <summary>
        /// 获得已选的表格行代码数组
        /// </summary>
        /// <param name="repeater">表格</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(Repeater repeater)
        {
            return GetSelecteIds(repeater, true);
        }
        #endregion

        #region public static string[] GetUnSelecteIds(Repeater repeater) 获得未选的表格行代码数组
        /// <summary>
        /// 获得未选的表格行代码数组
        /// </summary>
        /// <param name="repeater">表格</param>
        /// <returns>代码数组</returns>
        public static string[] GetUnSelecteIds(Repeater repeater)
        {
            return GetSelecteIds(repeater, false);
        }
        #endregion

        #region public static string[] GetSelecteIds(Repeater repeater, bool isChecked)
        /// <summary>
        /// 获得表格行代码数组
        /// </summary>
        /// <param name="repeater">表格</param>
        /// <param name="paramChecked">选中状态</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(Repeater repeater, bool isChecked)
        {
            return GetSelecteIds(repeater, isChecked, "chkSelected");
        }
        #endregion

        #region public static string[] GetSelecteIds(Repeater repeater, bool isChecked, string control) 获取表格行代码数组
        /// <summary>
        /// 获取表格行代码数组
        /// </summary>
        /// <param name="repeater">表格</param>
        /// <param name="paramChecked">选中状态</param>
        /// <param name="control">控件名称</param>
        /// <returns></returns>
        public static string[] GetSelecteIds(Repeater repeater, bool isChecked, string control)
        {
            return GetSelecteIds(repeater, isChecked, control, string.Empty);
        }
        #endregion

        #region public static string[] GetSelecteIds(Repeater repeater, bool isChecked, string control, string key)
        /// <summary>
        /// 获得已选的表格行代码数组
        /// </summary>
        /// <param name="repeater">表格</param>
        /// <param name="isChecked">选中状态</param>
        /// <param name="control">控件名称</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(Repeater repeater, bool isChecked, string control, string key)
        {
            string[] ids = new string[0];
            string idList = string.Empty;
            string id = string.Empty;
            for (int i = 0; i < repeater.Items.Count; i++)
            {
                // 得到选中的ID
                CheckBox checkBox = (CheckBox)repeater.Items[i].FindControl(control);
                if (checkBox.Checked == isChecked)
                {
                    // 把选中的ID保存到字符串
                    if (string.IsNullOrEmpty(key))
                    {
                        id = ((HiddenField)repeater.Items[i].FindControl(key)).Value;
                    }
                    else
                    {
                        id = ((HiddenField)repeater.Items[i].FindControl("hidId")).Value;
                    }
                    if (id.Length > 0)
                    {
                        idList += id + ",";
                    }
                }
            }
            // 切分ID
            if (idList.Length > 1)
            {
                idList = idList.Substring(0, idList.Length - 1);
                ids = idList.Split(',').Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }
            return ids;
        }
        #endregion

        //
        // 表格选择记录功能部分 DataGrid
        //

        #region public static string[] GetSelecteIds(DataGrid dataGrid) 获得已选的表格行代码数组
        /// <summary>
        /// 获得已选的表格行代码数组
        /// </summary>
        /// <param name="dataGrid">表格</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(DataGrid dataGrid)
        {
            return GetSelecteIds(dataGrid, true);
        }
        #endregion

        #region public static string[] GetUnSelecteIds(DataGrid dataGrid) 获得未选的表格行代码数组
        /// <summary>
        /// 获得未选的表格行代码数组
        /// </summary>
        /// <param name="dataGrid">表格</param>
        /// <returns>代码数组</returns>
        public static string[] GetUnSelecteIds(DataGrid dataGrid)
        {
            return GetSelecteIds(dataGrid, false);
        }
        #endregion

        #region public static string[] GetSelecteIds(DataGrid dataGrid, bool paramChecked)
        /// <summary>
        /// 获得表格行代码数组
        /// </summary>
        /// <param name="dataGrid">表格</param>
        /// <param name="paramChecked">选中状态</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(DataGrid dataGrid, bool paramChecked)
        {
            return GetSelecteIds(dataGrid, paramChecked, "chkSelected");
        }
        #endregion

        #region public static string[] GetSelecteIds(DataGrid dataGrid, bool paramChecked, string paramControl)
        /// <summary>
        /// 获得已选的表格行代码数组
        /// </summary>
        /// <param name="gridView">表格</param>
        /// <param name="paramChecked">选中状态</param>
        /// <param name="paramControl">控件名称</param>
        /// <returns>代码数组</returns>
        public static string[] GetSelecteIds(DataGrid dataGrid, bool paramChecked, string paramControl)
        {
            string[] paramIDs = new string[0];
            string IDs = string.Empty;
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                // 得到选中的ID
                TableCell myTableCell = dataGrid.Items[i].Cells[0];
                CheckBox myCheckBox = (CheckBox)myTableCell.FindControl(paramControl);
                if (myCheckBox != null)
                {
                    if (myCheckBox.Checked == paramChecked)
                    {
                        // 把选中的ID保存到字符串
                        string ID = dataGrid.DataKeys[dataGrid.Items[i].ItemIndex].ToString();
                        if (ID.Length > 0)
                        {
                            IDs += ID + ",";
                        }
                    }
                }
            }
            // 切分ID
            if (IDs.Length > 1)
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
                paramIDs = IDs.Split(',').Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }
            return paramIDs;
        }
        #endregion

        //
        // 获取图标地址
        //

        #region public static string GetFileIcon(string fileName) 获取图标地址
        /// <summary>
        /// 获取图标地址
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>图标地址</returns>
        public static string GetFileIcon(string fileName)
        {
            // 这里是默认的图标
            string imageUrl = "Themes/Default/Images/Download.gif";
            // 截取后缀名,GetExtension读出来的后缀带"."的
            string extension = System.IO.Path.GetExtension(fileName).ToLower().Substring(1);
            // 这里查找是否有指定的图标
            if (File.Exists(HttpContext.Current.Server.MapPath("~/") + "Themes/Default/Images/" + extension + ".png"))
            {
                // 获取图标地址
                imageUrl = "Themes/Default/Images/" + extension + ".png";
            }
            return imageUrl;
        }
        #endregion

        #region public static bool CheckLAN()
        /// <summary>
        /// 当前电脑是否在局域网络里
        /// </summary>
        /// <returns></returns>
        public static bool CheckLAN()
        {
            string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if ((ipAddress.Substring(0, 3) == "127") || (ipAddress.Substring(0, 3) == "192") || (ipAddress.Substring(0, 3) == "10."))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region public static void CloseWindow(bool refreshOpener = null)
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="refreshOpener">是否刷新父窗体</param>
        public static void CloseWindow(bool refreshOpener = false)
        {
            HttpContext.Current.Response.Write("<script language=\"JavaScript\">" + System.Environment.NewLine);
            if (refreshOpener)
            {
                // window.opener != null 这个错误的调用方法
                HttpContext.Current.Response.Write("if(window.opener && !window.opener.closed){" + System.Environment.NewLine);
                HttpContext.Current.Response.Write("window.opener.location.href=window.opener.location.href;" + System.Environment.NewLine);
                HttpContext.Current.Response.Write("}" + System.Environment.NewLine);
            }
            HttpContext.Current.Response.Write("window.opener=null;" + System.Environment.NewLine);
            HttpContext.Current.Response.Write("window.open('','_self');" + System.Environment.NewLine);
            HttpContext.Current.Response.Write("window.close();");
            HttpContext.Current.Response.Write("</script>" + System.Environment.NewLine);
        }
        #endregion

        #region public static string GetItemName(string itemsTableName,string itemValue) 获取选项名称
        /// <summary>
        /// 将选项值转换为名称
        /// </summary>
        /// <param name="itemsTableName"></param>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static string GetItemName(string itemsTableName, string itemValue)
        {
            if (string.IsNullOrEmpty(itemValue))
                return null;
            return new BaseItemDetailsManager(itemsTableName).GetProperty(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemValue, itemValue), BaseItemDetailsEntity.FieldItemName);
        }
        #endregion

        #region public static string GetSafeQueryString(HttpRequestBase request, string queryName)
        /// <summary>
        /// 获取 HTTP 查询字符串变量的集合
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public static string GetSafeQueryString(HttpRequestBase request, string queryName)
        {
            string result = request[queryName];
            if (string.IsNullOrEmpty(result)) return string.Empty;
            return result;
        }
        #endregion

        #region public static string GetResponse(string url)
        /// <summary>
        /// 获取一个网页
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns>字符串返回值</returns>
        public static string GetResponse(string url)
        {
            string result = null;
            // string url = "http://www.MengGuRen.com";
            WebResponse webResponse = null;
            StreamReader streamReader = null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "GET";
                webResponse = httpWebRequest.GetResponse();
                streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
                result = streamReader.ReadToEnd();
            }
            catch
            {
                // handle error
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (webResponse != null)
                {
                    webResponse.Close();
                }
            }
            return result;
        }
        #endregion
    }
}