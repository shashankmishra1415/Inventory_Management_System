namespace InventorySystem.SharedLayer.Models.Response
{
    public class InfoForBarcodeResponse
    {
        public InvoiceInfoForBarcodePage? InvoiceInfoForBarcodePage { get; set; }
        public ProductInfoForBarcodePage? ProductInfoForBarcodePage { get; set; }
    }

    public class InvoiceInfoForBarcodePage
    {
        public string? InvoiceNumber { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public string? CompanyName { get; set; }
        public string? PurchaseOrderNo { get; set; }
        public string? MovementType { get; set; }
    }
    
    public class ProductInfoForBarcodePage
    {
        public string? ProductSKU { get; set; }
        public string? Manufacturer { get; set; }
        public string? Category  { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? NotScanedCount { get; set; }
        public int? IsScanCompleted { get; set; }
        public int? Printed { get; set; }
        public string? Status { get; set; }
    }             


}
