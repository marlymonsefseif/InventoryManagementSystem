using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
    public class InventoryDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryTransaction> Transactions { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ProductWarehouse> productWarehouse { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }


        public InventoryDbContext() { }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryTransaction>()
                .Property(t => t.TransactionType)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

        }
    }
}
