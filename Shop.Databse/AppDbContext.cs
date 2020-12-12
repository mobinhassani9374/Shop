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
        public DbSet<Product> Products { get; set; }
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

            var product = builder.Entity<Product>();

            product.HasKey(c => c.Id);

            product.Property(c => c.Description).HasMaxLength(Constants.Product_Description_Length).IsRequired(true);

            product.Property(c => c.Title).HasMaxLength(Constants.Product_Title_Length).IsRequired(true);

            product.Property(c => c.PrimaryImage).HasMaxLength(Constants.Product_PrimaryImage_Length).IsRequired(true);

            product.HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior
                .Cascade);
        }
    }
}
