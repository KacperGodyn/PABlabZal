using PABlabZalApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PABlabZalApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rental> Rentals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Car configuration
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MadeBy).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.PricePerDay).IsRequired().HasColumnType("decimal(18,2)");
            });

            // Client configuration
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired();
            });

            // Employee configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Position).IsRequired().HasMaxLength(100);
            });

            // Payment configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaymentDate).IsRequired().HasColumnType("datetime");

                entity.HasOne(e => e.Rental)
                    .WithMany(r => r.Payments)
                    .HasForeignKey(e => e.RentalId);
            });

            // Rental configuration
            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartDate).IsRequired().HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.HasOne(r => r.Car)
                    .WithMany(c => c.Rentals)
                    .HasForeignKey(r => r.CarId);

                entity.HasOne(r => r.Client)
                    .WithMany(c => c.Rentals)
                    .HasForeignKey(r => r.ClientId);

                entity.HasOne(r => r.Employee)
                    .WithMany(e => e.Rentals)
                    .HasForeignKey(r => r.EmployeeId);
            });
        }
    }
}
