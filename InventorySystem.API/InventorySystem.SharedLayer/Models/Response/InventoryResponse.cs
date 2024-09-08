namespace InventorySystem.SharedLayer.Models.Response
{
    public class InventoryResponse
    {
        public TotalInventoryInformation TotalInventoryInformation { get; set; }
        public TotalRecordInformation TotalRecordInformation { get; set; }
        public List<InventoryProductInformation> ProductInformation { get; set; }

    }

    public class TotalInventoryInformation
    {
        public int ReceivedCount { get; set; }
        public int SalesOrderPendingCount { get; set; }
        public int SalesOrderDispatchCount { get; set; }
        public int StockDamageCount { get; set; }
        public int StockReturnCount { get; set; }
        public int StockQuantity { get; set; }
        public decimal StockValue { get; set; }
    }

    public class TotalRecordInformation
    {
        public int TotalRecord { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
    }

    public class InventoryProductInformation
    {
        public string? ProductSKU { get; set; }
        public string? Product { get; set; }
        public int ProductId { get; set; }
        public int ReceivedCount { get; set; }
        public int SalesOrderPendingCount { get; set; }
        public int SalesOrderDispatchCount { get; set; }
        public int StockDamageCount { get; set; }
        public int StockReturnCount { get; set; }
        public int StockQuantity { get; set; }
        public decimal StockValue { get; set; }
    }
}
