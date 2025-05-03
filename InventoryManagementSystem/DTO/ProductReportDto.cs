namespace InventoryManagementSystem.Models
{
    public class ProductReportDto
    {
        public int ProductId { get; set; }  
        public string ProductName { get; set; }
        public int ProductLowStockThreshold { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int QuantityWarehouse { get; set; }
        public string CategoryName { get; set; }
    }
}
