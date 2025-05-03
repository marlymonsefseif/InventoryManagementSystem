using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.DTO
{
    public class ShowProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
