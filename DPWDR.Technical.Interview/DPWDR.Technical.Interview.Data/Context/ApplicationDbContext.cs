using DPWDR.Technical.Interview.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DPWDR.Technical.Interview.Data.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        public DbSet<Product> Products { get; set; }


    }
}
