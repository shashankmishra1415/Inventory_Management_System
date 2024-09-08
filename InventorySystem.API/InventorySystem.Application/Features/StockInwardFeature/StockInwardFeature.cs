using FluentValidation;
using InventorySystem.Application.Features.StockInwardFeature.Interfaces;
using InventorySystem.Application.Validator;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Newtonsoft.Json;

namespace InventorySystem.Application.Features.StockInwardFeature
{
    public class StockInwardFeature : IStockInwardFeature
    {
        private readonly IStockInwardRepository stockInwardRepository;

        public StockInwardFeature(IStockInwardRepository stockInwardRepository)
        {
            this.stockInwardRepository = stockInwardRepository;
        }

        public async Task<Response> Invoice(AddInvoiceRequest request, int userId)
        {
            IValidator<AddInvoiceRequest> validation = new StockInwardValidator();  
            validation.ValidateAndThrow(request);
            Response res = await stockInwardRepository.Post("AddInvoiceProfile", request, userId);
            res.ResponseCode = res.IsSuccess == 1 ? 201 : 400;
            return res;
        }

        public async Task<Response> Invoice(int pageNum, int pageSize, DateTime? fromDate, DateTime? toDate, string? invoiceNo, int vendorCompanyNameId, int moveTypeID, int warehouseLocationId)
        {
            Response response = new Response();
            response.Result = await stockInwardRepository.Invoice(pageNum, pageSize, fromDate, toDate, invoiceNo, vendorCompanyNameId, moveTypeID, warehouseLocationId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Invoice(int id)
        {
            Response res = new Response();
            res.Result = await stockInwardRepository.GetSingleRecordById<Invoice>("GetInvoiceDetailsById", id);
            res.IsSuccess = 1;
            res.Message = "Invoice id = " + id;
            return res;
        }

        public async Task<Response> AddProductInInvoice(List<AddProductInInvoiceRequest> request, int userId)
        {
            IValidator<List<AddProductInInvoiceRequest>> validation = new StockInwardValidator3();
            validation.ValidateAndThrow(request);
            AddProductInInvoiceWithJsonRequest addProductInInvoiceWithJson = new AddProductInInvoiceWithJsonRequest();
            addProductInInvoiceWithJson.Data = JsonConvert.SerializeObject(request);
            Response res = new Response();
            res = await stockInwardRepository.Post<AddProductInInvoiceWithJsonRequest>("AddProductInInvoice", addProductInInvoiceWithJson, userId);
            return res;
        }

        public async Task<Response> GetAllProductsOfAnInvoice(int id)
        {
            Response res = new Response();
            res.Result = await stockInwardRepository.GetListById<ProductsInInvoiceResponse>("GetProductsOfInvoice", id);
            res.IsSuccess = 1;
            res.Message = "Product List received";
            return res;
        }

        public async Task<Response> GetProductDetailsForAutoFill(int _productSku)
        {
            Response res = new Response();
            res.Result = await stockInwardRepository.GetSingleRecordById<ProductAutofillForSku>("GetProductInfoBySkuForAutofill", _productSku);
            res.IsSuccess = 1;
            res.Message = "Product Info received";
            return res;
        }

        public async Task<Response> GetInfoForBarcodePage(int productSkuProfileId)
        {
            Response response = new Response();
            response.Result = await stockInwardRepository.GetInfoForBarcodePage(productSkuProfileId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;

            return response;
        }

        public async Task<Response> InwardExcelData(List<InwardExcelRequest> request, int invoiceId, int userId)
        {
            Response response = new Response();
            var serializedRequest = JsonConvert.SerializeObject(request);
            List<InwardExcelResponse> res = await stockInwardRepository.GetExcelReponse(serializedRequest, invoiceId, userId);
            if (res != null)
            {
                response.IsSuccess = 1;
                response.Result = res;
                response.ResponseCode = 200;
            }
            else
            {
                response.IsSuccess = 0;
                response.Message = "Something went wrong. Please try again";
                response.ResponseCode = 500;
            }
            return response;
        }

        private string SerialNumberGenerator(int totalNum)
        {
            var list = new List<string>();
            var characters = "0123456789";
            int safeSide = totalNum + 10;
            var random = new Random();
            for (int j = 0; j < safeSide; j++)
            {
                var stringCharacters = new char[14];
                for (int i = 0; i < stringCharacters.Length; i++)
                {
                    stringCharacters[i] = characters[random.Next(characters.Length)];
                }
                var finalSerialNum = new String(stringCharacters);
                list.Add(finalSerialNum.ToString());
            }
            var serializedList = JsonConvert.SerializeObject(list);
            return serializedList;
        }

        public async Task<Response> GenerateBarcode(int productSkuProfileId, int userId)
        {
            Response response = new Response();
            CheckBarcodeGeneratedModel res = await stockInwardRepository.CheckBarcodeGenerated(productSkuProfileId);
            if (res == null)
            {
                response.IsSuccess = 1;
                response.Message = "This product does not exists , please contact admin ";
                response.ResponseCode = 204;
            }
            else
            {
                string data = "";
                List<VerifyBarcodeGeneratedResponse> result = new List<VerifyBarcodeGeneratedResponse>();
                data = SerialNumberGenerator(res.Quantity);
                result = await stockInwardRepository.VerifyBarcodeGenerated(data, productSkuProfileId, userId);
                while (result.Count() < res.Quantity)
                {
                    data = SerialNumberGenerator(res.Quantity - result.Count());
                    result = await stockInwardRepository.VerifyBarcodeGenerated(data, productSkuProfileId, userId);
                }
                response.IsSuccess = 1;
                response.Message = "Barcode";
                response.ResponseCode = 200;
                response.Result = result;
            }
            return response;
        }

        public async Task<Response> GetUnscannedSerialNum(int productSkuProfileId)
        {
            Response response = new Response();
            var res = await stockInwardRepository.GetUnscannedSerialNum(productSkuProfileId);
            response.IsSuccess = 1;
            response.Result = res;
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> SerialNumbers(int producSkuId)
        {
            Response response = new Response();
            response.Result = await stockInwardRepository.SerialNumbers(producSkuId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> UpdateSerialNumber(string serialNo, int userId)
        {
            Response response = new Response();
            response = await stockInwardRepository.UpdateSerialNumber(serialNo, userId);
            if (response != null)
            {
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

        public async Task<Response> PendingInvoice()
        {
            Response response = new Response();
            response.Result = await stockInwardRepository.PendingInvoice();
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> PendingSerialNumberForScanning(int invoiceNumberId)
        {
            Response response = new Response();
            response.Result = await stockInwardRepository.PendingSerialNumberForScanning(invoiceNumberId);
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

        public async Task<Response> ProductByIdStockInward(int id)
        {
            var response = new Response();
            response.Result = stockInwardRepository.GetSingleRecordById<ProductByIdStockInward>("ProductByIdSTockInward", id);
            return response;
        }

        public async Task<Response> CheckDuplicateInvoiceNumber(string invoiceNo)
        {
            Response response = new Response();
            var res = await stockInwardRepository.CheckDuplicateInvoiceNumber(invoiceNo);
            response.IsSuccess = res.IsExists;
            if (response.IsSuccess == 1)
            {
                response.Message = "Invoice number already exists.";
                response.ResponseCode = 409;
            }
            else
            {
                response.Message = "Invoice number does not exists.";
                response.ResponseCode = 200;
            }
            return response;
        }

        public async Task<Response> MarkComplete(int invoiceId)
        {
            Response response = await stockInwardRepository.Patch("MarkComplete", invoiceId);

            if (response == null)
            {
                response.Message = "Something went wrong.";
                response.ResponseCode = 500;
            }
            else
            {
                response.ResponseCode = 204;
            }
            return response;
        }

        public async Task<Response> UpdatePrintCount(int productSkuProfileId, int userId)
        {
            Response response = await stockInwardRepository.Patch("UpdatePrintCount", productSkuProfileId, userId);

            if (response == null)
            {
                response.Message = "Something went wrong.";
                response.ResponseCode = 500;
            }
            else
            {
                response.ResponseCode = 204;
            }
            return response;
        }

        public async Task<Response> ScanAllInfo(int id)
        {
            var response = new Response();
            ScanAllModelStockInward res = await stockInwardRepository.ScanAllInfo(id);
            if (res != null)
            {
                response.Result = res;
                response.IsSuccess = 1;
                response.ResponseCode = 200;
            }
            else
            {
                response.IsSuccess = 0;
                response.ResponseCode = 500;
                response.Message = "null from Database";
            }
            return response;
        }

        public async Task<Response> ScanAllSerialNum(int id)
        {
            var response = new Response();
            var res = await stockInwardRepository.ScanAllSerialNum(id);
            if (res != null)
            {
                response.Result = res;
                response.IsSuccess = 1;
                response.ResponseCode = 200;
            }
            else
            {
                response.IsSuccess = 0;
                response.ResponseCode = 500;
                response.Message = "null from Database";
            }
            return response;
        }
        public async Task<Response> UnscannedSerialNumberByInvoiceId(int id)
        {
            var response = new Response();
            var res = await stockInwardRepository.GetListById<SerialnoProductskuprofile>("GetUnscannedSerialNumberByInvoiceId", id);
            response.Result = res;
            response.IsSuccess = 1;
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> PrintAllByInvoiceBtnClicked(int id, int userId)
        {
            var response = new Response();
            var res = await stockInwardRepository.PrintAllByInvoiceBtnClicked(id, userId);
            response.Result = res;
            response.IsSuccess = 1;
            response.ResponseCode = 200;
            return response;
        }
    }
}