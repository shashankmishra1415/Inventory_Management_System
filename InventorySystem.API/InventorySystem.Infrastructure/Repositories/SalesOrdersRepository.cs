using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using System.Data;
using static InventorySystem.SharedLayer.Models.Response.ItemsResponse;

namespace InventorySystem.Infrastructure.Repositories
{
    public class SalesOrdersRepository : BaseRepository, ISalesOrdersRepository
    {
        private readonly DbContext dbContext;
        public SalesOrdersRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<SalesOrderDetailsResponse> BasicSalesItems(int pageNum, int pageSize, string? orderNo, int? customerId, int? movementTypeId, int? warehouseId, int? outType, DateTime? fromDate, DateTime? toDate, int? statusId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                parameters.Add("_orderNumber", orderNo);
                parameters.Add("_customerId", customerId);
                parameters.Add("_movementTypeId", movementTypeId);
                parameters.Add("_warehouseId", warehouseId);
                parameters.Add("_outType", outType);
                parameters.Add("_fromDate", fromDate?.ToString("yyyy-MM-dd"));
                parameters.Add("_toDate", toDate?.ToString("yyyy-MM-dd"));
                parameters.Add("_statusId", statusId);
                var list = db.QueryMultiple("GetDetailsofSalesOrder", parameters, commandType: CommandType.StoredProcedure);
                SalesOrderDetailsResponse Response = new SalesOrderDetailsResponse();
                Response.SalesDetail = list.Read<SalesBasicDetails>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }
        public async Task<ItemsResponse> ItemsInformation(int pageNum, int pageSize)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_limit", pageSize);
                parameters.Add("_offset", pageNum);
                var list = db.QueryMultiple("GetSalesOrderItemsInformation", parameters, commandType: CommandType.StoredProcedure);
                ItemsResponse Response = new ItemsResponse();
                Response.Items = list.Read<ItemsInfoResponse>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }
        public async Task<List<SaleOrderExcelResponse>> ExcelReponse(string request, int salesOrderId, int userId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_data", request);
                parameters.Add("_salesOrderId", salesOrderId);
                parameters.Add("_userId", userId);
                var response = db.Query<SaleOrderExcelResponse>("SaleOrderItemsImportExcelData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return response;
            }
        }
        public async Task<int> CheckDuplicateSalesOrderNumber(string salesOrderNo)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_salesOrderNumber", salesOrderNo);
                var response = db.Query<int>("CheckDuplicateSalesOrderNumber", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return response;
            }
        }
        public async Task<Response> BasicInfo(SalesOrderBasicInformationRequest request, int userId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_salesOrderNo", request.SalesOrderNo);
                parameters.Add("_dateOfSale", request.DateofSale.ToString("yyyy-MM-dd HH:mm:ss"));
                parameters.Add("_customerId", request.CustomerId);
                parameters.Add("_movementTypeId", request.MovementTypeId);
                parameters.Add("_warehouseId", request.WarehouseId);
                parameters.Add("_outType", request.OutType);
                parameters.Add("_userId", userId);
                var response = db.Query<Response>("SaveSalesOrderBasicInformation", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return response;
            }
        }
        public async Task<StockOutward> StockOutward(int pageNum, int pageSize)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_pageSize", pageSize);
                parameters.Add("_pageNum", pageNum);
                var list = db.QueryMultiple("GetStockOutwardList", parameters, commandType: CommandType.StoredProcedure);
                StockOutward Response = new StockOutward();
                Response.StockOutwardList = list.Read<StockOutwardList>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }

        public async Task<StockOutwardModel> SalesOwtwardItem(int id)
        {
            using (var db = dbContext.GetConnection())
            {
                StockOutwardModel res = new StockOutwardModel();
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);

                var response = db.QueryMultiple("GetSalesOutwardDetailsById", parameters, commandType: CommandType.StoredProcedure);
                res.StockOutwardList = response.Read<StockOutwardList>().SingleOrDefault();
                res.StockOutwardItem = response.Read<StockOutwardItemList>().ToList();

                return res;
            }
        }
        public async Task<StockOutward> DirectStockOutward(int pageNum, int pageSize)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_pageSize", pageSize);
                parameters.Add("_pageNum", pageNum);
                var list = db.QueryMultiple("GetDirectOutward", parameters, commandType: CommandType.StoredProcedure);
                StockOutward Response = new StockOutward();
                Response.StockOutwardList = list.Read<StockOutwardList>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }

        public async Task<Response> ScanDirectOutwardMobile(ScanMobileDirectOutwardRequest request, int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_serialNumber", request.SerialNumber);
                parameters.Add("_salesOrderId", request.SalesOrderId);
                parameters.Add("_userId", id);
                var response = await db.QueryFirstOrDefaultAsync<Response>("ScanDirectOutwardMobile", parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<DirectOutwardsScannedIngoList?> SalesDirectOutwardDetails(int soiId, int sobId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_soiId", soiId);
                parameters.Add("_sobId", sobId);
                var output = await db.QueryMultipleAsync("GetSalesDirectOutwardDetailsById", parameters, commandType: CommandType.StoredProcedure);
                DirectOutwardsScannedIngoList response = new DirectOutwardsScannedIngoList();
                response.sobInfo = output.Read<GetSalesOrderInfo>().SingleOrDefault();
                response.Listt = output.Read<DirectOutwardsScannedIngo>().ToList();
                return response;
            }
        }

        public async Task<DispatchedCountBySalesOrderResponse> DispatchedCountBySalesOrder(int salesOrderId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_salesOrderId", salesOrderId);
                return db.Query<DispatchedCountBySalesOrderResponse>("DispatchedCountBySalesOrder", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<Response> SerialNumberHistory(SerialNumberHistoryRequest request, int userId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_serialNumberId", request.SerialNumberProfileId);
                parameters.Add("_actionTypeId", request.ActionTypeId);
                parameters.Add("_locationId", request.LocationId);
                parameters.Add("_stockInOutOrderNumber", request.StockInOutOrderNumber);
                parameters.Add("_buyerSellerId", request.BuyerSellerId);
                parameters.Add("_userId", userId);
                var response = db.Query<Response>("SaveSerialNumberHistory", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return response;
            }
        }

        public async Task<Response> MarkItemAsReturn(string serialNumber, int userId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_serialnumber", serialNumber);
                parameters.Add("_userId", userId);
                var response = db.Query<Response>("UpdateIsReturnSaleOrderDispatchSerialNumber", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return response;
            }
        }

        public async Task<Response> MarkItemAsDamaged(string serialNumber, int userId, IsDamgeRequest request)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_serialnumber", serialNumber);
                parameters.Add("_damageDate", request.DamageDate);
                parameters.Add("_userId", userId);
                var response = db.Query<Response>("UpdateIsDamage", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return response;
            }
        }

        public async Task<ScanAllInfoSalesOrder> ScanAllInfo(int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                var output = await db.QueryMultipleAsync("ScanAllInfoSalesOrder", parameters, commandType: CommandType.StoredProcedure);
                ScanAllInfoSalesOrder response = new ScanAllInfoSalesOrder();
                response.scanAllSalesOrderInfo = output.Read<ScanAllSalesOrderInfo>().FirstOrDefault();
                response.scanAllSalesOrderProductInfo = output.Read<ScanAllSalesOrderProductInfo>().ToList();
                return response;
            }
        }

        public async Task<SerailNumberResponse> ScannedUnscanned(int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                var output = await db.QueryMultipleAsync("ScanAllSerialNumbersSalesOrderScannedUnscanned", parameters, commandType: CommandType.StoredProcedure);
                SerailNumberResponse response = new SerailNumberResponse();
                response.ScannedSerialNumber = output.Read<SerialNumberClass>().ToList();
                response.NotScannedSerialNumber = output.Read<SerialNumberClass>().ToList();
                return response;
            }
        }

        public async Task<int> SerialNumberSalesOrder(int id, string serialNo, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                parameters.Add("_serialNumber", serialNo);
                parameters.Add("_userId", userId);
                var response = await db.ExecuteAsync("UpdateSerialNumberSalesOrder", parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<Response> DirectSeriaNumberSalesOrder(DirectSerialNoSalesOrder request, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_serialNumber", request.SerialNumber);
                parameters.Add("_warehouseId", request.WarehouseId);
                parameters.Add("_userId", userId);
                parameters.Add("_salesOrderBasicInformationId", request.SalesOrderBasicInformationId);
                var response = (await db.QueryAsync<Response>("DirectUpdateSerialNumberSalesOrder", parameters, commandType: CommandType.StoredProcedure)).SingleOrDefault();
                return response;
            }
        }

        public async Task<int> MarkComplete(int id, int userId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                parameters.Add("_userId", userId);
                var response = await db.ExecuteAsync("SaleOrderMarkComplete", parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<List<SalesOrderExportExcelResponse>> ExportExcel(int id)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                var response = db.Query<SalesOrderExportExcelResponse>("salesOrderExportExcel", parameters, commandType: CommandType.StoredProcedure);
                return response.ToList();
            }
        }
    }
}
