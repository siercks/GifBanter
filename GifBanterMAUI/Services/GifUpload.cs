using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using GifBanterMAUI.Models;

namespace GifBanterMAUI.Services
{
    public class GifUpload
    {
        public string? FileName { get; set; }
        FirebaseClient firebaseClient = new FirebaseClient("https://gifbanter-1f7b4-default-rtdb.firebaseio.com/");
        public async Task<FileResult> OpenMediaPickerAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a GIF."
                });
                
                if(result.ContentType == "image/gif")
                {
                    return result;
                    //return new FileResult
                    //{
                    //    FileName = result.FileName,
                    //    ContentType = result.ContentType,
                    //    ByteBase64 = Convert.ToBase64String(result.DataArray),
                    //    Date = DateTime.Now
                    //};
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Invalid File Type", "Please select a GIF file.", "If you insist...");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "OK");
                return null;
            }
        }
        public async Task<Stream> ConvertFileResultToStream(FileResult fileResult)
        {
            if(fileResult == null)
            {
                return null;
            }
            return await fileResult.OpenReadAsync();
        }
        public Stream ConvertByteArrayToStream(byte[] byteArray)
        {
            if(byteArray == null)
            {
                return null;
            }
            return new MemoryStream(byteArray);
        }
        public string ConvertByteBase64ToString(byte[] byteArray)
        {
            if(byteArray == null)
            {
                return null;
            }
            return Convert.ToBase64String(byteArray);
        }
        public byte[] ConvertStringToByteBase64(string textString)
        {
            if(textString == null)
            {
                return null;
            }
            return Convert.FromBase64String(textString);
        }
        public async Task<GifFile> Upload(FileResult fileResult)
        {
            byte[] byteArray;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var stream = await ConvertFileResultToStream(fileResult);
                    stream.CopyTo(memoryStream);
                    byteArray = memoryStream.ToArray();
                }
                return new GifFile
                {
                    FileName = fileResult.FileName,
                    ContentType = fileResult.ContentType,
                    ByteBase64 = ConvertByteBase64ToString(byteArray),
                    Date = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            
        }
    }
}
