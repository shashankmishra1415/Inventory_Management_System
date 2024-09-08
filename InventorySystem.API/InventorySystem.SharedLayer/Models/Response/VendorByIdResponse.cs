namespace InventorySystem.SharedLayer.Models.Response
{
    public class VendorByIdResponse
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactMobile { get; set; }
        public string? ContactEmail { get; set; }
        public string? Address { get; set; }
        public string? GST { get; set; }
        public int CompanyTypeId { get; set; }
        public int VendorTypeId { get; set; }
        public int IsActive { get; set; }
    }
}
