using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PABlabZalApi.Core.Entities;
using PABlabZalApi.Infrastructure.Data;

namespace PABlabZalApi.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Cars.Any())
            {
                SeedCars(context);
            }

            if (!context.Clients.Any())
            {
                SeedClients(context);
            }

            if (!context.Employees.Any())
            {
                SeedEmployees(context);
            }

            if (!context.Payments.Any())
            {
                SeedPayments(context);
            }

            if (!context.Rentals.Any())
            {
                SeedRentals(context);
            }
        }

        private static void SeedCars(AppDbContext context)
        {
            var cars = new[]
            {
                new Car { MadeBy = "Ford", Model = "Mustang", LicensePlate = "KSA1221", PricePerDay = 50.00m },
                new Car { MadeBy = "Toyota", Model = "Camry", LicensePlate = "ABC1234", PricePerDay = 40.00m },
                new Car { MadeBy = "Honda", Model = "Civic", LicensePlate = "XYZ5678", PricePerDay = 45.00m },
                new Car { MadeBy = "Chevrolet", Model = "Cruze", LicensePlate = "DEF9876", PricePerDay = 42.00m },
                new Car { MadeBy = "BMW", Model = "X5", LicensePlate = "PQR4321", PricePerDay = 70.00m },
                new Car { MadeBy = "Audi", Model = "A4", LicensePlate = "GHI2468", PricePerDay = 65.00m },
                new Car { MadeBy = "Mercedes-Benz", Model = "C-Class", LicensePlate = "MNO1357", PricePerDay = 75.00m }
            };

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void SeedClients(AppDbContext context)
        {
            var clients = new[]
            {
                new Client { Name = "John", Surname = "Doe", PhoneNumber = 123456789 },
                new Client { Name = "Alice", Surname = "Smith", PhoneNumber = 987654321 },
                new Client { Name = "Michael", Surname = "Johnson", PhoneNumber = 555444333 },
                new Client { Name = "Emily", Surname = "Brown", PhoneNumber = 111222333 },
                new Client { Name = "William", Surname = "Wilson", PhoneNumber = 999888777 },
                new Client { Name = "Sophia", Surname = "Miller", PhoneNumber = 666777888 },
                new Client { Name = "Daniel", Surname = "Garcia", PhoneNumber = 333222111 }
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
        }

        private static void SeedEmployees(AppDbContext context)
        {
            var employees = new[]
            {
                new Employee { Name = "Jane", Position = "Manager" },
                new Employee { Name = "Bob", Position = "Sales Associate" }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        private static void SeedPayments(AppDbContext context)
        {
            var payments = new[]
            {
                new Payment { Amount = 100.00m, PaymentDate = DateTime.UtcNow.AddDays(-5), RentalId = 1 },
                new Payment { Amount = 80.00m, PaymentDate = DateTime.UtcNow.AddDays(-4), RentalId = 2 },
                new Payment { Amount = 120.00m, PaymentDate = DateTime.UtcNow.AddDays(-3), RentalId = 3 },
                new Payment { Amount = 90.00m, PaymentDate = DateTime.UtcNow.AddDays(-2), RentalId = 4 },
                new Payment { Amount = 110.00m, PaymentDate = DateTime.UtcNow.AddDays(-3), RentalId = 5 },
                new Payment { Amount = 95.00m, PaymentDate = DateTime.UtcNow.AddDays(-2), RentalId = 6 },
                new Payment { Amount = 105.00m, PaymentDate = DateTime.UtcNow.AddDays(-1), RentalId = 7 }
            };

            context.Payments.AddRange(payments);
            context.SaveChanges();
        }

        private static void SeedRentals(AppDbContext context)
        {
            var rentals = new[]
            {
                new Rental
                {
                    StartDate = DateTime.UtcNow.AddDays(-10),
                    EndDate = DateTime.UtcNow.AddDays(-8),
                    ClientId = 1,
                    CarId = 1,
                    EmployeeId = 1,
                    Payments = new List<Payment>
                    {
                        new Payment { Amount = 50.00m, PaymentDate = DateTime.UtcNow.AddDays(-9) }
                    }
                },
                new Rental
                {
                    StartDate = DateTime.UtcNow.AddDays(-12),
                    EndDate = DateTime.UtcNow.AddDays(-9),
                    ClientId = 2,
                    CarId = 2,
                    EmployeeId = 2,
                    Payments = new List<Payment>
                    {
                        new Payment { Amount = 40.00m, PaymentDate = DateTime.UtcNow.AddDays(-10) }
                    }
                },
                new Rental
                {
                    StartDate = DateTime.UtcNow.AddDays(-8),
                    EndDate = DateTime.UtcNow.AddDays(-6),
                    ClientId = 3,
                    CarId = 3,
                    EmployeeId = 1,
                    Payments = new List<Payment>
                    {
                        new Payment { Amount = 55.00m, PaymentDate = DateTime.UtcNow.AddDays(-7) }
                    }
                },
                new Rental
                {
                    StartDate = DateTime.UtcNow.AddDays(-7),
                    EndDate = DateTime.UtcNow.AddDays(-5),
                    ClientId = 4,
                    CarId = 4,
                    EmployeeId = 2,
                    Payments = new List<Payment>
                    {
                        new Payment { Amount = 42.00m, PaymentDate = DateTime.UtcNow.AddDays(-6) }
                    }
                },
                new Rental
                {
                    StartDate = DateTime.UtcNow.AddDays(-6),
                    EndDate = DateTime.UtcNow.AddDays(-3),
                    ClientId = 5,
                    CarId = 5,
                    EmployeeId = 1,
                    Payments = new List<Payment>
                    {
                        new Payment { Amount = 68.00m, PaymentDate = DateTime.UtcNow.AddDays(-4) }
                    }
                },
                new Rental
                {
                    StartDate = DateTime.UtcNow.AddDays(-9),
                    EndDate = DateTime.UtcNow.AddDays(-7),
                    ClientId = 6,
                    CarId = 6,
                    EmployeeId = 2,
                    Payments = new List<Payment>
                    {
                        new Payment { Amount = 60.00m, PaymentDate = DateTime.UtcNow.AddDays(-8) }
                    }
                },
                new Rental
                {
                    StartDate = DateTime.UtcNow.AddDays(-5),
                    EndDate = DateTime.UtcNow.AddDays(-2),
                    ClientId = 7,
                    CarId = 7,
                    EmployeeId = 1,
                    Payments = new List<Payment>
                    {
                        new Payment { Amount = 72.00m, PaymentDate = DateTime.UtcNow.AddDays(-3) }
                    }
                }
            };

            context.Rentals.AddRange(rentals);
            context.SaveChanges();
        }
    }
}