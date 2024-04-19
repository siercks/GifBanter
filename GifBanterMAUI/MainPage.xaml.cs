using Firebase.Database;
using Firebase.Database.Query;
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
        public MainPage()
        {
            InitializeComponent();

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

        private void OnCounterClicked(object sender, EventArgs e)
        {
            //StringBuilder sb = new StringBuilder();
            //if (!FileUp)
            //{

            //}
            firebaseClient.Child("GifPost").PostAsync(new GifPost
            {
                Title = PostEntry.Text,
                Date = DateTime.Now
            });
            PostEntry.Text = string.Empty;
            //SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
