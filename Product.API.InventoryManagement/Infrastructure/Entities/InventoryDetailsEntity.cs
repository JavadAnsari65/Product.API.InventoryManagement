using System.ComponentModel.DataAnnotations;

namespace Product.API.InventoryManagement.Infrastructure.Entities
{
    public class InventoryDetailsEntity
    {
        [Key]
        public int FactorId { get; set; }
        public Guid ProductId { get; set; }
        public bool IsBuy { get; set; } = false;
        public bool IsSell { get; set; } = false;
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }= DateTime.Now;

        // Navigation property
        public InventoryEntity Inventory { get; set; }

    }
}
