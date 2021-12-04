using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SIIMobile
{
    public partial class MainPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckSession();
        }
        public MainPage()
        {
            InitializeComponent();
            InitLogo.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.itpn.png");
        }

        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.txt");

        private async void CheckSession()
        {
            if (File.Exists(fileName))
            {
                string[] session = File.ReadAllText(fileName).ToString().Split(',');
                _ = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(session[0], session[1]);
                await Navigation.PushAsync(new Feed());
                Navigation.RemovePage(this);
            }
        }

        private async void Sign(object sender, EventArgs e)
        {
            try
            {
                var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(mail.Text, password.Text);
                if (Remember.IsChecked)
                {
                    File.WriteAllText(fileName, $"{mail.Text},{password.Text}");
                }
                await Navigation.PushAsync(new Feed());
                Navigation.RemovePage(this);
                mail.Text = "";
                password.Text = "";
            }
            catch(Exception ex)
            {
                _ = DisplayAlert("Error", "Verifica tus datos e intenta de nuevo.", "OK");
                Console.WriteLine("Something went wrong" + ex.Message);
            }
        }
    }
}
