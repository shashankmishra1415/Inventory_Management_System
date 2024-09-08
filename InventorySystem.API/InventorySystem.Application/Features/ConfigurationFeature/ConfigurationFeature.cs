using FluentValidation;
using InventorySystem.Application.Features.ConfigurationFeature.Interfaces;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.ConfigurationFeature
{
    public class ConfigurationFeature : IConfigurationFeature
    {
        public readonly IBaseRepository baseRepository;
        private readonly IValidator<CategoryTypeRequest> categoryTypeValidator;
        private readonly IValidator<ManufacturerTypeRequest> manufacturerTypeValidator;

        public ConfigurationFeature(IBaseRepository baseRepository, IValidator<CategoryTypeRequest> categoryTypeValidator, IValidator<ManufacturerTypeRequest> manufacturerTypeValidator)
        {
            this.baseRepository = baseRepository;
            this.categoryTypeValidator = categoryTypeValidator;
            this.manufacturerTypeValidator = manufacturerTypeValidator;
        }

        public async Task<Response> Category()
        {
            Response response = new Response();
            response.Result = await baseRepository.GetList<CategoryResponse>("GetCategorytype");
            response.IsSuccess = 1;
            response.Message = "Data Fetched Succesfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Category(int id)
        {
            Response response = new Response();
            response.Result = await baseRepository.GetSingleRecordById<CategoryTypeResponse>("GetByIdCategorytype", id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Succesfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Category(CategoryTypeRequest request, int userId)
        {
            categoryTypeValidator.ValidateAndThrow(request);
            Response response  = await baseRepository.Post<CategoryTypeRequest>("SaveCategorytype", request, userId);
            response.ResponseCode = 201;
            return response;
        }

        public async Task<Response> Category(CategoryTypeRequest request, int id, int userid)
        {
            categoryTypeValidator.ValidateAndThrow(request);
            Response response = await baseRepository.Put<CategoryTypeRequest>("UpdateCategorytype", request, id, userid);
            return response;
        }

        public async Task<Response> Category(int id, int userid)
        {
            Response response = await baseRepository.Delete("DeleteCategorytype", id, userid);
            return response;
        }

        public async Task<Response> Manufacturer()
        {
            Response response = new Response();
            response.Result = await baseRepository.GetList<ManufacturerResponse>("GetManufacturerType");
            response.IsSuccess = 1;
            response.Message = "Data Fetched Succesfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Manufacturer(int id)
        {
            Response response = new Response();
            response.Result = await baseRepository.GetSingleRecordById<ManufacturerTypeResponse>("GetByIdManufacturertype", id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Manufacturer(ManufacturerTypeRequest request, int userId)
        {
            manufacturerTypeValidator.ValidateAndThrow(request);
            Response response = await baseRepository.Post<ManufacturerTypeRequest>("SaveManufacturerType", request, userId);
            response.ResponseCode = 201;
            return response;
        }

        public async Task<Response> Manufacturer(ManufacturerTypeRequest request, int id, int userId)
        {
            manufacturerTypeValidator.ValidateAndThrow(request);
            Response response = await baseRepository.Put<ManufacturerTypeRequest>("UpdateManufacturerType", request, id, userId);
            return response;
        }

        public async Task<Response> Manufacturer(int id, int userId)
        {
            Response response = await baseRepository.Delete("DeleteManufacturertype", id, userId);
            return response;
        }
    }
}

