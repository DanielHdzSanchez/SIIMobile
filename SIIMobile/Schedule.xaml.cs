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
    public partial class Schedule : ContentPage
    {
        public Schedule()
        {
            InitializeComponent();
            foreach(Button b in Buttons.Children)
            {
                b.Clicked += async (sender, args) => await GetScheduleAsync(b.Text.ToLower());
            }
        }

        string user = CrossFirebaseAuth.Current.Instance.CurrentUser.Uid;

        async Task GetScheduleAsync(string day)
        {
            /*AM.Children.Clear();
            PM.Children.Clear();*/
            Cards.Children.Clear();
            var query = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(user)
                .Collection("horario")
                .Document(day)
                .GetAsync();
            string mat = query.Data["materias"].ToString();
            string hor = query.Data["hora"].ToString();
            string aul = query.Data["aula"].ToString();
            string[] materias = mat.Split(',');
            string[] horas = hor.Split(',');
            string[] aulas = aul.Split(',');
            int ix = 0;
            foreach (string s in materias)
            {
                Frame frame = new Frame();
                frame.Margin = 10;
                frame.BackgroundColor = Color.DarkBlue;
                frame.CornerRadius = 15;
                StackLayout stack = new StackLayout();
                stack.Orientation = StackOrientation.Horizontal;
                Label details = new Label();
                details.HorizontalOptions = LayoutOptions.Start;
                details.Text = $"{aulas[ix]} {horas[ix]}";
                Label subject = new Label();
                subject.HorizontalOptions = LayoutOptions.EndAndExpand;
                subject.Text = materias[ix];
                stack.Children.Add(details);
                stack.Children.Add(subject);
                frame.Content = stack;
                /*if(horas[ix].ElementAt(0) < 12)
                {
                    AM.Children.Add(frame);
                }
                else
                {
                    PM.Children.Add(frame);
                }*/
                Cards.Children.Add(frame);
                ix++;
            }
        }
    }
}