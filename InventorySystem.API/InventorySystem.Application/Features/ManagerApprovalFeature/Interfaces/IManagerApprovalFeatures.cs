using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.ManagerApprovalFeature.Interfaces
{
    public interface IManagerApprovalFeatures
    {
        public Task<Response> ManagerApproval(ManagerApprovalRequest request, int userId);
        public Task<Response> ManagerApproval(ManagerApprovalUpdateRequest request, int id, int userId);
        public Task<Response> ManagerApproval(int pageNum, int pageSize, int recordType, DateTime? fromDate, DateTime? toDate);
        public Task<Response> ManagerApproval(int id);
    }
}
