using BlogWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Data
{
    public class AppsDbContext : IdentityDbContext
    {
        public AppsDbContext(DbContextOptions<AppsDbContext> options):base(options)
        { 

        }
        
        public DbSet<Post> Posts { get; set; }

    }
}
