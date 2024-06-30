namespace test_app.Models
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string text, string correctAnswer)
            : base(text, new[] { "True", "False" }, correctAnswer) { }

        public override bool CheckAnswer(string answer)
        {
            return answer == CorrectAnswer;
        }

        public override string GetRecap()
        {
            return base.GetRecap();
        }
    }
}
