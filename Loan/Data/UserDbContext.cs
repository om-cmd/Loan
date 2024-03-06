
using Loan.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Loan.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
       

    }
}
