using budget.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace budget.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ApplicationDbContext dbContext;
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<TBudget_Article_TBudgetaire> TBudget_Article_TBudgetaire { get; set; }
    }
}
