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
    public partial class Residence : ContentPage
    {
        public Residence()
        {
            InitializeComponent();
            _ = GetResidenceInfo();
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
            Empresa.Text = query.Data["empresa"].ToString();
            Externo.Text = query.Data["externo"].ToString();
            Inicio.Text = query.Data["inicio"].ToString();
            Fin.Text = query.Data["fin"].ToString();
            Proyecto.Text = (string)query.Data["proyecto"];
            Reportes.Text = query.Data["reportes"].ToString();
            Calificacion.Text = query.Data["calificacion"].ToString();
        }
    }
}