using InventorySystem.SharedLayer.Response;

namespace InventorySystem.SharedLayer.Models.Response
{
    public class VendorListResponse
    {
        public List<VendorDetail> VendorDetail { get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }
    public class VendorDetail
    {
        public int Id { get; set; }
        public string CompanyType { get; set; }
        public string CompanyName { get; set; }
        public string VendorType { get; set; }
        public string ContactName { get; set; }
        public int IsActive { get; set; }
    }
}

