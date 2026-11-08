using Microsoft.AspNetCore.Mvc;
using Sep2026.Project1.WebApi.AppDbContextModels;

namespace Sep2026.Project1.WebApi.Controllers
{
    // api/number
    [Route("api/[controller]")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly AppDbContext _db;

        public NumberController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetNumbers()
        {
            var lst = _db.Project1Numbers.OrderBy(x => x.NumberId).ToList();
            return Ok(lst);
        }
    }
}
