using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTO
{
    public class RegisterDto
    {
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters.")]
        [MaxLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        public string FullName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
