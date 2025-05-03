using System.Threading.Tasks;
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
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ProductWarehouseService _productWarehouseService;
        public ProductController(ProductService productService, ProductWarehouseService productWarehouseService)
        {
            _productService = productService;
            _productWarehouseService = productWarehouseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ShowProductDto> products = _productService.GetAll().ToList();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            ShowProductDto Product = _productService.GetById(id);
            if (Product == null) 
                return NotFound();
            return Ok(Product);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/ProductReport")]
        public IActionResult GetProductReport()
        {
            IEnumerable<ProductReportDto> products = _productWarehouseService.GetProductReport().ToList();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(AddProductDto product)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            await _productService.Insert(product);
            return Ok("Inserted Successfully");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, EditProductDto productFromReq)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.Update(id, productFromReq);
            return Ok("Updated Successfully");
        }
    }
}
