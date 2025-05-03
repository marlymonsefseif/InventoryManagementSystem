using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<InventoryTransaction> Transactions { get; set; }
    }
}
