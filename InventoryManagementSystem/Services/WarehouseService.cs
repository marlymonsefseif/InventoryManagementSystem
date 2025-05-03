using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class WarehouseService
    {
        private readonly IGeneralRepository<Warehouse> _warehouseRepository;
        public WarehouseService(IGeneralRepository<Warehouse> repository)
        {
            _warehouseRepository = repository;
        }

        public IEnumerable<WarehouseWithProductDto> GetAll()
        {
            return _warehouseRepository.GetAll()
                .Select(w => new WarehouseWithProductDto
                {
                    WarehouseId = w.Id,
                    Name = w.Name,
                    Location = w.Location,
                    ProductName = w.productWarehouses.Select(p => p.Product.Name).ToList()
                }).ToList();
        }

    }
}
