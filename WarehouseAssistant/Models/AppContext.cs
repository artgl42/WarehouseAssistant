using Microsoft.EntityFrameworkCore;
using System;

namespace WarehouseAssistant.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public AppContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                    .HasOne(x => x.WarehouseFrom)
                    .WithMany(x => x.TransactionFrom)
                    .HasForeignKey(x => x.WarehouseFromId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Transaction>()
                        .HasOne(x => x.WarehouseIn)
                        .WithMany(x => x.TransactionIn)
                        .HasForeignKey(x => x.WarehouseInId)
                        .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" },
                new Product { Id = 3, Name = "Product 3" },
                new Product { Id = 4, Name = "Product 4" },
                new Product { Id = 5, Name = "Product 5" },
                new Product { Id = 6, Name = "Product 6" },
                new Product { Id = 7, Name = "Product 7" },
                new Product { Id = 8, Name = "Product 8" },
                new Product { Id = 9, Name = "Product 9" },
                new Product { Id = 10, Name = "Product 10" }
            );

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { Id = 1, Name = "From outside" },
                new Warehouse { Id = 2, Name = "To outside" },
                new Warehouse { Id = 3, Name = "Warehouse 1" },
                new Warehouse { Id = 4, Name = "Warehouse 2" },
                new Warehouse { Id = 5, Name = "Warehouse 3" }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, WarehouseInId = 3, WarehouseFromId = 1, ProductId = 1, Count = 100, DateTime = DateTime.Parse("2022-04-10") },
                new Transaction { Id = 2, WarehouseInId = 3, WarehouseFromId = 1, ProductId = 2, Count = 200, DateTime = DateTime.Parse("2022-04-10") },
                new Transaction { Id = 3, WarehouseInId = 3, WarehouseFromId = 1, ProductId = 3, Count = 300, DateTime = DateTime.Parse("2022-04-10") },
                new Transaction { Id = 4, WarehouseInId = 3, WarehouseFromId = 1, ProductId = 4, Count = 400, DateTime = DateTime.Parse("2022-04-10") },
                new Transaction { Id = 5, WarehouseInId = 3, WarehouseFromId = 1, ProductId = 1, Count = 10, DateTime = DateTime.Parse("2022-04-10") },
                new Transaction { Id = 6, WarehouseInId = 4, WarehouseFromId = 3, ProductId = 2, Count = 20, DateTime = DateTime.Parse("2022-04-11") },
                new Transaction { Id = 7, WarehouseInId = 3, WarehouseFromId = 4, ProductId = 2, Count = -20, DateTime = DateTime.Parse("2022-04-11") }
            );
        }
    }
}
