using Microsoft.EntityFrameworkCore;
using TabcorpTechTest.Models.Db;

namespace TabcorpTechTest.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
