using Microsoft.EntityFrameworkCore;

namespace WarehouseAssistant.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public AppContext(DbContextOptions options) : base(options) { }
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
        }
    }
}
