using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.ConfigurationFeature.Interfaces
{
    public interface IConfigurationFeature
    {
        public Task<Response> Category();
        public Task<Response> Category(int id);
        public Task<Response> Category(CategoryTypeRequest request, int userId);
        public Task<Response> Category(CategoryTypeRequest request, int id, int userid);
        public Task<Response> Category(int id, int userId);
        public Task<Response> Manufacturer();
        public Task<Response> Manufacturer(int id);
        public Task<Response> Manufacturer(ManufacturerTypeRequest request, int userId);
        public Task<Response> Manufacturer(ManufacturerTypeRequest request, int id, int userId);
        public Task<Response> Manufacturer(int id, int userId);
    }
}
