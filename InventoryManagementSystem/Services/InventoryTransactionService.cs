using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class InventoryTransactionService
    {
        private readonly IGeneralRepository<InventoryTransaction> _transactionService;
        private readonly IGeneralRepository<ProductWarehouse> _productWarehouseRepository;
        public InventoryTransactionService(IGeneralRepository<InventoryTransaction> repository
            ,IGeneralRepository<ProductWarehouse> otherRepository)
        {
            _transactionService = repository;
            _productWarehouseRepository = otherRepository;
        }

        public async Task InsertStock(AddStockDto stock)
        {
            ProductWarehouse productWarehouse = _productWarehouseRepository
                .SpecificGet(pw => pw.ProductId == stock.ProductId && pw.WarehouseId == stock.DestinationWarehouseId);
            if (productWarehouse != null)
            {
                productWarehouse.Quantity += stock.Quantity;
                _productWarehouseRepository.Update(productWarehouse);
                await _productWarehouseRepository.SaveChangesAsync();
            }
            else
            {
                var newStock = new ProductWarehouse()
                {
                    WarehouseId = stock.DestinationWarehouseId,
                    ProductId = stock.ProductId,
                    Quantity = stock.Quantity,
                };
                _productWarehouseRepository.Insert(newStock);
                await _productWarehouseRepository.SaveChangesAsync();
            }
            InventoryTransaction transaction = new InventoryTransaction
            {
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                DestinationWarehouseId = stock.DestinationWarehouseId,
                UserId = stock.UserId,
                TransactionType = (Models.TransactionType)stock.TransactionType,
                TransactionDate = stock.TransactionDate
            };
            _transactionService.Insert(transaction);
            await _transactionService.SaveChangesAsync();
        }

        public async Task RemoveStock(RemoveStockDto removeStock)
        {
            ProductWarehouse productWarehouse = _productWarehouseRepository
                .SpecificGet(pw => pw.ProductId == removeStock.ProductId && pw.WarehouseId == removeStock.SourceWarehouseId);
            if (productWarehouse != null)
            {
                productWarehouse.Quantity -= removeStock.Quantity;
                _productWarehouseRepository.Update(productWarehouse);
                await _productWarehouseRepository.SaveChangesAsync();
            }

            InventoryTransaction transaction = new InventoryTransaction
            {
                ProductId = removeStock.ProductId,
                Quantity = removeStock.Quantity,
                SourceWarehouseId = removeStock.SourceWarehouseId,
                UserId = removeStock.UserId,
                TransactionType = (Models.TransactionType)removeStock.TransactionType,
                TransactionDate = removeStock.TransactionDate
            };
            _transactionService.Insert(transaction);
            await _transactionService.SaveChangesAsync();
        }

        public IQueryable<TransactionReportDto> GetTransactionReport(int Id)
        {
            return _transactionService.Get(p => p.ProductId == Id)
                .Select(t => new TransactionReportDto
                {
                    ProductId = t.ProductId,
                    ProductName = t.Product.Name,
                    TransactionDate = t.TransactionDate,
                    TransactionType = (DTO.TransactionType)t.TransactionType,
                    Quantity = t.Quantity,
                });
        }

    }
}
