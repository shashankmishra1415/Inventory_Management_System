using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.SalesOrdersFeature
{
    public interface ISalesOrdersFeature
    {
        Task<Response> ItemsInformation(int pageNum, int pageSize);
        Task<Response> BasicInfo(SalesOrderBasicInformationRequest request, int userId);
        Task<Response> BasicSalesItems(int pageNum, int pageSize, string? orderNo, int? customerId, int? movementTypeId, int? warehouseId, int? outType, DateTime? fromDate, DateTime? toDate, int? statusId);
        Task<Response> SaleItemsInfo(SalesOrderItemsInformationRequest request, int userId);
        Task<Response> SalesOrderItem(int id, int userId);
        Task<Response> Item(int id);
        public Task<byte[]?> ConvertInventoryToExcel(int id);
        Task<Response> BasicInformation(int id);
        Task<Response> ProductSKU();
        Task<Response> TotalCount();
        Task<Response> MultipleItems(List<SalesOrderItemsInformationRequest> request, int userId);
        Task<Response> SalesOutwardExcelData(List<SaleOrderExcelInwardRequest> request, int salesOrderId, int userId);
        Task<Response> CheckDuplicateSalesOrderNumber(string salesOrderNo);
        Task<Response> SaleOrderStatus(SaleOrderStatusRequest request, int id, int userId);
        Task<Response> MarkStockComplete(int id, int userId);
        Task<Response> MarkStockDispatched(int id, int userId);
        Task<Response> SalesOutwardItem(int id);
        Task<Response> StockOutward(int pageNum, int pageSize);
        Task<Response> DirectStockOutward(int pageNum, int pageSize);
        Task<Response> ProductsOfDirectOutward(int id);
        Task<Response> ScanDirectOutwardMobile(ScanMobileDirectOutwardRequest request, int id);
        Task<Response> SalesDirectOutwardDetails(int SoiId, int SobId);
        Task<Response> DispatchedCountBySalesOrder(int salesOrderId);
        Task<Response> SerialNumberHistory(SerialNumberHistoryRequest request, int userId);
        Task<Response> MarkItemAsReturn(string serialnumber, int userId);
        Task<Response> MarkItemAsDamaged(string serialnumber, int userId, IsDamgeRequest request);
        Task<Response> ScanAllInfo(int id);
        Task<Response> ScannedUnscanned(int id);
        Task<Response> SerialNumberSalesOrder(int id, string serialNo, int userId);
        Task<Response> DirectSeriaNumberSalesOrder(DirectSerialNoSalesOrder request, int userId);
        Task<Response> MarkComplete(int id, int userId);
    }
}
