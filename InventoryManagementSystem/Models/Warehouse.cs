using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Warehouse : BaseModel
    {
        public string Name { get; set; }
        public string? Location { get; set; }


        //Navigation property
        public ICollection<ProductWarehouse>? productWarehouses { get; set; }
        [InverseProperty("FromWarehouse")]
        public ICollection<InventoryTransaction>? FromWarehouseTransfers { get; set; }
        [InverseProperty("ToWarehouse")]
        public ICollection<InventoryTransaction>? ToWarehouseTransfers { get; set; }
    }
}
