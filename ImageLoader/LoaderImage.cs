using AngleSharp.Html.Parser;
using MihaZupan;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageLoader
{
    public class LoaderImage
    {
        private string LoadUrl = "https://imgur.com/upload?beta";
        private string ClientId = "546c25a59c58ad7";
        private string linkForDownloadImgur = "https://api.imgur.com/3/image?client_id=546c25a59c58ad7";
        private string lingWithoutClientId = "https://api.imgur.com/3/image?client_id=";
        private int _position = -1;
        private int _positionPostImages = -1;
        // WebProxy[]
        public async Task<string> LoadImgOnImgur(string url, string clientId, IReadOnlyList<HttpToSocks5Proxy> proxies)
        {
            //System.Net.Sockets.SocketException polly
            HttpClient hc;
            if (proxies.Count != 0)
            {
                var proxyIndex = Interlocked.Increment(ref _position);
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxies[proxyIndex % proxies.Count],
                };
                hc = new HttpClient(handler: httpClientHandler);
            }
            else
            {
                hc = new HttpClient();
            }

            hc.DefaultRequestHeaders.Add("access-control-allow-credentials", "true");
            hc.DefaultRequestHeaders.Add("access-control-allow-origin", "https://imgur.com");

            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                { "image" , url },
                { "type", "URL" },
                { "name", "" }
            };

            var frm = new FormUrlEncodedContent(body);

            HttpResponseMessage res;
            if (clientId == string.Empty)
            {
                res = await hc.PostAsync(linkForDownloadImgur, frm);
            }
            else
            {
                res = await hc.PostAsync(lingWithoutClientId + clientId, frm);
            }

            var bodys = await res.Content.ReadAsStringAsync();
            var urlOnImg = JObject.Parse(bodys)["data"]["link"].ToString();
            //await Task.Delay(2000);
            return urlOnImg;
        }
        public async Task<string> LoadImgOnpostimages(string url, string userToken, IReadOnlyList<HttpToSocks5Proxy> proxies)
        {
            string token = "61aa06d6116f7331ad7b2ba9c7fb707ec9b182e8";
            string upload_session = GetUploadSession(32);
            string numFiles = "1";
            string gallery = "ZnC5v7R";
            DateTime forUnix = DateTime.Now;
            DateTimeOffset unixtime = forUnix;
            string ui = $"[\"{GetColorDepth()}\", {Screen.PrimaryScreen.Bounds.Width}, {Screen.PrimaryScreen.Bounds.Height}, \"true\", \"\", \"\",  \"{forUnix.ToString("dd.MM.yyyy")}, {forUnix.ToLongTimeString()}\"]";
            string optsize = "0";
            string expire = "0";
            var session_upload = unixtime.ToUnixTimeMilliseconds().ToString();
            HttpClient hc;
            if (proxies.Count != 0)
            {
                var proxyIndex = Interlocked.Increment(ref _positionPostImages);
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxies[proxyIndex % proxies.Count],
                };
                hc = new HttpClient(handler: httpClientHandler);
            }
            else
            {
                hc = new HttpClient();
            }
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                {"token", userToken == string.Empty ? token : userToken },
                {"upload_session", upload_session },
                {"url", url},
                {"numfiles", numFiles},
                {"galery", "" },
                {"ui", ui },
                {"optsize", optsize },
                {"expire", expire },
                {"session_upload", session_upload }
            };
            var frm = new FormUrlEncodedContent(body);
            var res = await hc.PostAsync("https://postimages.org/json/rr", frm);
            var bodys = await res.Content.ReadAsStringAsync();
            var mainUrl = JObject.Parse(bodys)["url"].ToString();
            var requestToHtml = await hc.GetAsync(mainUrl);
            requestToHtml.EnsureSuccessStatusCode();
            var responseHtml = await requestToHtml.Content.ReadAsStringAsync();
            var htmlParse = new HtmlParser();
            var doc = await htmlParse.ParseDocumentAsync(responseHtml);
            var directLink = doc.QuerySelector("input[id=code_direct]").GetAttribute("value");
            return directLink;
        }

        private string GetUploadSession(int e)
        {
            string book = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            string secretWord = string.Empty;
            for (int i = 0; i < e; i++)
            {
                int randomNum = rnd.Next(0, book.Length);
                secretWord += book[randomNum];
            }
            return secretWord;
        }

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        static int GetColorDepth()
        {
            var hdc = GetWindowDC(IntPtr.Zero);
            var bitspixel = GetDeviceCaps(hdc, 12);
            var planes = GetDeviceCaps(hdc, 14);

            return bitspixel * planes;
        }
    }
}
