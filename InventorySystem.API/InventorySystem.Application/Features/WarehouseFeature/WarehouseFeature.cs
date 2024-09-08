using FluentValidation;
using InventorySystem.Application.Features.WarehouseFeature.Interfaces;
using InventorySystem.Infrastructure.Repositories;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Common;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.WareFeature
{
    public class WarehouseFeature : IWarehouseFeature
    {
        public readonly IWarehouseRepository warehouseRepository;
        private readonly IValidator<WarehouseRequest> warehouseValidator;
        public WarehouseFeature(IWarehouseRepository warehouseRepository, IValidator<WarehouseRequest> _warehouseValidator)
        {
            this.warehouseRepository = warehouseRepository;
            this.warehouseValidator = _warehouseValidator;
        }

        public async Task<Response> Warehouse(int pageNum, int pageSize, string locationName, double capacity, int typeId, int statusId)
        {
            Response response = new Response();
            response.Result = await warehouseRepository.Warehouse(pageNum, pageSize, locationName, capacity, typeId, statusId);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> Warehouse(int id)
        {
            Response response = new Response();
            response.Result = await warehouseRepository.GetSingleRecordById<WarehouseResponse>("GetByIdWarehouse", id);
            response.IsSuccess = 1;
            response.Message = "Data Fetched Successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> DeleteWarehouse(int id,int userId)
        {
            Response response = await warehouseRepository.Delete("DeleteWarehouse", id,userId);
            return response;
        }

        public async Task<Response> Warehouse(WarehouseRequest request, int userId)
        {
            warehouseValidator.ValidateAndThrow(request);
            Response response  = await warehouseRepository.Post("AddWarehouse", request, userId);
            response.ResponseCode=201;
            return response;
        }

        public async Task<Response> Warehouse(WarehouseRequest request, int id, int userId)
        {
            warehouseValidator.ValidateAndThrow(request);
            Response response = await warehouseRepository.Put("EditWarehouse", request, id, userId);
            return response;
		}

		public async Task<Response> Count()
		{
			Response response = new Response();
			response.Result = await warehouseRepository.GetSingleRecord<ActiveCounterResponse>("WarehouseCount");
			response.IsSuccess = 1;
			response.Message = "Data Fetched Successfully.";
			response.ResponseCode = 200;
			return response;
		}
	}
}