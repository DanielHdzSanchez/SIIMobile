using Plugin.FirebaseAuth;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIIMobile
{
    public partial class App : Application
    {
        bool saved = false;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.DarkBlue;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
