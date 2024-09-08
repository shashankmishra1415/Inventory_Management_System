using FluentValidation;
using InventorySystem.Application.Features.VendorFeature.Interfaces;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Common;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.WareFeature
{
    public class VendorFeature : IVendorFeature
    {
        public readonly IVendorRepository vendorRepository;
        private readonly IBaseRepository baseRepository;
        private readonly IValidator<VendorRequest> vendorValidator;
        public VendorFeature(IVendorRepository vendorRepository, IValidator<VendorRequest> vendorValidator, IBaseRepository baseRepository)
        {
            this.vendorRepository = vendorRepository;
            this.vendorValidator = vendorValidator;
            this.baseRepository = baseRepository;
        }

        public async Task<Response> Vendor(int pageNum, int pageSize, string? companyName, string? contactName, int typeId, int vendorTypeId, int statusId)
        {
            Response response = new Response();
            response.Result = await vendorRepository.Vendor(pageNum, pageSize, companyName, contactName, typeId, vendorTypeId, statusId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Vendor(int id)
        {
            var response = new Response();
            response.Result = await baseRepository.GetSingleRecordById<VendorByIdResponse>("GetVendorById", id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Vendor(int id, int userId)
        {
            Response response = await baseRepository.Delete("DeleteVendor", id, userId);
            if(response.IsSuccess == 0)
            {
                response.ResponseCode = 400;
            }
            else
            {
                response.ResponseCode = 200;
            }
            return response;
        }

        public async Task<Response> Vendor(VendorRequest request, int userId)
        {
            vendorValidator.ValidateAndThrow(request);
            Response response  = await baseRepository.Post<VendorRequest>("AddVendor", request, userId);
            response.ResponseCode = 201;
            return response;
        }

        public async Task<Response> Vendor(VendorRequest request, int id, int userid)
        {
            vendorValidator.ValidateAndThrow(request);
            Response response = await baseRepository.Put<VendorRequest>("EditVendor", request, id, userid);
            if(response.IsSuccess == 1)
            {
                response.ResponseCode = 204;
            }
            else
            {
                response.ResponseCode = 400;
            }
            return response;
        }

        public async Task<Response> VendorCount()
        {
            Response response = new Response();
            response.Result= await baseRepository.GetSingleRecord<ActiveCounterResponse>("GetVendorStatusCount");
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }
    }
}