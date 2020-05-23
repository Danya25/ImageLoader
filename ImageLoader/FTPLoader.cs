using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImageLoader
{
    public class JsonObject
    {
        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Server")]
        public string Server { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }
    }
    public class JsonModel
    {
        public List<JsonObject> FTP { get; set; }
    }

    public class FTP
    {
        public async Task LoadInFtp(string path,string ip, string fileName ,string pathToFile, string password="", string login="")
        {
            using(var client= new WebClient())
            {
                client.Credentials = new NetworkCredential(login, password);
                client.UploadFile($"ftp://{ip}/{path.Trim('/')}/{fileName}", WebRequestMethods.Ftp.UploadFile, pathToFile);
            }
        }
    }
}
