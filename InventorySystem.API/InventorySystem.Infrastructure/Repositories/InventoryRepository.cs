using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using System.Collections.Generic;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class InventoryRepository : BaseRepository, IInventoryRepository
    {
        private readonly DbContext dbContext;
        public InventoryRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<InventoryResponse> Inventory(int offset, int pageSize, DateTime startDate, DateTime endDate, string productSKU, string sortColumn, string sortOrder, int warehouseId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", offset);
                parameters.Add("_startDate", startDate.ToString("yyyy-MM-dd"));
                parameters.Add("_endDate", endDate.ToString("yyyy-MM-dd"));
                parameters.Add("_productSKU", productSKU);
                parameters.Add("_sortColumn", sortColumn);
                parameters.Add("_sortOrder", sortOrder);
                parameters.Add("_warehouseId", warehouseId);

                //GetInventoryDetail is the previous SP which was used earlier
                var result = db.QueryMultiple("GetInventoryDetails", parameters, commandType: CommandType.StoredProcedure);

                InventoryResponse response = new InventoryResponse();
                response.ProductInformation = result.Read<InventoryProductInformation>().ToList();
                response.TotalInventoryInformation = result.Read<TotalInventoryInformation>().SingleOrDefault();
                response.TotalRecordInformation = result.Read<TotalRecordInformation>().SingleOrDefault();

                return response;
            }
        }

        public async Task<InventoryExcelResponse> GetAllInventory(DateTime startDate, DateTime endDate, string name, int locationId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {

                var parameters = new DynamicParameters();
                parameters.Add("_startDate", startDate.ToString("yyyy-MM-dd"));
                parameters.Add("_endDate", endDate.ToString("yyyy-MM-dd"));
                parameters.Add("_locationId", locationId);
                parameters.Add("_name", name);
                var result = db.QueryMultiple("aaaExcelGetInventoryDetail", parameters, commandType: CommandType.StoredProcedure);
                InventoryExcelResponse excel = new InventoryExcelResponse();
                excel.totalProduct = result.Read<AllProductCount>().SingleOrDefault();
                excel.ProductInformation = result.Read<ExcelResponse>().ToList();
                return excel;
            }

        }

    }
}
