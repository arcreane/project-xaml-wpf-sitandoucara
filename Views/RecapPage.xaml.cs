using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using test_app.Models;

namespace test_app.Views
{
    public partial class RecapPage : ContentPage
    {
        private string category;
        private Type nextPageType;

        public RecapPage(ObservableCollection<AnswerResult> answerResults, int score, string category, Type nextPageType)
        {
            InitializeComponent();
            DisplayRecap(answerResults);
            ScoreLabel.Text = $"Score: {score}/5";
            this.category = category;
            this.nextPageType = nextPageType;
        }

        private void DisplayRecap(ObservableCollection<AnswerResult> answerResults)
        {
            foreach (var result in answerResults)
            {
                var recapLabel = new Label
                {
                    Text = $"{result.Question.Text}\nYour answer: {result.UserAnswer} - {(result.IsCorrect ? "Correct" : "Incorrect")}\nCorrect answer: {result.Question.CorrectAnswer}",
                    TextColor = result.IsCorrect ? Colors.Green : Colors.Red,
                    FontSize = 18,
                    Margin = new Thickness(0, 10)
                };
                RecapContainer.Children.Add(recapLabel);
            }
        }

        private async void OnNextStageClicked(object sender, EventArgs e)
        {
            if (nextPageType != null)
            {
                var nextPage = (ContentPage)Activator.CreateInstance(nextPageType, new object[] { category });
                await Navigation.PushAsync(nextPage);
            }
            else
            {
                await DisplayAlert("Prochaine étape", "La prochaine étape de questions est en cours de développement.", "OK");
            }
        }
    }
}
