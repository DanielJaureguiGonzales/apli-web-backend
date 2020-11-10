using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Extensions;

namespace TrainingGain.Api.Domain.Persistance.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Specialist> Specialists { get; set; }
        public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }   
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //User entity 
            builder.Entity<User>().ToTable("users");
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(15);
            builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(15);
            builder.Entity<User>().Property(u => u.Birth).IsRequired();
            builder.Entity<User>().Property(u => u.Address).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.Phone).IsRequired();
            builder.Entity<User>().Property(u => u.Age).IsRequired();
            builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.Country).IsRequired().HasMaxLength(10);
            builder.Entity<User>().Property(u => u.Gender).IsRequired().HasMaxLength(20);
            builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(8);
            builder.Entity<User>().HasOne(u => u.Customer).WithOne(c => c.User).HasForeignKey<Customer>(u => u.UserId);
            builder.Entity<User>().HasOne(u => u.Specialist).WithOne(s => s.User).HasForeignKey<Specialist>(u => u.UserId);

            //Customer entity 
            builder.Entity<Customer>().ToTable("customers");
            builder.Entity<Customer>().HasKey(c => c.Id);
            builder.Entity<Customer>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Customer>().Property(c => c.Description).IsRequired().HasMaxLength(30);

            // Specialist entity
            builder.Entity<Specialist>().ToTable("specialists");
            builder.Entity<Specialist>().HasKey(s => s.Id);
            builder.Entity<Specialist>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Specialist>().Property(s => s.Specialty).IsRequired().HasMaxLength(25);
            builder.Entity<Specialist>().HasMany(s => s.Sessions).WithOne(ss => ss.Specialist).HasForeignKey(ss => ss.SpecialistId);

            //SubscriptionPlan entity 
            builder.Entity<SubscriptionPlan>().ToTable("subscription_plans");
            builder.Entity<SubscriptionPlan>().HasKey(sp => sp.Id);
            builder.Entity<SubscriptionPlan>().Property(sp => sp.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SubscriptionPlan>().Property(sp => sp.Description).IsRequired().HasMaxLength(50);
            builder.Entity<SubscriptionPlan>().Property(sp => sp.Cost).IsRequired().HasMaxLength(50);

            // Subscription entity

            builder.Entity<Subscription>().ToTable("subscriptions");
            builder.Entity<Subscription>().HasKey(s => new { s.CustomerId, s.SubscriptionPlanId });
            builder.Entity<Subscription>().Property(s => s.StartDate).IsRequired();
            builder.Entity<Subscription>().Property(s => s.ExpiryDate).IsRequired();
            builder.Entity<Subscription>().HasOne(s => s.Customer).WithMany(c => c.Subscriptions).HasForeignKey(s => s.CustomerId);
            builder.Entity<Subscription>().HasOne(s => s.SubscriptionPlan).WithMany(sp => sp.Subscriptions).HasForeignKey(s => s.SubscriptionPlanId);

            // Session entity
            builder.Entity<Session>().ToTable("sessions");
            builder.Entity<Session>().HasKey(ss => ss.Id);
            builder.Entity<Session>().Property(ss => ss.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Session>().Property(ss => ss.Tittle).IsRequired().HasMaxLength(10);
            builder.Entity<Session>().Property(ss => ss.Description).IsRequired().HasMaxLength(50);
            builder.Entity<Session>().Property(ss => ss.StartDate).IsRequired();

            // History entity 

            builder.Entity<History>().ToTable("histories");
            builder.Entity<History>().HasKey(h => new { h.CustomerId, h.SessionId });
            builder.Entity<History>().Property(h=>h.Watched).IsRequired();
            builder.Entity<History>().HasOne(h=>h.Customer).WithMany(c=>c.Histories).HasForeignKey(h=>h.CustomerId);
            builder.Entity<History>().HasOne(h=>h.Session).WithMany(s=>s.Histories).HasForeignKey(h=>h.SessionId);

            // Review entity

            builder.Entity<Review>().ToTable("reviews");
            builder.Entity<Review>().HasKey(r => new { r.CustomerId, r.SpecialistId });
            builder.Entity<Review>().Property(r => r.Description).IsRequired().HasMaxLength(200);
            builder.Entity<Review>().Property(r => r.Rank).IsRequired();
            builder.Entity<Review>().HasOne(r => r.Customer).WithMany(c => c.Reviews).HasForeignKey(r => r.CustomerId);
            builder.Entity<Review>().HasOne(r => r.Specialist).WithMany(s => s.Reviews).HasForeignKey(r => r.SpecialistId);

            builder.ApplySnakeCaseNamingConvention();
        }
        
    }
}
