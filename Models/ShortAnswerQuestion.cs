//texte and little more
namespace test_app.Models
{
    public class ShortAnswerQuestion : Question
    {
        public ShortAnswerQuestion(string text, string correctAnswer)
            : base(text, new[] { correctAnswer }, correctAnswer) {}

        public override bool CheckAnswer(string answer)
        {
            return answer.Trim().Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
        }

        public override string GetRecap()
        {
            return base.GetRecap();
        }
    }
}
