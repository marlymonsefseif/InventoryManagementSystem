using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTO
{
    public class RemoveStockDto
    {
        [Key]
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int SourceWarehouseId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
