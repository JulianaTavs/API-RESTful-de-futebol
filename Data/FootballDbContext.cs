using API_RESTful_de_futebol.Models;
using Microsoft.EntityFrameworkCore;

namespace API_RESTful_de_futebol.Data
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options)
        {
        }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
        }
    }
}