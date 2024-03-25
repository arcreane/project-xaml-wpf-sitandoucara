//Organisation d’Éléments
namespace test_app.Models
{
    public class SequenceQuestion : Question
    {
        public SequenceQuestion(string text, string[] choices, string correctAnswer)
            : base(text, choices, correctAnswer) {}

        public override bool CheckAnswer(string answer)
        {
            //logique pour vérifier l'ordre des éléments
            return answer == CorrectAnswer;
        }
    }
}
