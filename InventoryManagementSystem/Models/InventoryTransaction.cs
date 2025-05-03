using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public enum TransactionType
    {
        AddStock,
        RemoveStock,
        TransferStock
    }
    public class InventoryTransaction : BaseModel
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }

        [ForeignKey("FromWarehouse")]
        public int? SourceWarehouseId { get; set; }
        [ForeignKey("ToWarehouse")]
        public int? DestinationWarehouseId { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }

        //Navigation Proprety
        public Product? Product { get; set; }
        public Warehouse? FromWarehouse { get; set; }
        public Warehouse? ToWarehouse { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
