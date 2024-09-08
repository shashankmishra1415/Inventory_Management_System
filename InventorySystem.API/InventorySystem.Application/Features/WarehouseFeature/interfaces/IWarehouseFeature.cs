using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.Infrastructure.Repositories.Interfaces;

namespace InventorySystem.Application.Features.WarehouseFeature.Interfaces
{
    public interface IWarehouseFeature
    {
        public Task<Response> Warehouse(int pageNum, int pageSize, string locationName, double capacity, int typeId, int statusId);
        public Task<Response> Warehouse(int id);
        public Task<Response> DeleteWarehouse(int id, int userId);
        public Task<Response> Warehouse(WarehouseRequest request, int userId);
        public Task<Response> Warehouse(WarehouseRequest request, int id, int userId);
		public Task<Response> Count();

	}
}
