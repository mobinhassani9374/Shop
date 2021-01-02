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
        public DbSet<ProductVote> ProductVote { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<SlideShow> SlideShows { get; set; }
        public DbSet<Info> Infoes { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<LogService> LogServices { get; set; }
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

            var cart = builder.Entity<Cart>();

            cart.HasKey(c => c.Id);

            cart.HasOne(c => c.Product)
                .WithMany(c => c.Carts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior
                .Cascade);

            var order = builder.Entity<Order>();

            order.HasKey(c => c.Id);

            order.Property(c => c.IdPay_id).HasMaxLength(200);

            order.HasMany(c => c.Details)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            var orderDetail = builder.Entity<OrderDetail>();

            orderDetail.HasKey(c => c.Id);

            orderDetail.HasOne(c => c.Product)
                .WithMany(c => c.OrderDetails)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            var slideShow = builder.Entity<SlideShow>();

            slideShow.HasKey(c => c.Id);
            slideShow.Property(c => c.ImageName).HasMaxLength(Constants.SlideShow_ImageName_Length).IsRequired(true);

            var productVote = builder.Entity<ProductVote>();

            productVote.HasKey(c => c.Id);

            productVote.Property(c => c.UserId).HasMaxLength(Constants.UserId_Length).IsRequired(true);

            productVote.Property(c => c.Comment).IsRequired(true);

            productVote.HasOne(c => c.Product)
                .WithMany(c => c.Votes)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            var info = builder.Entity<Info>();
            info.HasKey(c => c.Id);

            info.Property(c => c.InstagramAccount).HasMaxLength(1000);
            info.Property(c => c.TelegramChanal).HasMaxLength(1000);
            info.Property(c => c.PhoneNumber1).HasMaxLength(1000);
            info.Property(c => c.PhoneNumber2).HasMaxLength(1000);
            info.Property(c => c.PhoneNumber3).HasMaxLength(1000);
            info.Property(c => c.PhoneNumber3).HasMaxLength(1000);
            info.Property(c => c.PhoneNumber4).HasMaxLength(1000);

            var contactUs = builder.Entity<ContactUs>();
            contactUs.HasKey(c => c.Id);
            contactUs.Property(c => c.UserId).IsRequired(true).HasMaxLength(Constants.UserId_Length);

            var logService = builder.Entity<LogService>();

            logService.HasKey(c => c.Id);

            logService.Property(c => c.Method).HasMaxLength(50);
            logService.Property(c => c.IpAddress).HasMaxLength(100);
        }
    }
}
