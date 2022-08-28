using BankTransaction.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TransactionModel> TransactionModels { get; set; }
    }

}
