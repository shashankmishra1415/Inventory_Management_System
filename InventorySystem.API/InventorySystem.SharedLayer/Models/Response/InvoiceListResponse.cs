using InventorySystem.SharedLayer.Response;
namespace InventorySystem.SharedLayer.Models.Response
{
    public class InvoiceListResponse
    {
        public List<InvoiceDetail> InvoiceDetail { get; set; }
        public PaginationResponse PaginationResponses { get; set; }
    }
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string CompanyName { get; set; }
        public string MovementType { get; set; }
        public string LocationName { get; set; }
        public string DateOfPurchase { get; set; }
        public string ItemType { get; set; }
        public string Status { get; set; }
    }
}