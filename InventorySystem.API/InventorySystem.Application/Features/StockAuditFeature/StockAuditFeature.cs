using InventorySystem.Application.Features.StockAuditFeature.interfaces;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.StockAuditFeature
{
    public class StockAuditFeature : IStockAuditFeature
    {
        public readonly IStockAuditRepository stockAuditRepository;

        public StockAuditFeature(IStockAuditRepository stockAuditRepository)
        {
            this.stockAuditRepository = stockAuditRepository;
        }

        public async Task<Response> StockAudit(StockAuditRequest request, int userId)
        {
            Response response = new Response();
            response.Result = await stockAuditRepository.Post<StockAuditRequest>("StartStockAuditInitiation", request, userId);
            response.IsSuccess = 1;
            response.ResponseCode = 201;
            response.Message = "StockAudit Saved Succesfully";
            return response;
        }

        public async Task<Response> StockAuditByWarehouseId(int id)
        {
            Response response = new Response();
            response.Result = await stockAuditRepository.GetListById<StockAuditByWarehouseIdResponse>("GetAuditItemCountAsPerWarehouseId", id);
            response.IsSuccess = 1;
            response.ResponseCode = 200;
            response.Message = "Data Fetched Successfully";
            return response;
        }

        public async Task<Response> MarkAuditComplete(MarkAuditCompleteRequest request, int id, int userId)
        {
            Response response = await stockAuditRepository.Put<MarkAuditCompleteRequest>("MarkAuditComplete", request, id, userId);
            response.IsSuccess = 1;
            response.ResponseCode = 204;
            response.Message = "MarkAudit updated Successfully";
            return response;
        }

        public async Task<Response> MarkAuditComplete(int auditId, int categoryId, string serialNumber, int userId)
        {
            Response response = await stockAuditRepository.MarkAuditComplete(auditId, categoryId, serialNumber, userId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> StockAudit(int pageNum, int pageSize, int? warehouseId, DateTime? fromDate, DateTime? toDate, int? userId, int? status)
        {
            Response response = new Response();
            if ((fromDate != null && toDate == null) || (toDate != null && fromDate == null))
            {
                response.IsSuccess = 0;
                response.Message = ("Please fill both dates");
            }
            else
            {
                response.Result = await stockAuditRepository.StockAudit(pageNum, pageSize, warehouseId, fromDate, toDate, userId, status);
                response.IsSuccess = 1;
                response.Message = "Data Fetched Successfully.";
                response.ResponseCode = 200;
            }
            return response;
        }

        public async Task<Response> StockAudit(int id, int pageNum, int pageSize, string? productSKU, int? manufacturerName, int? categoryName)
        {
            Response response = new Response();
            response.Result = await stockAuditRepository.StockAudit(id, pageNum, pageSize, productSKU, manufacturerName, categoryName);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }
        public async Task<Response> StockAudit(int id)
        {
            Response response = new Response();
            response.Result = await stockAuditRepository.StockAudit(id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;

            return response;
        }
        public async Task<Response> StockAuditLocation(int id)
        {
            Response response = new Response();
            response.Result = await stockAuditRepository.StockAuditLocation(id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }
        public async Task<Response> TotalCount()
        {
            Response response = new Response();
            response.Result = await stockAuditRepository.GetSingleRecord<StockAuditCountResponse>("GetStockAuditScannedCount");
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }
    }
}
