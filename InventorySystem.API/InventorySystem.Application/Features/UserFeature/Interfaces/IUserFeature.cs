using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.UserFeature.Interfaces
{
    public interface IUserFeature
    {
        public Task<Response> User(int id);
        public Task<Response> DeleteUser(int id, int userId);
        public Task<Response> User(int pageNum, int pageSize, string name, string mobile, int status, int warehouseId, int departmentId);
        public Task<Response> User(SaveUserRequest request, int userId);
        public Task<Response> User(EditUserRequest request, int id, int userid);
        public Task<Response> UpdateLoginUser(UpdateUserLogin request, int id, int userid);
        public Task<Response> UserDetailForMobile(int userid);
        public Task<Response> UserProfileDetail(int userid);
        public Task<Response> EditUserProfile(EditUserProfileRequest request, int Id);
        public Task<Response> ResetPassword(ResetPasswordRequest request, int id);
        public Task<Response> Count();
    }
}
