namespace InventorySystem.SharedLayer.Models.Request
{
    public class ProductRequest
    {
        public string? ProductSku { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufacturerId { get; set; }
        public string? EANCode { get; set; }
        public double Price { get; set; }
        public int? IsActive { get; set; }
    }
}
