using Firebase.Database;
using Firebase.Database.Query;
using GifBanterMAUI.Models;
using GifBanterMAUI.Services;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace GifBanterMAUI
{
    public partial class MainPage : ContentPage
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://gifbantermaui-default-rtdb.firebaseio.com/");
        public ObservableCollection<GifPost> GifPosts { get; set; } = new ObservableCollection<GifPost>();
        
        public ObservableCollection<GifPost> LastFourGifPosts
        {
            get
            {
                return new ObservableCollection<GifPost>(GifPosts?.TakeLast(4));
            }
        }
        GifUpload uploadGif;
        public MainPage()
        {
            InitializeComponent();

            uploadGif = new GifUpload();

            BindingContext = this;
            var gifCollection = firebaseClient
                .Child("GifPost")
                .AsObservable<GifPost>()
                .Subscribe((item) =>
                {
                    //GifPosts.Clear()
                    if (item.Object != null)
                    {
                        GifPosts.Add(new GifPost
                        {
                            Title = item.Object.Title,
                            Date = item.Object.Date
                        });
                    }
                    //foreach (var item in d.Object)
                    //{
                    //    GifPosts.Add(item);
                    //}
                });
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    //StringBuilder sb = new StringBuilder();
        //    //if (!FileUp)
        //    //{

        //    //}
        //    firebaseClient.Child("GifPost").PostAsync(new GifPost
        //    {
        //        Title = PostEntry.Text,
        //        Date = DateTime.Now
        //    });
        //    PostEntry.Text = string.Empty;
        //    //SemanticScreenReader.Announce(CounterBtn.Text);
        //}
        private async void UploadGifClicked(object sender, EventArgs e)
        {
            var image = await uploadGif.OpenMediaPickerAsync();
            var gifFile = await uploadGif.Upload(image);
            Image_Uploaded.Source = ImageSource.FromStream(() 
                => uploadGif.ConvertByteArrayToStream(uploadGif.ConvertStringToByteBase64(gifFile.ByteBase64)));
            var gifCollection = firebaseClient
                .Child("GifPost")
                .PostAsync(new GifFile
                {
                    Title = gifFile.FileName,
                    ByteBase64 = gifFile.ByteBase64,
                    ContentType = gifFile.ContentType,
                    Date = DateTime.Now
                });
            //gifCollection.Add(gifFile);
            
            //GifPosts.Add(new GifPost
            //{
            //    Title = gifFile.FileName,
            //    Date = DateTime.Now
            //});
        }
    }

}
