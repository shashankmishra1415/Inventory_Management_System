using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.Dropdown_Feature
{
	public class DropdownFeature : IDropdownFeature
	{
		public readonly IDropdownRepository dropdownRepository;
		public DropdownFeature(IDropdownRepository dropdownRepository)
		{
			this.dropdownRepository = dropdownRepository;
		}
		public async Task<Response> VendorType()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetVendorTypeDropdown");
			response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
			return response;
		}
		public async Task<Response> Category()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetCategoryDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		public async Task<Response> Department()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetDepartmentDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		public async Task<Response> Manufacturer()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetManufacturersDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		public async Task<Response> Status()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetActiveDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		public async Task<Response> WarehouseType()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetWarehouseTypeDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		public async Task<Response> CompanyType()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetCompanyTypeDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		public async Task<Response> WarehouseLocation()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetWarhouseLocationDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
        public async Task<Response> CustomerType()
        {
            Response response = new Response();
            response.Result = await dropdownRepository.GetList<DropdownResponse>("GetCustomerDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
        }
        public async Task<Response> CustomMovementType()
        {
            Response response = new Response();
            response.Result = await dropdownRepository.GetList<DropdownResponse>("GetMovementCustomDropdownForStockInward");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
        }

        public async Task<Response> MovementType()
        {
            Response response = new Response();
            response.Result = await dropdownRepository.GetList<DropdownResponse>("GetMovementDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1; 
            return response;
        }
        public async Task<Response> SaleOrderStatus()
        {
            Response response = new Response();
            response.Result = await dropdownRepository.GetList<DropdownResponse>("GetSaleOrderStatusDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
        }
        public async Task<Response> ProductSKU()
        {
            Response response = new Response();
            response.Result = await dropdownRepository.GetList<DropdownResponse>("GetProductSKUDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
        }
		public async Task<Response> OutType()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("getOutType");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
        }
		public async Task<Response> SaleOrderMovementType()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetSaleOrderMovementType");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}

        public async Task<Response> User()
        {
            Response response = new Response();
            response.Result = await dropdownRepository.GetList<DropdownResponse>("GetUser");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
        }

		public async Task<Response> StockAuditCategoryDropdown()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("StockAuditCategoryDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		public async Task<Response> ActionTypeDropdown()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetActionTypeDropdown");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
		
		public async Task<Response> RecordType()
		{
			Response response = new Response();
			response.Result = await dropdownRepository.GetList<DropdownResponse>("GetRecordType");
            response.ResponseCode = 200;
            response.Message = "Data fetched successfully.";
            response.IsSuccess = 1;
            return response;
		}
	}
}
