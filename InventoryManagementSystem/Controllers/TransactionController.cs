using System.Threading.Tasks;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly InventoryTransactionService _transactionService;
        public TransactionController(InventoryTransactionService inventoryTransactionService)
        {
            _transactionService = inventoryTransactionService;
        }

        [HttpPost("/AddStock")]
        public async Task<IActionResult> AddStock(AddStockDto stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _transactionService.InsertStock(stock);
            return Ok("Inserted Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/RemoveStock")]
        public async Task<IActionResult> RemoveStock(RemoveStockDto stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _transactionService.RemoveStock(stock);
            return Ok("Removed Successfully");
        }

        [HttpGet("{ProductId:int}/{FromWarehouseId:int}/{ToWarehouseId:int}/{Quantity:int}")]
        public IActionResult TransferStock(int ProductId, int FromWarehouseId, int ToWarehouseId, int Quantity)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = _transactionService.TransferStock(ProductId, FromWarehouseId, ToWarehouseId, Quantity);
            return Ok("Transfered Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/TransactionReport/{id:int}")]
        public IActionResult TransactionReport(int id)
        {
            var transactionReport = _transactionService.GetTransactionReport(id);
            return Ok(transactionReport);
        }
    }
}
