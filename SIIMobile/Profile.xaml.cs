using Plugin.CloudFirestore;
using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.IO;
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
        bool service = false;
        bool residence = false;
        public Profile()
        {
            InitializeComponent();
            //Back.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.back.png");
            //ProfileImg.Source = ImageSource.FromResource("SIIMobile.Assets.imgs.pikachu.png");
            _ = GetInfoAsync();
            _ = GetSocialInfo();
            _ = GetResidenceInfo();
        }

        private async Task GetInfoAsync()
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(CrossFirebaseAuth.Current.Instance.CurrentUser.Uid)
                .GetAsync();
            StudentName.Text = document.Data["Name"].ToString();
            Career.Text = "Carrera: " + document.Data["Career"].ToString();
            ControlNumber.Text = "No de Control: " + document.Data["NoControl"].ToString();
            Specialty.Text = "Especialidad: " + document.Data["Specialty"].ToString();
            Semester.Text = "Semestre: " + document.Data["Semester"].ToString();
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
            
            if (service == true)
            {
                await Navigation.PushAsync(new SocialService());
            }
            else
            {
                _ = DisplayAlert("Opción no habilitada", "Este proceso aún no está disponible", "Ok");
            }
        }

        
        

        async private void ProfessionalResidence_Clicked(object sender, EventArgs e)
        {
            
            if (residence == true)
            {
                await Navigation.PushAsync(new Residence());
            }
            else
            {
                _ = DisplayAlert("Opción no habilitada", "Este proceso aún no está disponible", "Ok");
            }
            
        }
        string user = CrossFirebaseAuth.Current.Instance.CurrentUser.Uid;
        async Task GetResidenceInfo()
        {
            var query = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(user)
                .Collection("residence")
                .Document("professional")
                .GetAsync();
            if (query.Exists)
            {
                residence = true;
            }
        }

        async Task GetSocialInfo()
        {
            var query = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(user)
                .Collection("social")
                .Document("service")
                .GetAsync();
            if (query.Exists)
            {
                service = true;
            }
        }

        /*async private void Salir(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            await Navigation.PopToRootAsync();
        }*/
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.txt");
        async private void Finish(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            CrossFirebaseAuth.Current.Instance.SignOut();
            await Navigation.PopToRootAsync();
        }
    }
}