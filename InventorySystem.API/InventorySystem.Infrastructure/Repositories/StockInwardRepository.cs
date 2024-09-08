using Dapper;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using InventorySystem.SharedLayer.Response;
using System.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class StockInwardRepository : BaseRepository, IStockInwardRepository
    {
        private readonly DbContext dbContext;

        public StockInwardRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<InvoiceListResponse> Invoice(int pageNum, int pageSize, DateTime? fromDate, DateTime? toDate, string? invoiceNo, int vendorCompanyNameId, int moveTypeID, int warehouseLocationId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_pageSize", pageSize);
                parameters.Add("_pageNum", pageNum);
                parameters.Add("_invoiceNo", invoiceNo);
                parameters.Add("_vendorCompanyNameId", vendorCompanyNameId);
                parameters.Add("_moveTypeId", moveTypeID);
                parameters.Add("_warehouseLocationId", warehouseLocationId);
                parameters.Add("_fromDate", fromDate != null ? Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd") : null);
                parameters.Add("_toDate", toDate != null ? Convert.ToDateTime(toDate).ToString("yyyy-MM-dd") : null);
                var list = db.QueryMultiple("GetStockInwardList", parameters, commandType: CommandType.StoredProcedure);
                InvoiceListResponse Response = new InvoiceListResponse();
                Response.InvoiceDetail = list.Read<InvoiceDetail>().ToList();
                Response.PaginationResponses = list.Read<PaginationResponse>().SingleOrDefault();
                return Response;
            }
        }

        public async Task<InfoForBarcodeResponse> GetInfoForBarcodePage(int productSkuProfileId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_productSkuProfileId", productSkuProfileId);
                var list = db.QueryMultiple("GetInfoForBarcodePage", parameters, commandType: CommandType.StoredProcedure);
                InfoForBarcodeResponse Response = new InfoForBarcodeResponse();
                Response.InvoiceInfoForBarcodePage = list.Read<InvoiceInfoForBarcodePage>().SingleOrDefault();
                Response.ProductInfoForBarcodePage = list.Read<ProductInfoForBarcodePage>().SingleOrDefault();
                return Response;
            }
        }
        public async Task<List<InwardExcelResponse>> GetExcelReponse(string request, int invoiceId, int userId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_data", request);
                parameters.Add("_stockInwardBasicInformationId", invoiceId);
                parameters.Add("_userId", userId);
                var response = db.Query<InwardExcelResponse>("StockInwardExcelImport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return response;
            }
        }
        public async Task<SerailNumberResponse> SerialNumbers(int productSkuId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_productSkuId", productSkuId);
                var list = db.QueryMultiple("SerialsNumbers", parameters, commandType: CommandType.StoredProcedure);
                SerailNumberResponse response = new SerailNumberResponse();
                response.ScannedSerialNumber = list.Read<SerialNumberClass>().ToList();
                response.NotScannedSerialNumber = list.Read<SerialNumberClass>().ToList();
                return response;
            }
        }

        public async Task<Response> UpdateSerialNumber(string serialNo, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_serialNumber", serialNo);
                parameters.Add("_userId", userId);
                Response response = db.QueryFirstOrDefault<Response>("SerialNumberUpdate", parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<CheckBarcodeGeneratedModel> CheckBarcodeGenerated(int productSkuProfileId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_productSkuProfileId", productSkuProfileId);
                var response = db.QueryFirstOrDefault<CheckBarcodeGeneratedModel?>("CheckBarcodeGenerated", parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<List<VerifyBarcodeGeneratedResponse>> VerifyBarcodeGenerated(string list, int productSkuProfileId, int userId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_list", list);
                parameters.Add("_productSkuProfileId", productSkuProfileId);
                parameters.Add("_userId", userId);
                return db.Query<VerifyBarcodeGeneratedResponse>("VerifySerialNumber", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public async Task<List<InvoiceDetail>> PendingInvoice()
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();

                var response = db.Query<InvoiceDetail>("GetPendingInvoiceList", parameters, commandType: CommandType.StoredProcedure).ToList();
                return response;
            }
        }

        public async Task<List<GetPendingSerialNumberForScanningResponse>> PendingSerialNumberForScanning(int invoiceNumberId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_invoiceNumberId", invoiceNumberId);
                var response = db.Query<GetPendingSerialNumberForScanningResponse>("GetPendingSerialNumberForScanning", parameters, commandType: CommandType.StoredProcedure).ToList();
                return response;
            }
        }

        public async Task<VerifyBarcodeGeneratedResponse> GetUnscannedSerialNum(int productSkuProfileId)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_productSkuProfileId", productSkuProfileId);
                return db.Query<VerifyBarcodeGeneratedResponse>("UnscannedSerialNumbers", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public async Task<CheckDuplicateInvoiceNumberResponse> CheckDuplicateInvoiceNumber(string invoiceNo)
        {
            using (var db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_invoiceNumber", invoiceNo);
                var response = db.Query<CheckDuplicateInvoiceNumberResponse>("CheckDuplicateInvoiceNumber", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return response;
            }
        }

        public async Task<ScanAllModelStockInward> ScanAllInfo(int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                var list = db.QueryMultiple("ScanAllStockInwardsInfo", parameters, commandType: CommandType.StoredProcedure);
                ScanAllModelStockInward Response = new ScanAllModelStockInward();
                Response.ScanAllStockInwardProductList = list.Read<ScanAllStockInwardProductList>().ToList();
                Response.ScanAllStockInwardInvoiceInfo = list.Read<ScanAllStockInwardInvoiceInfo>().FirstOrDefault();
                return Response;
            }
        }
        public async Task<SerailNumberResponse> ScanAllSerialNum(int id)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                var list = db.QueryMultiple("ScanAllStockInwardsSerialNumbers", parameters, commandType: CommandType.StoredProcedure);
                SerailNumberResponse Response = new SerailNumberResponse();
                Response.ScannedSerialNumber = list.Read<SerialNumberClass>().ToList();
                Response.NotScannedSerialNumber = list.Read<SerialNumberClass>().ToList();
                return Response;
            }

        }
        public async Task<Response> PrintAllByInvoiceBtnClicked(int id, int userId)
        {
            using (IDbConnection db = dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("_id", id);
                parameters.Add("_userId", userId);
                return db.Query("PrintAllByInvoiceBtnClicked", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
    }
}
