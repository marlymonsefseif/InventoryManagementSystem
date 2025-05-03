using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class ProductWarehouseService
    {
        private readonly IGeneralRepository<ProductWarehouse> _productWarehouseRepository;
        public ProductWarehouseService(IGeneralRepository<ProductWarehouse> repository)
        {
            _productWarehouseRepository = repository;
        }

        public async Task DeleteProduct(int warehouseId, int productId)
        {
            ProductWarehouse productWarehouse = _productWarehouseRepository
                .SpecificGet(pw => pw.WarehouseId == warehouseId && pw.ProductId == productId);
            if (productWarehouse != null)
            {
                productWarehouse.IsDeleted = true;
                _productWarehouseRepository.Update(productWarehouse);
                await _productWarehouseRepository.SaveChangesAsync();
            } 
        }

        public IQueryable<ProductReportDto> GetProductReport()
        {

            return _productWarehouseRepository.GetAll()
                .Where(q => q.Quantity < q.Product.LowStockThreshold)
                .Select(r => new ProductReportDto
                {
                    ProductId = r.ProductId,
                    ProductName = r.Product.Name,
                    ProductLowStockThreshold = r.Product.LowStockThreshold,
                    WarehouseId = r.WarehouseId,
                    WarehouseName = r.Warehouse.Name,
                    QuantityWarehouse = r.Quantity,
                    CategoryName = r.Product.category.Name
                });

        }
    }
}
