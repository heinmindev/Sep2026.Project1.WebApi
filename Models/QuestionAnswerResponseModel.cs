namespace Sep2026.Project1.WebApi.Models
{
    public class QuestionAnswerResponseModel
    {
        public int QuestionId { get; set; }

        public int QuestionNo { get; set; }

        public string QuestionName { get; set; } = null!;

        public int AnswerNo { get; set; }

        public List<string> Answers { get; set; } = new();
    }
}
