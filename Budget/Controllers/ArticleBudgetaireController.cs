using budget.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace budget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleBudgetaireController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ArticleBudgetaireController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
        [HttpGet]
        public IActionResult GetAllArticles()
        {
            var AllArticles = this.dbContext.TBudget_Article_TBudgetaire.ToList();
            return Ok(AllArticles);
        }

        [HttpPost]
        public IActionResult getArticleById(long id)
        {
            var article = dbContext.TBudget_Article_TBudgetaire.FirstOrDefault(a => a.id == id);
            if (article == null)
            {
                return NotFound(); 
            }

            return Ok(article);

        }
    }
}
