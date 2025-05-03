using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.DTO
{
    public class TransactionReportDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }
        public int? SourceWarehouseId { get; set; }
        public int? DestinationWarehouseId { get; set; }
    }
}
