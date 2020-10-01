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

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Specialist> Specialist { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<SubscriptionDetail> Subscriptiondetail { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCurrency> Usercurrency { get; set; }
        public virtual DbSet<UserSpecialist> Userspecialist { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("cities");

                entity.HasIndex(e => e.CountryId)
                    .HasName("City_Country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("Country_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Poblation).HasColumnName("poblation");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("City_Country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Continent)
                    .IsRequired()
                    .HasColumnName("continent")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Density).HasColumnName("density");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Poblation).HasColumnName("poblation");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currencies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.UserId)
                    .HasName("Customer_User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customer_User");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("sessions");

                entity.HasIndex(e => e.UserId)
                    .HasName("Session_User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("time");

                entity.Property(e => e.Enable).HasColumnName("enable");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Tittle)
                    .IsRequired()
                    .HasColumnName("tittle")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Session_User");
            });

            modelBuilder.Entity<Specialist>(entity =>
            {
                entity.ToTable("specialists");

                entity.HasIndex(e => e.UserId)
                    .HasName("Specialist_User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .HasColumnName("specialty")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Specialist)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Specialist_User");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("subscriptions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SubscriptionDetail>(entity =>
            {
                entity.ToTable("subscription_details");

                entity.HasIndex(e => e.SubscriptionId)
                    .HasName("suscriptionDetail_Suscription");

                entity.HasIndex(e => e.UserId)
                    .HasName("suscriptionDetail_User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiryDate")
                    .HasColumnType("date");

                entity.Property(e => e.IssueDate)
                    .HasColumnName("issueDate")
                    .HasColumnType("date");

                entity.Property(e => e.SubscriptionId).HasColumnName("Subscription_id");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Subscriptiondetail)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("suscriptionDetail_Suscription");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subscriptiondetail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("suscriptionDetail_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.CityId)
                    .HasName("User_City");

                entity.HasIndex(e => e.CountryId)
                    .HasName("User_Country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.CountryId).HasColumnName("Country_id");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("emailAddress")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(12)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_Country");
            });

            modelBuilder.Entity<UserCurrency>(entity =>
            {
                entity.ToTable("user_currencies");

                entity.HasIndex(e => e.CurrencyId)
                    .HasName("userCurrency_Currency");

                entity.HasIndex(e => e.UserId)
                    .HasName("userCurrency_User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrencyId).HasColumnName("Currency_id");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Usercurrency)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userCurrency_Currency");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Usercurrency)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userCurrency_User");
            });

            modelBuilder.Entity<UserSpecialist>(entity =>
            {
                entity.HasKey(e => e.Userspecialist1)
                    .HasName("PRIMARY");

                entity.ToTable("user_specialists");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("UserSpecialist_Customer");

                entity.HasIndex(e => e.SpecialistId)
                    .HasName("UserSpecialist_Specialist");

                entity.Property(e => e.Userspecialist1).HasColumnName("userspecialist");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_id");

                entity.Property(e => e.SpecialistId).HasColumnName("Specialist_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Userspecialist)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserSpecialist_Customer");

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.Userspecialist)
                    .HasForeignKey(d => d.SpecialistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserSpecialist_Specialist");
            });

            modelBuilder.ApplySnakeCaseNamingConvention();
        }
        
    }
}
