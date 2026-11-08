using System.ComponentModel.DataAnnotations;

namespace Sep2026.Project1.WebApi.Models
{
    public class AnswerCreateRequestModel
    {
        public int QuestionId { get; set; }

        [Range(1, 10)]
        public int AnswerNo { get; set; }

        [Required]
        public string AnswerResult { get; set; } = null!;
    }

    public class AnswerUpsertRequestModel
    {
        [Range(1, 10)]
        public int AnswerNo { get; set; }

        [Required]
        public string AnswerResult { get; set; } = null!;
    }
}
