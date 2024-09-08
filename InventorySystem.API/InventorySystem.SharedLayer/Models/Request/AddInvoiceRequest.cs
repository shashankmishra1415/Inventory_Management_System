namespace InventorySystem.SharedLayer.Models.Request
{
    public class AddInvoiceRequest
    {
        public string? InvoiceNo { get; set; }
        public string? PurchaseOrderNo { get; set; }
        //public DateTime DateOfPurchase { get; set; }
        public string DateOfPurchase { get; set; }
        public int? VendorCompanyNameId { get; set; }
        public int? MoveTypeId { get; set; }
        public int? WarehouseLocationId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? StatusId { get; set; }
    }
}