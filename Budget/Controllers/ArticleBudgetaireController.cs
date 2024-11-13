using budget.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using budget.Models.Entities;
namespace budget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleBudgetaireController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly HttpClient _httpClient;

        public ArticleBudgetaireController(ApplicationDbContext context,  HttpClient httpClient)
        {
            this.dbContext = context;
            _httpClient = httpClient;
        }
        [HttpGet]
        public IActionResult GetAllArticles()
        {
            var AllArticles = this.dbContext.TBudget_Article_TBudgetaire.ToList();
            return Ok(AllArticles);
        }

        [HttpPost("getArticleById")]
        public IActionResult GetArticleById(long id)
        {
            var article = dbContext.TBudget_Article_TBudgetaire.FirstOrDefault(a => a.id == id);
            if (article == null)
            {
                return NotFound(); 
            }

            return Ok(article);
        }
        [HttpPost("getPiecesById")] //getPiecesById using articlebudgetaireId
        public async Task<IActionResult> GetArticleByIdAsync(long id)

        {
            var url = "https://localhost:7140/api/Pieces/getPiecesById?id="+id;

            var json = JsonSerializer.Serialize(new { id });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request
                var response = await _httpClient.PostAsync(url, content);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and parse the response
                    var responseData = await response.Content.ReadAsStringAsync();
                    return Ok(responseData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve article");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateArticle([FromBody] TBudget_Article_TBudgetaire article)
        {
            if (article == null)
            {
                return BadRequest("Article data is null.");
            }
            try
            {
                dbContext.TBudget_Article_TBudgetaire.Add(article);
                await dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(CreateArticle), new { id = article.id }, article);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
