using InventorySystem.Application.Features.Admin_Feature.Interfaces;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Response;
using Newtonsoft.Json;

namespace InventorySystem.Application.Features.Admin_Feature
{
    public class AdminFeature : IAdminFeature
    {
        private readonly IAdminRepository adminRepository;

        public AdminFeature(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public async Task<Response> StockCountByWarehouse()
        {
            var data = await adminRepository.GetList<StockWarehouseCountResponse>("AdminGetStockCountByWarehouse");
            Response response = new Response();
            response.Result = data;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;

        }

        public async Task<Response> ReceivedGoodsDetailsByLocation(string monthName, int locationId)
        {
            GoodsByLocationResponse model = new GoodsByLocationResponse();
            GoodsSummaryByLocation summary = new GoodsSummaryByLocation();
            Response response = new Response();
            model.details = await adminRepository.ReceivedGoodsDetailsByLocation(monthName, locationId);
            summary.Quantity = model.details.Sum(item => item.StockQuantity);
            summary.Value = model.details.Sum(item => item.StockValue);
            model.summary = summary;
            response.Result = model;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> DispatchedGoodsDetailsByLocation(string filterMonth, int locationId)
        {
            GoodsByLocationResponse model = new GoodsByLocationResponse();
            GoodsSummaryByLocation summary = new GoodsSummaryByLocation();
            Response response = new Response();
            model.details = await adminRepository.DispatchedGoodsDetailsByLocation(filterMonth, locationId);
            summary.Quantity = model.details.Sum(item => item.StockQuantity);
            summary.Value = model.details.Sum(item => item.StockValue);
            model.summary = summary;
            response.Result = model;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> InventoryDetailByCategoryForLocation(int warehouseId)
        {
            var model = await adminRepository.InventoryDetailByCategoryForLocation(warehouseId);
            Response response = new Response();
            response.Result = model;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> InventoryDetailForCategoryOnLocation(int warehouseId, int categoryId)
        {
            var model = await adminRepository.InventoryDetailForCategoryOnLocation(warehouseId, categoryId);
            Response response = new Response();
            response.Result = model;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> InventoryDetailAtLocation(int locationId)
        {
            Response response = new Response();
            var result = await adminRepository.InventoryDetailByLocation<InventoryDetailResponseMobile>(locationId);
            response.Result = result;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<Response> InventoryDetailByBrandLocation(int locationId)
        {
            Response response = new Response();
            var result = await adminRepository.InventoryDetailByBrandLocation<InventoryDetailByBrandResponse>(locationId);
            response.Result = result;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }


        public async Task<Response> InventoryDetailByVendorForLocation(int locationId)
        {
            Response response = new Response();
            var result = await adminRepository.InventoryDetailByVendorForLocation<GetInventoryDetailByBrandResponseJSON>(locationId);
            List<InventoryDetailByBrandResponse> getInventoryDetailBy = new List<InventoryDetailByBrandResponse>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    InventoryDetailByBrandResponse obj = new InventoryDetailByBrandResponse();
                    obj.Id = item.Id;
                    obj.Name = item.Name;
                    obj.TotalPrice = item.TotalPrice;
                    obj.JsonList = JsonConvert.DeserializeObject<List<GetInventoryDetailJsonList>>(item.JsonList, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    getInventoryDetailBy.Add(obj);
                }
            }
            response.Result = getInventoryDetailBy;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }


        public async Task<Response> InventoryByBrandLocation(int warehouseId)
        {
            Response response = new Response();
            var resp = await adminRepository.InventoryByBrandLocation(warehouseId);
            List<InventoryDetailByBrandLocation> inventory = new List<InventoryDetailByBrandLocation>();
            if (resp != null)
            {
                foreach (var item in resp)
                {
                    inventory.Add(new InventoryDetailByBrandLocation()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        TotalPrice = item.TotalPrice,
                        BrandDetails = JsonConvert.DeserializeObject<List<BrandDetails>>(item.JsonList, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                    });
                }
            }
            response.Result = inventory;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }
        public async Task<Response> InventoryDetailByCategoryLocation(int warehouseId)
        {
            Response response = new Response();
            var resp = await adminRepository.InventoryDetailByCategoryLocation(warehouseId);
            List<InventoryDetailByCategoryForLocation> inventory = new List<InventoryDetailByCategoryForLocation>();
            if (resp != null)
            {
                foreach (var item in resp)
                {
                    inventory.Add(new InventoryDetailByCategoryForLocation()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        TotalPrice = item.TotalPrice,
                        Productdetails = JsonConvert.DeserializeObject<List<Productdetails>>(item.JsonList, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                    });
                }
            }
            response.Result = inventory;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }
        public async Task<Response> SearchBySerialNumber(string serialNumber)
        {
            Response response = new Response();
            var result = await adminRepository.SearchBySerialNumber(serialNumber);
            response.Result = result;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }
    }
}
