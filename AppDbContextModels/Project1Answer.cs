namespace Sep2026.Project1.WebApi.AppDbContextModels
{
    public class Project1Answer
    {
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }

        public int AnswerNo { get; set; }

        public string AnswerResult { get; set; } = null!;
    }
}
