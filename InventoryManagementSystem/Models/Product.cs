using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Product : BaseModel
    {
        [Required]
        [MinLength(3,ErrorMessage = "Product name must be at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Low Stock Threshold cannot be negative.")]
        public int LowStockThreshold { get; set; }

        [ForeignKey("category")]
        public int CategoryId { get; set; }

        //Navigation proprety
        public ICollection<InventoryTransaction>? inventoryTransactions { get; set; }
        public ICollection<ProductWarehouse>? productWarehouses { get; set; }
        public Category? category { get; set; }
    }
}
