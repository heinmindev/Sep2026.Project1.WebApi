using Microsoft.AspNetCore.Mvc;
using Sep2026.Project1.WebApi.AppDbContextModels;
using Sep2026.Project1.WebApi.Models;

namespace Sep2026.Project1.WebApi.Controllers
{
    // api/question
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _db;

        public QuestionController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            var lst = _db.Project1Questions.OrderBy(x => x.QuestionNo).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuestion(int id)
        {
            var item = _db.Project1Questions.FirstOrDefault(x => x.QuestionId == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateQuestion(QuestionRequestModel requestModel)
        {
            int newId = (_db.Project1Questions.Max(x => (int?)x.QuestionId) ?? 0) + 1;
            var item = new Project1Question
            {
                QuestionId = newId,
                QuestionNo = newId,
                QuestionName = requestModel.QuestionName.Trim(),
            };
            _db.Project1Questions.Add(item);
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult UpsertQuestion(int id, QuestionRequestModel requestModel)
        {
            var item = _db.Project1Questions.FirstOrDefault(x => x.QuestionId == id);
            if (item is null)
            {
                return NotFound();
            }

            item.QuestionName = requestModel.QuestionName.Trim();
            _db.SaveChanges();

            return Ok("Updating Successful.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            var item = _db.Project1Questions.FirstOrDefault(x => x.QuestionId == id);
            if (item is null)
            {
                return NotFound();
            }

            if (_db.Project1Answers.Any(x => x.QuestionId == id))
            {
                return Conflict("Delete this question's answers first.");
            }

            _db.Project1Questions.Remove(item);
            _db.SaveChanges();

            return Ok("Deleting Successful.");
        }

        // Some questions have two rows for one AnswerNo in the source data, so every match is returned.
        [HttpGet("{id}/answer/{answerNo}")]
        public IActionResult GetAnswer(int id, int answerNo)
        {
            var question = _db.Project1Questions.FirstOrDefault(x => x.QuestionId == id);
            if (question is null)
            {
                return NotFound();
            }

            var answers = _db.Project1Answers
                .Where(x => x.QuestionId == id && x.AnswerNo == answerNo)
                .OrderBy(x => x.AnswerId)
                .Select(x => x.AnswerResult)
                .ToList();
            if (answers.Count == 0)
            {
                return NotFound();
            }

            return Ok(new QuestionAnswerResponseModel
            {
                QuestionId = question.QuestionId,
                QuestionNo = question.QuestionNo,
                QuestionName = question.QuestionName,
                AnswerNo = answerNo,
                Answers = answers,
            });
        }
    }
}
