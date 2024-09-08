using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.Dropdown_Feature
{
	public interface IDropdownFeature
	{
		public Task<Response> VendorType();
		public Task<Response> Category();
		public Task<Response> Department();		
		public Task<Response> Manufacturer();
		public Task<Response> Status();
		public Task<Response> WarehouseType();
		public Task<Response> CompanyType();
		public Task<Response> WarehouseLocation();
		public Task<Response> CustomerType();
        public Task<Response> MovementType();
        public Task<Response> CustomMovementType();
        public Task<Response> SaleOrderStatus();
		public Task<Response> ProductSKU();
		public Task<Response> OutType();
		public Task<Response> SaleOrderMovementType();
		public Task<Response> User();
		public Task<Response> StockAuditCategoryDropdown();
		public Task<Response> ActionTypeDropdown();
		public Task<Response> RecordType();
    }
}
