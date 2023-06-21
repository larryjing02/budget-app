using Microsoft.EntityFrameworkCore;

namespace MiniBudget {
    public class ExpenseContext : DbContext {
        public DbSet<ExpenseModel> Expenses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=MiniBudget.db");
        }
    }
}