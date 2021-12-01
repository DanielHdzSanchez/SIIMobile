using Plugin.CloudFirestore;
using Plugin.FirebaseAuth;
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
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            //Back.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.back.png");
            ProfileImg.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.pikachu.png");
            _ = GetInfoAsync();
        }

        private async Task GetInfoAsync()
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(CrossFirebaseAuth.Current.Instance.CurrentUser.Uid)
                .GetAsync();
            StudentName.Text = document.Data["Name"].ToString();
            Career.Text = document.Data["Career"].ToString();
            ControlNumber.Text = document.Data["NoControl"].ToString();
            Specialty.Text = document.Data["Specialty"].ToString();
        }

        async private void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Grades_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Grades());
        }

        async private void Schedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Schedule());
        }

        async private void SocialService_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SocialService());
        }

        async private void ProfessionalResidence_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Residence());
        }

        private void Progress_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}