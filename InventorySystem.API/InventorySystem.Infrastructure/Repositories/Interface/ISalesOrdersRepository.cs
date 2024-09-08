using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface ISalesOrdersRepository : IBaseRepository
    {
        Task<SalesOrderDetailsResponse> BasicSalesItems(int pageNum, int pageSize, string? orderNo, int? customerId, int? movementTypeId, int? warehouseId, int? outType, DateTime? fromDate, DateTime? toDate, int? statusId);
        Task<ItemsResponse> ItemsInformation(int pageNum, int pageSize);
        Task<List<SaleOrderExcelResponse>> ExcelReponse(string request, int invoiceId, int userId);
        Task<int> CheckDuplicateSalesOrderNumber(string salesOrderNo);
        Task<Response> BasicInfo(SalesOrderBasicInformationRequest request, int userId);
        Task<StockOutwardModel> SalesOwtwardItem(int id);
        Task<StockOutward> StockOutward(int pageNum, int pageSize);
        Task<StockOutward> DirectStockOutward(int pageNum, int pageSize);
        Task<Response> ScanDirectOutwardMobile(ScanMobileDirectOutwardRequest request, int id);
        Task<DirectOutwardsScannedIngoList?> SalesDirectOutwardDetails(int soiId, int sobId);
        Task<DispatchedCountBySalesOrderResponse> DispatchedCountBySalesOrder(int salesOrderId);
        Task<Response> SerialNumberHistory(SerialNumberHistoryRequest request, int userId);
        Task<Response> MarkItemAsReturn(string serialNumber, int userId);
        Task<Response> MarkItemAsDamaged(string serialNumber, int userId, IsDamgeRequest request);
        Task<ScanAllInfoSalesOrder> ScanAllInfo(int id);
        Task<SerailNumberResponse> ScannedUnscanned(int id);
        Task<int> SerialNumberSalesOrder(int id, string serialNo, int userId);
        Task<Response> DirectSeriaNumberSalesOrder(DirectSerialNoSalesOrder request, int userId);
        Task<List<SalesOrderExportExcelResponse>> ExportExcel(int id);
        Task<int> MarkComplete(int id, int userId);
    }
}
