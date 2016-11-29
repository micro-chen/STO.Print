using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Text;
    using System.Threading;

    internal sealed class ResourceFileRequestResourceHandler : CefResourceHandler
    {
        private static int _requestNo;

        private byte[] responseData;
        private Uri _uri;
        private int pos;
        private bool process = true;

        private readonly IDictionary<string, string> resources = new Dictionary<string, string>
            {
                { "1.html", "" },
                { "2.html", "" },
                { "3.html", "" },
                { "4.html", "" },
            };

        protected override bool ProcessRequest(CefRequest request, CefCallback callback)
        {
            _uri = new Uri(request.Url);
            //判断是否本地目录文件
            var filePath = Path.GetFullPath(System.Environment.CurrentDirectory + "\\Resources" + _uri.AbsolutePath);
            if (File.Exists(filePath))
            {
                responseData = ReadFileToBinary(filePath);
                callback.Continue();
                return true;
            }

            //判断是否资源文件
            var segments = _uri.Segments;
            var file = segments[segments.Length - 1];
            string resource;

            if (resources.TryGetValue(file, out resource) &&
                !String.IsNullOrEmpty(resource))
            {
                responseData = Encoding.UTF8.GetBytes(resource);
                callback.Continue();

                return true;
            }

            process = false;
            callback.Continue();
            return false;
        }

        protected override void GetResponseHeaders(CefResponse response, out long responseLength, out string redirectUrl)
        {
            if(process)
            {
                SetMimeType(_uri, ref response);
            }
            else
            {
                response.MimeType = "text/html";
            }
            response.Status = process ? 200 : 404;
            var headers = new NameValueCollection(StringComparer.InvariantCultureIgnoreCase);
            headers.Add("Cache-Control", "private");
            headers.Add("Access-Control-Allow-Origin", "*");
            response.SetHeaderMap(headers);
            responseLength = process ? responseData.LongLength : 0;
            redirectUrl = null;
        }

        protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        {
            if (bytesToRead == 0 || pos >= (responseData!=null ?responseData.Length:0))
            {
                bytesRead = 0;
                return false;
            }
            else
            {
                if (responseData != null) response.Write(responseData, pos, bytesToRead);
                pos += bytesToRead;
                bytesRead = bytesToRead;
                return true;
            }
        }

        protected override bool CanGetCookie(CefCookie cookie)
        {
            return false;
        }

        protected override bool CanSetCookie(CefCookie cookie)
        {
            return false;
        }

        protected override void Cancel()
        {
        }

        /// <summary>
        /// 根据URL文件件名，设置文件类型
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="response"></param>
        private void SetMimeType(Uri uri, ref CefResponse response)
        {
            switch (Path.GetExtension(uri.AbsolutePath))
            {
                case ".html":
                    response.MimeType = "text/html";
                    break;
                case ".js":
                    response.MimeType = "application/x-javascript";
                    break;
                case ".css":
                    response.MimeType = "text/css";
                    break;
                case ".json":
                    response.MimeType = "application/x-javascript";
                    break;
                case ".txt":
                    response.MimeType = "text/plain";
                    break;
                case ".xml":
                    response.MimeType = "text/xml";
                    break;
                case ".jpg":
                    response.MimeType = "image/jpeg";
                    break;
                case ".png":
                    response.MimeType = "image/png";
                    break;
                case ".gif":
                    response.MimeType = "image/gif";
                    break;
                case ".ico":
                    response.MimeType = "image/x-icon";
                    break;
                case ".bmp":
                    response.MimeType = "image/bmp";
                    break;
                case ".swf":
                    response.MimeType = "application/x-shockwave-flash";
                    break;
                case ".gz":
                    response.MimeType = "application/x-gzip";
                    break;
                case ".zip":
                    response.MimeType = "application/zip";
                    break;
                default:
                    response.MimeType = "text/html";
                    break;
            }
        }
        private static byte[] ReadFileToBinary(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] bFile = new byte[fs.Length];
            BinaryReader r = new BinaryReader(fs);
            bFile = r.ReadBytes((int)fs.Length);
            r.Close();
            r = null;
            fs.Close();
            return bFile;
        }
    }
}
