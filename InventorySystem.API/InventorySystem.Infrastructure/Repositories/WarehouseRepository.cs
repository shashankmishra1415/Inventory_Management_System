using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;
using Dapper;
using System.Data;
using InventorySystem.SharedLayer.Response;

namespace InventorySystem.Infrastructure.Repositories
{
    public class WarehouseRepository : BaseRepository, IWarehouseRepository
    {
        private readonly DbContext dbContext;
        public WarehouseRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<WarehouseListResponse> Warehouse(int pageNum, int pageSize, string locationName, double capacity, int typeId, int statusId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                parameters.Add("_capacity", capacity);
                parameters.Add("_typeId", typeId);
                parameters.Add("_statusId", statusId);
                parameters.Add("_locationName", locationName);
                var list = db.QueryMultiple("GetWarehouseList", parameters, commandType: CommandType.StoredProcedure);
                WarehouseListResponse Response = new WarehouseListResponse();
                Response.WarehouseDetail = list.Read<WarehouseDetail>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }
    }
}