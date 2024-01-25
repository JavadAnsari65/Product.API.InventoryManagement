using System.ComponentModel.DataAnnotations;

namespace Product.API.InventoryManagement.Infrastructure.Entities
{
    public class InventoryEntity
    {
        [Key]
        public Guid ProductId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime LastDateUpdate { get; set; } = DateTime.Now;

        // Navigation property
        public List<InventoryDetailsEntity> InventoryDetails { get; set; }
    }
}
