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
            UserIcon.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.pikachu.png");
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