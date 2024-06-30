//classe abstraite-heritages
namespace test_app.Models
{
    public abstract class Question
    {
        public string Text { get; }
        public string[] Choices { get; }
        public string CorrectAnswer { get; }

        protected Question(string text, string[] choices, string correctAnswer)
        {
            Text = text;
            Choices = choices;
            CorrectAnswer = correctAnswer;
        }

        public abstract bool CheckAnswer(string answer);

        public virtual string GetRecap()
        {
            return $"{Text}\nCorrect Answer: {CorrectAnswer}";
        }
    }
}
