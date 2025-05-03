using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;
        private readonly ProductWarehouseService _productWarehouseService;
        public WarehouseController(WarehouseService warehouseService,ProductWarehouseService productWarehouseService)
        {
            _warehouseService = warehouseService;
            _productWarehouseService = productWarehouseService;
        }

        [HttpGet("/WarehouseWithProducts")]
        public IActionResult GetAll()
        {
            IEnumerable<WarehouseWithProductDto> warehouses = _warehouseService.GetAll();
            return Ok(warehouses);
        }
    }
}
