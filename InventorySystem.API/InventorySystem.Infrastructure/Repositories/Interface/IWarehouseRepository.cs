using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interfaces
{
    public interface IWarehouseRepository : IBaseRepository
    {
        public Task<WarehouseListResponse> Warehouse(int pageNum, int pageSize, string locationName, double capacity, int typeId, int statusId);
    }
}