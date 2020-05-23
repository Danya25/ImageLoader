using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLoader
{
    public class Model
    {
        public string Field1 { get; set; }
        public string Title { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
    }
    public class FTPUrl
    {
        public string OriginalLink { get; set; }
        public string ConvertedFTPLink { get; set; }
    }
}