using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class StockAuditResponse
    {
        public List<StockAuditDetail> StockAuditDetail { get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }
    public class StockAuditDetail
    {
        public int id { get; set; } 
        public string WarehouseName { get; set; }
        public DateTime AuditInitiatedOn { get; set; }
        public string UserName { get; set; }
        public string AuditStatus { get; set; }
    }
}
