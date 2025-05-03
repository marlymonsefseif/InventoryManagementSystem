namespace InventoryManagementSystem.DTO
{
    public class WarehouseWithProductDto
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? ProductId { get; set; }
        public List<string> ProductName { get; set; }
    }
}
