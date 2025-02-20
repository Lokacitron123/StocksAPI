using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Ensures the base class configurations for Identity are applied

            // Define composite primary key for Portfolio using UserId and StockId
            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.UserId, p.StockId }));

            // Configure one-to-many relationship between User and Portfolio
            builder.Entity<Portfolio>()
                .HasOne(u => u.User) // Portfolio has one associated User
                .WithMany(p => p.Portfolios) // User can have many Portfolios
                .HasForeignKey(p => p.UserId); // UserId is the foreign key

            // Configure one-to-many relationship between A Stock and Portfolio
            builder.Entity<Portfolio>()
                .HasOne(u => u.Stock) // Portfolio has one associated Stock
                .WithMany(p => p.Portfolios) // User can have many Stocks
                .HasForeignKey(p => p.StockId); // StockId is the foreign key

            // Seed predefined roles into the IdentityRole table
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "d6a43e34-1bde-4b92-95f6-8ddedb8e77aa",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "e0f3fbc1-3e3a-4c5c-8326-bd2f187b9a3b",
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            builder.Entity<IdentityRole>().HasData(roles); // Add role data to the database
        }
    }
}
