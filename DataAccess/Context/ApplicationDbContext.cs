using Microsoft.EntityFrameworkCore;
using System;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class ApplicationDbContext :IdentityDbContext<IdentityUser>
    {
        protected ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Service> Services { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تنظیم ارتباط خودارجاعی
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Services)
                .WithOne(y => y.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);



            // تنظیم فیلد Slug به عنوان یکتا
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            modelBuilder.Entity<Service>()
                .HasIndex(s => s.Slug)
                .IsUnique();


            // Seed data برای جدول Service
            modelBuilder.Entity<Service>().HasData(
                
                   
                new Service
                {
                    ServiceId = 1,
                    ServiceName = "رنگ مو",
                    Description = "رنگ‌آمیزی تخصصی با بهترین مواد",
                    Price = 300000,
                    Duration = 90,
                    Slug = "رنگ مو", 
                    ImageUrl = "~/images/Logo.png"
                },
                new Service
                {
                    ServiceId = 2,
                    ServiceName = "ترمیم",
                    Description = "خدمات مانیکور و زیبایی ناخن",
                    Price = 150000,
                    Duration = 60,
                    Slug = "ترمیم",  
                    ImageUrl = "~/images/Logo.png"
                    
                }
            );
        
        }
    }
}
