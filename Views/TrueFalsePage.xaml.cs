using test_app.Models;

namespace test_app.Views
{
    public partial class TrueFalsePage : BaseQuizPage
    {
        public TrueFalsePage(string category) : base(category)
        {
            InitializeComponent();
        }

        protected override string GetQuestionType() => "TF";

        protected override Question CreateQuestion(string text, string[] choices, string correctAnswer)
        {
            return new TrueFalseQuestion(text, correctAnswer);
        }

        protected override void SetQuestionLabelText(string text)
        {
            QuestionLabel.Text = text;
        }

        protected override void ClearAnswerButtonsContainer()
        {
            AnswerButtonsContainer.Children.Clear();
        }

        protected override void AddAnswerButton(View button)
        {
            AnswerButtonsContainer.Children.Add(button);
        }

        protected override Type GetNextPageType()
        {
            return null; 
        }

        protected override void SetQuestionProgressText(string text)
        {
            QuestionProgressLabel.Text = text;
        }
    }
}
