using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SIIMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitLogo.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.itpn.png");
            
        }

        private async void Continuar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(mail.Text, password.Text);
                await Navigation.PushAsync(new Feed());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Something went wrong");
                
            }

            
        }
    }
}
