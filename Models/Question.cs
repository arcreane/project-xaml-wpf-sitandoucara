namespace test_app.Models
{
    public abstract class Question
    {
        public string Text { get; set; }
        public string[] Choices { get; set; }
        public string CorrectAnswer { get; set; }

        protected Question(string text, string[] choices, string correctAnswer)
        {
            Text = text;
            Choices = choices;
            CorrectAnswer = correctAnswer;
        }

        public abstract bool CheckAnswer(string answer);
    }
}
