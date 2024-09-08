using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
namespace InventorySystem.Application.Features.StockAuditFeature.interfaces
{
    public interface IStockAuditFeature
    {
        public Task<Response> StockAudit(StockAuditRequest request, int userId);
        public Task<Response> StockAuditByWarehouseId(int id);
        public Task<Response> MarkAuditComplete(MarkAuditCompleteRequest request, int id, int userid);
        public Task<Response> MarkAuditComplete(int auditId, int categoryId, string serialNumber, int userId);
        public Task<Response> StockAudit(int pageNum, int pageSize, int? warehouseId, DateTime? fromDate, DateTime? toDate, int? userId, int? status);
        public Task<Response> StockAudit(int id, int pageNum, int pageSize, string? ProductSKU, int? ManufacturerName, int? CategoryName);
        public Task<Response> StockAudit(int id);
        public Task<Response> StockAuditLocation(int id);
        public Task<Response> TotalCount();
    }
}
