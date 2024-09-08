namespace InventorySystem.SharedLayer.Models.Response
{
    public class ScanAllModelStockInward
    {
        public List<ScanAllStockInwardProductList>? ScanAllStockInwardProductList { set; get; }
        public ScanAllStockInwardInvoiceInfo? ScanAllStockInwardInvoiceInfo { set; get; }
    }

    public class ScanAllStockInwardInvoiceInfo
    {
        public int? Id{set; get;}
        public string? InvoiceNo{set; get;}
        public string? PurchaseOrderNo{set; get;}
        public DateTime? DateOfPurchase{set; get;}
        public string? CompanyName{set; get;}
        public string? MovementType { set; get; }
    }

    public class ScanAllStockInwardProductList
    {
        public int? Id { get; set; }
        public string? ProductSKU{set; get; }
        public string? Category{set; get; }
        public string? Manufacturer{set; get; }
        public string? ProductName{set; get; }
        public int? Quantity{set; get; }
        public Decimal? Price { set; get; }
    }
}