using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface IStockAuditRepository : IBaseRepository
    {
        public Task<Response> MarkAuditComplete(int auditId, int categoryId, string serialNumber, int userId);
        public Task<StockAuditResponse> StockAudit(int pageNum, int pageSize, int? warehouseId, DateTime? fromDate, DateTime? toDate, int? userId, int? status);
        public Task<StockAuditListResponse> StockAudit(int id, int pageNum, int pageSize, string? ProductSKU, int? ManufacturerName, int? CategoryName);
        public Task<List<StockAuditList>> StockAudit(int id);
        public Task<StockAuditDetail> StockAuditLocation(int id);
    }
}
