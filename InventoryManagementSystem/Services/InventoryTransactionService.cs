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

        //public void TransferStock(int productId, int fromWarehouseId, int toWarehouseId, int quantity)
        //{
        //    var fromInventory = _productWarehouseRepository
        //        .Get(pw => pw.ProductId == productId && pw.WarehouseId == fromWarehouseId);

        //    var toInventory = _productWarehouseRepository
        //        .Get(pw => pw.ProductId == productId && pw.WarehouseId == toWarehouseId);

        //    if (fromInventory == null || fromInventory < quantity)
        //        throw new InvalidOperationException("Not enough stock to transfer.");

        //    if (toInventory == null)
        //    {
        //        // Create new record if destination warehouse doesn't have this product yet
        //        toInventory = new ProductWarehouse
        //        {
        //            ProductId = productId,
        //            WarehouseId = toWarehouseId,
        //            Quantity = 0
        //        };
        //        _productWarehouseRepository.Add(toInventory);
        //    }

        //    fromInventory.Quantity -= quantity;
        //    toInventory.Quantity += quantity;

        //    // Log transaction
        //    var transaction = new InventoryTransaction
        //    {
        //        ProductId = productId,
        //        Quantity = quantity,
        //        TransactionType = TransactionType.TransferStock,
        //        TransactionDate = DateTime.Now,
        //        SourceWarehouseId = fromWarehouseId,
        //        DestinationWarehouseId = toWarehouseId
        //    };
        //    _transactionRepository.Add(transaction);

        //    _context.SaveChanges();
        //}


        public IQueryable<TransactionReportDto> GetTransactionReport(int Id)
        {
            return _transactionService.Get(p => p.Product.CategoryId == Id)
                .Select(t => new TransactionReportDto
                {
                    ProductId = t.ProductId,
                    CategoryId = t.Product.CategoryId,
                    ProductName = t.Product.Name,
                    TransactionDate = t.TransactionDate,
                    Quantity = t.Quantity,
                    SourceWarehouseId = t.SourceWarehouseId,
                    DestinationWarehouseId = t.DestinationWarehouseId
                });
        }

    }
}
