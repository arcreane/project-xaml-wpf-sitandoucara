using test_app.Views;

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

        private async void OnCategoryTapped(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var category = label.GestureRecognizers.OfType<TapGestureRecognizer>().FirstOrDefault()?.CommandParameter.ToString();
            if (!string.IsNullOrEmpty(category) && _categories.ContainsKey(category))
            {
                var quizPage = new QuizPage(category);
                await Navigation.PushAsync(quizPage);
            }
        }

        private async void OnCreateQuizTapped(object sender, EventArgs e)
        {
            await DisplayAlert("Création de quiz", "Création de question.", "OK");
        }
    }
}
