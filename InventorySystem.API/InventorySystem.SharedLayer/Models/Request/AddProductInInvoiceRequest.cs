namespace InventorySystem.SharedLayer.Models.Request
{
    public class AddProductInInvoiceRequest
    {
        public int ProductSkuId { get; set; }
        public int Quantity { get; set; } 
        public int InvoiceNoId { get; set; }
        public decimal Price { get; set; }
    }

    public class AddProductInInvoiceWithJsonRequest
    {
        public string Data { get; set; }
    }
}
