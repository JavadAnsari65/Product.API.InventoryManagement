namespace Product.API.InventoryManagement.DTO.InternalAPI.Response
{
    public class InventoryResponse
    {
        public Guid ProductId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime LastDateUpdate { get; set; }
    }
}
