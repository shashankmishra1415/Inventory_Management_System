using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class WarehouseListResponse
    {
        public List<WarehouseDetail> WarehouseDetail { get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }
    public class WarehouseDetail
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Capacity { get; set; }
        public string LocationName { get; set; }
        public string Status { get; set; }
    }
}
