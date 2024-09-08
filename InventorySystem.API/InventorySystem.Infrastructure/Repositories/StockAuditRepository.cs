using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class StockAuditRepository : BaseRepository, IStockAuditRepository
    {
        private readonly DbContext dbContext;
        public StockAuditRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> MarkAuditComplete(int auditId, int categoryId, string serialNumber, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_auditId", auditId);
                parameters.Add("_categoryId", categoryId);
                parameters.Add("_serialNumber", serialNumber);
                parameters.Add("_userId", userId);
                return db.Query<Response>("MarkAuditCompleteForSerialNumber", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<StockAuditResponse> StockAudit(int pageNum, int pageSize, int? warehouseId, DateTime? fromDate, DateTime? toDate, int? userId, int? status)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                parameters.Add("_warehouseId", warehouseId);
                parameters.Add("_fromDate", fromDate?.ToString("yyyy-MM-dd"));
                parameters.Add("_toDate", toDate?.ToString("yyyy-MM-dd"));
                parameters.Add("_userId", userId);
                parameters.Add("_status", status);
                var list = db.QueryMultiple("GetStockAuditLocation", parameters, commandType: CommandType.StoredProcedure);
                StockAuditResponse Response = new StockAuditResponse();
                Response.StockAuditDetail = list.Read<StockAuditDetail>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }

        public async Task<StockAuditListResponse> StockAudit(int id, int pageNum, int pageSize, string? productSKU, int? manufacturerName, int? categoryName)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                parameters.Add("_ProductSKU", productSKU);
                parameters.Add("_ManufacturerName", manufacturerName);
                parameters.Add("_CategoryName", categoryName);
                var list = db.QueryMultiple("GetStockAuditDetails", parameters, commandType: CommandType.StoredProcedure);
                StockAuditListResponse Response = new StockAuditListResponse();
                Response.StockAuditList = list.Read<StockAuditList>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }

        public async Task<List<StockAuditList>> StockAudit(int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                var result = await db.QueryAsync<StockAuditList>("GetStockAuditDetailsById", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<StockAuditDetail> StockAuditLocation(int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                return db.Query<StockAuditDetail>("GetStockAuditLocationById", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
    }
}
