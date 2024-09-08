using FluentValidation;
using InventorySystem.Application.Helpers;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Newtonsoft.Json;

namespace InventorySystem.Application.Features.SalesOrdersFeature
{
    public class SalesOrdersFeature : ISalesOrdersFeature
    {
        public readonly ISalesOrdersRepository salesOrdersRepository;
        private readonly IValidator<SalesOrderBasicInformationRequest> salesValidator;
        private readonly IValidator<SalesOrderItemsInformationRequest> salesItemsValidator;

        public SalesOrdersFeature(
            ISalesOrdersRepository salesOrdersRepository,
            IValidator<SalesOrderBasicInformationRequest> salesValidator,
            IValidator<SalesOrderItemsInformationRequest> salesItemsValidator
        )
        {
            this.salesOrdersRepository = salesOrdersRepository;
            this.salesValidator = salesValidator;
            this.salesItemsValidator = salesItemsValidator;
        }
        public async Task<Response> ItemsInformation(int pageNum, int pageSize)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.ItemsInformation(pageNum, pageSize);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> BasicInfo(SalesOrderBasicInformationRequest request, int userId)
        {
            salesValidator.ValidateAndThrow(request);
            Response res = new Response();
            res.Result = await salesOrdersRepository.BasicInfo(request, userId);
            res.IsSuccess = 1;
            res.Message = "Sales Order Added Successfully";
            res.ResponseCode = 201;
            return res;
        }

        public async Task<Response> BasicSalesItems(int pageNum, int pageSize, string? orderNo, int? customerId, int? movementTypeId, int? warehouseId, int? outType, DateTime? fromDate, DateTime? toDate, int? statusId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.BasicSalesItems(pageNum, pageSize, orderNo, customerId, movementTypeId, warehouseId, outType, fromDate, toDate, statusId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> SaleItemsInfo(SalesOrderItemsInformationRequest request, int userId)
        {
            salesItemsValidator.ValidateAndThrow(request);
            Response response = new Response();
            response.Result = await salesOrdersRepository.Post<SalesOrderItemsInformationRequest>("SaveSalesOrderItemsInformation", request, userId);
            response.IsSuccess = 1;
            response.Message = "Added Successfully";
            return response;
        }

        public async Task<Response> SalesOrderItem(int id, int userId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.Delete("DeleteSalesOrderItemInformation", id, userId);
            return response;
        }

        public async Task<Response> Item(int id)
        {
            var response = new Response();
            response.Result = await salesOrdersRepository.GetListById<SaleItemsInformationResponse>("GetSaleItemsInformationById", id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<byte[]?> ConvertInventoryToExcel(int id)
        {
            {
                List<SalesOrderExportExcelResponse> data = await salesOrdersRepository.ExportExcel(id);
                List<string> columns = new List<string>() { "S.No.", "ProductSKU", "SerialNumber", "DispatchDate" };
                return await ExcelConverter.ConvertToExcel<SalesOrderExportExcelResponse>("saleorderDetail", columns, data);
            }
        }

        public async Task<Response> BasicInformation(int id)
        {
            var response = new Response();
            response.Result = await salesOrdersRepository.GetSingleRecordById<BasicInformationResponse>("GetSalesBasicInformationById", id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> ProductSKU()
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.GetList<ProductSKUListResponse>("GetSalesOrderProductList");
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> TotalCount()
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.GetSingleRecord<TotalCountForSalesOrderDetaisResponse>("GetTotalCountForSalesOrderDetails");
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> MultipleItems(List<SalesOrderItemsInformationRequest> request, int userId)
        {
            SalesOrderItemsInformationJsonRequest saleOrderWithJson = new SalesOrderItemsInformationJsonRequest();
            saleOrderWithJson.Data = JsonConvert.SerializeObject(request);
            Response res = new Response();
            res.Result = await salesOrdersRepository.Post<SalesOrderItemsInformationJsonRequest>("AddMultipleSaveItems", saleOrderWithJson, userId);
            res.IsSuccess = 1;
            res.Message = "Saved Multiple Items";
            res.ResponseCode = 201;
            return res;
        }

        public async Task<Response> SalesOutwardExcelData(List<SaleOrderExcelInwardRequest> request, int salesOrderId, int userId)
        {
            Response response = new Response();
            var serializedRequest = JsonConvert.SerializeObject(request);
            List<SaleOrderExcelResponse> res = await salesOrdersRepository.ExcelReponse(serializedRequest, salesOrderId, userId);
            if (res != null)
            {
                if (res.Count() == 0)
                {
                    SaleOrderExcelResponse data = new SaleOrderExcelResponse();
                    data.Errors = "No Errors. Successfully Uploaded";
                    data.SerialNumber = " ";
                    res.Add(data);
                }
                response.IsSuccess = 1;
                response.Result = res;
                response.ResponseCode = 200;
            }
            else
            {
                response.IsSuccess = 0;
                response.Message = "Null From DB";
                response.ResponseCode = 500;
            }
            return response;
        }

        public async Task<Response> CheckDuplicateSalesOrderNumber(string salesOrderNo)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.CheckDuplicateSalesOrderNumber(salesOrderNo);

            if (response.Result != null)
            {
                if (response.IsSuccess == 1)
                {
                    response.Message = "Sales Order number already exists.";
                    response.ResponseCode = 409;
                }
                else
                {
                    response.Message = "Sales Order number does not exists.";
                    response.ResponseCode = 200;
                }
            }
            return response;
        }

        public async Task<Response> SaleOrderStatus(SaleOrderStatusRequest request, int id, int userId)
        {
            Response response = await salesOrdersRepository.Patch<SaleOrderStatusRequest>("UpdateSaleOrderItemStatus", request, id, userId);
            response.IsSuccess = 1;
            response.Message = "Updated Successfully";
            return response;
        }

        public async Task<Response> MarkStockDispatched(int id, int userId)
        {
            Response response = await salesOrdersRepository.Patch("MarkStockAsDispatched", id, userId);
            response.IsSuccess = 1;
            response.Message = "Updated Successfully";
            return response;
        }

        public async Task<Response> MarkStockComplete(int id, int userId)
        {
            Response response = await salesOrdersRepository.Patch("MarkStockOutwardAsComplete", id, userId);
            response.IsSuccess = 1;
            response.Message = "Updated Successfully";
            return response;
        }

        public async Task<Response> SalesOutwardItem(int id)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.SalesOwtwardItem(id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> StockOutward(int pageNum, int pageSize)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.StockOutward(pageNum, pageSize);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }
        public async Task<Response> DirectStockOutward(int pageNum, int pageSize)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.DirectStockOutward(pageNum, pageSize);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> ProductsOfDirectOutward(int id)
        {
            Response response = new Response();
            List<DirectOutWardProductsResponse>? result = await salesOrdersRepository.GetListById<DirectOutWardProductsResponse>("GetProductsOfDirectOutwardById", id);
            if (result != null)
            {
                response.Result = result;
                response.IsSuccess = 1;
                response.ResponseCode = 200;
            }
            else
            {
                response.Message = "Something went wrong. Null From DB";
                response.ResponseCode = 500;
                response.IsSuccess = 0;
            }
            return response;
        }

        public async Task<Response> ScanDirectOutwardMobile(ScanMobileDirectOutwardRequest request, int id)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.ScanDirectOutwardMobile(request, id);
            response.IsSuccess = 1;
            response.ResponseCode = 201;
            response.Message = "Scanned Succesfully";
            return response;
        }

        public async Task<Response> SalesDirectOutwardDetails(int soiId, int sobId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.SalesDirectOutwardDetails(soiId, sobId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> DispatchedCountBySalesOrder(int salesOrderId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.DispatchedCountBySalesOrder(salesOrderId);
            response.IsSuccess = 1;
            response.Message = "Data fetched Successfully";
            response.ResponseCode = 200;
            return response;
        }
        public async Task<Response> SerialNumberHistory(SerialNumberHistoryRequest request, int userId)
        {
            Response res = new Response();
            res.Result = await salesOrdersRepository.SerialNumberHistory(request, userId);
            res.IsSuccess = 1;
            res.Message = "Serial Number history added successfully";
            res.ResponseCode = 201;
            return res;
        }
        public async Task<Response> MarkItemAsReturn(string serialNumber, int userId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.MarkItemAsReturn(serialNumber, userId);
            response.IsSuccess = 1;
            response.Message = "Updated Successfully";
            response.ResponseCode = 204;
            return response;
        }
        public async Task<Response> MarkItemAsDamaged(string serialNumber, int userId, IsDamgeRequest request)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.MarkItemAsDamaged(serialNumber, userId, request);
            response.IsSuccess = 1;
            response.Message = "Updated Successfully";
            response.ResponseCode = 204;
            return response;
        }

        public async Task<Response> ScanAllInfo(int id)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.ScanAllInfo(id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> ScannedUnscanned(int id)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.ScannedUnscanned(id);
            if (response != null)
            {
                response.IsSuccess = 1;
                response.Message = "Data Fetched Successfully.";
                response.ResponseCode = 200;
            }
            else
            {
                response.IsSuccess = 0;
                response.Message = "Something went wrong, please try again";
                response.ResponseCode = 500;
            }
            return response;
        }

        public async Task<Response> SerialNumberSalesOrder(int id, string serialNumber, int userId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.SerialNumberSalesOrder(id, serialNumber, userId);
            if (response != null)
            {
                if (response.Result == 1)
                {
                    response.IsSuccess = 1;
                    response.Message = "Serial Number Scanned";
                    response.ResponseCode = 200;
                }
                else
                {
                    response.IsSuccess = 1;
                    response.Message = "Serial Number not Scanned";
                    response.ResponseCode = 200;
                }
            }
            else
            {
                response.IsSuccess = 0;
                response.Message = "Something went wrong, please try again";
                response.ResponseCode = 500;
            }
            return response;
        }

        public async Task<Response> MarkComplete(int id, int userId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.MarkComplete(id, userId);
            if (response != null)
            {
                if (response.Result == 1)
                {
                    response.IsSuccess = 1;
                    response.Message = "Serial Number Scanned";
                    response.ResponseCode = 200;
                }
                else
                {
                    response.IsSuccess = 1;
                    response.Message = "Serial Number doesnt exist in the warehouse";
                    response.ResponseCode = 200;
                }
            }
            else
            {
                response.IsSuccess = 0;
                response.Message = "Something went wrong, please try again";
                response.ResponseCode = 500;
            }
            return response;
        }

        public async Task<Response> DirectSeriaNumberSalesOrder(DirectSerialNoSalesOrder request, int userId)
        {
            Response response = new Response();
            response.Result = await salesOrdersRepository.DirectSeriaNumberSalesOrder(request, userId);
            response.IsSuccess = 1;
            response.Message = "Serial Number Updated Succesfully";
            response.ResponseCode = 201;
            return response;
        }
    }
}
