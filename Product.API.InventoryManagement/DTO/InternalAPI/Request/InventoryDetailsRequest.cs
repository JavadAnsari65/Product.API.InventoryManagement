namespace Product.API.InventoryManagement.DTO.InternalAPI.Request
{
    public class InventoryDetailsRequest
    {
        public Guid ProductId { get; set; }
        public bool IsBuy { get; set; } = false;
        public bool IsSell { get; set; } = false;
        public int Quantity { get; set; }
    }
}
