using test_app.Views;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace test_app
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<string, string> _categories = new Dictionary<string, string>
        {
            { "Mathématique", "Math" },
            { "Histoire", "History" },
            { "Géographie", "Geography" },
            { "Biologie", "Biology" }
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var category = button.Text;
            var quizPage = new QuizPage(category);
            await Navigation.PushAsync(quizPage);
        }

        private async void OnCreateQuizClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Création de quiz", "Création de question.", "OK");
        }
    }
}
