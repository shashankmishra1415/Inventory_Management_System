using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.InventoryFeatures.interfaces
{
    public interface IInventoryFeature
    {
        public Task<Response> Inventory(int pageNum, int pageSize, DateTime startDate, DateTime endDate, string productSKU, string sortColumn, string sortOrder, int warehouseId);

        public Task<byte[]?> ConvertInventoryToExcel(DateTime startDate, DateTime endDate, string? name, int locationId);

    }
}
