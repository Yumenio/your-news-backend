using Microsoft.EntityFrameworkCore;
using News.Api.Models;
using System.Collections.ObjectModel;

namespace News.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
