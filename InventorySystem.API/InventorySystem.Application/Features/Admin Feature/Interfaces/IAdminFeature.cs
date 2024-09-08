using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.Admin_Feature.Interfaces
{
    public interface IAdminFeature
    {
        public Task<Response> StockCountByWarehouse();
        public Task<Response> ReceivedGoodsDetailsByLocation(string monthName, int locationId);
        public Task<Response> DispatchedGoodsDetailsByLocation(string filterMonth, int locationId);
        public Task<Response> InventoryDetailByCategoryForLocation(int warehouseId);
        public Task<Response> InventoryDetailForCategoryOnLocation(int warehouseId, int categoryId);
        public Task<Response> InventoryDetailAtLocation(int locationId); 
        public Task<Response> InventoryDetailByBrandLocation(int locationId);
        public Task<Response> InventoryDetailByVendorForLocation(int locationId);
        public Task<Response> InventoryByBrandLocation(int warehouseId);
        public Task<Response> InventoryDetailByCategoryLocation(int warehouseId);
        public Task<Response> SearchBySerialNumber(string serialNumber);
    }
}
