using Microsoft.EntityFrameworkCore;
using DataAccess.Data.ModelsConfiguration;
using Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class WebApiDbContext : IdentityDbContext
    {
        public virtual DbSet<AnimalType> AnimalTypes { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Weighting> Weightings { get; set; }
        public WebApiDbContext()
        {
            Database.EnsureCreated();
        }
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeightingConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
