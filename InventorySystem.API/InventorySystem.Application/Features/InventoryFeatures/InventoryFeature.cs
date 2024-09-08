using InventorySystem.Application.Features.InventoryFeatures.interfaces;
using InventorySystem.Application.Helpers;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.Infrastructure.Repositories.Interfaces;
using InventorySystem.SharedLayer.Models.Response;

namespace InventorySystem.Application.Features.InventoryFeatures
{
    public class InventoryFeature : IInventoryFeature
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IBaseRepository baseRepository;
        public InventoryFeature(IInventoryRepository inventoryRepository, IBaseRepository baseRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.baseRepository = baseRepository;
        }

        public async Task<Response> Inventory(int pageNum, int pageSize, DateTime startDate, DateTime endDate, string productSKU, string sortColumn, string sortOrder, int warehouseId)
        {
            int offset = (pageNum - 1) * pageSize;

            InventoryResponse inventory = await inventoryRepository.Inventory(offset, pageSize, startDate, endDate, productSKU, sortColumn, sortOrder, warehouseId);
            inventory.TotalRecordInformation.PageNum = pageNum;
            inventory.TotalRecordInformation.PageSize = pageSize;         
            
            Response response = new Response();
            response.Result = inventory;
            response.IsSuccess = 1;
            response.Message = "Data fetched successfully.";
            response.ResponseCode = 200;
            return response;
        }

        public async Task<byte[]?> ConvertInventoryToExcel(DateTime startDate, DateTime endDate, string? name, int locationId)
        {
            {
                InventoryExcelResponse data = await inventoryRepository.GetAllInventory(startDate, endDate, name, locationId);
                foreach (var t in data.ProductInformation)
                {
                    t.StockQuantity = t.ReceivedInventory - (t.DispatchedInventory + t.PendingInventory);
                    t.TotalPrice = t.StockQuantity * t.TotalPrice; 

                }

                List<string> columns = new List<string>() { "S.No.", "ProductSKU", "Name", "ReceivedInventory ( " + data.totalProduct.ReceivedInventory + " )", "DispatchedInventory( " + data.totalProduct.DispatchedInventory + " )", "PendingInventory( " + data.totalProduct.PendingInventory + " )","DamagedInventory("+data.totalProduct.DamageInventory+" ) ","Stock Qty(" + (data.totalProduct.ReceivedInventory - (data.totalProduct.DispatchedInventory + data.totalProduct.PendingInventory)) + ")", "Price( " + data.totalProduct.TotalPrice + " )" };
                return await ExcelConverter.ConvertToExcel<ExcelResponse>("InventoryDetails", columns, data.ProductInformation);
            }
        }
    }
}
