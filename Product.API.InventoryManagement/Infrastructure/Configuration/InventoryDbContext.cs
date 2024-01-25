using Microsoft.EntityFrameworkCore;
using Product.API.InventoryManagement.Infrastructure.Entities;

namespace Product.API.InventoryManagement.Infrastructure.Configuration
{
    public class InventoryDbContext:DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options):base(options)
        {
               
        }

        public DbSet<InventoryEntity> InventoryProducts { get; set; }
        public DbSet<InventoryDetailsEntity> InventoryDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryDetailsEntity>()
                .HasOne(x => x.Inventory)
                .WithMany(y => y.InventoryDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Product.InventoryManagement;Integrated Security=True; MultipleActiveResultSets=True; Trust Server Certificate=True");
            }
        }
    }
}
