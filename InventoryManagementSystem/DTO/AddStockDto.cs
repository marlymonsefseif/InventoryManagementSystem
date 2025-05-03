using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTO
{
    public enum TransactionType
    {
        AddStock,
        RemoveStock,
        TransferStock
    }
    public class AddStockDto
    {
        [Key]
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int DestinationWarehouseId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
