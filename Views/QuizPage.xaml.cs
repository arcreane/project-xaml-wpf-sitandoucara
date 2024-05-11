//Gestion de la question de tupe choix multiple
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
    public partial class QuizPage : ContentPage
    {
        private ObservableCollection<Question> questions = new ObservableCollection<Question>();

        public QuizPage(string category)
        {
            InitializeComponent();
            LoadQuestions(category);
        }

        private async void LoadQuestions(string category)
        {
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    await conn.OpenAsync();
                    string tableName = GetTableName(category);  // Determine la bonne table 
                    var query = $"SELECT * FROM {tableName} WHERE category = @category";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@category", category);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows)
                            {
                                QuestionLabel.Text = "Tableau vide";
                                return;
                            }
                            while (await reader.ReadAsync())
                            {
                                var text = reader["text"].ToString();
                                var choices = JsonConvert.DeserializeObject<string[]>(reader["choices"].ToString());
                                var correctAnswer = reader["correct_answer"].ToString();
                                questions.Add(new MultipleChoiceQuestion(text, choices, correctAnswer)); //Affirmer la natire Multiple des questions
                            }
                        }
                    }
                }
                DisplayFirstQuestion();
            }
            catch (Exception ex)
            {
                QuestionLabel.Text = "Erreur d'accès à la base de données";
                Debug.WriteLine("Erreur: " + ex.Message);
            }
        }

//gestions des categories (questions)
        private string GetTableName(string category) 
        {
        switch (category)
            {
            case "Histoire":
                return "History";
            case "Mathématique":
                return "Math";
            case "Géographie":
                return "geography"; 
            case "Biologie":
                return "Biology"; 
            default:
                return "General";
            }
        }
        
        private void DisplayFirstQuestion()
        {
            if (questions.Any())
            {
                var firstQuestion = questions.First();
                QuestionLabel.Text = firstQuestion.Text;
                SetAnswers(firstQuestion.Choices);
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
            var correctAnswer = questions.First().CorrectAnswer;  
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
