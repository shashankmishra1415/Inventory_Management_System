using Dapper;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly DbContext dbContext;
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserListResponse> User(int pageNum, int pageSize, string name, string mobile, int status ,  int warehouseId,int departmentId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);               
                parameters.Add("_name", name);
                parameters.Add("_departmentId",departmentId);
                parameters.Add("_warehouseId",warehouseId);
                parameters.Add("_mobile", mobile);
                parameters.Add("_status", status);
                var list = db.QueryMultiple("GetUserList", parameters, commandType: CommandType.StoredProcedure);
                UserListResponse Response = new UserListResponse();
                Response.UserDetail = list.Read<UserDetail>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();               
                return Response;
            }
        }

        public async Task<UserDetailResponse> User(SaveUserRequest request, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_name", request.Name);
                parameters.Add("_email", request.Email);
                parameters.Add("_mobile", request.Mobile);
                parameters.Add("_status", request.Status);
                parameters.Add("_wareHouseId", request.WareHouseId);
                parameters.Add("_departmentId", request.DepartmentId);
                return db.Query<UserDetailResponse>("SaveUser", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<UserDetailMobilResponse> UserDetailForMobile(int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_userId", userId);
                var result = db.Query<UserDetailMobilResponse>("UserDetail", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
        }
    }
}
