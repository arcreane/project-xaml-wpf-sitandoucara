using test_app.Views;
namespace test_app
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnMathClicked(object sender, EventArgs e)
        {
            var quizPage = new QuizPage("Mathématique");
            await Navigation.PushAsync(quizPage);
        }

        private async void OnHistoryClicked(object sender, EventArgs e)
        {
            var quizPage = new QuizPage("Histoire");
            await Navigation.PushAsync(quizPage);
        }

        private async void OnGeographyClicked(object sender, EventArgs e)
        {
            var quizPage = new QuizPage("Géographie");
            await Navigation.PushAsync(quizPage);
        }

        private async void OnBiologyClicked(object sender, EventArgs e)
        {
            var quizPage = new QuizPage("Biologie");
            await Navigation.PushAsync(quizPage);
        }

        private async void OnCreateQuizClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Création de quiz", "Création de question.", "OK");
        }
    }
}
