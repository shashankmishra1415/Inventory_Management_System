using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.VendorFeature.Interfaces
{
    public interface IVendorFeature
    {
        public Task<Response> Vendor(int pageNum, int pageSize, string? companyName, string contactName, int typeId, int vendorTypeId, int statusId);
        public Task<Response> Vendor(int id);
        public Task<Response> Vendor(int id, int userId);
        public Task<Response> Vendor(VendorRequest request, int userId);
        public Task<Response> Vendor(VendorRequest request, int id, int userid);
        public Task<Response> VendorCount();
    }
}