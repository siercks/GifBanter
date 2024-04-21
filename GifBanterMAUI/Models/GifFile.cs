using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GifBanterMAUI.Models
{
    public class GifFile
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string ByteBase64 { get; set; }
        public DateTime Date { get; set; }
    }
}
