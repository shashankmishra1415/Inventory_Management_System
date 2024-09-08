using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface IInventoryRepository
    {
        public Task<InventoryResponse> Inventory(int offset, int pageSize, DateTime startDate, DateTime endDate, string projectSKU, string sortColumn, string sortOrder, int warehouseId);
        public Task<InventoryExcelResponse> GetAllInventory(DateTime startDate, DateTime endDate, string? name, int locationId);
    }
}
