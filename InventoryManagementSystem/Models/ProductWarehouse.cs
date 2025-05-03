using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class ProductWarehouse : BaseModel
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }

        //Navigation Property
        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
