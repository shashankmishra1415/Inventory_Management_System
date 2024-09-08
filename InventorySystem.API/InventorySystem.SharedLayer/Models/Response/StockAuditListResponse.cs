using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class StockAuditListResponse
    {
        public List<StockAuditList> StockAuditList{ get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }
    public class StockAuditList
    {
        public int ProductId { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public string ManufacturerName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int Scan { get; set; }
    }
}

