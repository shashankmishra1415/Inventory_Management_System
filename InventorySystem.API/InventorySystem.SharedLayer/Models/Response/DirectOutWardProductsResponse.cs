namespace InventorySystem.SharedLayer.Models.Response
{
    public class DirectOutWardProductsResponse
    {
        public int? Id {get; set;}
        public string? Name {get; set;}
        public string? ProductSKU {get; set;}
        public string? EANCode {get; set;}
        public string? Category {get; set;}
        public string? Manufacturer {get; set;}
        public int? ItemQuantity  {get; set; }
        public bool? IsDispatced { get; set;}
        public bool? IsReturnToManufacturer { get; set; }
    }
}
