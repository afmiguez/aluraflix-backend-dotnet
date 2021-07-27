using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AluraFlixContext:DbContext
    {
        public DbSet<Video> Videos { get; set; }

        public AluraFlixContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}