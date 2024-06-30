using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using test_app.Data;
using test_app.Models;

namespace test_app.Views
{
    public abstract class BaseQuizPage : ContentPage
    {
        protected ObservableCollection<Question> questions = new ObservableCollection<Question>();
        protected ObservableCollection<AnswerResult> answerResults = new ObservableCollection<AnswerResult>();
        protected int currentQuestionIndex = 0;
        protected int score = 0;
        protected string category;

        private readonly Dictionary<string, string> _categoryToTableMap = new Dictionary<string, string>
        {
            { "Mathématique", "Math" },
            { "Histoire", "History" },
            { "Géographie", "Geography" },
            { "Biologie", "Biology" }
        };

        public BaseQuizPage(string category)
        {
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
                    var query = $"SELECT * FROM {tableName} WHERE category = @category AND type = @type LIMIT 5";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@type", GetQuestionType());
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows)
                            {
                                SetQuestionLabelText("Aucune question disponible");
                                return;
                            }
                            while (await reader.ReadAsync())
                            {
                                var text = reader["text"].ToString();
                                var correctAnswer = reader["correct_answer"].ToString();
                                var choices = JsonConvert.DeserializeObject<string[]>(reader["choices"].ToString());
                                questions.Add(CreateQuestion(text, choices, correctAnswer));
                            }
                        }
                    }
                }
                DisplayQuestion();
            }
            catch (Exception ex)
            {
                SetQuestionLabelText("Erreur d'accès à la base de données");
                Debug.WriteLine("Erreur: " + ex.Message);
            }
        }

        protected abstract string GetQuestionType();
        protected abstract Question CreateQuestion(string text, string[] choices, string correctAnswer);
        protected abstract void SetQuestionLabelText(string text);
        protected abstract void ClearAnswerButtonsContainer();
        protected abstract void AddAnswerButton(View button);

        private void DisplayQuestion()
        {
            if (questions.Any() && currentQuestionIndex < questions.Count)
            {
                var question = questions[currentQuestionIndex];
                SetQuestionLabelText(question.Text);
                SetQuestionProgressText($"Question {currentQuestionIndex + 1}/{questions.Count}");
                SetAnswers(question.Choices);
            }
            else
            {
                NavigateToRecapPage();
            }
        }

        private void SetAnswers(string[] answers)
        {
            ClearAnswerButtonsContainer();
            foreach (var answer in answers)
            {
                var frame = new Frame
                {
                    BorderColor = Color.FromArgb("#204F54"),
                    CornerRadius = 16,
                    Padding = new Thickness(10),
                    HasShadow = false,
                    WidthRequest = this.Width - 60,
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
                AddAnswerButton(frame);
            }
        }

        private async void OnAnswerTapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var label = (Label)frame.Content;
            var answer = label.Text;
            var question = questions[currentQuestionIndex];
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
            await Navigation.PushAsync(new RecapPage(answerResults, score, category, GetNextPageType()));
        }

        protected abstract Type GetNextPageType();
        protected abstract void SetQuestionProgressText(string text);

        private string GetTableName(string category)
        {
            return _categoryToTableMap.ContainsKey(category) ? _categoryToTableMap[category] : "General";
        }

        protected async void OnBackHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
