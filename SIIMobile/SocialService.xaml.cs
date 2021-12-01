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
    public partial class SocialService : ContentPage
    {
        public SocialService()
        {
            InitializeComponent();
            _ = GetSocialInfo();
        }

        string user = CrossFirebaseAuth.Current.Instance.CurrentUser.Uid;
        int HOURS = 480;

        async Task GetSocialInfo()
        {
            var query = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(user)
                .Collection("social")
                .Document("service")
                .GetAsync();
            Dependencia.Text = query.Data["dependencia"].ToString();
            Programa.Text = query.Data["programa"].ToString();
            Responsable.Text = query.Data["responsable"].ToString();
            HorasAcumuladas.Text = query.Data["horasacumuladas"].ToString();
            Reportes.Text = (string)query.Data["reportes"];
            Inicio.Text = query.Data["inicio"].ToString();
            Final.Text = query.Data["fin"].ToString();
            Progreso.Text = (string)query.Data["status"];
        }
    }
}