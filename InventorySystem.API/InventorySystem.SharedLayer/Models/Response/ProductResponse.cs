namespace InventorySystem.SharedLayer.Models.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string ProductSku { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public string EANCode { get; set; }
        public string? IsActive { get; set; }
        public  decimal Price { get; set; }
    }
}  