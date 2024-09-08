using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using System.Data;
using System.Text;

namespace InventorySystem.Infrastructure.Repositories
{
    public class ManagerApprovalRepository: BaseRepository,IManagerApprovalRepository
    {
        private readonly DbContext dbContext;
        public ManagerApprovalRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ManagerApprovalResponse> ManagerApproval(int pageNum, int pageSize, int recordType, DateTime? fromDate, DateTime? toDate)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                parameters.Add("_recordType", recordType);
                parameters.Add("_fromDate", fromDate);
                parameters.Add("_toDate", toDate);
                var result = db.QueryMultiple("GetReturnAndDamageManagerApprovalRecord", parameters, commandType: CommandType.StoredProcedure);

                ManagerApprovalResponse Response = new ManagerApprovalResponse();

                Response.ManagerApprovalList = result.Read<ManagerApproval>().ToList();
                Response.PaginationResponses = result.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }
    }
}
