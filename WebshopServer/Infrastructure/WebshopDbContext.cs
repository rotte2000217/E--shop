using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Models;

namespace WebshopServer.Infrastructure
{
    public class WebshopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public WebshopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    Name = "Buyer"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Seller"
                },
                new Role()
                {
                    Id = 3,
                    Name = "Admin"
                }
            );

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebshopDbContext).Assembly);
        }
    }
}
