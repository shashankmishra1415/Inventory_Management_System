namespace InventorySystem.SharedLayer.Models.Response
{
    public class WarehouseResponse
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string? Type { get; set; }
        public Decimal MaxCapacity { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? IsActive { get; set; }
    }
}
