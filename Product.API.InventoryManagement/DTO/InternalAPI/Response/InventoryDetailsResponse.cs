namespace Product.API.InventoryManagement.DTO.InternalAPI.Response
{
    public class InventoryDetailsResponse
    {
        public Guid ProductId { get; set; }
        public bool IsBuy { get; set; } = false;
        public bool IsSell { get; set; } = false;
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
