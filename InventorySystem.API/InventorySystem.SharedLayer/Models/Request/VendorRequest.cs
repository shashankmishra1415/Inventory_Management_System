namespace InventorySystem.SharedLayer.Models.Request
{
    public class VendorRequest
    {
        public string CompanyName { get; set; }
        public int CompanyTypeId { get; set; }
        public int? VendorTypeId { get; set; }
        public string? GST { get; set; }
        public int IsActive { get; set; } = 1;
        public string? Address { get; set; }
        public string? ContactName { get; set; }
        public string? ContactMobile { get; set; }
        public string? ContactEmail { get; set; }
    }
}