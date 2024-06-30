namespace test_app.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(string text, string[] choices, string correctAnswer)
            : base(text, choices, correctAnswer) { }

        public override bool CheckAnswer(string answer)
        {
            return answer == CorrectAnswer;
        }

        public override string GetRecap()
        {
            return base.GetRecap() + $"\nYour choices were: {string.Join(", ", Choices)}";
        }
    }
}
