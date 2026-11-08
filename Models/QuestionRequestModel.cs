using System.ComponentModel.DataAnnotations;

namespace Sep2026.Project1.WebApi.Models
{
    public class QuestionRequestModel
    {
        [Required]
        [StringLength(500)]
        public string QuestionName { get; set; } = null!;
    }
}
