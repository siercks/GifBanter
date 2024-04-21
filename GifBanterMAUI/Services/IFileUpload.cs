using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GifBanterMAUI.Services
{
    public interface IFileUpload
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        //[Required(ErrorMessage = "Please select a file.")]
        Task<bool> UploadFile(IFormatProvider file);
        public string FileName { get; set; }
    }
}
