using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace GifBanterMAUI.Services
{
    public class GifUpload : IFileUpload
    {
        public string? FileName { get; set; }
        FirebaseClient firebaseClient = new FirebaseClient("https://gifbanter-1f7b4-default-rtdb.firebaseio.com/");

        //public async Task<bool> UploadFile(IFormatProvider file)
        //{
        //    //string path ="";

        //    //try
        //    //{
        //    //    if (!file.Len)
        //    //    {
        //    //        var result = await firebaseClient
        //    //                        .Child(path)
        //    //                        .PutAsync(file); 
        //    //    }
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    throw new Exception("Upload failed. Try again?", e);
        //    //}
        //    throw new Exception();
        //}

        public Task<bool> UploadFile(IFormatProvider file)
        {
            throw new NotImplementedException();
        }
    }
}
