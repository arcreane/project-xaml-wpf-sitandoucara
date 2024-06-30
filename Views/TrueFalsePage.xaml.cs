using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using test_app.Models;
using test_app.Data;
using System.Linq;
using Newtonsoft.Json;

namespace test_app.Views
{
    public partial class TrueFalsePage : ContentPage
    {
        private ObservableCollection<Question> questions = new ObservableCollection<Question>();
        private ObservableCollection<AnswerResult> answerResults = new ObservableCollection<AnswerResult>();
        private int currentQuestionIndex = 0;
        private int score = 0;
        private string category;

        private readonly Dictionary<string, string> _categoryToTableMap = new Dictionary<string, string>
        {
            { "Mathématique", "Math" },
            { "Histoire", "History" },
            { "Géographie", "Geography" },
            { "Biologie", "Biology" }
        };

        public TrueFalsePage(string category)
        {
            InitializeComponent();
            this.category = category;
            LoadQuestions(category);
        }

        private async void LoadQuestions(string category)
        {
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    await conn.OpenAsync();
                    var tableName = GetTableName(category);
                    var query = $"SELECT * FROM {tableName} WHERE category = @category AND type = 'TF' LIMIT 5";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@category", category);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows)
                            {
                                QuestionLabel.Text = "Aucune question disponible";
                                return;
                            }
                            while (await reader.ReadAsync())
                            {
                                var text = reader["text"].ToString();
                                var choices = JsonConvert.DeserializeObject<string[]>(reader["choices"].ToString());
                                var correctAnswer = reader["correct_answer"].ToString();
                                questions.Add(new TrueFalseQuestion(text, correctAnswer));
                            }
                        }
                    }
                }
                DisplayQuestion();
            }
            catch (Exception ex)
            {
                QuestionLabel.Text = "Erreur d'accès à la base de données";
                Debug.WriteLine("Erreur: " + ex.Message);
            }
        }

        private string GetTableName(string category)
        {
            return _categoryToTableMap.ContainsKey(category) ? _categoryToTableMap[category] : "General";
        }

        private void DisplayQuestion()
        {
            if (questions.Any() && currentQuestionIndex < questions.Count)
            {
                var question = questions[currentQuestionIndex];
                QuestionLabel.Text = question.Text;
                QuestionProgressLabel.Text = $"Question {currentQuestionIndex + 1}/{questions.Count}";
                SetAnswers(question.Choices);
            }
            else
            {
                NavigateToRecapPage();
            }
        }

        private void SetAnswers(string[] answers)
        {
            AnswerButtonsContainer.Children.Clear();
            foreach (var answer in answers)
            {
                var frame = new Frame
                {
                    BorderColor = Color.FromArgb("#204F54"),
                    CornerRadius = 16,
                    Padding = new Thickness(10),
                    HasShadow = false,
                    WidthRequest = this.Width - 60, // Taking padding into account
                    HeightRequest = 60,
                    Content = new Label
                    {
                        Text = answer,
                        TextColor = Color.FromArgb("#204F54"),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold
                    }
                };

                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += OnAnswerTapped;
                frame.GestureRecognizers.Add(tapGesture);
                AnswerButtonsContainer.Children.Add(frame);
            }
        }

        private async void OnAnswerTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var label = (Label)frame.Content;
            var answer = label.Text;
            var question = questions[currentQuestionIndex];
            var correctAnswer = question.CorrectAnswer;
            bool isCorrect = question.CheckAnswer(answer);

            answerResults.Add(new AnswerResult
            {
                Question = question,
                UserAnswer = answer,
                IsCorrect = isCorrect
            });

            if (isCorrect)
            {
                await DisplayAlert("Réponse", "Correct", "OK");
                score++;
            }
            else
            {
                await DisplayAlert("Réponse", "Incorrect", "OK");
            }
            currentQuestionIndex++;
            DisplayQuestion();
        }

        private async void NavigateToRecapPage()
        {
            await Navigation.PushAsync(new RecapPage(answerResults, score, category, null));
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnBackHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
