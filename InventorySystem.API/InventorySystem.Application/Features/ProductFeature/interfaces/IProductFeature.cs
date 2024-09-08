using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.ProductFeature
{
    public interface IProductFeature
    {
        public Task<Response> Product(int id);
        public Task<Response> Product(ProductRequest request, int userId);
        public Task<Response> Product(ProductRequest request, int id, int userid);
        public Task<Response> Product(int id, int userId);
        public Task<Response> TotalCount();
        public Task<Response> Product(int pageNum, int pageSize, string productSku, string productName, string eanCode, int categoryId, int manufacturerId);
    }
}
