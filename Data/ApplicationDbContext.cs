using Microsoft.EntityFrameworkCore;

namespace digikala_netCore.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<User> User {get;set;}

    }
}
