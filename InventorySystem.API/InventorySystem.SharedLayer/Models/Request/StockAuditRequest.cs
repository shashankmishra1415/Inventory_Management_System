
namespace InventorySystem.SharedLayer.Models.Request
{
    public class StockAuditRequest
    {
        public int warehouseId { get; set; }
        public DateTime auditInitiatedOn { get; set; }
    }
}
