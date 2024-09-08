namespace InventorySystem.SharedLayer.Models.Response
{
    public class ProductsInInvoiceResponse
    {
        public int? StockInwardItemInformationId { get; set; }
        public int? StockInwardBasicInformationId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductSKU { get; set; }
        public string? Manufacturer { get; set; }
        public string? Category { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }   
        public decimal? Price { get; set; }
        public int? PendingCount { get; set; }
        public int? BarcodeGenerated { get; set; }
        public int? IsSentForManagerApproval { get; set; }
        public int? IsApprovedByManager { get; set; }
    }
}