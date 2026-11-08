using Microsoft.AspNetCore.Mvc;
using Sep2026.Project1.WebApi.AppDbContextModels;
using Sep2026.Project1.WebApi.Models;

namespace Sep2026.Project1.WebApi.Controllers
{
    // api/answer
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AnswerController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAnswers(int? questionId)
        {
            var query = _db.Project1Answers.AsQueryable();
            if (questionId is not null)
            {
                query = query.Where(x => x.QuestionId == questionId);
            }

            var lst = query.OrderBy(x => x.QuestionId).ThenBy(x => x.AnswerNo).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnswer(int id)
        {
            var item = _db.Project1Answers.FirstOrDefault(x => x.AnswerId == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateAnswer(AnswerCreateRequestModel requestModel)
        {
            if (!_db.Project1Questions.Any(x => x.QuestionId == requestModel.QuestionId))
            {
                return NotFound();
            }

            if (_db.Project1Answers.Any(x => x.QuestionId == requestModel.QuestionId && x.AnswerNo == requestModel.AnswerNo))
            {
                return Conflict("This question already has an answer with that number.");
            }

            var item = new Project1Answer
            {
                QuestionId = requestModel.QuestionId,
                AnswerNo = requestModel.AnswerNo,
                AnswerResult = requestModel.AnswerResult.Trim(),
            };
            _db.Project1Answers.Add(item);
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult UpsertAnswer(int id, AnswerUpsertRequestModel requestModel)
        {
            var item = _db.Project1Answers.FirstOrDefault(x => x.AnswerId == id);
            if (item is null)
            {
                return NotFound();
            }

            if (_db.Project1Answers.Any(x => x.QuestionId == item.QuestionId && x.AnswerNo == requestModel.AnswerNo && x.AnswerId != id))
            {
                return Conflict("This question already has an answer with that number.");
            }

            item.AnswerNo = requestModel.AnswerNo;
            item.AnswerResult = requestModel.AnswerResult.Trim();
            _db.SaveChanges();

            return Ok("Updating Successful.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnswer(int id)
        {
            var item = _db.Project1Answers.FirstOrDefault(x => x.AnswerId == id);
            if (item is null)
            {
                return NotFound();
            }

            _db.Project1Answers.Remove(item);
            _db.SaveChanges();

            return Ok("Deleting Successful.");
        }
    }
}
