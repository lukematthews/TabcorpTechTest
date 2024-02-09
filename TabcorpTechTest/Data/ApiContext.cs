using Microsoft.EntityFrameworkCore;
using TabcorpTechTest.Models.Db;

namespace TabcorpTechTest.Data
{
    public class ApiContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public ApiContext() { }
    }
}
