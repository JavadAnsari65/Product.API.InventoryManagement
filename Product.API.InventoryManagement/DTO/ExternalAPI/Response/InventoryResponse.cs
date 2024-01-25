namespace Product.API.InventoryManagement.DTO.ExternalAPI.Response
{
    public class InventoryResponse
    {
        public Guid ProductId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime LastDateUpdate { get; set; }
    }
}
