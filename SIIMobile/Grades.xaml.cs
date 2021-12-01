using Plugin.CloudFirestore;
using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Chips;
using Xamarin.Forms.Xaml;

namespace SIIMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Grades : ContentPage
    {
        public Grades()
        {
            InitializeComponent();
            _ = GetSemesters();
        }
        string user = CrossFirebaseAuth.Current.Instance.CurrentUser.Uid;

        async private void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async Task GetSemesters()
        {
            var query = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(user)
                .Collection("Grades")
                .GetAsync();
            int semesters = query.Count;


            for (int i = 1; i <= semesters; i++)
            {
                Button chip = new Button();
                chip.Text = i.ToString();
                chip.Padding = 8.5;
                chip.Margin = 5;
                chip.CornerRadius = 10;
                chip.WidthRequest = 40;
                chip.BackgroundColor = Color.Gray;
                chip.Clicked += async (sender, args) => await GetGradesAsync(chip.Text);
                ChipGroup.Children.Add(chip);
            }
        }

        async Task GetGradesAsync(string semester)
        {
            GradesLayout.Children.Clear();
            SelectedSemester.Text = $"Semestre: {semester}";
            var query = await CrossCloudFirestore.Current
                .Instance
                .Collection("student")
                .Document(user)
                .Collection("Grades")
                .Document(semester)
                .GetAsync();
            string sig = query.Data["Signatures"].ToString();
            string gra = query.Data["Grades"].ToString();
            string[] signatures = sig.Split(',');
            string[] grades = gra.Split(',');
            int ix = 0;
            foreach (string s in signatures)
            {
                Frame frame = new Frame();
                frame.Margin = 10;
                frame.BackgroundColor = Color.DarkBlue;
                frame.CornerRadius = 10;
                StackLayout stack = new StackLayout();
                stack.Orientation = StackOrientation.Horizontal;
                Label name = new Label();
                name.HorizontalOptions = LayoutOptions.Start;
                name.Text = s;
                Label g = new Label();
                g.HorizontalOptions = LayoutOptions.EndAndExpand;
                g.Text = grades[ix];
                stack.Children.Add(name);
                stack.Children.Add(g);
                frame.Content = stack;
                GradesLayout.Children.Add(frame);
                ix++;
            }
        }
    }
}