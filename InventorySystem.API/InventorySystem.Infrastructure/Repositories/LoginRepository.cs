using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbContext dbContext;
        public LoginRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<LoginResponse> Login(LoginRequest credentials)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_email", credentials.UserName);
                parameters.Add("_password", credentials.Password);
                return db.Query<LoginResponse>("LoginUser", parameters, commandType: CommandType.StoredProcedure).Single();
            }
        }
    }
}
