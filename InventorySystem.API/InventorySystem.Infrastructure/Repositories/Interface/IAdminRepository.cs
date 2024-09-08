using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface IAdminRepository : IBaseRepository
    {
        public Task<List<GoodsDetailsByLocation>> ReceivedGoodsDetailsByLocation(string monthName, int locationId);
        public Task<List<GoodsDetailsByLocation>> DispatchedGoodsDetailsByLocation(string filterMonth, int locationId);
        public Task<List<InventoryDetailByCategoryForLocationResponse>> InventoryDetailByCategoryForLocation(int warehouseId);
        public Task<List<InventoryDetailForCategoryOnLocationResponse>> InventoryDetailForCategoryOnLocation(int warehouseId, int categoryId);
        public Task<InventoryDetailResponseMobile> InventoryDetailByLocation<T>(int locationId);
        public Task<List<InventoryDetailByBrandResponse>> InventoryDetailByBrandLocation<T>(int locationId);
        public Task<List<GetInventoryDetailByBrandResponseJSON>> InventoryDetailByVendorForLocation<T>(int locationId);
        public Task<GlobalSearchWebResponse> SearchBySerialNumber(string serialNumber);       
        public Task<List<GetInventoryDetailByBrandLocationJSON>> InventoryByBrandLocation(int warehouseId);
        public Task<List<GetInventoryDetailByCategoryForLocationJson>> InventoryDetailByCategoryLocation(int warehouseId);
    }
}
