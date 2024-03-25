namespace test_app.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(string text, string[] choices, string correctAnswer)
            : base(text, choices, correctAnswer) { }

        public override bool CheckAnswer(string answer)
        {
        //logique
            return answer == CorrectAnswer;
        }
    }
}
