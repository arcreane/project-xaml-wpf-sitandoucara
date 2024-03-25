using System;
using Microsoft.Maui.Controls;

namespace test_app.Views
{
    public partial class QuizPage : ContentPage
    {
        private string category;
        private string correctAnswer;

        public QuizPage(string category)
        {
            InitializeComponent();
            this.category = category;
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (category == "Mathématique")
            {
                QuestionLabel.Text = "Combien font 2x2 ?";
                correctAnswer = "4";
                SetAnswers(new[] { "8", "10", "4", "6" });
            }
            else if (category == "Histoire")
            {
                QuestionLabel.Text = "Qui était le premier président des États-Unis ?";
                correctAnswer = "George Washington";
                SetAnswers(new[] { "George Washington", "Thomas Jefferson", "Abraham Lincoln", "John Adams" });
            }
        }

        private void SetAnswers(string[] answers)
{
    AnswerButtonsContainer.Children.Clear();
    foreach (var answer in answers)
    {
        var button = new Button
        {
            Text = answer,
            BackgroundColor = Colors.Black,  
            TextColor = Colors.White         
        };
        button.Clicked += OnAnswerClicked;
        AnswerButtonsContainer.Children.Add(button);
    }
}

        private async void OnAnswerClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var answer = button.Text;
            if (answer == correctAnswer)
            {
                await DisplayAlert("Réponse", "Correct", "OK");
            }
            else
            {
                await DisplayAlert("Réponse", "Incorrect", "OK");
            }
        }
    }
}
