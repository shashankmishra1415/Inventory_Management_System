using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface IManagerApprovalRepository:IBaseRepository
    {
        public Task<ManagerApprovalResponse> ManagerApproval(int pageNum, int pageSize, int recordType, DateTime? fromDate, DateTime? toDate);
    }
}
