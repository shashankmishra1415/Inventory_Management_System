using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface IVendorRepository
    {
        public Task<VendorListResponse> Vendor(int pageNum, int pageSize, string? companyName, string contactName, int typeId, int vendorTypeId, int statusId);
    }
}