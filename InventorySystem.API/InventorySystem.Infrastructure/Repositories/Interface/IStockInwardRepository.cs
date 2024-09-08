using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Infrastructure.Repositories.Interface
{
    public interface IStockInwardRepository : IBaseRepository
    {
        public Task<InvoiceListResponse> Invoice(int pageNum, int pageSize, DateTime? fromDate, DateTime? toDate, string? invoiceNo, int vendorCompanyNameId, int moveTypeID, int warehouseLocationId);
        public Task<InfoForBarcodeResponse> GetInfoForBarcodePage(int productSkuProfileId);
        public Task<List<InwardExcelResponse>> GetExcelReponse(string request, int invoiceId, int userId);
        public Task<CheckBarcodeGeneratedModel> CheckBarcodeGenerated(int productSkuProfileId);
        public Task<List<VerifyBarcodeGeneratedResponse>> VerifyBarcodeGenerated(string list, int productSkuProfileId, int userId);
        public Task<SerailNumberResponse> SerialNumbers(int productSkuId);
        public Task<Response> UpdateSerialNumber(string serialNo, int userId);
        public Task<List<InvoiceDetail>> PendingInvoice();
        public Task<List<GetPendingSerialNumberForScanningResponse>> PendingSerialNumberForScanning(int invoiceNumberId);
        public Task<VerifyBarcodeGeneratedResponse> GetUnscannedSerialNum(int productSkuProfileId);
        public Task<CheckDuplicateInvoiceNumberResponse> CheckDuplicateInvoiceNumber(string invoiceNo);
        public Task<ScanAllModelStockInward> ScanAllInfo(int id);
        public Task<SerailNumberResponse> ScanAllSerialNum(int id);
        public Task<Response> PrintAllByInvoiceBtnClicked(int id, int userId);
    }
}