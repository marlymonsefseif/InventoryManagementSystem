using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly ProductWarehouseService _productWarehouseService;
        public ProductWarehouseController(ProductWarehouseService productWarehouseService)
        {
            _productWarehouseService = productWarehouseService;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{warehouseId:int}/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int warehouseId, int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productWarehouseService.DeleteProduct(warehouseId, productId);
            return Ok("Deleted Successfully");
        }
    }
}
