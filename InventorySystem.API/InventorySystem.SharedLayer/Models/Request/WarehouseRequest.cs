namespace InventorySystem.SharedLayer.Models.Request
{
    public class WarehouseRequest
    {
        public string LocationName { get; set; }
        public int WarehouseTypeId { get; set; }
        public Decimal MaxCapacity { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public int IsActive { get; set; }
    }
}