namespace InventoryManagementSystem.DTO
{
    public class TransactionReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
