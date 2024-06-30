//Association d’Images
namespace test_app.Models
{
    public class MatchQuestion : Question
    {
        public MatchQuestion(string text, string[] choices, string correctAnswer)
            : base(text, choices, correctAnswer) {}

        public override bool CheckAnswer(string answer)
        {
            //logique pour vérifier les associations
            return answer == CorrectAnswer;
        }

        public override string GetRecap()
        {
            return base.GetRecap() + $"\nYour matches were: {string.Join(", ", Choices)}";
        }
    }
}
