using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Database.Identity.Entities;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var user = builder.Entity<User>();

            user
                .Property(c => c.FullName)
                .HasMaxLength(500)
                .IsRequired(true);
        }
    }
}
