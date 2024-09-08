namespace InventorySystem.SharedLayer.Models.Response
{
    public class StockAuditByWarehouseIdResponse
    {
        public int AuditId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public int TotalAuditDoneCount { get; set; }
    }
}
