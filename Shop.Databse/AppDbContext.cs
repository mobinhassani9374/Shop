using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Database.Identity.Entities;
using Shop.Domain;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Database
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var user = builder.Entity<User>();

            user
                .Property(c => c.FullName)
                .HasMaxLength(Constants.User_FullName_Length)
                .IsRequired(true);

            var category = builder.Entity<Category>();

            category.HasKey(c => c.Id);

            category.Property(c => c.Title)
                .HasMaxLength(Constants.Category_Title_Length)
                .IsRequired(true);

            category.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId);
        }
    }
}
