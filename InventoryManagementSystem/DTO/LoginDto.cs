using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTO
{
    public class LoginDto
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
