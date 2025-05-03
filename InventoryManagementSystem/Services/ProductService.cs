using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class ProductService
    {
        private readonly IGeneralRepository<Product> _productRepository;
        public ProductService(IGeneralRepository<Product> repository)
        {
            _productRepository = repository;
        }

        public async Task Delete(int id)
        {
            Product item = _productRepository.GetById(id);
            _productRepository.Delete(id);
            item.IsDeleted = true;
            await _productRepository.SaveChangesAsync();
        }

        public IQueryable<ShowProductDto> GetAll()
        {
            return _productRepository.GetAll()
                .Select(p => new ShowProductDto
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    });
        }

        public ShowProductDto GetById(int id)
        {
            return _productRepository.Get(p => p.Id == id)
                .Select(p => new ShowProductDto 
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).FirstOrDefault();
        }

        public async Task Insert(AddProductDto entity)
        {
            Product product = new Product
            {
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Quantity = entity.Quantity,
                LowStockThreshold = entity.LowStockThreshold,
                CategoryId = entity.CategoryId
            };
            product.IsDeleted = false;
            _productRepository.Insert(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task Update(int id ,EditProductDto entity)
        {
            Product product = new Product
            {
                Id = id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Quantity = entity.Quantity,
                LowStockThreshold = entity.LowStockThreshold,
                CategoryId = entity.CategoryId,
                IsDeleted = false,
            };
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}
