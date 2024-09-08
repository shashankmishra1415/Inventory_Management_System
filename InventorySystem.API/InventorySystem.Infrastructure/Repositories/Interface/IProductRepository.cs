using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface IProductRepository : IBaseRepository
    {
        public Task<ProductListResponse> Product(int pageNum, int pageSize, string productSku, string productName, string eanCode, int categoryId, int manufacturerId);
    }
}
