using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class ProductListResponse
    {
        public List<GetAllProduct> ProductsDetail { get; set; }
        public PaginationResponse PaginationResponses { get; set; }

    }

    public class GetAllProduct
    {
        public int Id { get; set; }
        public string ProductSku { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ManufactrerName { get; set; } 
        public string EANCode { get; set; }
        public double Price { get; set; }
        public string? IsActive { get; set; }
    }
}
