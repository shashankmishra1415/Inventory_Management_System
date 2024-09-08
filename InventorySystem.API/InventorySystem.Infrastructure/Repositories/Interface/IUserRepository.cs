using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        public Task<UserListResponse> User(int pageNum, int pageSize, string name, string mobile, int status, int warehouseId,int departmentId);
        public Task<UserDetailResponse> User(SaveUserRequest request , int userId);
        public Task<UserDetailMobilResponse> UserDetailForMobile(int userId); 
    }
}