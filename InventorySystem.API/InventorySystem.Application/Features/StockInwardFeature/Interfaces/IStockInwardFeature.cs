using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.StockInwardFeature.Interfaces
{
    public interface IStockInwardFeature
    {
        public Task<Response> Invoice(AddInvoiceRequest request, int userId);
        public Task<Response> Invoice(int pageNum, int pageSize, DateTime? fromDate, DateTime? toDate, string? invoiceNo, int vendorCompanyNameId, int moveTypeID, int warehouseLocationId);
        public Task<Response> Invoice(int id);
        public Task<Response> AddProductInInvoice(List<AddProductInInvoiceRequest> request, int userId);

        public Task<Response> GetAllProductsOfAnInvoice(int id);
        public Task<Response> GetProductDetailsForAutoFill(int productSku);
        public Task<Response> GetInfoForBarcodePage(int productSkuProfileId);
        public Task<Response> InwardExcelData(List<InwardExcelRequest> request, int invoiceId, int userId);
        public Task<Response> GenerateBarcode(int productSkuProfileId, int userId);
        public Task<Response> GetUnscannedSerialNum(int productSkuProfileId);
        public Task<Response> SerialNumbers(int producSkuId);
        public Task<Response> UpdateSerialNumber(string serialNo, int userId);
        public Task<Response> PendingInvoice();
        public Task<Response> PendingSerialNumberForScanning(int invoiceNumberId);

        public Task<Response> ProductByIdStockInward(int id);
        public Task<Response> CheckDuplicateInvoiceNumber(string invoiceNo);
        public Task<Response> MarkComplete(int invoiceId);
        public Task<Response> UpdatePrintCount(int productSkuProfileId, int userId);
        public Task<Response> ScanAllInfo(int id);
        public Task<Response> ScanAllSerialNum(int id);
        public Task<Response> UnscannedSerialNumberByInvoiceId(int id);
        public Task<Response> PrintAllByInvoiceBtnClicked(int id, int userId);
    }
}