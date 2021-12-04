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
                Frame frame = new Frame();
                frame.BackgroundColor = Color.DarkBlue;
                frame.Margin = 15;
                frame.CornerRadius = 10;
                StackLayout stack1 = new StackLayout();
                StackLayout stack2 = new StackLayout();
                stack2.Orientation = StackOrientation.Horizontal;
                Label title = new Label();
                title.Text = doc.Data["title"].ToString();
                title.TextColor = Color.White;
                title.FontSize = 20;
                Label date = new Label();
                date.Text = doc.Data["date"].ToString();
                date.TextColor = Color.White;
                date.FontSize = 12;
                date.VerticalOptions = LayoutOptions.Center;
                date.HorizontalOptions = LayoutOptions.EndAndExpand;
                Label body = new Label();
                body.Text = doc.Data["body"].ToString();
                body.TextColor = Color.White;
                stack2.Children.Add(title);
                stack2.Children.Add(date);
                stack1.Children.Add(stack2);
                stack1.Children.Add(body);
                frame.Content = stack1;
                MainLayout.Children.Add(frame);
            }
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