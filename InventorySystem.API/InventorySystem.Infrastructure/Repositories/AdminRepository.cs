using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        private readonly DbContext dbContext;
        public AdminRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GoodsDetailsByLocation>> ReceivedGoodsDetailsByLocation(string monthName, int locationId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_filterMonth", monthName);
                parameters.Add("_locationId", locationId);
                return db.Query<GoodsDetailsByLocation>("GetReceivedGoodsDetailsByMonthByLocationId", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<GoodsDetailsByLocation>> DispatchedGoodsDetailsByLocation(string filterMonth, int locationId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_filterMonth", filterMonth);
                parameters.Add("_locationId", locationId);
                return db.Query<GoodsDetailsByLocation>("GetDispatchedGoodsDetailsByMonthByLocationId", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<InventoryDetailByCategoryForLocationResponse>> InventoryDetailByCategoryForLocation(int warehouseId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_warehouseId", warehouseId);
                return db.Query<InventoryDetailByCategoryForLocationResponse>("GetInventoryDetailByCategoryForLocation", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<InventoryDetailForCategoryOnLocationResponse>> InventoryDetailForCategoryOnLocation(int warehouseId, int categoryId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_warehouseId", warehouseId);
                parameters.Add("_categoryId", categoryId);
                return db.Query<InventoryDetailForCategoryOnLocationResponse>("GetInventoryDetailForCategoryOnLocation", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<InventoryDetailResponseMobile> InventoryDetailByLocation<T>(int locationId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("_warehouseId", locationId);
                var result = db.QueryMultiple("GetInventoryDetailAtLocation", parameters, commandType: CommandType.StoredProcedure);
                InventoryDetailResponseMobile inventoryDetailResponseMobile = new InventoryDetailResponseMobile();
                inventoryDetailResponseMobile.stock = result.Read<TotalStock>().SingleOrDefault();
                inventoryDetailResponseMobile.inventoryDetail = result.Read<InventoryDetail>().SingleOrDefault();
                return inventoryDetailResponseMobile;
            }
        }

        public async Task<List<InventoryDetailByBrandResponse>> InventoryDetailByBrandLocation<T>(int locationId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_warehouseId", locationId);
                return db.Query<InventoryDetailByBrandResponse>("GetInventoryDetailByBrandForLocation", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<GetInventoryDetailByBrandResponseJSON>> InventoryDetailByVendorForLocation<T>(int locationId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("_warehouseId", locationId);
                return db.Query<GetInventoryDetailByBrandResponseJSON>("GetInventoryDetailByVendorForLocation", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<GetInventoryDetailByBrandLocationJSON>> InventoryByBrandLocation(int warehouseId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_warehouseId", warehouseId);
                var result = db.Query<GetInventoryDetailByBrandLocationJSON>("GetInventoryDetailByBrandForLocation", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public async Task<List<GetInventoryDetailByCategoryForLocationJson>> InventoryDetailByCategoryLocation(int warehouseId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_warehouseId", warehouseId);
                var result = db.Query<GetInventoryDetailByCategoryForLocationJson>("GetInventoryDetailByCategoryForLocation", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }
        public async Task<GlobalSearchWebResponse> SearchBySerialNumber(string serialNumber)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_serialNumber", serialNumber);
                var list = db.QueryMultiple("GlobalSearchWeb", parameters, commandType: CommandType.StoredProcedure);
                GlobalSearchWebResponse Response = new GlobalSearchWebResponse();
                Response.globalSearchWebDetail = list.Read<GlobalSearchWebDetail>().SingleOrDefault();
                Response.globalSearchWebStatus = list.Read<GlobalSearchWebStatus>().ToList();
                return Response;
            }
        }
    }
}

