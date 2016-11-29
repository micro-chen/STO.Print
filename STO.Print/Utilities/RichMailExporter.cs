//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace STO.Print.Utilities
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.Export;
    using DevExpress.XtraRichEdit.Services;

    /// <summary>
    /// 把RichEditControl里面的内容导出为HTML和嵌入图片资源的辅助函数
    /// </summary>
    public class RichMailExporter : DevExpress.Office.Services.IUriProvider{
        int _imageId;
        readonly RichEditControl _control;
        List<LinkedAttachementInfo> _attachments;

        public RichMailExporter(RichEditControl control)
        {
            Guard.ArgumentNotNull(control, "control");
            _control = control;
        }

        /// <summary>
        /// 导出内容和嵌入资源
        /// </summary>
        /// <param name="htmlBody">HTML内容</param>
        /// <param name="attach">附件资源</param>
        public virtual void Export(out string htmlBody, out List<LinkedAttachementInfo> attach,bool isGetText =false)
        {
            _attachments = new List<LinkedAttachementInfo>();
            _control.BeforeExport += OnBeforeExport;
            if (isGetText)
            {
                htmlBody = _control.Document.GetText(_control.Document.Range);
            }
            else
            {
                htmlBody = _control.Document.GetHtmlText(_control.Document.Range, this);
            }
            _control.BeforeExport -= OnBeforeExport;
            attach = _attachments;
        }

        void OnBeforeExport(object sender, BeforeExportEventArgs e)
        {
            HtmlDocumentExporterOptions options = e.Options as HtmlDocumentExporterOptions;
            if (options != null)
            {
                options.Encoding = Encoding.UTF8;
            }
        }

        public string CreateCssUri(string rootUri, string styleText, string relativeUri)
        {
            return string.Empty;
        }

        #region IUriProvider Members
        public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
        {
            string imageName = String.Format("image{0}", _imageId);
            _imageId++;
            OfficeImageFormat imageFormat = GetActualImageFormat(image.RawFormat);
            Stream stream = new MemoryStream(image.GetImageBytes(imageFormat));
            string mediaContentType = OfficeImage.GetContentType(imageFormat);
            LinkedAttachementInfo info = new LinkedAttachementInfo(stream, mediaContentType, imageName);
            _attachments.Add(info);
            return "cid:" + imageName;
        }

        private OfficeImageFormat GetActualImageFormat(OfficeImageFormat richEditImageFormat)
        {
            if (richEditImageFormat == OfficeImageFormat.Exif ||
                richEditImageFormat == OfficeImageFormat.MemoryBmp)
                return OfficeImageFormat.Png;
            return richEditImageFormat;
        }
        #endregion

    }
    /// <summary>
    /// 用来传递附件信息
    /// </summary>
    [Serializable]
    public class LinkedAttachementInfo
    {
        private Stream stream;
        private string mimeType;
        private string contentId;

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="stream">附件流内容</param>
        /// <param name="mimeType">附件类型</param>
        /// <param name="contentId">内容ID</param>
        public LinkedAttachementInfo(Stream stream, string mimeType, string contentId)
        {
            this.stream = stream;
            this.mimeType = mimeType;
            this.contentId = contentId;
        }

        /// <summary>
        /// 附件流内容
        /// </summary>
        public Stream Stream { get { return stream; } }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string MimeType { get { return mimeType; } }

        /// <summary>
        /// 内容ID
        /// </summary>
        public string ContentId { get { return contentId; } }
    }
}
