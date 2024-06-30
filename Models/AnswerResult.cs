//check question verification
namespace test_app.Models
{
    public class AnswerResult
    {
        public Question Question { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}