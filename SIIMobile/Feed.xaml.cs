using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIIMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Feed : ContentPage
    {
        List<Ad> news = new List<Ad>();

        public Feed()
        {
            InitializeComponent();
            //Back.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.back.png");
            //MainIcon.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.itpn.png");
            UserIcon.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.user.png");
            _ = GetNews();

        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            return System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

        async Task GetNews()
        {
            var query = await CrossCloudFirestore.Current
                .Instance
                .Collection("news")
                .GetAsync();
            foreach (IDocumentSnapshot doc in query.Documents)
            {
                news.Add(new Ad {
                    title = doc.Data["title"].ToString(),
                    date = doc.Data["date"].ToString(),
                    body = doc.Data["body"].ToString()
                });

            }
            Ads.ItemsSource = news;
        }

        async private void GoToProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        async private void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}